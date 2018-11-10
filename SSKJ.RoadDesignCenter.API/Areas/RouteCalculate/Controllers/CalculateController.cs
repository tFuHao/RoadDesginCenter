using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSKJ.RoadDesignCenter.API.Controllers;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteCalculate;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using SSKJ.RoadDesignCenter.Models.ProjectModel.Calculate;
using SSKJ.RoadDesignCenter.Utility;
using SSKJ.RoadDesignCenter.Utility.CalculateModels;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteCalculate.Controllers
{
    [Route("api/Calculate/[action]")]
    [Area("RouteCalculate")]
    public class CalculateController : BaseController
    {
        public readonly IBrokenChainageBusines brokenBll;
        public readonly IFlatCurve_IntersectionBusines intersectionBll;
        public readonly IVerticalCurve_GradeChangePointBusines gradeBll;

        public readonly ICalculateBusines calculateBll;

        //public readonly CalculateUtility calculateUtility;

        public CalculateController(IBrokenChainageBusines brokenBll, IFlatCurve_IntersectionBusines intersectionBll, IVerticalCurve_GradeChangePointBusines gradeBll, ICalculateBusines calculateBll)
        {
            this.brokenBll = brokenBll;
            this.intersectionBll = intersectionBll;
            this.gradeBll = gradeBll;
            this.calculateBll = calculateBll;
            //this.calculateUtility = calculateUtility;
        }

        // 计算中桩坐标
        [HttpPost]
        public async Task<IActionResult> CalcCenterCoord(CalcParams calcParams)
        {
            var broken = await brokenBll.GetListAsync(b => b.RouteId == calcParams.RouteId, UserInfo.DataBaseName);
            var intersection = await intersectionBll.GetListAsync(i => i.RouteId == calcParams.RouteId, UserInfo.DataBaseName);
            var grade = await gradeBll.GetListAsync(g => g.RouteId == calcParams.RouteId, UserInfo.DataBaseName);

            var _broken = Utility.Tools.MapperUtils.MapToList<RoadDesignCenter.Models.ProjectModel.BrokenChainage, Utility.CalculateModels.BrokenChainage>(broken);
            var _intersection = Utility.Tools.MapperUtils.MapToList<FlatCurve_Intersection, Intersection>(intersection);
            var _grade = Utility.Tools.MapperUtils.MapToList<VerticalCurve_GradeChangePoint, GradeChangePoint>(grade);

            try
            {
                CalculateUtility calculateUtility = new CalculateUtility();
                var list = calculateUtility.CalcCenterCoord(_intersection, _grade, _broken, calcParams.BeginStake.ToString(), calcParams.EndStake.ToString(), Convert.ToInt32(calcParams.Interval), 1);

                //var list = calculateBll.CalcCenterCoord(calcParams.BeginStake.ToString(), calcParams.EndStake.ToString(), Convert.ToInt32(calcParams.Interval),calcParams.RouteId, 1,UserInfo.DataBaseName);
                return SuccessData(list);
            }
            catch (Exception ex)
            {

                throw;
            }
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