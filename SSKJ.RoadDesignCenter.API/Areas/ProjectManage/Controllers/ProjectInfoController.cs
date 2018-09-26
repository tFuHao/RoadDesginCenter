using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SSKJ.RoadDesignCenter.API.Areas.ProjectManage.Controllers
{
    public class ProjectInfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}