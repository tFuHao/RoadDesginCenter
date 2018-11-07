using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteCalculate.Controllers
{
    /// <summary>
    /// 坐标反算桩号
    /// </summary>
    public class CoordBackCalculateStakeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}