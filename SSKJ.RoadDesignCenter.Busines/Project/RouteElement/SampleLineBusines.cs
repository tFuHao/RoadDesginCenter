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

        public async Task<bool> CreateAsync(SampleLine entity, string connectionString = null)
        {
            return await SampleRepe.CreateAsync(entity, connectionString);
        }

        public async Task<bool> CreateAsync(IEnumerable<SampleLine> entityList, string connectionString = null)
        {
            return await SampleRepe.CreateAsync(entityList, connectionString);
        }

        public async Task<bool> DeleteAsync(string keyValue, string connectionString = null)
        {
            return await SampleRepe.DeleteAsync(keyValue, connectionString);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string connectionString = null)
        {
            return await SampleRepe.DeleteAsync(keyValues, connectionString);
        }

        public async Task<bool> DeleteAsync(SampleLine entity, string connectionString = null)
        {
            return await SampleRepe.DeleteAsync(entity, connectionString);
        }

        public async Task<bool> DeleteAsync(IEnumerable<SampleLine> entityList, string connectionString = null)
        {
            return await SampleRepe.DeleteAsync(entityList, connectionString);
        }

        public async Task<SampleLine> GetEntityAsync(Expression<Func<SampleLine, bool>> where, string connectionString = null)
        {
            return await SampleRepe.GetEntityAsync(where, connectionString);
        }

        public async Task<SampleLine> GetEntityAsync(string keyValue, string connectionString = null)
        {
            return await SampleRepe.GetEntityAsync(keyValue, connectionString);
        }

        public async Task<IEnumerable<SampleLine>> GetListAsync(Expression<Func<SampleLine, bool>> where, string connectionString = null)
        {
            return await SampleRepe.GetListAsync(where, connectionString);
        }

        public async Task<Tuple<IEnumerable<SampleLine>, int>> GetListAsync<Tkey>(Expression<Func<SampleLine, bool>> where, Func<SampleLine, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string connectionString = null)
        {
            return await SampleRepe.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, connectionString);
        }

        public async Task<IEnumerable<SampleLine>> GetListAsync(string connectionString = null)
        {
            return await SampleRepe.GetListAsync(connectionString);
        }

        public async Task<bool> UpdateAsync(SampleLine entity, string connectionString = null)
        {
            return await SampleRepe.UpdateAsync(entity, connectionString);
        }

        public async Task<bool> UpdateAsync(IEnumerable<SampleLine> entityList, string connectionString = null)
        {
            return await SampleRepe.UpdateAsync(entityList, connectionString);
        }
    }
}