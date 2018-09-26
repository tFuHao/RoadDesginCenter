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
    public class BrokenChainageBusines : IBrokenChainageBusines
    {

        public IBrokenChainRepository BrokenRepo;

        public BrokenChainageBusines(IBrokenChainRepository brokenRepo)
        {
            this.BrokenRepo = brokenRepo;
        }

        public async Task<bool> CreateAsync(BrokenChainage entity, string connectionString = null)
        {
            return await BrokenRepo.CreateAsync(entity, connectionString);
        }

        public async Task<bool> CreateAsync(IEnumerable<BrokenChainage> entityList, string connectionString = null)
        {
            return await BrokenRepo.CreateAsync(entityList, connectionString);
        }

        public async Task<bool> DeleteAsync(string keyValue, string connectionString = null)
        {
            return await BrokenRepo.DeleteAsync(keyValue, connectionString);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string connectionString = null)
        {
            return await BrokenRepo.DeleteAsync(keyValues, connectionString);
        }

        public async Task<bool> DeleteAsync(BrokenChainage entity, string connectionString = null)
        {
            return await BrokenRepo.DeleteAsync(entity, connectionString);
        }

        public async Task<bool> DeleteAsync(IEnumerable<BrokenChainage> entityList, string connectionString = null)
        {
            return await BrokenRepo.DeleteAsync(entityList, connectionString);
        }

        public async Task<BrokenChainage> GetEntityAsync(Expression<Func<BrokenChainage, bool>> where, string connectionString = null)
        {
            return await BrokenRepo.GetEntityAsync(where, connectionString);
        }

        public async Task<BrokenChainage> GetEntityAsync(string keyValue, string connectionString = null)
        {
            return await BrokenRepo.GetEntityAsync(keyValue, connectionString);
        }

        public async Task<IEnumerable<BrokenChainage>> GetListAsync(Expression<Func<BrokenChainage, bool>> where, string connectionString = null)
        {
            return await BrokenRepo.GetListAsync(where, connectionString);
        }

        public async Task<Tuple<IEnumerable<BrokenChainage>, int>> GetListAsync<Tkey>(Expression<Func<BrokenChainage, bool>> where, Func<BrokenChainage, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string connectionString = null)
        {
            return await BrokenRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, connectionString);
        }

        public async Task<IEnumerable<BrokenChainage>> GetListAsync(string connectionString = null)
        {
            return await BrokenRepo.GetListAsync(connectionString);
        }

        public async Task<bool> UpdateAsync(BrokenChainage entity, string connectionString = null)
        {
            return await BrokenRepo.UpdateAsync(entity, connectionString);
        }

        public async Task<bool> UpdateAsync(IEnumerable<BrokenChainage> entityList, string connectionString = null)
        {
            return await BrokenRepo.UpdateAsync(entityList, connectionString);
        }
    }
}
