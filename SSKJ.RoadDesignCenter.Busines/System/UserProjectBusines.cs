using SSKJ.RoadDesignCenter.IBusines.System;
using SSKJ.RoadDesignCenter.IRepository.System;
using SSKJ.RoadDesignCenter.Models.SystemModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.Busines.System
{
   public class UserProjectBusines:IUserProjectBusines
    {
        private readonly IUserProjectRepository userProjectRepository;
        public UserProjectBusines(IUserProjectRepository userRepository)
        {
            userProjectRepository = userRepository;
        }

        public async Task<bool> CreateAsync(UserProject entity, string dataBaseName = null)
        {
            return await userProjectRepository.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<UserProject> entityList, string dataBaseName = null)
        {
            return await userProjectRepository.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await userProjectRepository.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await userProjectRepository.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(UserProject entity, string dataBaseName = null)
        {
            return await userProjectRepository.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<UserProject> entityList, string dataBaseName = null)
        {
            return await userProjectRepository.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<UserProject> GetEntityAsync(Expression<Func<UserProject, bool>> where, string dataBaseName = null)
        {
            return await userProjectRepository.GetEntityAsync(where, dataBaseName);
        }

        public async Task<UserProject> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await userProjectRepository.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<UserProject>> GetListAsync(Expression<Func<UserProject, bool>> where, string dataBaseName = null)
        {
            return await userProjectRepository.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<UserProject>, int>> GetListAsync<Tkey>(Expression<Func<UserProject, bool>> where, Func<UserProject, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await userProjectRepository.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<UserProject>> GetListAsync(string dataBaseName = null)
        {
            return await userProjectRepository.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(UserProject entity, string dataBaseName = null)
        {
            return await userProjectRepository.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<UserProject> entityList, string dataBaseName = null)
        {
            return await userProjectRepository.UpdateAsync(entityList, dataBaseName);
        }
    }
}
