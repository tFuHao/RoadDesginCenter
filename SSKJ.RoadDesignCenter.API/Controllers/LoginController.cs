using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Jose;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using SSKJ.RoadDesignCenter.API.Models;
using SSKJ.RoadDesignCenter.IBusines.System;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using SSKJ.RoadDesignCenter.Utility;

namespace SSKJ.RoadDesignCenter.API.Controllers
{
    [Route("api/Login/[action]")]
    public class LoginController : Controller
    {
        private readonly IUserProjectBusines userProjectBll;
        private readonly IBusines.Project.IUserBusines prjUserBll;

        public LoginController(IBusines.Project.IUserBusines prjUserBll, IUserProjectBusines userProjectBll)
        {
            this.prjUserBll = prjUserBll;
            this.userProjectBll = userProjectBll;
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
                if (string.IsNullOrEmpty(model.ProjectCode))
                    return BadRequest(new { message = "项目代码不能为空，请输入后再试!" });

                var entity = await userProjectBll.GetEntityAsync(p => p.PrjIdentification == model.ProjectCode);

                if (entity == null)
                    return BadRequest(new { message = "项目代码有误或不存在，请重新输入!" });

                if (string.IsNullOrEmpty(entity.PrjDataBase))
                    return BadRequest(new { message = "出错了，请稍后重试!" });

                var str = "server=139.224.200.194;port=3306;database=" + entity.PrjDataBase + ";user id=root;password=SSKJ*147258369";
                var user = await prjUserBll.GetEntityAsync(u => u.Account == model.UserName && u.Password == model.Password, str);

                if (user == null)
                    return BadRequest(new { message = "用户名或密码错误，请重新输入!" });

                var _user = Utility.Tools.MapperUtils.MapTo<User, UserInfoModel>(user);
                _user.ConnectionString = str;
                _user.TokenExpiration = DateTime.Now.AddDays(1);

                string token = Utility.Tools.TokenUtils.ToToken(_user);

                return Ok(new { code = 1, token });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "出错了，请稍后重试!" });
            }
        }

        [HttpGet]
        public IActionResult GetUserInfo()
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

                return Ok(userInfo);
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