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

        public async Task<bool> CreateAsync(FlatCurve_CurveElement entity, string connectionString = null)
        {
            return await FlatBrokenChainRepo.CreateAsync(entity, connectionString);
        }

        public async Task<bool> CreateAsync(IEnumerable<FlatCurve_CurveElement> entityList, string connectionString = null)
        {
            return await FlatBrokenChainRepo.CreateAsync(entityList, connectionString);
        }

        public async Task<bool> DeleteAsync(string keyValue, string connectionString = null)
        {
            return await FlatBrokenChainRepo.DeleteAsync(keyValue, connectionString);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string connectionString = null)
        {
            return await FlatBrokenChainRepo.DeleteAsync(keyValues, connectionString);
        }

        public async Task<bool> DeleteAsync(FlatCurve_CurveElement entity, string connectionString = null)
        {
            return await FlatBrokenChainRepo.DeleteAsync(entity, connectionString);
        }

        public async Task<bool> DeleteAsync(IEnumerable<FlatCurve_CurveElement> entityList, string connectionString = null)
        {
            return await FlatBrokenChainRepo.DeleteAsync(entityList, connectionString);
        }

        public async Task<FlatCurve_CurveElement> GetEntityAsync(Expression<Func<FlatCurve_CurveElement, bool>> where, string connectionString = null)
        {
            return await FlatBrokenChainRepo.GetEntityAsync(where, connectionString);
        }

        public async Task<FlatCurve_CurveElement> GetEntityAsync(string keyValue, string connectionString = null)
        {
            return await FlatBrokenChainRepo.GetEntityAsync(keyValue, connectionString);
        }

        public async Task<IEnumerable<FlatCurve_CurveElement>> GetListAsync(Expression<Func<FlatCurve_CurveElement, bool>> where, string connectionString = null)
        {
            return await FlatBrokenChainRepo.GetListAsync(where, connectionString);
        }

        public async Task<Tuple<IEnumerable<FlatCurve_CurveElement>, int>> GetListAsync<Tkey>(Expression<Func<FlatCurve_CurveElement, bool>> where, Func<FlatCurve_CurveElement, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string connectionString = null)
        {
            return await FlatBrokenChainRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, connectionString);
        }

        public async Task<IEnumerable<FlatCurve_CurveElement>> GetListAsync(string connectionString = null)
        {
            return await FlatBrokenChainRepo.GetListAsync(connectionString);
        }

        public async Task<bool> UpdateAsync(FlatCurve_CurveElement entity, string connectionString = null)
        {
            return await FlatBrokenChainRepo.UpdateAsync(entity, connectionString);
        }

        public async Task<bool> UpdateAsync(IEnumerable<FlatCurve_CurveElement> entityList, string connectionString = null)
        {
            return await FlatBrokenChainRepo.UpdateAsync(entityList, connectionString);
        }
    }
}