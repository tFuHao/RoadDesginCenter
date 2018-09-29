using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.IRepository.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.Busines.Project.RouteElement
{
    public class StakeBusines : IStakeBusines
    {

        public IStakeRepository StakeRepo;

        public StakeBusines(IStakeRepository stakeRepo)
        {
            StakeRepo = stakeRepo;
        }

        public async Task<bool> CreateAsync(Stake entity, string dataBaseName = null)
        {
            return await StakeRepo.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<Stake> entityList, string dataBaseName = null)
        {
            return await StakeRepo.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await StakeRepo.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await StakeRepo.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(Stake entity, string dataBaseName = null)
        {
            return await StakeRepo.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<Stake> entityList, string dataBaseName = null)
        {
            return await StakeRepo.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<Stake> GetEntityAsync(Expression<Func<Stake, bool>> where, string dataBaseName = null)
        {
            return await StakeRepo.GetEntityAsync(where, dataBaseName);
        }

        public async Task<Stake> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await StakeRepo.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<Stake>> GetListAsync(Expression<Func<Stake, bool>> where, string dataBaseName = null)
        {
            return await StakeRepo.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<Stake>, int>> GetListAsync<Tkey>(Expression<Func<Stake, bool>> where, Func<Stake, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await StakeRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<Stake>> GetListAsync(string dataBaseName = null)
        {
            return await StakeRepo.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(Stake entity, string dataBaseName = null)
        {
            return await StakeRepo.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<Stake> entityList, string dataBaseName = null)
        {
            return await StakeRepo.UpdateAsync(entityList, dataBaseName);
        }
    }
}