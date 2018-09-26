using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSKJ.RoadDesignCenter.IBusines.Project;
using SSKJ.RoadDesignCenter.Utility;

namespace SSKJ.RoadDesignCenter.API.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserBusines prjUserBll;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UsersController(IUserBusines prjUserBll, IHttpContextAccessor httpContextAccessor)
        {
            this.prjUserBll = prjUserBll;
            this.httpContextAccessor = httpContextAccessor;
            CurrentUser.Configure(httpContextAccessor);
        }

        public async Task<IActionResult> Index()
        {
            var connectionString = CurrentUser.UserConnectionString;
            if (connectionString==null)
            {
                return RedirectToAction("Index", "Login");
            }
            var user = await prjUserBll.GetListAsync(connectionString);
            var users = await prjUserBll.GetListAsync(u => true, u=>u.UserId, false, 30, 1, connectionString);

            return View(users.Item1);
        }
    }
}