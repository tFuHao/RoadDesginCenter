using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.IRepository.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.Busines.Project.RouteElement
{
    public class AddStakeBusines : IAddStakeBusines
    {
        public IAddStakeRepository AddStakeRepo;

        public AddStakeBusines(IAddStakeRepository addStakeRepo)
        {
            AddStakeRepo = addStakeRepo;
        }

        public async Task<bool> CreateAsync(AddStake entity, string connectionString = null)
        {
            return await AddStakeRepo.CreateAsync(entity, connectionString);
        }

        public async Task<bool> CreateAsync(IEnumerable<AddStake> entityList, string connectionString = null)
        {
            return await AddStakeRepo.CreateAsync(entityList, connectionString);
        }

        public async Task<bool> DeleteAsync(string keyValue, string connectionString = null)
        {
            return await AddStakeRepo.DeleteAsync(keyValue, connectionString);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string connectionString = null)
        {
            return await AddStakeRepo.DeleteAsync(keyValues, connectionString);
        }

        public async Task<bool> DeleteAsync(AddStake entity, string connectionString = null)
        {
            return await AddStakeRepo.DeleteAsync(entity, connectionString);
        }

        public async Task<bool> DeleteAsync(IEnumerable<AddStake> entityList, string connectionString = null)
        {
            return await AddStakeRepo.DeleteAsync(entityList, connectionString);
        }

        public async Task<AddStake> GetEntityAsync(Expression<Func<AddStake, bool>> where, string connectionString = null)
        {
            return await AddStakeRepo.GetEntityAsync(where, connectionString);
        }

        public async Task<AddStake> GetEntityAsync(string keyValue, string connectionString = null)
        {
            return await AddStakeRepo.GetEntityAsync(keyValue, connectionString);
        }

        public async Task<IEnumerable<AddStake>> GetListAsync(Expression<Func<AddStake, bool>> where, string connectionString = null)
        {
            return await AddStakeRepo.GetListAsync(where, connectionString);
        }

        public async Task<Tuple<IEnumerable<AddStake>, int>> GetListAsync<Tkey>(Expression<Func<AddStake, bool>> where, Func<AddStake, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string connectionString = null)
        {
            return await AddStakeRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, connectionString);
        }

        public async Task<IEnumerable<AddStake>> GetListAsync(string connectionString = null)
        {
            return await AddStakeRepo.GetListAsync(connectionString);
        }

        public async Task<bool> UpdateAsync(AddStake entity, string connectionString = null)
        {
            return await AddStakeRepo.UpdateAsync(entity, connectionString);
        }

        public async Task<bool> UpdateAsync(IEnumerable<AddStake> entityList, string connectionString = null)
        {
            return await AddStakeRepo.UpdateAsync(entityList, connectionString);
        }
    }
}