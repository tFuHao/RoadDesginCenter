using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSKJ.RoadDesignCenter.API.Models;
using SSKJ.RoadDesignCenter.IBusines.System;
using SSKJ.RoadDesignCenter.Utility;

namespace SSKJ.RoadDesignCenter.API.Controllers
{
    [Route("api/Login/[action]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IUserProjectBusines userProjectBll;
        private readonly IBusines.Project.IUserBusines prjUserBll;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LoginController(IBusines.Project.IUserBusines prjUserBll, IUserProjectBusines userProjectBll, IHttpContextAccessor httpContextAccessor)
        {
            this.prjUserBll = prjUserBll;
            this.userProjectBll = userProjectBll;
            this.httpContextAccessor = httpContextAccessor;
            CurrentUser.Configure(httpContextAccessor);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetUsers(LoginModel model)
        {
            var entity = await userProjectBll.GetEntityAsync(p => p.PrjDataBase == model.ProjectCode);
            var list = await userProjectBll.GetListAsync(p => p.PrjDataBase == model.ProjectCode);
            if (entity != null)
            {
                var str = "server=139.224.200.194;port=3306;database=" + model.ProjectCode + ";user id=root;password=SSKJ*147258369";
                var user = await prjUserBll.GetEntityAsync(u => u.Account == model.UserName && u.Password == model.Password, str);
                if (user != null)
                {
                    CurrentUser.UserConnectionString = str;

                    List<Claim> claim = new List<Claim>();

                    var aaa = new ClaimsIdentity("123");
                    aaa.AddClaims(claim);

                    var bbb = new ClaimsPrincipal(aaa);
                    await AuthenticationHttpContextExtensions.SignInAsync(HttpContext, bbb, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(5),
                        IsPersistent = false,
                        AllowRefresh = false
                    });

                    return RedirectToAction("Index", "Users");
                }
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

    }
}