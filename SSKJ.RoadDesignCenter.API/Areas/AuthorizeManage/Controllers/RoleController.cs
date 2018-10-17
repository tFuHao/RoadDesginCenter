using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SSKJ.RoadDesignCenter.API.Areas.AuthorizeManage.Data;
using SSKJ.RoadDesignCenter.API.Models;
using SSKJ.RoadDesignCenter.IBusines.Project;
using SSKJ.RoadDesignCenter.IBusines.Project.Authorize;
using SSKJ.RoadDesignCenter.IBusines.System;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using SSKJ.RoadDesignCenter.Models.SystemModel;
using IUserBusines = SSKJ.RoadDesignCenter.IBusines.Project.IUserBusines;

namespace SSKJ.RoadDesignCenter.API.Areas.AuthorizeManage.Controllers
{
    [Route("api/Role/[action]")]
    public class RoleController : Controller
    {
        private readonly IRoleBusines RoleBus;

        private readonly IUserBusines UserBus;

        private readonly IModuleBusines ModuleBus;

        private readonly IButtonBusines ButtonBus;

        private readonly IColumnBusines ColumnBus;

        private readonly IAuthorizeBusines AuthorizeBus;

        public RoleController(IRoleBusines roleBus, IUserBusines userBus, IModuleBusines moduleBus, IButtonBusines buttonBus, IColumnBusines columnBus, IAuthorizeBusines authorizeBus)
        {
            RoleBus = roleBus;
            UserBus = userBus;
            ModuleBus = moduleBus;
            ButtonBus = buttonBus;
            ColumnBus = columnBus;
            AuthorizeBus = authorizeBus;
        }

        public async Task<IActionResult> GetRoles(int pageSize, int pageIndex)
        {
            var result = await RoleBus.GetListAsync(e => true, e => e.SortCode, true, pageSize, pageIndex, GetConStr());
            return Json(new
            {
                data = result.Item1,
                count = result.Item2
            });
        }

        public async Task<IActionResult> Insert(Role input)
        {
            if (ModelState.IsValid)
            {
                if (input.RoleId == null)
                {
                    input.RoleId = Guid.NewGuid().ToString();
                    input.DeleteMark = 0;
                    input.CreateDate = DateTime.Now;
                    input.CreateUserId = GetUserId();
                    var result = await RoleBus.CreateAsync(input, GetConStr());
                    return Json(result);
                }
                else
                {
                    if (input.DeleteMark == 1)
                    {
                        return Json(false);
                    }

                    var entity = await RoleBus.GetEntityAsync(e => e.RoleId == input.RoleId, GetConStr());
                    if (entity == null)
                        return null;
                    entity.FullName = input.FullName;
                    entity.SortCode = input.SortCode;
                    //entity.DeleteMark = input.DeleteMark;
                    entity.EnabledMark = input.EnabledMark;
                    entity.Description = input.Description;
                    entity.ModifyDate = DateTime.Now;
                    entity.ModifyUserId = GetUserId();
                    var result = await RoleBus.UpdateAsync(entity, GetConStr());
                    return Json(result);
                }
            }

            foreach (var data in ModelState.Values)
            {
                if (data.Errors.Count > 0)
                {
                    return Json(data.Errors[0].ErrorMessage);
                }
            }

            return null;
        }

        public async Task<IActionResult> Delete(List<Role> list)
        {
            var result = false;
            if (list.Any())
            {
                list.ForEach(async i =>
                {
                    var users = await UserBus.GetListAsync(e => e.RoleId == i.RoleId, GetConStr());
                    users.ToList().ForEach(async j =>
                    {
                        j.RoleId = null;
                        await UserBus.UpdateAsync(j, GetConStr());
                    });
                });
                result = await RoleBus.DeleteAsync(list, GetConStr());
            }

            return Json(result);
        }

        /// <summary>
        /// 更改角色的开启状态
        /// </summary>
        /// <param name="list">将要更改的列表</param>
        /// <returns></returns>
        public async Task<IActionResult> ChangeEnable(string roleId)
        {
            var entity = await RoleBus.GetEntityAsync(e => e.RoleId == roleId, GetConStr());
            if (entity.DeleteMark != 1)
            {
                if (entity.EnabledMark == 1)
                {
                    entity.EnabledMark = 0;
                }
                else
                {
                    entity.EnabledMark = 1;
                }
                var result = await RoleBus.UpdateAsync(entity, GetConStr());
                return Json(result);
            }

            return Json(false);
        }

