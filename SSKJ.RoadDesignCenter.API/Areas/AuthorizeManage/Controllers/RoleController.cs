using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SSKJ.RoadDesignCenter.API.Controllers;
using SSKJ.RoadDesignCenter.IBusines.Project;
using SSKJ.RoadDesignCenter.IBusines.Project.Authorize;
using SSKJ.RoadDesignCenter.IBusines.System;
using SSKJ.RoadDesignCenter.Models;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using SSKJ.RoadDesignCenter.Models.SystemModel;
using IUserBusines = SSKJ.RoadDesignCenter.IBusines.Project.IUserBusines;

namespace SSKJ.RoadDesignCenter.API.Areas.AuthorizeManage.Controllers
{
    [Route("api/Role/[action]")]
    [Area("AuthorizeManage")]
    public class RoleController : BaseController
    {
        private readonly IRoleBusines RoleBus;
        private readonly IUserRelationBusines roleUserBll;

        private readonly IUserBusines UserBus;

        private readonly IModuleBusines ModuleBus;

        private readonly IButtonBusines ButtonBus;

        private readonly IColumnBusines ColumnBus;

        private readonly IAuthorizeBusines AuthorizeBus;

        public RoleController(IRoleBusines roleBus, IUserBusines userBus, IModuleBusines moduleBus, IButtonBusines buttonBus, IColumnBusines columnBus, IAuthorizeBusines authorizeBus, IUserRelationBusines roleUserBll)
        {
            RoleBus = roleBus;
            UserBus = userBus;
            ModuleBus = moduleBus;
            ButtonBus = buttonBus;
            ColumnBus = columnBus;
            AuthorizeBus = authorizeBus;
            this.roleUserBll = roleUserBll;
        }

        public async Task<IActionResult> GetRoles(int pageSize, int pageIndex)
        {
            var result = await RoleBus.GetListAsync(e => true, e => e.SortCode, true, pageSize, pageIndex, GetUserInfo().DataBaseName);
            var users = await roleUserBll.GetListAsync(GetUserInfo().DataBaseName);
            return Json(new
            {
                data = result.Item1.Select(role =>new{
                    role.RoleId,
                    role.FullName,
                    role.Description,
                    UserNumber= users.ToList().FindAll(r=>r.ObjectId==role.RoleId).Count()
                }),
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
                    input.CreateUserId = GetUserInfo().UserId;
                    var result = await RoleBus.CreateAsync(input, GetUserInfo().DataBaseName);
                    return Json(result);
                }
                else
                {
                    var entity = await RoleBus.GetEntityAsync(e => e.RoleId == input.RoleId, GetUserInfo().DataBaseName);
                    if (entity == null)
                        return null;
                    entity.FullName = input.FullName;
                    entity.Description = input.Description;
                    entity.ModifyDate = DateTime.Now;
                    entity.ModifyUserId = GetUserInfo().UserId;
                    var result = await RoleBus.UpdateAsync(entity, GetUserInfo().DataBaseName);
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
                    var users = await UserBus.GetListAsync(e => e.RoleId == i.RoleId, GetUserInfo().DataBaseName);
                    users.ToList().ForEach(async j =>
                    {
                        j.RoleId = null;
                        await UserBus.UpdateAsync(j, GetUserInfo().DataBaseName);
                    });
                });
                result = await RoleBus.DeleteAsync(list, GetUserInfo().DataBaseName);
            }

            return Json(result);
        }


        /// <summary>
        /// 更改角色成员时获取项目成员
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetUsers(string roleId)
        {
            try
            {
                var roleUsers = await roleUserBll.GetListAsync(r => r.ObjectId == roleId, GetUserInfo().DataBaseName);
                var allUsers = await UserBus.GetListAsync(e => e.EnabledMark == 1 && e.RoleId != "PrjAdmin", GetUserInfo().DataBaseName);
                return Ok(new { checkeds = roleUsers.Select(r => r.UserId), users = allUsers });
            }
            catch (Exception)
            {
                return BadRequest(false);
            }
        }
        public async Task<IActionResult> SaveRoleUsers(string roleId, List<string> userIds)
        {
            try
            {
                var users = await UserBus.GetListAsync(u => u.RoleId == roleId, GetUserInfo().DataBaseName);
                var delList = await roleUserBll.GetListAsync(r => r.ObjectId == roleId, GetUserInfo().DataBaseName);
                await roleUserBll.DeleteAsync(delList, GetUserInfo().DataBaseName);

                var list = new List<UserRelation>();
                if (userIds.Count() > 0)
                {
                    if (users.Count()>0)
                    {
                        var _users = users.ToList().FindAll(u => !(userIds.Any(id => u.UserId == id)));
                        if (_users.Count>0)
                        {
                            _users.ForEach(user =>
                            {
                                var entity = new UserRelation
                                {
                                    UserRelationId = Guid.NewGuid().ToString(),
                                    ObjectId = roleId,
                                    UserId = user.UserId,
                                    IsDefault = 1
                                };
                                list.Add(entity);
                            });
                        }
                    }
                    userIds.ForEach(id =>
                    {
                        var isDefault = 0;
                        if (users.Count() > 0 && users.Any(u => u.UserId == id))
                            isDefault = 1;
                        var entity = new UserRelation
                        {
                            UserRelationId = Guid.NewGuid().ToString(),
                            ObjectId = roleId,
                            UserId = id,
                            IsDefault = isDefault
                        };
                        list.Add(entity);
                    });
                }
                else
                {
                    if (users.Count() > 0)
                    {
                        users.ToList().ForEach(user =>
                        {
                            var entity = new UserRelation
                            {
                                UserRelationId = Guid.NewGuid().ToString(),
                                ObjectId = roleId,
                                UserId = user.UserId,
                                IsDefault = 1
                            };
                            list.Add(entity);
                        });
                    }
                }

                var result = await roleUserBll.CreateAsync(list, GetUserInfo().DataBaseName);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(false);
            }
        }

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="roleId">将要授权的角色ID</param>
        /// <param name="moduleList">可以看到的module</param>
        /// <param name="buttonList">module对应的按钮</param>
        /// <param name="columnList">页面的视图</param>
        /// <returns></returns>
        public async Task<IActionResult> SetAuthorize(string objectId, List<AuthorizeIdType> modules, List<AuthorizeIdType> buttons, List<AuthorizeIdType> columns, List<AuthorizeIdType> routes)
        {
            try
            {
                var result = await AuthorizeBus.SavePermission(objectId, GetUserInfo().UserId, 2, modules, buttons, columns, routes, GetUserInfo().DataBaseName);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(false);
            }
        }
    }
}