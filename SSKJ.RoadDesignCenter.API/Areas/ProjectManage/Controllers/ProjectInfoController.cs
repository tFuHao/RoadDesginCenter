using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SSKJ.RoadDesignCenter.API.Models;
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

        public ProjectInfoController(IUserProjectBusines userProjectBll, IPrjInfoBusines prjInfoBll)
        {
            this.userProjectBll = userProjectBll;
            this.prjInfoBll = prjInfoBll;
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

                var result = await prjInfoBll.GetListAsync(e => true, e => e.ModifyDate, true, pageSize, pageIndex, userInfo.ConnectionString);

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
        public async Task<IActionResult> SvaeEntity(RoadDesignCenter.Models.ProjectModel.ProjectInfo entity)
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

                if (!string.IsNullOrEmpty(entity.ProjectId))
                {
                    entity.ModifyDate = DateTime.Now;
                    var result = await prjInfoBll.UpdateAsync(entity, userInfo.ConnectionString);

                    if (result) return Ok(new { message = "操作成功!" });
                    else return BadRequest(new { message = "操作失败!" });
                }
                else
                {
                    entity.ProjectId = Guid.NewGuid().ToString();
                    var result = await prjInfoBll.CreateAsync(entity, userInfo.ConnectionString);

                    if (result)
                    {
                        var userProject = new UserProject
                        {
                            UserPrjId = Guid.NewGuid().ToString(),
                            UserId = userInfo.UserId,
                            PrjIdentification = ""
                        };
                        return Ok(new { message = "操作成功!" });
                    }
                    else return BadRequest(new { message = "操作失败!" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "操作失败!" });
            }
        }
    }
}