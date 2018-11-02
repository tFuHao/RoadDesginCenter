using SSKJ.RoadDesignCenter.Models.ProjectModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.IBusines.Project.RouteElement
{
    public interface IRouteBusines : IBaseBusines<Route>
    {
        /// <summary>
        /// 根据条件where获取数据
        /// </summary>
        /// <param name="where">条件where</param>
        /// <param name="dataBaseName">数据库名称</param>
        /// <returns></returns>
        Task<string> GetTreeListAsync(Expression<Func<Route, bool>> where, string dataBaseName);

        Task<string> GetRouteAuthorizes(int category, string objectId, string dataBaseName);
    }
}