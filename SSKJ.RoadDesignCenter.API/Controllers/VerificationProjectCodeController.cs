using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SSKJ.RoadDesignCenter.IBusines.System;

namespace SSKJ.RoadDesignCenter.API.Controllers
{
    public class VerificationProjectCodeController : Controller
    {
        private readonly IModuleBusines sysModuleBll;
        private readonly IMemoryCache memoryCache;


        public VerificationProjectCodeController(IModuleBusines sysModuleBll, IMemoryCache memoryCache)
        {
            this.sysModuleBll = sysModuleBll;
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string code)
        {
            var list = sysModuleBll.GetList();
            memoryCache.Set("ProjectDB", code);
            //HttpContext.Session.SetString("ProjectCode", "road_project_001");
            return Content("value1");
        }
    }
}