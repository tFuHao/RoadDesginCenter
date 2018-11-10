using SSKJ.RoadDesignCenter.Models.ProjectModel.Calculate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.IBusines.Project.RouteCalculate
{
    public interface ICalculateBusines
    {
        Task LoadFlatCurve(string routeId, string dbName);

        Task LoadVerticalCurve(string routeId, string dbName);
        Task LoadBrokenChainage(string routeId, string dbName);
        Task CreatRoute(string routeId, double starStake, string dbName);
        Task<List<CenterCoord>> CalcCenterCoord(string beginStake, string endStake, int interval, string routeId, double starStake, string dbName, double[] stkes = null);
    }
}
