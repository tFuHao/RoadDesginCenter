using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.IRepository.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.Busines.Project.RouteElement
{
    public class FlatCurve_IntersectionBusines : IFlatCurve_IntersectionBusines
    {
        public IFlatCurve_IntersectionRepository FlatCurveRepo;

        public FlatCurve_IntersectionBusines(IFlatCurve_IntersectionRepository flatCurveRepo)
        {
            FlatCurveRepo = flatCurveRepo;
        }

        public async Task<bool> CreateAsync(FlatCurve_Intersection entity, string connectionString = null)
        {
            return await FlatCurveRepo.CreateAsync(entity, connectionString);
        }

        public async Task<bool> CreateAsync(IEnumerable<FlatCurve_Intersection> entityList, string connectionString = null)
        {
            return await FlatCurveRepo.CreateAsync(entityList, connectionString);
        }

        public async Task<bool> DeleteAsync(string keyValue, string connectionString = null)
        {
            return await FlatCurveRepo.DeleteAsync(keyValue, connectionString);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string connectionString = null)
        {
            return await FlatCurveRepo.DeleteAsync(keyValues, connectionString);
        }

        public async Task<bool> DeleteAsync(FlatCurve_Intersection entity, string connectionString = null)
        {
            return await FlatCurveRepo.DeleteAsync(entity, connectionString);
        }

        public async Task<bool> DeleteAsync(IEnumerable<FlatCurve_Intersection> entityList, string connectionString = null)
        {
            return await FlatCurveRepo.DeleteAsync(entityList, connectionString);
        }

        public async Task<FlatCurve_Intersection> GetEntityAsync(Expression<Func<FlatCurve_Intersection, bool>> where, string connectionString = null)
        {
            return await FlatCurveRepo.GetEntityAsync(where, connectionString);
        }

        public async Task<FlatCurve_Intersection> GetEntityAsync(string keyValue, string connectionString = null)
        {
            return await FlatCurveRepo.GetEntityAsync(keyValue, connectionString);
        }

        public async Task<IEnumerable<FlatCurve_Intersection>> GetListAsync(Expression<Func<FlatCurve_Intersection, bool>> where, string connectionString = null)
        {
            return await FlatCurveRepo.GetListAsync(where, connectionString);
        }

        public async Task<Tuple<IEnumerable<FlatCurve_Intersection>, int>> GetListAsync<Tkey>(Expression<Func<FlatCurve_Intersection, bool>> where, Func<FlatCurve_Intersection, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string connectionString = null)
        {
            return await FlatCurveRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, connectionString);
        }

        public async Task<IEnumerable<FlatCurve_Intersection>> GetListAsync(string connectionString = null)
        {
            return await FlatCurveRepo.GetListAsync(connectionString);
        }

        public async Task<bool> UpdateAsync(FlatCurve_Intersection entity, string connectionString = null)
        {
            return await FlatCurveRepo.UpdateAsync(entity, connectionString);
        }

        public async Task<bool> UpdateAsync(IEnumerable<FlatCurve_Intersection> entityList, string connectionString = null)
        {
            return await FlatCurveRepo.UpdateAsync(entityList, connectionString);
        }
    }
}