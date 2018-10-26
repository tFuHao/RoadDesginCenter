using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSKJ.RoadDesignCenter.API.Controllers;
using SSKJ.RoadDesignCenter.IBusines.Project;
using SSKJ.RoadDesignCenter.IBusines.Project.Authorize;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.IBusines.System;
using SSKJ.RoadDesignCenter.Models;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.API.Areas.AuthorizeManage.Controllers
{
    [Route("api/ProjectUsers/[action]")]
    [Area("AuthorizeManage")]
    public class ProjectUsersController : BaseController
    {
        public readonly IAuthorizeBusines authBll;
        private readonly IBusines.Project.IUserBusines userBll;
        private readonly IUserRelationBusines roleUserBll;
        private readonly IRouteBusines routeBll;
        private readonly IModuleBusines moduleBll;
        private readonly IButtonBusines buttonBll;
        private readonly IColumnBusines columnBll;
        private readonly IRoleBusines roleBll;

        public ProjectUsersController(IBusines.Project.IUserBusines userBll, IRouteBusines routeBll, IAuthorizeBusines authBll, IModuleBusines moduleBll, IButtonBusines buttonBll, IColumnBusines columnBll, IRoleBusines roleBll, IUserRelationBusines roleUserBll)
        {
            this.userBll = userBll;
            this.routeBll = routeBll;
            this.authBll = authBll;
            this.moduleBll = moduleBll;
            this.buttonBll = buttonBll;
            this.columnBll = columnBll;
            this.roleBll = roleBll;
            this.roleUserBll = roleUserBll;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersData(int pageSize, int pageIndex, string keyword)
        {
            try
            {
                var users = new Tuple<IEnumerable<User>, int>(null, 0);
                if (!string.IsNullOrEmpty(keyword))
                    users = await userBll.GetListAsync(u => u.RealName.Contains(keyword) && u.RoleId != "PrjAdmin", u => u.CreateDate, true, pageSize, pageIndex, GetUserInfo().DataBaseName);
                else
                    users = await userBll.GetListAsync(u => u.RoleId != "PrjAdmin", u => u.CreateDate, true, pageSize, pageIndex, GetUserInfo().DataBaseName);

                return Ok(new { data = users.Item1, count = users.Item2 });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetRoleData()
        {
            try
            {
                var roles = await roleBll.GetListAsync(GetUserInfo().DataBaseName);
                return Ok(roles);
            }
            catch (Exception)
            {
                return BadRequest(false);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetUserPermission(int category, string objectId)
        {
            try
            {
                var result = await authBll.GetModuleAndRoutePermission(category, objectId, GetUserInfo().DataBaseName);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(false);
            }
        }

        public IActionResult GetButtonAndColumnTree(List<string> halfKeys, List<string> checkedKeys, string strAuthorizes, string strModules, string strButtons, string strColumns)
        {
            var result = authBll.GetButtonAndColumnPermission(halfKeys, checkedKeys, strAuthorizes, strModules, strButtons, strColumns);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEntity(User entity)
        {
            try
            {
                var result = false;
                if (string.IsNullOrEmpty(entity.UserId))
                {
                    entity.UserId = Guid.NewGuid().ToString();
                    entity.CreateDate = DateTime.Now;
                    entity.CreateUserId = GetUserInfo().UserId;
                    entity.Gender = 0;
                    entity.EnabledMark = 1;
                    entity.HeadIcon = "/userAvatar/avatar.jpg";
                    entity.Secretkey = Guid.NewGuid().ToString();
                    entity.Password = Utility.Tools.MD5Utils.Sign(entity.Password, entity.Secretkey);
                    result = await userBll.CreateAsync(entity, GetUserInfo().DataBaseName);
                }
                else
                {
                    var user = await userBll.GetEntityAsync(entity.UserId, GetUserInfo().DataBaseName);
                    user.ModifyDate = DateTime.Now;
                    user.ModifyUserId = GetUserInfo().UserId;
                    user.Account = entity.Account;
                    user.RealName = entity.RealName;
                    user.RoleId = entity.RoleId;

                    result = await userBll.UpdateAsync(user, GetUserInfo().DataBaseName);
                }
                if (result)
                {
                    var roleUserEntity = await roleUserBll.GetEntityAsync(r => r.UserId == entity.UserId && r.IsDefault == 1, GetUserInfo().DataBaseName);
                    if (roleUserEntity != null)
                        await roleUserBll.DeleteAsync(roleUserEntity, GetUserInfo().DataBaseName);

                    var ruEntity = new UserRelation
                    {
                        UserRelationId = Guid.NewGuid().ToString(),
                        ObjectId = entity.RoleId,
                        UserId = entity.UserId,
                        IsDefault = 1
                    };
                    await roleUserBll.CreateAsync(ruEntity, GetUserInfo().DataBaseName);
                    return Ok(result);
                }
                else
                    return BadRequest(result);
            }
            catch (Exception)
            {
                return BadRequest(false);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(List<User> list)
        {
            try
            {
                var userAuth = await authBll.GetListAsync(a => list.Any(u => u.UserId == a.ObjectId), GetUserInfo().DataBaseName);
                var roleUser = await roleUserBll.GetListAsync(r => list.Any(u => u.UserId == r.UserId), GetUserInfo().DataBaseName);
                var result = await userBll.DeleteAsync(list, GetUserInfo().DataBaseName);
                if (result)
                {
                    await authBll.DeleteAsync(userAuth, GetUserInfo().DataBaseName);
                    await roleUserBll.DeleteAsync(roleUser, GetUserInfo().DataBaseName);

                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception)
            {
                return BadRequest(false);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveUserPermission(string userId, List<AuthorizeIdType> modules, List<AuthorizeIdType> buttons, List<AuthorizeIdType> columns, List<AuthorizeIdType> routes)
        {
            try
            {
                var result = await authBll.SavePermission(userId, GetUserInfo().UserId, 1, modules, buttons, columns, routes, GetUserInfo().DataBaseName);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(false);
            }
        }

        /// <summary>
        /// 重置用户密码为123456
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string userId)
        {
            try
            {
                var user = await userBll.GetEntityAsync(userId, GetUserInfo().DataBaseName);
                user.Secretkey = Guid.NewGuid().ToString();
                user.Password = Utility.Tools.MD5Utils.Sign("123456", user.Secretkey);

                var result = await userBll.UpdateAsync(user, GetUserInfo().DataBaseName);
                if (result)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception)
            {
                return BadRequest(false);
            }

        }
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="userId">主键值</param>
        /// <param name="state">状态：1-启动；0-禁用</param>
        [HttpPost]
        public async Task<IActionResult> UpdateState(string userId, int state)
        {
            try
            {
                var user = await userBll.GetEntityAsync(userId, GetUserInfo().DataBaseName);
                user.EnabledMark = state;

                var result = await userBll.UpdateAsync(user, GetUserInfo().DataBaseName);
                if (result)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception)
            {
                return BadRequest(false);
            }
        }
    }
}