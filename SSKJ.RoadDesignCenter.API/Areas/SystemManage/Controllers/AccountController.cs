using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSKJ.RoadDesignCenter.IBusines.System;
using SSKJ.RoadDesignCenter.Models.SystemModel;

namespace SSKJ.RoadDesignCenter.API.Areas.SystemManage.Controllers
{
    [Route("api/Account/[action]")]
    [Area("SystemManage")]
    public class AccountController : Controller
    {
        private readonly IUserBusines sysUserBll;
        private readonly IUserProjectBusines userProjectBll;
        private readonly IAreaBusines areaBll;

        public AccountController(IUserBusines sysUserBll, IUserProjectBusines userProjectBll, IAreaBusines areaBll)
        {
            this.sysUserBll = sysUserBll;
            this.userProjectBll = userProjectBll;
            this.areaBll = areaBll;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountData(int pageSize, int pageIndex)
        {
            try
            {
                var data = await sysUserBll.GetListAsync(e => true, e => e.CreateDate, true, pageSize, pageIndex);
                return Ok(new
                {
                    data = data.Item1,
                    count = data.Item2
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAreaData()
        {
            var data = await areaBll.GetListAsync();
            return Ok(data);
        }
    }
}