using SSKJ.RoadDesignCenter.IBusines.Project;
using SSKJ.RoadDesignCenter.IRepository.Project;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.Busines.Project
{
    /// <summary>
    /// 用户业务逻辑服务
    /// </summary>
    public class UserBusines : IUserBusines
    {
        private readonly IUserRepository userRepository;

        public UserBusines(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> CreateAsync(User entity, string connectionString = null)
        {
            return await userRepository.CreateAsync(entity, connectionString);
        }

        public async Task<bool> CreateAsync(IEnumerable<User> entityList, string connectionString = null)
        {
            return await userRepository.CreateAsync(entityList, connectionString);
        }

        public async Task<bool> DeleteAsync(string keyValue, string connectionString = null)
        {
            return await userRepository.DeleteAsync(keyValue, connectionString);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string connectionString = null)
        {
            return await userRepository.DeleteAsync(keyValues, connectionString);
        }

        public async Task<bool> DeleteAsync(User entity, string connectionString = null)
        {
            return await userRepository.DeleteAsync(entity, connectionString);
        }

        public async Task<bool> DeleteAsync(IEnumerable<User> entityList, string connectionString = null)
        {
            return await userRepository.DeleteAsync(entityList, connectionString);
        }

        public async Task<User> GetEntityAsync(Expression<Func<User, bool>> where, string connectionString = null)
        {
            return await userRepository.GetEntityAsync(where, connectionString);
        }

        public async Task<User> GetEntityAsync(string keyValue, string connectionString = null)
        {
            return await userRepository.GetEntityAsync(keyValue, connectionString);
        }

        public async Task<IEnumerable<User>> GetListAsync(Expression<Func<User, bool>> where, string connectionString = null)
        {
            return await userRepository.GetListAsync(where, connectionString);
        }

        public async Task<Tuple<IEnumerable<User>, int>> GetListAsync<Tkey>(Expression<Func<User, bool>> where, Func<User, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string connectionString = null)
        {
            return await userRepository.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, connectionString);
        }

        public async Task<IEnumerable<User>> GetListAsync(string connectionString = null)
        {
            return await userRepository.GetListAsync(connectionString);
        }

        public async Task<bool> UpdateAsync(User entity, string connectionString = null)
        {
            return await userRepository.UpdateAsync(entity, connectionString);
        }

        public async Task<bool> UpdateAsync(IEnumerable<User> entityList, string connectionString = null)
        {
            return await userRepository.UpdateAsync(entityList, connectionString);
        }
    }
}
