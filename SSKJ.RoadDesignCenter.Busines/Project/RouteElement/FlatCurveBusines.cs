using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.Busines.Project.RouteElement
{
    public class FlatCurveBusines : IFlatCurveBusines
    {

        private readonly IFlatCurveBusines FlatBus;

        public FlatCurveBusines(IFlatCurveBusines flatBus)
        {
            FlatBus = flatBus;
        }

        public async Task<bool> CreateAsync(FlatCurve entity, string dataBaseName = null)
        {
            return await FlatBus.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<FlatCurve> entityList, string dataBaseName = null)
        {
            return await FlatBus.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await FlatBus.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await FlatBus.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(FlatCurve entity, string dataBaseName = null)
        {
            return await FlatBus.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<FlatCurve> entityList, string dataBaseName = null)
        {
            return await FlatBus.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<FlatCurve> GetEntityAsync(Expression<Func<FlatCurve, bool>> where, string dataBaseName = null)
        {
            return await FlatBus.GetEntityAsync(where, dataBaseName);
        }

        public async Task<FlatCurve> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await FlatBus.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<FlatCurve>> GetListAsync(Expression<Func<FlatCurve, bool>> where, string dataBaseName = null)
        {
            return await FlatBus.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<FlatCurve>, int>> GetListAsync<Tkey>(Expression<Func<FlatCurve, bool>> where, Func<FlatCurve, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await FlatBus.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<FlatCurve>> GetListAsync(string dataBaseName = null)
        {
            return await FlatBus.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(FlatCurve entity, string dataBaseName = null)
        {
            return await FlatBus.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<FlatCurve> entityList, string dataBaseName = null)
        {
            return await FlatBus.UpdateAsync(entityList, dataBaseName);
        }
    }
}