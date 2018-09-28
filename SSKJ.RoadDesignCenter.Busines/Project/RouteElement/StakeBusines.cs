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

        public async Task<bool> CreateAsync(Stake entity, string connectionString = null)
        {
            return await StakeRepo.CreateAsync(entity, connectionString);
        }

        public async Task<bool> CreateAsync(IEnumerable<Stake> entityList, string connectionString = null)
        {
            return await StakeRepo.CreateAsync(entityList, connectionString);
        }

        public async Task<bool> DeleteAsync(string keyValue, string connectionString = null)
        {
            return await StakeRepo.DeleteAsync(keyValue, connectionString);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string connectionString = null)
        {
            return await StakeRepo.DeleteAsync(keyValues, connectionString);
        }

        public async Task<bool> DeleteAsync(Stake entity, string connectionString = null)
        {
            return await StakeRepo.DeleteAsync(entity, connectionString);
        }

        public async Task<bool> DeleteAsync(IEnumerable<Stake> entityList, string connectionString = null)
        {
            return await StakeRepo.DeleteAsync(entityList, connectionString);
        }

        public async Task<Stake> GetEntityAsync(Expression<Func<Stake, bool>> where, string connectionString = null)
        {
            return await StakeRepo.GetEntityAsync(where, connectionString);
        }

        public async Task<Stake> GetEntityAsync(string keyValue, string connectionString = null)
        {
            return await StakeRepo.GetEntityAsync(keyValue, connectionString);
        }

        public async Task<IEnumerable<Stake>> GetListAsync(Expression<Func<Stake, bool>> where, string connectionString = null)
        {
            return await StakeRepo.GetListAsync(where, connectionString);
        }

        public async Task<Tuple<IEnumerable<Stake>, int>> GetListAsync<Tkey>(Expression<Func<Stake, bool>> where, Func<Stake, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string connectionString = null)
        {
            return await StakeRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, connectionString);
        }

        public async Task<IEnumerable<Stake>> GetListAsync(string connectionString = null)
        {
            return await StakeRepo.GetListAsync(connectionString);
        }

        public async Task<bool> UpdateAsync(Stake entity, string connectionString = null)
        {
            return await StakeRepo.UpdateAsync(entity, connectionString);
        }

        public async Task<bool> UpdateAsync(IEnumerable<Stake> entityList, string connectionString = null)
        {
            return await StakeRepo.UpdateAsync(entityList, connectionString);
        }
    }
}