using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SSKJ.RoadDesignCenter.API.Models;
using SSKJ.RoadDesignCenter.IBusines.System;
using SSKJ.RoadDesignCenter.Models.SystemModel;

namespace SSKJ.RoadDesignCenter.API.Areas.SystemManage.Controllers
{
    [Route("api/Module/[action]")]
    [Area("SystemManage")]
    public class ModuleController : Controller
    {
        private readonly IModuleBusines moduleBll;
        private readonly IButtonBusines buttonBll;
        private readonly IColumnBusines columnBll;

        public ModuleController(IModuleBusines moduleBll, IButtonBusines buttonBll, IColumnBusines columnBll)
        {
            this.moduleBll = moduleBll;
            this.buttonBll = buttonBll;
            this.columnBll = columnBll;
        }

        [HttpGet]
        public async Task<IActionResult> GetModuleTreeGrid(string keyword)
        {
            try
            {
                var data = "";
                if (!string.IsNullOrEmpty(keyword))
                    data = await moduleBll.GetTreeListAsync(f => f.FullName.Contains(keyword));
                else
                    data = await moduleBll.GetTreeListAsync(f=>true);

                return Ok(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetButtonTreeGrid(string moduleId)
        {
            var data = "";
            if (!string.IsNullOrEmpty(moduleId))
                data = await buttonBll.GetTreeListAsync(f => f.ModuleId.Equals(moduleId));
            else
                data = await buttonBll.GetTreeListAsync(f=>true);

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetColumnTreeGrid(string moduleId)
        {
            IEnumerable<ModuleColumn> data = null;
            if (!string.IsNullOrEmpty(moduleId))
                data = await columnBll.GetListAsync(f => f.ModuleId.Equals(moduleId));
            else
                data = await columnBll.GetListAsync();

            return Ok(data.OrderBy(o => o.SortCode).ToList());
        }

        [HttpPost]
        public IActionResult ButtonListToTree(List<ModuleButton> list)
        {
            return Ok(buttonBll.ButtonListToTree(list));
        }

        [HttpPost]
        public async Task<IActionResult> SaveEntity(Module module, List<ModuleButton> buttons, List<ModuleColumn> columns)
        {
            try
            {
                string strToken = "";
                if (Request.Headers.TryGetValue("x-access-token", out StringValues token))
                    strToken = token.ToString();

                if (string.IsNullOrEmpty(strToken))
                    return BadRequest(new { type = 0, message = "登录超时，请重新登录!" });

                var userInfo = Utility.Tools.TokenUtils.ToObject<UserInfoModel>(strToken);

                if (userInfo.TokenExpiration <= DateTime.Now)
                    return BadRequest(new { type = 0, message = "登录超时，请重新登录!" });

                var result = false;
                var entity = await moduleBll.GetEntityAsync(module.ModuleId);
                if (entity != null)
                {
                    var _buttons = await buttonBll.GetListAsync(b => b.ModuleId == entity.ModuleId);
                    var _columns = await columnBll.GetListAsync(c => c.ModuleId == entity.ModuleId);

                    entity = Utility.Tools.MapperUtils.MapTo<Module, Module>(module);
                    entity.ModifyDate = DateTime.Now;
                    entity.ModifyUserId = userInfo.UserId;
                    result = await moduleBll.UpdateAsync(entity);

                    if (_buttons.Count() > 0)
                        await buttonBll.DeleteAsync(_buttons);
                    if (_columns.Count() > 0)
                        await columnBll.DeleteAsync(_columns);
                }
                else
                {
                    module.CreateUserId = userInfo.UserId;
                    module.CreateDate = DateTime.Now;
                    result = await moduleBll.CreateAsync(module);
                }
                if (result)
                {
                    if (buttons.Count > 0)
                    {
                        var result_btn = await buttonBll.CreateAsync(buttons);
                        if (!result_btn)
                        {
                            await moduleBll.DeleteAsync(module);
                            return BadRequest(new { type = 0, message = "操作失败!" });
                        }
                    }
                    if (columns.Count > 0)
                    {
                        var result_col = await columnBll.CreateAsync(columns);
                        if (!result_col)
                        {
                            await moduleBll.DeleteAsync(module);
                            await buttonBll.DeleteAsync(buttons);
                            return BadRequest(new { type = 0, message = "操作失败!" });
                        }
                    }
                    return Ok(new { type = 1, message = "操作成功!" });
                }
                return BadRequest(new { type = 0, message = "操作失败!" });
            }
            catch (Exception)
            {
                return BadRequest(new { type = 0, message = "操作失败!" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string moduleId)
        {
            try
            {
                var list = await moduleBll.GetListAsync(m => m.EnabledMark == 1);
                var modules = GetModules(list.ToList(), moduleId);
                modules.Add(list.Single(m => m.ModuleId == moduleId));

                var buttons = await buttonBll.GetListAsync(b => modules.Any(m => m.ModuleId == b.ModuleId));                
                var columns = await columnBll.GetListAsync(c => modules.Any(m => m.ModuleId == c.ModuleId));

                await buttonBll.DeleteAsync(buttons);
                await columnBll.DeleteAsync(columns);
                await moduleBll.DeleteAsync(modules);

                return Ok(new { type = 1, message = "操作成功!" });
            }
            catch (Exception)
            {
                return BadRequest(new { type = 0, message = "操作失败!" });
            }
        }

        public List<Module> GetModules(List<Module> list, string pId)
        {
            var _list = list.Where(f => f.ParentId == pId).ToList();

            return _list.Concat(_list.SelectMany(t => GetModules(list, t.ModuleId))).ToList();
        }
    }
}