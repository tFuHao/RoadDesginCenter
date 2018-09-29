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

        public async Task<bool> CreateAsync(FlatCurve_Intersection entity, string dataBaseName = null)
        {
            return await FlatCurveRepo.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<FlatCurve_Intersection> entityList, string dataBaseName = null)
        {
            return await FlatCurveRepo.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await FlatCurveRepo.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await FlatCurveRepo.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(FlatCurve_Intersection entity, string dataBaseName = null)
        {
            return await FlatCurveRepo.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<FlatCurve_Intersection> entityList, string dataBaseName = null)
        {
            return await FlatCurveRepo.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<FlatCurve_Intersection> GetEntityAsync(Expression<Func<FlatCurve_Intersection, bool>> where, string dataBaseName = null)
        {
            return await FlatCurveRepo.GetEntityAsync(where, dataBaseName);
        }

        public async Task<FlatCurve_Intersection> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await FlatCurveRepo.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<FlatCurve_Intersection>> GetListAsync(Expression<Func<FlatCurve_Intersection, bool>> where, string dataBaseName = null)
        {
            return await FlatCurveRepo.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<FlatCurve_Intersection>, int>> GetListAsync<Tkey>(Expression<Func<FlatCurve_Intersection, bool>> where, Func<FlatCurve_Intersection, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await FlatCurveRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<FlatCurve_Intersection>> GetListAsync(string dataBaseName = null)
        {
            return await FlatCurveRepo.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(FlatCurve_Intersection entity, string dataBaseName = null)
        {
            return await FlatCurveRepo.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<FlatCurve_Intersection> entityList, string dataBaseName = null)
        {
            return await FlatCurveRepo.UpdateAsync(entityList, dataBaseName);
        }
    }
}