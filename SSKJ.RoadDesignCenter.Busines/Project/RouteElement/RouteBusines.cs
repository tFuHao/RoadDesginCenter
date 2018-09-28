using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.IRepository.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.Busines.Project.RouteElement
{
    public class RouteBusines : IRouteBusines
    {
        public IRouteRepository RouteRepo;

        public RouteBusines(IRouteRepository routeRepo)
        {
            RouteRepo = routeRepo;
        }

        public async Task<bool> CreateAsync(Route entity, string connectionString = null)
        {
            return await RouteRepo.CreateAsync(entity, connectionString);
        }

        public async Task<bool> CreateAsync(IEnumerable<Route> entityList, string connectionString = null)
        {
            return await RouteRepo.CreateAsync(entityList, connectionString);
        }

        public async Task<bool> DeleteAsync(string keyValue, string connectionString = null)
        {
            return await RouteRepo.DeleteAsync(keyValue, connectionString);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string connectionString = null)
        {
            return await RouteRepo.DeleteAsync(keyValues, connectionString);
        }

        public async Task<bool> DeleteAsync(Route entity, string connectionString = null)
        {
            return await RouteRepo.DeleteAsync(entity, connectionString);
        }

        public async Task<bool> DeleteAsync(IEnumerable<Route> entityList, string connectionString = null)
        {
            return await RouteRepo.DeleteAsync(entityList, connectionString);
        }

        public async Task<Route> GetEntityAsync(Expression<Func<Route, bool>> where, string connectionString = null)
        {
            return await RouteRepo.GetEntityAsync(where, connectionString);
        }

        public async Task<Route> GetEntityAsync(string keyValue, string connectionString = null)
        {
            return await RouteRepo.GetEntityAsync(keyValue, connectionString);
        }

        public async Task<IEnumerable<Route>> GetListAsync(Expression<Func<Route, bool>> where, string connectionString = null)
        {
            return await RouteRepo.GetListAsync(where, connectionString);
        }

        public async Task<Tuple<IEnumerable<Route>, int>> GetListAsync<Tkey>(Expression<Func<Route, bool>> where, Func<Route, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string connectionString = null)
        {
            return await RouteRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, connectionString);
        }

        public async Task<IEnumerable<Route>> GetListAsync(string connectionString = null)
        {
            return await RouteRepo.GetListAsync(connectionString);
        }

        public async Task<bool> UpdateAsync(Route entity, string connectionString = null)
        {
            return await RouteRepo.UpdateAsync(entity, connectionString);
        }

        public async Task<bool> UpdateAsync(IEnumerable<Route> entityList, string connectionString = null)
        {
            return await RouteRepo.UpdateAsync(entityList, connectionString);
        }
    }
}