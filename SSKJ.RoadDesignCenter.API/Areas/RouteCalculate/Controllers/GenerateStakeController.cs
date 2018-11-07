using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteCalculate.Controllers
{
    /// <summary>
    /// 生成桩号表
    /// </summary>
    public class GenerateStakeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}