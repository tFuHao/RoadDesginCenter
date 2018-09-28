using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteManage_RouteElement.Controllers
{
    [Route("api/Default/[action]")]
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return Content("");
        }
    }
}