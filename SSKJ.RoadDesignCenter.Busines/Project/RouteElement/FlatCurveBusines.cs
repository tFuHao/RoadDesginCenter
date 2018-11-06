using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.IRepository.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.Busines.Project.RouteElement
{
    public class FlatCurveBusines : IFlatCurveBusines
    {

        public IFlatCurveRepository FlatCurveRepo;

        public FlatCurveBusines(IFlatCurveRepository flatCurveRepo)
        {
            FlatCurveRepo = flatCurveRepo;
        }

        public async Task<bool> CreateAsync(FlatCurve entity, string dataBaseName = null)
        {
            return await FlatCurveRepo.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<FlatCurve> entityList, string dataBaseName = null)
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

        public async Task<bool> DeleteAsync(FlatCurve entity, string dataBaseName = null)
        {
            return await FlatCurveRepo.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<FlatCurve> entityList, string dataBaseName = null)
        {
            return await FlatCurveRepo.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<FlatCurve> GetEntityAsync(Expression<Func<FlatCurve, bool>> where, string dataBaseName = null)
        {
            return await FlatCurveRepo.GetEntityAsync(where, dataBaseName);
        }

        public async Task<FlatCurve> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await FlatCurveRepo.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<FlatCurve>> GetListAsync(Expression<Func<FlatCurve, bool>> where, string dataBaseName = null)
        {
            return await FlatCurveRepo.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<FlatCurve>, int>> GetListAsync<Tkey>(Expression<Func<FlatCurve, bool>> where, Func<FlatCurve, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await FlatCurveRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<FlatCurve>> GetListAsync(string dataBaseName = null)
        {
            return await FlatCurveRepo.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(FlatCurve entity, string dataBaseName = null)
        {
            return await FlatCurveRepo.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<FlatCurve> entityList, string dataBaseName = null)
        {
            return await FlatCurveRepo.UpdateAsync(entityList, dataBaseName);
        }
    }
}