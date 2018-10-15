using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SSKJ.RoadDesignCenter.API.Models;
using SSKJ.RoadDesignCenter.IBusines.Project;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.API.Controllers
{
    [Route("api/Role/[action]")]
    public class RoleController : Controller
    {
        public IRoleBusines RoleBus;

        public IUserBusines UserBus;

        public RoleController(IRoleBusines roleBus, IUserBusines userBus)
        {
            RoleBus = roleBus;
            UserBus = userBus;
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