        /// <summary>
        /// 更改角色成员时获取项目成员
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetUsers()
        {
            var result = await UserBus.GetListAsync(e => e.EnabledMark == 1, GetConStr());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetModuleTree(string roleId)
        {
            var list = await ModuleBus.GetListAsync();
            var tree = Data.ModuleTreeJson.TreeGridJson(list.OrderBy(e => e.SortCode).ToList());
            var check = await AuthorizeBus.GetListAsync(e => e.ObjectId == roleId && e.ItemType == 1, GetConStr());
            return Ok(new { tree, check });
        }

        [HttpPost]
        public async Task<IActionResult> GetButtonTree(string roleId, List<string> moduleList)
        {
            var tree = new List<ModuleConvertButton>();
            foreach (var i in moduleList)
            {
                var module = await ModuleBus.GetEntityAsync(e => e.ModuleId == i);
                var parent = new ModuleConvertButton()
                {
                    ModuleButtonId = module.ModuleId,
                    FullName = module.FullName
                };
                var buttonList = await ButtonBus.GetListAsync(e => e.ModuleId == i);
                parent.Children = buttonList;
                if (parent.Children.Any())
                    tree.Add(parent);
            }

            var check = await AuthorizeBus.GetListAsync(e => e.ObjectId == roleId && e.ItemType == 2, GetConStr());

            return Ok(new { tree, check });
        }

        [HttpPost]
        public async Task<IActionResult> GetViewTree(string roleId, List<string> moduleList)
        {
            var tree = new List<ColumnTreeDto>();
            foreach (var i in moduleList)
            {
                var module = await ModuleBus.GetEntityAsync(e => e.ModuleId == i);
                var parent = new ColumnTreeDto()
                {
                    ParentId = module.ParentId,
                    ModuleId = module.ModuleId,
                    FullName = module.FullName,
                    ModuleColumnId = module.ModuleId
                };
                parent.Children = await ColumnBus.GetListAsync(e => e.ModuleId == i);
                if (parent.Children.Any())
                    tree.Add(parent);
            }

            var check = await AuthorizeBus.GetListAsync(e => e.ObjectId == roleId && e.ItemType == 3, GetConStr());
            return Ok(new { tree, check });
        }

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="roleId">将要授权的角色ID</param>
        /// <param name="moduleList">可以看到的module</param>
        /// <param name="buttonList">module对应的按钮</param>
        /// <param name="columnList">页面的视图</param>
        /// <returns></returns>
        public async Task<IActionResult> SetAuthorize(string roleId, List<string> moduleList, List<string> buttonList, List<string> columnList)
        {
            var userId = GetUserId();
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(roleId))
            {
                var insertList = new List<Authorize>();
                var moduleCount = await AuthorizeBus.GetListAsync(e => e.ItemType == 1, GetConStr());
                var buttonCount = await AuthorizeBus.GetListAsync(e => e.ItemType == 2, GetConStr());
                var columnCount = await AuthorizeBus.GetListAsync(e => e.ItemType == 3, GetConStr());

                var delete = await AuthorizeBus.GetListAsync(e => e.ObjectId == roleId, GetConStr());
                await AuthorizeBus.DeleteAsync(delete, GetConStr());

                for (var i = 0; i < moduleList.Count; i++)
                {
                    var temp = new Authorize()
                    {
                        AuthorizeId = Guid.NewGuid().ToString(),
                        Category = null,
                        ObjectId = roleId,
                        ItemType = 1,
                        ItemId = moduleList[i],
                        SortCode = moduleCount.Count() + insertList.FindAll(e => e.ItemType == 1).Count + 1,
                        CreateDate = DateTime.Now,
                        CreateUserId = userId
                    };
                    insertList.Add(temp);
                }

                for (var i = 0; i < buttonList.Count; i++)
                {
                    var temp = new Authorize()
                    {
                        AuthorizeId = Guid.NewGuid().ToString(),
                        Category = null,
                        ObjectId = roleId,
                        ItemType = 2,
                        ItemId = buttonList[i],
                        SortCode = buttonCount.Count() + insertList.FindAll(e => e.ItemType == 2).Count + 1,
                        CreateDate = DateTime.Now,
                        CreateUserId = userId
                    };
                    insertList.Add(temp);
                }

                for (var i = 0; i < columnList.Count; i++)
                {
                    var temp = new Authorize()
                    {
                        AuthorizeId = Guid.NewGuid().ToString(),
                        Category = null,
                        ObjectId = roleId,
                        ItemType = 3,
                        ItemId = columnList[i],
                        SortCode = columnCount.Count() + insertList.FindAll(e => e.ItemType == 3).Count + 1,
                        CreateDate = DateTime.Now,
                        CreateUserId = userId
                    };
                    insertList.Add(temp);
                }
                var result = await AuthorizeBus.CreateAsync(insertList, GetConStr());

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        public async Task<IActionResult> GetAuthorize()
        {
            var userId = GetUserId();
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await UserBus.GetEntityAsync(e => e.UserId == userId, GetConStr());
                var role = await RoleBus.GetEntityAsync(e => e.RoleId == user.RoleId, GetConStr());

                var tempModule = await AuthorizeBus.GetListAsync(e => e.ObjectId == role.RoleId && e.ItemType == 1, GetConStr());
                var module = new List<AuthorizeModuleDto>();
                foreach (var temp in tempModule)
                {
                    var entity = await ModuleBus.GetEntityAsync(e => e.ModuleId == temp.ItemId);
                    module.Add(Mapper.Map<Module, AuthorizeModuleDto>(entity));
                }

                return Ok(module);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// 从token中获得当前登录的用户ID  
        /// </summary>
        /// <returns></returns>
        public string GetUserId()
        {
            var result = "";
            try
            {
                string strToken = "";
                if (Request.Headers.TryGetValue("x-access-token", out StringValues token))
                    strToken = token.ToString();

                var userInfo = Utility.Tools.TokenUtils.ToObject<UserInfoModel>(strToken);
                result = userInfo.UserId;
            }
            catch (Exception)
            {

            }
            return result;
        }

        /// <summary>
        /// 从token中获得当前用户数据库名称
        /// </summary>
        /// <returns></returns>
        public string GetConStr()
        {
            var result = "";
            try
            {
                string strToken = "";
                if (Request.Headers.TryGetValue("x-access-token", out StringValues token))
                    strToken = token.ToString();

                var userInfo = Utility.Tools.TokenUtils.ToObject<UserInfoModel>(strToken);

                result = userInfo.DataBaseName;
            }
            catch (Exception)
            {

            }
            return result;
        }
    }
}