using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSKJ.RoadDesignCenter.API.Controllers;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using SSKJ.RouteCalculate;
using SSKJ.RouteCalculate.CalculateModels;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteCalculate.Controllers
{
    [Route("api/Calculate/[action]")]
    [Area("RouteCalculate")]
    public class CalculateController : BaseController
    {
        public readonly IBrokenChainageBusines brokenBll;
        public readonly IFlatCurve_IntersectionBusines intersectionBll;
        public readonly IVerticalCurve_GradeChangePointBusines gradeBll;
        public readonly IAddStakeBusines stakesBll;

        public CalculateController(IBrokenChainageBusines brokenBll, IFlatCurve_IntersectionBusines intersectionBll, IVerticalCurve_GradeChangePointBusines gradeBll, IAddStakeBusines stakesBll)
        {
            this.brokenBll = brokenBll;
            this.intersectionBll = intersectionBll;
            this.gradeBll = gradeBll;
            this.stakesBll = stakesBll;
        }

        // 计算中桩坐标
        [HttpPost]
        public async Task<IActionResult> CalcCenterCoord(CalcParams calcParams)
        {
            try
            {
                CalculateUtility calculateUtility = new CalculateUtility();
                var stakes = await stakesBll.GetListAsync(s => s.RouteId == calcParams.RouteId, UserInfo.DataBaseName);
                var brokens = await brokenBll.GetListAsync(b => b.RouteId == calcParams.RouteId, UserInfo.DataBaseName);
                var intersections = await intersectionBll.GetListAsync(i => i.RouteId == calcParams.RouteId, UserInfo.DataBaseName);
                //var grade = await gradeBll.GetListAsync(g => g.RouteId == calcParams.RouteId, UserInfo.DataBaseName);

                var _broken = Utility.Tools.MapperUtils.MapToList<RoadDesignCenter.Models.ProjectModel.BrokenChainage, SSKJ.RouteCalculate.CalculateModels.BrokenChainage>(brokens);
                var _intersection = Utility.Tools.MapperUtils.MapToList<FlatCurve_Intersection, Intersection>(intersections);
                //var _grade = Utility.Tools.MapperUtils.MapToList<VerticalCurve_GradeChangePoint, GradeChangePoint>(grade);

                List<GradeChangePoint> gradeChang = new List<GradeChangePoint>()
            {
                new GradeChangePoint(){ Stake = "A0.000", H = 870.9000, R = 0.0000 },
                new GradeChangePoint(){ Stake = "A150.000", H = 870.5565, R = 200000.0000 },
                new GradeChangePoint(){ Stake = "A750.000", H = 868.7565, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "A1120.000",H = 869.8665, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "A1540.000", H = 866.8065, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "A1900.000", H = 865.7265, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "A2100.000", H = 866.3265, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "A2470.000", H = 865.2165, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "A2720.000", H = 865.9665, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "A3650.000", H = 863.1765, R = 120000.0000 },
                new GradeChangePoint(){ Stake = "A4200.000", H = 864.8265, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "A4800.000", H = 861.3112, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "A5212.030", H = 862.5490, R = 0.0000 },
                new GradeChangePoint(){ Stake = "B5430.000", H = 863.2390, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "B5920.000", H = 860.7890, R = 6500.0000 },
                new GradeChangePoint(){ Stake = "B6500.000", H = 878.1890, R = 14000.0000 },
                new GradeChangePoint(){ Stake = "B6780.000", H = 890.7490, R = 7000.0000 },
                new GradeChangePoint(){ Stake = "B7000.000", H = 897.1290, R = 3500.0000 },
                new GradeChangePoint(){ Stake = "C7970.000", H = 881.5440, R = 15000.0000 },
                new GradeChangePoint(){ Stake = "C9180.000", H = 876.7040, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "C10150.000", H = 861.5540, R = 7000.0000 },
                new GradeChangePoint(){ Stake = "C10600.000", H = 874.1540, R = 6000.0000 },
                new GradeChangePoint(){ Stake = "C10910.000", H = 871.6740, R = 6000.0000 },
                new GradeChangePoint(){ Stake = "C11280.000", H = 855.3940, R = 6000.0000 },
                new GradeChangePoint(){ Stake = "C11860.000", H = 853.1830, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "C13330.000", H = 872.2930, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "C14010.000", H = 877.7330, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "C14840.000", H = 881.3630, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "C15900.000", H = 871.1230, R = 16000.0000 },
                new GradeChangePoint(){ Stake = "C17390.000", H = 881.5930, R = 12000.0000 },
                new GradeChangePoint(){ Stake = "C18180.000", H = 876.0630, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "C20200.000", H = 886.1630, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "C20650.000", H = 884.3630, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "C21500.000", H = 888.6130, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "C22240.000", H = 883.4330, R = 90000.0000 },
                new GradeChangePoint(){ Stake = "C23080.000", H = 889.3130, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "D24760.000", H = 872.3930, R = 10000.0000 },
                new GradeChangePoint(){ Stake = "D25650.000", H = 881.2930, R = 10000.0000 },
                new GradeChangePoint(){ Stake = "D26400.000", H = 873.7930, R = 15000.0000 },
                new GradeChangePoint(){ Stake = "D27010.000", H = 876.2330, R = 12000.0000 },
                new GradeChangePoint(){ Stake = "E31010.000", H = 811.8100, R = 15000.0000 },
                new GradeChangePoint(){ Stake = "E31460.000", H = 804.6100, R = 12000.0000 },
                new GradeChangePoint(){ Stake = "E32400.000", H = 801.7900, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "E32700.000", H = 802.6900, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "E33430.000", H = 799.7700, R = 80000.0000 },
                new GradeChangePoint(){ Stake = "E34250.000", H = 802.2300, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "E34780.000", H = 805.7800, R = 26000.0000 },
                new GradeChangePoint(){ Stake = "E35170.000", H = 804.6100, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E35600.000", H = 805.9000, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E36160.000", H = 804.2200, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E36440.000", H = 805.0600, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E37570.000", H = 801.6700, R = 32000.0000 },
                new GradeChangePoint(){ Stake = "E38040.000", H = 806.3700, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "E38670.000", H = 818.9700, R = 28000.0000 },
                new GradeChangePoint(){ Stake = "E39040.000", H = 802.3200, R = 28000.0000 },
                new GradeChangePoint(){ Stake = "E39740.000", H = 807.2200, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E40620.000", H = 801.9400, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E41270.000", H = 806.4900, R = 28000.0000 },
                new GradeChangePoint(){ Stake = "E41800.000", H = 817.0900, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "E42410.000", H = 803.0600, R = 70000.0000 },
                new GradeChangePoint(){ Stake = "E42820.000", H = 804.2900, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "E43400.000", H = 802.5500, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "E43800.000", H = 803.7500, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "E44700.000", H = 799.2500, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "E45350.000", H = 795.3500, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "E45820.000", H = 796.7600, R = 35000.0000 },
                new GradeChangePoint(){ Stake = "E47350.000", H = 824.3000, R = 22000.0000 },
                new GradeChangePoint(){ Stake = "E48000.000", H = 826.2500, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "E48520.000", H = 830.9300, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "E48750.000", H = 824.4900, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "E49350.000", H = 826.8900, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E50000.000", H = 822.3400, R = 14000.0000 },
                new GradeChangePoint(){ Stake = "E50630.000", H = 790.8400, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "F51700.000", H = 787.6300, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "F52410.000", H = 795.4400, R = 10000.0000 },
                new GradeChangePoint(){ Stake = "F52740.000", H = 789.1700, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "F53030.000", H = 797.8700, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "F53365.000", H = 786.8100, R = 28000.0000 },
                new GradeChangePoint(){ Stake = "F54000.000", H = 788.7200, R = 46000.0000 },
                new GradeChangePoint(){ Stake = "F54940.000", H = 835.7200, R = 38000.0000 },
                new GradeChangePoint(){ Stake = "F55450.000", H = 837.2500, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "F55670.000", H = 832.8500, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "F56020.000", H = 836.0000, R = 12000.0000 },
                new GradeChangePoint(){ Stake = "F56420.000", H = 828.8000, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "F57250.000", H = 787.3000, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "F57440.000", H = 786.6800, R = 0.0000 }
            };

                if (calcParams.AddStake && stakes.Count() > 0)
                    calcParams.Stakes = stakes.OrderBy(o => o.SerialNumber).Select(s => Convert.ToDouble(s.Stake)).ToList();

                RouteElementModel routeElement = new RouteElementModel()
                {
                    Intersections = _intersection.OrderBy(o => o.SerialNumber),
                    BrokenChainages = _broken.OrderBy(o => o.SerialNumber),
                    GradeChangePoints = gradeChang
                };
                var list = calculateUtility.CalcCenterCoord(routeElement, calcParams);

                return SuccessData(list);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // 计算边桩坐标
        [HttpPost]
        public async Task<IActionResult> CalcSideCoord(CalcParams calcParams)
        {
            try
            {
                CalculateUtility calculateUtility = new CalculateUtility();
                var stakes = await stakesBll.GetListAsync(s => s.RouteId == calcParams.RouteId, UserInfo.DataBaseName);
                var brokens = await brokenBll.GetListAsync(b => b.RouteId == calcParams.RouteId, UserInfo.DataBaseName);
                var intersections = await intersectionBll.GetListAsync(i => i.RouteId == calcParams.RouteId, UserInfo.DataBaseName);
                //var grade = await gradeBll.GetListAsync(g => g.RouteId == calcParams.RouteId, UserInfo.DataBaseName);

                var _broken = Utility.Tools.MapperUtils.MapToList<RoadDesignCenter.Models.ProjectModel.BrokenChainage, SSKJ.RouteCalculate.CalculateModels.BrokenChainage>(brokens);
                var _intersection = Utility.Tools.MapperUtils.MapToList<FlatCurve_Intersection, Intersection>(intersections);
                //var _grade = Utility.Tools.MapperUtils.MapToList<VerticalCurve_GradeChangePoint, GradeChangePoint>(grade);

                List<GradeChangePoint> gradeChang = new List<GradeChangePoint>()
            {
                new GradeChangePoint(){ Stake = "A0.000", H = 870.9000, R = 0.0000 },
                new GradeChangePoint(){ Stake = "A150.000", H = 870.5565, R = 200000.0000 },
                new GradeChangePoint(){ Stake = "A750.000", H = 868.7565, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "A1120.000",H = 869.8665, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "A1540.000", H = 866.8065, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "A1900.000", H = 865.7265, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "A2100.000", H = 866.3265, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "A2470.000", H = 865.2165, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "A2720.000", H = 865.9665, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "A3650.000", H = 863.1765, R = 120000.0000 },
                new GradeChangePoint(){ Stake = "A4200.000", H = 864.8265, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "A4800.000", H = 861.3112, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "A5212.030", H = 862.5490, R = 0.0000 },
                new GradeChangePoint(){ Stake = "B5430.000", H = 863.2390, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "B5920.000", H = 860.7890, R = 6500.0000 },
                new GradeChangePoint(){ Stake = "B6500.000", H = 878.1890, R = 14000.0000 },
                new GradeChangePoint(){ Stake = "B6780.000", H = 890.7490, R = 7000.0000 },
                new GradeChangePoint(){ Stake = "B7000.000", H = 897.1290, R = 3500.0000 },
                new GradeChangePoint(){ Stake = "C7970.000", H = 881.5440, R = 15000.0000 },
                new GradeChangePoint(){ Stake = "C9180.000", H = 876.7040, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "C10150.000", H = 861.5540, R = 7000.0000 },
                new GradeChangePoint(){ Stake = "C10600.000", H = 874.1540, R = 6000.0000 },
                new GradeChangePoint(){ Stake = "C10910.000", H = 871.6740, R = 6000.0000 },
                new GradeChangePoint(){ Stake = "C11280.000", H = 855.3940, R = 6000.0000 },
                new GradeChangePoint(){ Stake = "C11860.000", H = 853.1830, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "C13330.000", H = 872.2930, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "C14010.000", H = 877.7330, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "C14840.000", H = 881.3630, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "C15900.000", H = 871.1230, R = 16000.0000 },
                new GradeChangePoint(){ Stake = "C17390.000", H = 881.5930, R = 12000.0000 },
                new GradeChangePoint(){ Stake = "C18180.000", H = 876.0630, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "C20200.000", H = 886.1630, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "C20650.000", H = 884.3630, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "C21500.000", H = 888.6130, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "C22240.000", H = 883.4330, R = 90000.0000 },
                new GradeChangePoint(){ Stake = "C23080.000", H = 889.3130, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "D24760.000", H = 872.3930, R = 10000.0000 },
                new GradeChangePoint(){ Stake = "D25650.000", H = 881.2930, R = 10000.0000 },
                new GradeChangePoint(){ Stake = "D26400.000", H = 873.7930, R = 15000.0000 },
                new GradeChangePoint(){ Stake = "D27010.000", H = 876.2330, R = 12000.0000 },
                new GradeChangePoint(){ Stake = "E31010.000", H = 811.8100, R = 15000.0000 },
                new GradeChangePoint(){ Stake = "E31460.000", H = 804.6100, R = 12000.0000 },
                new GradeChangePoint(){ Stake = "E32400.000", H = 801.7900, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "E32700.000", H = 802.6900, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "E33430.000", H = 799.7700, R = 80000.0000 },
                new GradeChangePoint(){ Stake = "E34250.000", H = 802.2300, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "E34780.000", H = 805.7800, R = 26000.0000 },
                new GradeChangePoint(){ Stake = "E35170.000", H = 804.6100, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E35600.000", H = 805.9000, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E36160.000", H = 804.2200, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E36440.000", H = 805.0600, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E37570.000", H = 801.6700, R = 32000.0000 },
                new GradeChangePoint(){ Stake = "E38040.000", H = 806.3700, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "E38670.000", H = 818.9700, R = 28000.0000 },
                new GradeChangePoint(){ Stake = "E39040.000", H = 802.3200, R = 28000.0000 },
                new GradeChangePoint(){ Stake = "E39740.000", H = 807.2200, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E40620.000", H = 801.9400, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E41270.000", H = 806.4900, R = 28000.0000 },
                new GradeChangePoint(){ Stake = "E41800.000", H = 817.0900, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "E42410.000", H = 803.0600, R = 70000.0000 },
                new GradeChangePoint(){ Stake = "E42820.000", H = 804.2900, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "E43400.000", H = 802.5500, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "E43800.000", H = 803.7500, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "E44700.000", H = 799.2500, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "E45350.000", H = 795.3500, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "E45820.000", H = 796.7600, R = 35000.0000 },
                new GradeChangePoint(){ Stake = "E47350.000", H = 824.3000, R = 22000.0000 },
                new GradeChangePoint(){ Stake = "E48000.000", H = 826.2500, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "E48520.000", H = 830.9300, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "E48750.000", H = 824.4900, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "E49350.000", H = 826.8900, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E50000.000", H = 822.3400, R = 14000.0000 },
                new GradeChangePoint(){ Stake = "E50630.000", H = 790.8400, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "F51700.000", H = 787.6300, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "F52410.000", H = 795.4400, R = 10000.0000 },
                new GradeChangePoint(){ Stake = "F52740.000", H = 789.1700, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "F53030.000", H = 797.8700, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "F53365.000", H = 786.8100, R = 28000.0000 },
                new GradeChangePoint(){ Stake = "F54000.000", H = 788.7200, R = 46000.0000 },
                new GradeChangePoint(){ Stake = "F54940.000", H = 835.7200, R = 38000.0000 },
                new GradeChangePoint(){ Stake = "F55450.000", H = 837.2500, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "F55670.000", H = 832.8500, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "F56020.000", H = 836.0000, R = 12000.0000 },
                new GradeChangePoint(){ Stake = "F56420.000", H = 828.8000, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "F57250.000", H = 787.3000, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "F57440.000", H = 786.6800, R = 0.0000 }
            };

                if (calcParams.AddStake && stakes.Count() > 0)
                    calcParams.Stakes = stakes.OrderBy(o => o.SerialNumber).Select(s => Convert.ToDouble(s.Stake)).ToList();

                RouteElementModel routeElement = new RouteElementModel()
                {
                    Intersections = _intersection.OrderBy(o => o.SerialNumber),
                    BrokenChainages = _broken.OrderBy(o => o.SerialNumber),
                    GradeChangePoints = gradeChang
                };
                var list = calculateUtility.CalcSideCoord(routeElement, calcParams);

                return SuccessData(list);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // 计算中桩高程
        [HttpPost]
        public async Task<IActionResult> CalcCenterHight(CalcParams calcParams)
        {
            try
            {
                CalculateUtility calculateUtility = new CalculateUtility();
                var stakes = await stakesBll.GetListAsync(s => s.RouteId == calcParams.RouteId, UserInfo.DataBaseName);
                var brokens = await brokenBll.GetListAsync(b => b.RouteId == calcParams.RouteId, UserInfo.DataBaseName);
                var intersections = await intersectionBll.GetListAsync(i => i.RouteId == calcParams.RouteId, UserInfo.DataBaseName);
                //var grade = await gradeBll.GetListAsync(g => g.RouteId == calcParams.RouteId, UserInfo.DataBaseName);

                var _broken = Utility.Tools.MapperUtils.MapToList<RoadDesignCenter.Models.ProjectModel.BrokenChainage, SSKJ.RouteCalculate.CalculateModels.BrokenChainage>(brokens);
                var _intersection = Utility.Tools.MapperUtils.MapToList<FlatCurve_Intersection, Intersection>(intersections);
                //var _grade = Utility.Tools.MapperUtils.MapToList<VerticalCurve_GradeChangePoint, GradeChangePoint>(grade);

                List<GradeChangePoint> gradeChang = new List<GradeChangePoint>()
            {
                new GradeChangePoint(){ Stake = "A0.000", H = 870.9000, R = 0.0000 },
                new GradeChangePoint(){ Stake = "A150.000", H = 870.5565, R = 200000.0000 },
                new GradeChangePoint(){ Stake = "A750.000", H = 868.7565, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "A1120.000",H = 869.8665, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "A1540.000", H = 866.8065, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "A1900.000", H = 865.7265, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "A2100.000", H = 866.3265, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "A2470.000", H = 865.2165, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "A2720.000", H = 865.9665, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "A3650.000", H = 863.1765, R = 120000.0000 },
                new GradeChangePoint(){ Stake = "A4200.000", H = 864.8265, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "A4800.000", H = 861.3112, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "A5212.030", H = 862.5490, R = 0.0000 },
                new GradeChangePoint(){ Stake = "B5430.000", H = 863.2390, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "B5920.000", H = 860.7890, R = 6500.0000 },
                new GradeChangePoint(){ Stake = "B6500.000", H = 878.1890, R = 14000.0000 },
                new GradeChangePoint(){ Stake = "B6780.000", H = 890.7490, R = 7000.0000 },
                new GradeChangePoint(){ Stake = "B7000.000", H = 897.1290, R = 3500.0000 },
                new GradeChangePoint(){ Stake = "C7970.000", H = 881.5440, R = 15000.0000 },
                new GradeChangePoint(){ Stake = "C9180.000", H = 876.7040, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "C10150.000", H = 861.5540, R = 7000.0000 },
                new GradeChangePoint(){ Stake = "C10600.000", H = 874.1540, R = 6000.0000 },
                new GradeChangePoint(){ Stake = "C10910.000", H = 871.6740, R = 6000.0000 },
                new GradeChangePoint(){ Stake = "C11280.000", H = 855.3940, R = 6000.0000 },
                new GradeChangePoint(){ Stake = "C11860.000", H = 853.1830, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "C13330.000", H = 872.2930, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "C14010.000", H = 877.7330, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "C14840.000", H = 881.3630, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "C15900.000", H = 871.1230, R = 16000.0000 },
                new GradeChangePoint(){ Stake = "C17390.000", H = 881.5930, R = 12000.0000 },
                new GradeChangePoint(){ Stake = "C18180.000", H = 876.0630, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "C20200.000", H = 886.1630, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "C20650.000", H = 884.3630, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "C21500.000", H = 888.6130, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "C22240.000", H = 883.4330, R = 90000.0000 },
                new GradeChangePoint(){ Stake = "C23080.000", H = 889.3130, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "D24760.000", H = 872.3930, R = 10000.0000 },
                new GradeChangePoint(){ Stake = "D25650.000", H = 881.2930, R = 10000.0000 },
                new GradeChangePoint(){ Stake = "D26400.000", H = 873.7930, R = 15000.0000 },
                new GradeChangePoint(){ Stake = "D27010.000", H = 876.2330, R = 12000.0000 },
                new GradeChangePoint(){ Stake = "E31010.000", H = 811.8100, R = 15000.0000 },
                new GradeChangePoint(){ Stake = "E31460.000", H = 804.6100, R = 12000.0000 },
                new GradeChangePoint(){ Stake = "E32400.000", H = 801.7900, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "E32700.000", H = 802.6900, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "E33430.000", H = 799.7700, R = 80000.0000 },
                new GradeChangePoint(){ Stake = "E34250.000", H = 802.2300, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "E34780.000", H = 805.7800, R = 26000.0000 },
                new GradeChangePoint(){ Stake = "E35170.000", H = 804.6100, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E35600.000", H = 805.9000, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E36160.000", H = 804.2200, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E36440.000", H = 805.0600, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E37570.000", H = 801.6700, R = 32000.0000 },
                new GradeChangePoint(){ Stake = "E38040.000", H = 806.3700, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "E38670.000", H = 818.9700, R = 28000.0000 },
                new GradeChangePoint(){ Stake = "E39040.000", H = 802.3200, R = 28000.0000 },
                new GradeChangePoint(){ Stake = "E39740.000", H = 807.2200, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E40620.000", H = 801.9400, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E41270.000", H = 806.4900, R = 28000.0000 },
                new GradeChangePoint(){ Stake = "E41800.000", H = 817.0900, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "E42410.000", H = 803.0600, R = 70000.0000 },
                new GradeChangePoint(){ Stake = "E42820.000", H = 804.2900, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "E43400.000", H = 802.5500, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "E43800.000", H = 803.7500, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "E44700.000", H = 799.2500, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "E45350.000", H = 795.3500, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "E45820.000", H = 796.7600, R = 35000.0000 },
                new GradeChangePoint(){ Stake = "E47350.000", H = 824.3000, R = 22000.0000 },
                new GradeChangePoint(){ Stake = "E48000.000", H = 826.2500, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "E48520.000", H = 830.9300, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "E48750.000", H = 824.4900, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "E49350.000", H = 826.8900, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E50000.000", H = 822.3400, R = 14000.0000 },
                new GradeChangePoint(){ Stake = "E50630.000", H = 790.8400, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "F51700.000", H = 787.6300, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "F52410.000", H = 795.4400, R = 10000.0000 },
                new GradeChangePoint(){ Stake = "F52740.000", H = 789.1700, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "F53030.000", H = 797.8700, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "F53365.000", H = 786.8100, R = 28000.0000 },
                new GradeChangePoint(){ Stake = "F54000.000", H = 788.7200, R = 46000.0000 },
                new GradeChangePoint(){ Stake = "F54940.000", H = 835.7200, R = 38000.0000 },
                new GradeChangePoint(){ Stake = "F55450.000", H = 837.2500, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "F55670.000", H = 832.8500, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "F56020.000", H = 836.0000, R = 12000.0000 },
                new GradeChangePoint(){ Stake = "F56420.000", H = 828.8000, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "F57250.000", H = 787.3000, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "F57440.000", H = 786.6800, R = 0.0000 }
            };

                if (calcParams.AddStake && stakes.Count() > 0)
                    calcParams.Stakes = stakes.OrderBy(o => o.SerialNumber).Select(s => Convert.ToDouble(s.Stake)).ToList();

                RouteElementModel routeElement = new RouteElementModel()
                {
                    Intersections = _intersection.OrderBy(o => o.SerialNumber),
                    BrokenChainages = _broken.OrderBy(o => o.SerialNumber),
                    GradeChangePoints = gradeChang
                };
                var list = calculateUtility.CalcCenterHight(routeElement, calcParams);

                return SuccessData(list);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // 坐标反算桩号
        [HttpPost]
        public async Task<IActionResult> CoordBackCalcStake(CalcParams calcParams)
        {
            try
            {
                CalculateUtility calculateUtility = new CalculateUtility();
                var brokens = await brokenBll.GetListAsync(b => b.RouteId == calcParams.RouteId, UserInfo.DataBaseName);
                var intersections = await intersectionBll.GetListAsync(i => i.RouteId == calcParams.RouteId, UserInfo.DataBaseName);
                //var grade = await gradeBll.GetListAsync(g => g.RouteId == calcParams.RouteId, UserInfo.DataBaseName);

                var _broken = Utility.Tools.MapperUtils.MapToList<RoadDesignCenter.Models.ProjectModel.BrokenChainage, SSKJ.RouteCalculate.CalculateModels.BrokenChainage>(brokens);
                var _intersection = Utility.Tools.MapperUtils.MapToList<FlatCurve_Intersection, Intersection>(intersections);
                //var _grade = Utility.Tools.MapperUtils.MapToList<VerticalCurve_GradeChangePoint, GradeChangePoint>(grade);

                List<GradeChangePoint> gradeChang = new List<GradeChangePoint>()
            {
                new GradeChangePoint(){ Stake = "A0.000", H = 870.9000, R = 0.0000 },
                new GradeChangePoint(){ Stake = "A150.000", H = 870.5565, R = 200000.0000 },
                new GradeChangePoint(){ Stake = "A750.000", H = 868.7565, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "A1120.000",H = 869.8665, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "A1540.000", H = 866.8065, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "A1900.000", H = 865.7265, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "A2100.000", H = 866.3265, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "A2470.000", H = 865.2165, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "A2720.000", H = 865.9665, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "A3650.000", H = 863.1765, R = 120000.0000 },
                new GradeChangePoint(){ Stake = "A4200.000", H = 864.8265, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "A4800.000", H = 861.3112, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "A5212.030", H = 862.5490, R = 0.0000 },
                new GradeChangePoint(){ Stake = "B5430.000", H = 863.2390, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "B5920.000", H = 860.7890, R = 6500.0000 },
                new GradeChangePoint(){ Stake = "B6500.000", H = 878.1890, R = 14000.0000 },
                new GradeChangePoint(){ Stake = "B6780.000", H = 890.7490, R = 7000.0000 },
                new GradeChangePoint(){ Stake = "B7000.000", H = 897.1290, R = 3500.0000 },
                new GradeChangePoint(){ Stake = "C7970.000", H = 881.5440, R = 15000.0000 },
                new GradeChangePoint(){ Stake = "C9180.000", H = 876.7040, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "C10150.000", H = 861.5540, R = 7000.0000 },
                new GradeChangePoint(){ Stake = "C10600.000", H = 874.1540, R = 6000.0000 },
                new GradeChangePoint(){ Stake = "C10910.000", H = 871.6740, R = 6000.0000 },
                new GradeChangePoint(){ Stake = "C11280.000", H = 855.3940, R = 6000.0000 },
                new GradeChangePoint(){ Stake = "C11860.000", H = 853.1830, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "C13330.000", H = 872.2930, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "C14010.000", H = 877.7330, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "C14840.000", H = 881.3630, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "C15900.000", H = 871.1230, R = 16000.0000 },
                new GradeChangePoint(){ Stake = "C17390.000", H = 881.5930, R = 12000.0000 },
                new GradeChangePoint(){ Stake = "C18180.000", H = 876.0630, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "C20200.000", H = 886.1630, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "C20650.000", H = 884.3630, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "C21500.000", H = 888.6130, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "C22240.000", H = 883.4330, R = 90000.0000 },
                new GradeChangePoint(){ Stake = "C23080.000", H = 889.3130, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "D24760.000", H = 872.3930, R = 10000.0000 },
                new GradeChangePoint(){ Stake = "D25650.000", H = 881.2930, R = 10000.0000 },
                new GradeChangePoint(){ Stake = "D26400.000", H = 873.7930, R = 15000.0000 },
                new GradeChangePoint(){ Stake = "D27010.000", H = 876.2330, R = 12000.0000 },
                new GradeChangePoint(){ Stake = "E31010.000", H = 811.8100, R = 15000.0000 },
                new GradeChangePoint(){ Stake = "E31460.000", H = 804.6100, R = 12000.0000 },
                new GradeChangePoint(){ Stake = "E32400.000", H = 801.7900, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "E32700.000", H = 802.6900, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "E33430.000", H = 799.7700, R = 80000.0000 },
                new GradeChangePoint(){ Stake = "E34250.000", H = 802.2300, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "E34780.000", H = 805.7800, R = 26000.0000 },
                new GradeChangePoint(){ Stake = "E35170.000", H = 804.6100, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E35600.000", H = 805.9000, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E36160.000", H = 804.2200, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E36440.000", H = 805.0600, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E37570.000", H = 801.6700, R = 32000.0000 },
                new GradeChangePoint(){ Stake = "E38040.000", H = 806.3700, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "E38670.000", H = 818.9700, R = 28000.0000 },
                new GradeChangePoint(){ Stake = "E39040.000", H = 802.3200, R = 28000.0000 },
                new GradeChangePoint(){ Stake = "E39740.000", H = 807.2200, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E40620.000", H = 801.9400, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E41270.000", H = 806.4900, R = 28000.0000 },
                new GradeChangePoint(){ Stake = "E41800.000", H = 817.0900, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "E42410.000", H = 803.0600, R = 70000.0000 },
                new GradeChangePoint(){ Stake = "E42820.000", H = 804.2900, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "E43400.000", H = 802.5500, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "E43800.000", H = 803.7500, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "E44700.000", H = 799.2500, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "E45350.000", H = 795.3500, R = 20000.0000 },
                new GradeChangePoint(){ Stake = "E45820.000", H = 796.7600, R = 35000.0000 },
                new GradeChangePoint(){ Stake = "E47350.000", H = 824.3000, R = 22000.0000 },
                new GradeChangePoint(){ Stake = "E48000.000", H = 826.2500, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "E48520.000", H = 830.9300, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "E48750.000", H = 824.4900, R = 40000.0000 },
                new GradeChangePoint(){ Stake = "E49350.000", H = 826.8900, R = 30000.0000 },
                new GradeChangePoint(){ Stake = "E50000.000", H = 822.3400, R = 14000.0000 },
                new GradeChangePoint(){ Stake = "E50630.000", H = 790.8400, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "F51700.000", H = 787.6300, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "F52410.000", H = 795.4400, R = 10000.0000 },
                new GradeChangePoint(){ Stake = "F52740.000", H = 789.1700, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "F53030.000", H = 797.8700, R = 25000.0000 },
                new GradeChangePoint(){ Stake = "F53365.000", H = 786.8100, R = 28000.0000 },
                new GradeChangePoint(){ Stake = "F54000.000", H = 788.7200, R = 46000.0000 },
                new GradeChangePoint(){ Stake = "F54940.000", H = 835.7200, R = 38000.0000 },
                new GradeChangePoint(){ Stake = "F55450.000", H = 837.2500, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "F55670.000", H = 832.8500, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "F56020.000", H = 836.0000, R = 12000.0000 },
                new GradeChangePoint(){ Stake = "F56420.000", H = 828.8000, R = 60000.0000 },
                new GradeChangePoint(){ Stake = "F57250.000", H = 787.3000, R = 50000.0000 },
                new GradeChangePoint(){ Stake = "F57440.000", H = 786.6800, R = 0.0000 }
            };

                RouteElementModel routeElement = new RouteElementModel()
                {
                    Intersections = _intersection.OrderBy(o => o.SerialNumber),
                    BrokenChainages = _broken.OrderBy(o => o.SerialNumber),
                    GradeChangePoints = gradeChang
                };
                var list = calculateUtility.CoordBackCalcStake(routeElement, calcParams.Coords);

                return SuccessData(list);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}