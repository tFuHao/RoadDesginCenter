using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSKJ.RoadDesignCenter.API.Controllers;
using SSKJ.RoadDesignCenter.Models.ProjectModel.Calculate;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteCalculate.Controllers
{
    [Route("api/Calculate/[action]")]
    [Area("RouteCalculate")]
    public class CalculateController : BaseController
    {
        // 计算中桩坐标
        public IActionResult CalcCenterCoord(int pageSize, int pageIndex,CalcParams calcParams)
        {
            return Ok();
        }

        // 计算边桩坐标
        public IActionResult CalcSideCoord(int pageSize, int pageIndex, CalcParams calcParams)
        {
            return Ok();
        }

        // 计算中桩高程
        public IActionResult CalcCenterHight(int pageSize, int pageIndex, CalcParams calcParams)
        {
            return Ok();
        }

        // 坐标反算桩号
        public IActionResult CoordBackCalcStake(int pageSize, int pageIndex, CalcParams calcParams)
        {
            return Ok();
        }
    }
}