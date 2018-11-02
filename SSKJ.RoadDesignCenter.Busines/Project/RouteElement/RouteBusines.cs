using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.IRepository.Project;
using SSKJ.RoadDesignCenter.IRepository.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.Busines.Project.RouteElement
{
    public class RouteBusines : IRouteBusines
    {
        public IRouteRepository RouteRepo;
        public IAuthorizeRepository authorizeRepo;

        public RouteBusines(IRouteRepository routeRepo, IAuthorizeRepository authorizeRepo)
        {
            RouteRepo = routeRepo;
            this.authorizeRepo = authorizeRepo;
        }

        public async Task<bool> CreateAsync(Route entity, string dataBaseName = null)
        {
            return await RouteRepo.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<Route> entityList, string dataBaseName = null)
        {
            return await RouteRepo.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await RouteRepo.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await RouteRepo.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(Route entity, string dataBaseName = null)
        {
            return await RouteRepo.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<Route> entityList, string dataBaseName = null)
        {
            return await RouteRepo.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<Route> GetEntityAsync(Expression<Func<Route, bool>> where, string dataBaseName = null)
        {
            return await RouteRepo.GetEntityAsync(where, dataBaseName);
        }

        public async Task<Route> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await RouteRepo.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<Route>> GetListAsync(Expression<Func<Route, bool>> where, string dataBaseName = null)
        {
            return await RouteRepo.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<Route>, int>> GetListAsync<Tkey>(Expression<Func<Route, bool>> where, Func<Route, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await RouteRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<Route>> GetListAsync(string dataBaseName = null)
        {
            return await RouteRepo.GetListAsync(dataBaseName);
        }
        public async Task<string> GetRouteAuthorizes(int category, string objectId, string dataBaseName)
        {
            if (objectId == "System" || objectId == "PrjManager")
                return null;
            else if (objectId == "PrjAdmin")
            {
                var routes = await RouteRepo.GetListAsync(dataBaseName);
                return routes.OrderBy(o => o.CreateDate).ToList().RouteTreeJson();
            }
            else
            {
                var routes = await RouteRepo.GetListAsync(dataBaseName);
                var authorizes = await authorizeRepo.GetListAsync(a => a.Category == category && a.ObjectId == objectId && a.ItemType == 4, dataBaseName);
                var _routes = routes.ToList().FindAll(m => authorizes.Any(a => a.ItemId == m.RouteId));
                return _routes.OrderBy(o => o.CreateDate).ToList().RouteTreeJson();
            }
        }

        public async Task<string> GetTreeListAsync(Expression<Func<Route, bool>> where, string dataBaseName)
        {
            var data = await RouteRepo.GetListAsync(where, dataBaseName);
            return TreeData.RouteTreeJson(data.OrderBy(o => o.CreateDate).ToList(), dataBaseName);
        }

        public async Task<bool> UpdateAsync(Route entity, string dataBaseName = null)
        {
            return await RouteRepo.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<Route> entityList, string dataBaseName = null)
        {
            return await RouteRepo.UpdateAsync(entityList, dataBaseName);
        }
    }
}