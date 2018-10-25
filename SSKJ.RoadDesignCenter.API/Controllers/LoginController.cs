using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SSKJ.RoadDesignCenter.API.Models;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.API.Controllers
{
    [Route("api/Login/[action]")]
    public class LoginController : BaseController
    {
        private readonly IBusines.System.IUserProjectBusines userProjectBll;
        private readonly IBusines.System.IUserBusines sysUserBll;
        private readonly IBusines.Project.IUserBusines prjUserBll;
        private readonly IBusines.Project.Authorize.IAuthorizeBusines authorizeBll;

        public LoginController(IBusines.Project.IUserBusines prjUserBll, IBusines.System.IUserProjectBusines userProjectBll, IBusines.System.IUserBusines sysUserBll, IBusines.Project.Authorize.IAuthorizeBusines authorizeBll)
        {
            this.prjUserBll = prjUserBll;
            this.userProjectBll = userProjectBll;
            this.sysUserBll = sysUserBll;
            this.authorizeBll = authorizeBll;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> LoginIn(LoginModel model)
        {
            try
            {
                var _user = new UserInfoModel();

                if (string.IsNullOrEmpty(model.ProjectCode))
                {
                    if (model.UserName.ToLower() == "system")
                    {
                        if (model.Password == "123456")
                            _user.UserId = _user.RoleId = "System";
                        else
                            return BadRequest(new { type = 0, message = "密码错误，请重新输入!" });
                    }
                    else
                    {
                        var user = await sysUserBll.GetEntityAsync(u => u.Account == model.UserName);
                        if (user == null)
                            return BadRequest(new { type = 0, message = "用户名错误或用户名不存在，请重新输入!" });
                        else
                        {
                            var paw = Utility.Tools.MD5Utils.Sign(model.Password, user.Secretkey);
                            if (user.Password != paw)
                                return BadRequest(new { type = 0, message = "密码错误，请重新输入!" });
                        }
                        _user = Utility.Tools.MapperUtils.MapTo<RoadDesignCenter.Models.SystemModel.User, UserInfoModel>(user);
                        _user.RoleId = "PrjManager";
                    }
                }
                else
                {
                    var entity = await userProjectBll.GetEntityAsync(p => p.PrjIdentification == model.ProjectCode);

                    if (entity == null)
                        return BadRequest(new { type = 0, message = "项目代码有误或不存在，请重新输入!" });

                    if (string.IsNullOrEmpty(entity.PrjDataBase))
                        return BadRequest(new { type = 0, message = "出错了，请稍后重试!" });

                    var user = await prjUserBll.GetEntityAsync(u => u.Account == model.UserName, entity.PrjDataBase);

                    if (user == null)
                        return BadRequest(new { type = 0, message = "用户名错误或用户名不存在，请重新输入!" });
                    else
                    {
                        var paw = Utility.Tools.MD5Utils.Sign(model.Password, user.Secretkey);
                        if (user.Password != paw)
                            return BadRequest(new { type = 0, message = "密码错误，请重新输入!" });
                    }

                    if (user.EnabledMark == 0)
                        return BadRequest(new { type = 0, message = "该角色已被锁定，请联系管理员解锁" });

                    _user = Utility.Tools.MapperUtils.MapTo<User, UserInfoModel>(user);
                    _user.DataBaseName = entity.PrjDataBase;
                }
                _user.TokenExpiration = DateTime.Now.AddDays(1);

                string token = Utility.Tools.TokenUtils.ToToken(_user);
                var authorize = new AuthorizeModel
                {
                    ModuleAuthorizes = await authorizeBll.GetModuleAuthorizes(2, _user.RoleId, _user.DataBaseName),
                    ButtonAuthorizes = await authorizeBll.GetButtonAuthorizes(2, _user.RoleId, _user.DataBaseName),
                    ColumnAuthorizes = await authorizeBll.GetColumnAuthorizes(2, _user.RoleId, _user.DataBaseName),
                    RouteAuthorizes = await authorizeBll.GetRouteAuthorizes(2, _user.RoleId, _user.DataBaseName)
                };

                return Ok(new { type = 1, role = _user.RoleId, authorize, token });
            }
            catch (Exception)
            {
                return BadRequest(new { type = 0, message = "出错了，请稍后重试!" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPermission()
        {
            try
            {
                var userInfo = GetUserInfo();

                if (userInfo.TokenExpiration <= DateTime.Now)
                    return BadRequest(new { type = 0, message = "登录超时，请重新登录!" });

                var authorize = new AuthorizeModel
                {
                    UserInfo=userInfo,
                    ModuleAuthorizes = await authorizeBll.GetModuleAuthorizes(2, userInfo.RoleId, userInfo.DataBaseName),
                    ButtonAuthorizes = await authorizeBll.GetButtonAuthorizes(2, userInfo.RoleId, userInfo.DataBaseName),
                    ColumnAuthorizes = await authorizeBll.GetColumnAuthorizes(2, userInfo.RoleId, userInfo.DataBaseName),
                    RouteAuthorizes = await authorizeBll.GetRouteAuthorizes(2, userInfo.RoleId, userInfo.DataBaseName)
                };

                return Ok(authorize);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "出错了，请稍后重试!" });
            }
        }

        [HttpPost]
        public IActionResult LoginOut()
        {
            HttpContext.Session.Clear();
            return Ok();
        }

    }
}