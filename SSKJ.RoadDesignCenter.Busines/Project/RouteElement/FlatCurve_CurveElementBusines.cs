using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.IRepository.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.Busines.Project.RouteElement
{
    public class FlatCurve_CurveElementBusines : IFlatCurve_CurveElementBusines
    {
        public IFlatCurve_CurveElementRepository FlatBrokenChainRepo;

        public FlatCurve_CurveElementBusines(IFlatCurve_CurveElementRepository flatBrokenChainRepo)
        {
            FlatBrokenChainRepo = flatBrokenChainRepo;
        }

        public async Task<bool> CreateAsync(FlatCurve_CurveElement entity, string dataBaseName = null)
        {
            return await FlatBrokenChainRepo.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<FlatCurve_CurveElement> entityList, string dataBaseName = null)
        {
            return await FlatBrokenChainRepo.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await FlatBrokenChainRepo.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await FlatBrokenChainRepo.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(FlatCurve_CurveElement entity, string dataBaseName = null)
        {
            return await FlatBrokenChainRepo.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<FlatCurve_CurveElement> entityList, string dataBaseName = null)
        {
            return await FlatBrokenChainRepo.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<FlatCurve_CurveElement> GetEntityAsync(Expression<Func<FlatCurve_CurveElement, bool>> where, string dataBaseName = null)
        {
            return await FlatBrokenChainRepo.GetEntityAsync(where, dataBaseName);
        }

        public async Task<FlatCurve_CurveElement> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await FlatBrokenChainRepo.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<FlatCurve_CurveElement>> GetListAsync(Expression<Func<FlatCurve_CurveElement, bool>> where, string dataBaseName = null)
        {
            return await FlatBrokenChainRepo.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<FlatCurve_CurveElement>, int>> GetListAsync<Tkey>(Expression<Func<FlatCurve_CurveElement, bool>> where, Func<FlatCurve_CurveElement, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await FlatBrokenChainRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<FlatCurve_CurveElement>> GetListAsync(string dataBaseName = null)
        {
            return await FlatBrokenChainRepo.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(FlatCurve_CurveElement entity, string dataBaseName = null)
        {
            return await FlatBrokenChainRepo.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<FlatCurve_CurveElement> entityList, string dataBaseName = null)
        {
            return await FlatBrokenChainRepo.UpdateAsync(entityList, dataBaseName);
        }
    }
}