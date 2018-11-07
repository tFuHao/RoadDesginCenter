using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteCalculate.Controllers
{
    /// <summary>
    /// 计算边桩坐标
    /// </summary>
    public class SideCoordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}