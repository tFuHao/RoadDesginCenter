using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSKJ.RoadDesignCenter.IBusines.System;
using SSKJ.RoadDesignCenter.Models.SystemModel;

namespace SSKJ.RoadDesignCenter.API.Controllers
{
    [Route("/api/SystemInfo/[action]")]
    [ApiController]
    public class SystemInfoController : Controller
    {
        private readonly IModuleBusines moduleBll;

        public SystemInfoController(IModuleBusines moduleBll)
        {
            this.moduleBll = moduleBll;
        }

        [HttpGet]
        public async Task<ActionResult> GetModuleAsync()
        {
            var modules = await moduleBll.GetListAsync();
            return Json(modules);
        }
    }
}