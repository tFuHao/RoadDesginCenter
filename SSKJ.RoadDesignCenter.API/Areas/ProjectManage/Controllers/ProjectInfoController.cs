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

                var result = await prjInfoBll.GetListAsync(e => true, e => e.ModifyDate, true, pageSize, pageIndex, userInfo.dataBaseName);

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
        public async Task<IActionResult> SvaeEntity(ProjectInfoModel entity)
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

                if (!string.IsNullOrEmpty(entity.PrjInfo.ProjectId))
                {
                    entity.PrjInfo.ModifyDate = DateTime.Now;
                    var result = await prjInfoBll.UpdateAsync(entity.PrjInfo, userInfo.dataBaseName);

                    if (result) return Ok(new { message = "操作成功!" });
                    else return BadRequest(new { message = "操作失败!" });
                }
                else
                {
                    var projects = await userProjectBll.GetListAsync();
                    int? prjSerialNumber = 0;
                    if (projects.Count() > 0) prjSerialNumber = projects.Select(p => p.SerialNumber).Max() + 1;
                    else prjSerialNumber++;

                    var dbName = "road_project_00" + prjSerialNumber;
                    var db = await Utility.Tools.DataBaseUtils.CreateDataBase(dbName);
                    if (!db) return BadRequest(new { message = "初始化数据库失败!" });

                    entity.PrjInfo.ProjectId = Guid.NewGuid().ToString();
                    entity.PrjInfo.SerialNumber = prjSerialNumber;
                    var result = await prjInfoBll.CreateAsync(entity.PrjInfo, userInfo.dataBaseName);

                    entity.UserInfo.UserId = Guid.NewGuid().ToString();
                    entity.UserInfo.CreateDate = DateTime.Now;
                    entity.UserInfo.CreateUserId = userInfo.UserId;
                    entity.UserInfo.RoleId = "PrjManager";
                    var resultU = await userBll.CreateAsync(entity.UserInfo);

                    if (!result) return BadRequest(new { message = "操作失败!" });

                    var userProject = new UserProject
                    {
                        UserPrjId = Guid.NewGuid().ToString(),
                        UserId = userInfo.UserId,
                        PrjIdentification = "",
                        PrjDataBase = dbName
                    };
                    var resultUP = await userProjectBll.CreateAsync(userProject);
                    if (!resultUP)
                    {
                       var a= await prjInfoBll.DeleteAsync(entity.PrjInfo, userInfo.dataBaseName);
                        if (a) await Utility.Tools.DataBaseUtils.DeleteDataBase(dbName);
                    }
                    return Ok(new { message = "操作成功!" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "操作失败!" });
            }
        }
    }
}