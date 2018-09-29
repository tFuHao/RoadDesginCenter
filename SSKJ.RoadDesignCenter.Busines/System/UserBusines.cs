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

        public async Task<bool> CreateAsync(User entity, string dataBaseName = null)
        {
            return await userRepository.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<User> entityList, string dataBaseName = null)
        {
            return await userRepository.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await userRepository.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await userRepository.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(User entity, string dataBaseName = null)
        {
            return await userRepository.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<User> entityList, string dataBaseName = null)
        {
            return await userRepository.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<User> GetEntityAsync(Expression<Func<User, bool>> where, string dataBaseName = null)
        {
            return await userRepository.GetEntityAsync(where, dataBaseName);
        }

        public async Task<User> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await userRepository.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<User>> GetListAsync(Expression<Func<User, bool>> where, string dataBaseName = null)
        {
            return await userRepository.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<User>, int>> GetListAsync<Tkey>(Expression<Func<User, bool>> where, Func<User, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await userRepository.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<User>> GetListAsync(string dataBaseName = null)
        {
            return await userRepository.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(User entity, string dataBaseName = null)
        {
            return await userRepository.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<User> entityList, string dataBaseName = null)
        {
            return await userRepository.UpdateAsync(entityList, dataBaseName);
        }
    }
}
