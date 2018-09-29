using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.IRepository.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.Busines.Project.RouteElement
{
    public class SampleLineBusines : ISampleLineBusines
    {
        public ISampleLineRepository SampleRepe;

        public SampleLineBusines(ISampleLineRepository sampleRepe)
        {
            SampleRepe = sampleRepe;
        }

        public async Task<bool> CreateAsync(SampleLine entity, string dataBaseName = null)
        {
            return await SampleRepe.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<SampleLine> entityList, string dataBaseName = null)
        {
            return await SampleRepe.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await SampleRepe.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await SampleRepe.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(SampleLine entity, string dataBaseName = null)
        {
            return await SampleRepe.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<SampleLine> entityList, string dataBaseName = null)
        {
            return await SampleRepe.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<SampleLine> GetEntityAsync(Expression<Func<SampleLine, bool>> where, string dataBaseName = null)
        {
            return await SampleRepe.GetEntityAsync(where, dataBaseName);
        }

        public async Task<SampleLine> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await SampleRepe.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<SampleLine>> GetListAsync(Expression<Func<SampleLine, bool>> where, string dataBaseName = null)
        {
            return await SampleRepe.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<SampleLine>, int>> GetListAsync<Tkey>(Expression<Func<SampleLine, bool>> where, Func<SampleLine, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await SampleRepe.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<SampleLine>> GetListAsync(string dataBaseName = null)
        {
            return await SampleRepe.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(SampleLine entity, string dataBaseName = null)
        {
            return await SampleRepe.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<SampleLine> entityList, string dataBaseName = null)
        {
            return await SampleRepe.UpdateAsync(entityList, dataBaseName);
        }
    }
}