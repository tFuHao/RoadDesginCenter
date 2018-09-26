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

        public async Task<bool> CreateAsync(UserProject entity, string connectionString = null)
        {
            return await userProjectRepository.CreateAsync(entity, connectionString);
        }

        public async Task<bool> CreateAsync(IEnumerable<UserProject> entityList, string connectionString = null)
        {
            return await userProjectRepository.CreateAsync(entityList, connectionString);
        }

        public async Task<bool> DeleteAsync(string keyValue, string connectionString = null)
        {
            return await userProjectRepository.DeleteAsync(keyValue, connectionString);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string connectionString = null)
        {
            return await userProjectRepository.DeleteAsync(keyValues, connectionString);
        }

        public async Task<bool> DeleteAsync(UserProject entity, string connectionString = null)
        {
            return await userProjectRepository.DeleteAsync(entity, connectionString);
        }

        public async Task<bool> DeleteAsync(IEnumerable<UserProject> entityList, string connectionString = null)
        {
            return await userProjectRepository.DeleteAsync(entityList, connectionString);
        }

        public async Task<UserProject> GetEntityAsync(Expression<Func<UserProject, bool>> where, string connectionString = null)
        {
            return await userProjectRepository.GetEntityAsync(where, connectionString);
        }

        public async Task<UserProject> GetEntityAsync(string keyValue, string connectionString = null)
        {
            return await userProjectRepository.GetEntityAsync(keyValue, connectionString);
        }

        public async Task<IEnumerable<UserProject>> GetListAsync(Expression<Func<UserProject, bool>> where, string connectionString = null)
        {
            return await userProjectRepository.GetListAsync(where, connectionString);
        }

        public async Task<Tuple<IEnumerable<UserProject>, int>> GetListAsync<Tkey>(Expression<Func<UserProject, bool>> where, Func<UserProject, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string connectionString = null)
        {
            return await userProjectRepository.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, connectionString);
        }

        public async Task<IEnumerable<UserProject>> GetListAsync(string connectionString = null)
        {
            return await userProjectRepository.GetListAsync(connectionString);
        }

        public async Task<bool> UpdateAsync(UserProject entity, string connectionString = null)
        {
            return await userProjectRepository.UpdateAsync(entity, connectionString);
        }

        public async Task<bool> UpdateAsync(IEnumerable<UserProject> entityList, string connectionString = null)
        {
            return await userProjectRepository.UpdateAsync(entityList, connectionString);
        }
    }
}
