using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SSKJ.RoadDesignCenter.API.Models;
using SSKJ.RoadDesignCenter.IBusines.Project;
using SSKJ.RoadDesignCenter.IBusines.Project.ProjectInfo;
using SSKJ.RoadDesignCenter.IBusines.System;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using SSKJ.RoadDesignCenter.Models.SystemModel;

namespace SSKJ.RoadDesignCenter.API.Areas.ProjectManage.Controllers
{
    [Route("api/ProjectInfo/[action]")]
    [Area("ProjectManage")]
    public class ProjectInfoController : Controller
    {
        private readonly IUserProjectBusines userProjectBll;
        private readonly IPrjInfoBusines prjInfoBll;
        private readonly IBusines.Project.IUserBusines userBll;

        public ProjectInfoController(IUserProjectBusines userProjectBll, IPrjInfoBusines prjInfoBll, IBusines.Project.IUserBusines userBll)
        {
            this.userProjectBll = userProjectBll;
            this.prjInfoBll = prjInfoBll;
            this.userBll = userBll;
        }

        [HttpGet]
        public async Task<IActionResult> GetList(int pageSize, int pageIndex)
        {
            try
            {
                string strToken = "";
                if (Request.Headers.TryGetValue("x-access-token", out StringValues token))
                    strToken = token.ToString();

                if (string.IsNullOrEmpty(strToken))
                    return BadRequest(new { message = "登录超时，请重新登录!" });

                var userInfo = Utility.Tools.TokenUtils.ToObject<UserInfoModel>(strToken);

                if (userInfo.TokenExpiration <= DateTime.Now)
                    return BadRequest(new { message = "登录超时，请重新登录!" });

                var result = new Tuple<IEnumerable<UserProject>, int>(null, 0);

                if (userInfo.RoleId == "PrjManager")
                    result = await userProjectBll.GetListAsync(e => e.UserId == userInfo.UserId, e => e.SerialNumber, true, pageSize, pageIndex);
                else if (userInfo.RoleId == "System")
                    result = new Tuple<IEnumerable<UserProject>, int>(null, 0);
                else
                {
                    var entity = await userProjectBll.GetListAsync(e => e.PrjDataBase == userInfo.DataBaseName);
                    result = new Tuple<IEnumerable<UserProject>, int>(entity, entity.Count());
                }

                return Ok(new
                {
                    data = result.Item1,
                    count = result.Item2
                });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "操作失败!" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveEntity(ProjectInfoModel entity)
        {
            var dbName = "";
            var upId = "";
            try
            {
                string strToken = "";
                if (Request.Headers.TryGetValue("x-access-token", out StringValues token))
                    strToken = token.ToString();

                if (string.IsNullOrEmpty(strToken))
                    return BadRequest(new { message = "登录超时，请重新登录!" });

                var userInfo = Utility.Tools.TokenUtils.ToObject<UserInfoModel>(strToken);

                if (userInfo.RoleId != "PrjManager")
                    return BadRequest(new { message = "权限不足，操作失败!" });

                if (userInfo.TokenExpiration <= DateTime.Now)
                    return BadRequest(new { message = "登录超时，请重新登录!" });

                if (!string.IsNullOrEmpty(entity.PrjInfo.ProjectId))
                {
                    var up = await userProjectBll.GetEntityAsync(u => u.ProjectId == entity.PrjInfo.ProjectId);
                    entity.PrjInfo.ModifyDate = DateTime.Now;
                    var result = await prjInfoBll.UpdateAsync(entity.PrjInfo, up.PrjDataBase);


                    up.PrjName = entity.PrjInfo.PrjName;
                    up.Description = entity.PrjInfo.Description;
                    up.DesignUnit = entity.PrjInfo.DesignUnit;
                    up.OwnerUnit = entity.PrjInfo.OwnerUnit;
                    up.SupervisoryUnit = entity.PrjInfo.SupervisoryUnit;
                    up.ConstructionUnit = entity.PrjInfo.ConstructionUnit;
                    up.ModifyDate = DateTime.Now;
                    await userProjectBll.UpdateAsync(up);

                    if (result) return Ok(new { message = "操作成功!" });
                    else return BadRequest(new { message = "操作失败!" });
                }
                else
                {
                    var projects = await userProjectBll.GetListAsync();
                    int? prjSerialNumber = 0;
                    if (projects.Count() > 0) prjSerialNumber = projects.Select(p => p.SerialNumber).Max() + 1;
                    else prjSerialNumber++;

                    dbName = "road_project_00" + prjSerialNumber;
                    var db = await Utility.Tools.DataBaseUtils.CreateDataBase(dbName);
                    if (!db) return BadRequest(new { message = "初始化数据库失败!" });

                    entity.PrjInfo.ProjectId = Guid.NewGuid().ToString();
                    entity.PrjInfo.SerialNumber = prjSerialNumber;
                    var result = await prjInfoBll.CreateAsync(entity.PrjInfo, dbName);

                    var resultU = false;
                    if (string.IsNullOrEmpty(entity.UserInfo.UserId))
                    {
                        entity.UserInfo.UserId = Guid.NewGuid().ToString();
                        entity.UserInfo.Secretkey = Guid.NewGuid().ToString();
                        entity.UserInfo.Password= Utility.Tools.MD5Utils.Sign(entity.UserInfo.Password, entity.UserInfo.Secretkey);
                        entity.UserInfo.CreateDate = DateTime.Now;
                        entity.UserInfo.CreateUserId = userInfo.UserId;
                        entity.UserInfo.RoleId = "PrjAdmin";

                        resultU = await userBll.CreateAsync(entity.UserInfo, dbName);
                    }

                    if (!result && !resultU)
                    {
                        await Utility.Tools.DataBaseUtils.DeleteDataBase(dbName);
                        return BadRequest(new { message = "操作失败!" });
                    }

                    var userProject = new UserProject
                    {
                        UserPrjId = Guid.NewGuid().ToString(),
                        UserId = userInfo.UserId,
                        ProjectId = entity.PrjInfo.ProjectId,
                        SerialNumber = prjSerialNumber,
                        PrjIdentification = "",
                        PrjDataBase = dbName,
                        PrjName = entity.PrjInfo.PrjName,
                        Description = entity.PrjInfo.Description,
                        OwnerUnit=entity.PrjInfo.OwnerUnit,
                        DesignUnit = entity.PrjInfo.DesignUnit,
                        SupervisoryUnit = entity.PrjInfo.SupervisoryUnit,
                        ConstructionUnit = entity.PrjInfo.ConstructionUnit,
                        ModifyDate = DateTime.Now
                    };
                    var resultUP = await userProjectBll.CreateAsync(userProject);
                    upId = userProject.UserPrjId;
                    if (!resultUP)
                    {
                        var a = await prjInfoBll.DeleteAsync(entity.PrjInfo, dbName);
                        if (a) await Utility.Tools.DataBaseUtils.DeleteDataBase(dbName);
                    }
                    return Ok(new { message = "操作成功!" });
                }
            }
            catch (Exception)
            {
                await Utility.Tools.DataBaseUtils.DeleteDataBase(dbName);
                if (!string.IsNullOrEmpty(upId)) await userProjectBll.DeleteAsync(upId);

                return BadRequest(new { message = "操作失败!" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(List<UserProject> list)
        {
            try
            {
                string strToken = "";
                if (Request.Headers.TryGetValue("x-access-token", out StringValues token))
                    strToken = token.ToString();

                if (string.IsNullOrEmpty(strToken))
                    return BadRequest(new { message = "登录超时，请重新登录!" });

                var userInfo = Utility.Tools.TokenUtils.ToObject<UserInfoModel>(strToken);

                if (userInfo.TokenExpiration <= DateTime.Now)
                    return BadRequest(new { message = "登录超时，请重新登录!" });

                list.ForEach(async p =>
                {
                    await Utility.Tools.DataBaseUtils.DeleteDataBase(p.PrjDataBase);
                });
                var result = await userProjectBll.DeleteAsync(list);
                if (result)
                    return Ok(new { message = "操作成功!" });
                else
                    return BadRequest(new { message = "操作失败!" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "操作失败!" });
            }
        }
    }
}