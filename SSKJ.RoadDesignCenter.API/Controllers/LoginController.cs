using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SSKJ.RoadDesignCenter.API.Models;
using SSKJ.RoadDesignCenter.IBusines.System;
using SSKJ.RoadDesignCenter.Utility;

namespace SSKJ.RoadDesignCenter.API.Controllers
{
    [ApiController]
    [Route("api/Login/[action]")]    
    public class LoginController : ControllerBase
    {
        private readonly IUserProjectBusines userProjectBll;
        private readonly IBusines.Project.IUserBusines prjUserBll;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly AppSettings appSettings;

        public LoginController(IBusines.Project.IUserBusines prjUserBll, IUserProjectBusines userProjectBll, IHttpContextAccessor httpContextAccessor, IOptions<AppSettings> appSettings)
        {
            this.prjUserBll = prjUserBll;
            this.userProjectBll = userProjectBll;
            this.httpContextAccessor = httpContextAccessor;
            this.appSettings = appSettings.Value;
            CurrentUser.Configure(httpContextAccessor);
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginIn(LoginModel model)
        {
            if (!string.IsNullOrEmpty(model.ProjectCode))
            {
                var entity = await userProjectBll.GetEntityAsync(p => p.PrjIdentification == model.ProjectCode);
                if (entity != null)
                {
                    var str = "server=139.224.200.194;port=3306;database=" + entity.PrjDataBase + ";user id=root;password=SSKJ*147258369";
                    var user = await prjUserBll.GetEntityAsync(u => u.Account == model.UserName && u.Password == model.Password, str);
                    if (user != null)
                    {
                        CurrentUser.UserConnectionString = str;
                        CurrentUser.UserAccount = user.Account;
                        CurrentUser.UserOID = user.UserId;

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes(appSettings.Secret);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                new Claim(ClaimTypes.Name, user.Account)
                            }),
                            Expires = DateTime.UtcNow.AddDays(7),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var token = tokenHandler.CreateToken(tokenDescriptor);

                        return Ok(token);
                    }
                    else
                    {
                        return BadRequest(new { message = "用户名或密码错误，请重新输入!" });
                    }
                }
                else
                {
                    return BadRequest(new { message = "项目代码有误或不存在，请重新输入!" });
                }
            }
            else
            {
                return BadRequest(new { message = "项目代码不能为空，请输入后再试!" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = CurrentUser.UserOID;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new { message = "登录超时，请重新登录!" });
            }
            else
            {
                var user = await userProjectBll.GetEntityAsync(userId);
                return Ok(user);
            }
        }

        [HttpPost]
        public IActionResult LoginOut()
        {
            httpContextAccessor.HttpContext.Session.Clear();
            return Ok();
        }

    }
}