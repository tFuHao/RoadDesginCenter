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

        public async Task<bool> CreateAsync(BrokenChainage entity, string dataBaseName = null)
        {
            return await BrokenRepo.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<BrokenChainage> entityList, string dataBaseName = null)
        {
            return await BrokenRepo.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await BrokenRepo.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await BrokenRepo.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(BrokenChainage entity, string dataBaseName = null)
        {
            return await BrokenRepo.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<BrokenChainage> entityList, string dataBaseName = null)
        {
            return await BrokenRepo.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<BrokenChainage> GetEntityAsync(Expression<Func<BrokenChainage, bool>> where, string dataBaseName = null)
        {
            return await BrokenRepo.GetEntityAsync(where, dataBaseName);
        }

        public async Task<BrokenChainage> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await BrokenRepo.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<BrokenChainage>> GetListAsync(Expression<Func<BrokenChainage, bool>> where, string dataBaseName = null)
        {
            return await BrokenRepo.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<BrokenChainage>, int>> GetListAsync<Tkey>(Expression<Func<BrokenChainage, bool>> where, Func<BrokenChainage, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await BrokenRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<BrokenChainage>> GetListAsync(string dataBaseName = null)
        {
            return await BrokenRepo.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(BrokenChainage entity, string dataBaseName = null)
        {
            return await BrokenRepo.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<BrokenChainage> entityList, string dataBaseName = null)
        {
            return await BrokenRepo.UpdateAsync(entityList, dataBaseName);
        }
    }
}
