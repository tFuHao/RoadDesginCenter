using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.IRepository.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.Busines.Project.RouteElement
{
    public class AddStakeBusines : IAddStakeBusines
    {
        public IAddStakeRepository AddStakeRepo;

        public AddStakeBusines(IAddStakeRepository addStakeRepo)
        {
            AddStakeRepo = addStakeRepo;
        }

        public async Task<bool> CreateAsync(AddStake entity, string dataBaseName = null)
        {
            return await AddStakeRepo.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<AddStake> entityList, string dataBaseName = null)
        {
            return await AddStakeRepo.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await AddStakeRepo.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await AddStakeRepo.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(AddStake entity, string dataBaseName = null)
        {
            return await AddStakeRepo.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<AddStake> entityList, string dataBaseName = null)
        {
            return await AddStakeRepo.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<AddStake> GetEntityAsync(Expression<Func<AddStake, bool>> where, string dataBaseName = null)
        {
            return await AddStakeRepo.GetEntityAsync(where, dataBaseName);
        }

        public async Task<AddStake> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await AddStakeRepo.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<AddStake>> GetListAsync(Expression<Func<AddStake, bool>> where, string dataBaseName = null)
        {
            return await AddStakeRepo.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<AddStake>, int>> GetListAsync<Tkey>(Expression<Func<AddStake, bool>> where, Func<AddStake, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await AddStakeRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<AddStake>> GetListAsync(string dataBaseName = null)
        {
            return await AddStakeRepo.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(AddStake entity, string dataBaseName = null)
        {
            return await AddStakeRepo.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<AddStake> entityList, string dataBaseName = null)
        {
            return await AddStakeRepo.UpdateAsync(entityList, dataBaseName);
        }
    }
}