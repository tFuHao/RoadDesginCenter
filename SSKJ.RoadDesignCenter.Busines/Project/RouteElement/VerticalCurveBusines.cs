using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.IRepository.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.Busines.Project.RouteElement
{
    public class VerticalCurveBusines : IVerticalCurveBusines
    {
        private readonly IVerticalCurveRepository VerticalRepo;

        public VerticalCurveBusines(IVerticalCurveRepository verticalRepo)
        {
            VerticalRepo = verticalRepo;
        }

        public async Task<bool> CreateAsync(VerticalCurve entity, string dataBaseName = null)
        {
            return await VerticalRepo.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<VerticalCurve> entityList, string dataBaseName = null)
        {
            return await VerticalRepo.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await VerticalRepo.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await VerticalRepo.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(VerticalCurve entity, string dataBaseName = null)
        {
            return await VerticalRepo.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<VerticalCurve> entityList, string dataBaseName = null)
        {
            return await VerticalRepo.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<VerticalCurve> GetEntityAsync(Expression<Func<VerticalCurve, bool>> where, string dataBaseName = null)
        {
            return await VerticalRepo.GetEntityAsync(where, dataBaseName);
        }

        public async Task<VerticalCurve> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await VerticalRepo.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<VerticalCurve>> GetListAsync(Expression<Func<VerticalCurve, bool>> where, string dataBaseName = null)
        {
            return await VerticalRepo.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<VerticalCurve>, int>> GetListAsync<Tkey>(Expression<Func<VerticalCurve, bool>> where, Func<VerticalCurve, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await VerticalRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<VerticalCurve>> GetListAsync(string dataBaseName = null)
        {
            return await VerticalRepo.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(VerticalCurve entity, string dataBaseName = null)
        {
            return await VerticalRepo.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<VerticalCurve> entityList, string dataBaseName = null)
        {
            return await VerticalRepo.UpdateAsync(entityList, dataBaseName);
        }
    }
}
