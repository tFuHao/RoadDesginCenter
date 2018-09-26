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
    /// 模块业务逻辑服务
    /// </summary>
    public class ModuleBusines : IModuleBusines
    {
        private readonly IModuleRepository moduleRepository;

        public ModuleBusines(IModuleRepository moduleRepository)
        {
            this.moduleRepository = moduleRepository;
        }

        public async Task<bool> CreateAsync(Module entity, string connectionString = null)
        {
            return await moduleRepository.CreateAsync(entity, connectionString);
        }

        public async Task<bool> CreateAsync(IEnumerable<Module> entityList, string connectionString = null)
        {
            return await moduleRepository.CreateAsync(entityList, connectionString);
        }

        public async Task<bool> DeleteAsync(string keyValue, string connectionString = null)
        {
            return await moduleRepository.DeleteAsync(keyValue, connectionString);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string connectionString = null)
        {
            return await moduleRepository.DeleteAsync(keyValues, connectionString);
        }

        public async Task<bool> DeleteAsync(Module entity, string connectionString = null)
        {
            return await moduleRepository.DeleteAsync(entity, connectionString);
        }

        public async Task<bool> DeleteAsync(IEnumerable<Module> entityList, string connectionString = null)
        {
            return await moduleRepository.DeleteAsync(entityList, connectionString);
        }

        public async Task<Module> GetEntityAsync(Expression<Func<Module, bool>> where, string connectionString = null)
        {
            return await moduleRepository.GetEntityAsync(where, connectionString);
        }

        public async Task<Module> GetEntityAsync(string keyValue, string connectionString = null)
        {
            return await moduleRepository.GetEntityAsync(keyValue, connectionString);
        }

        public async Task<IEnumerable<Module>> GetListAsync(Expression<Func<Module, bool>> where, string connectionString = null)
        {
            return await moduleRepository.GetListAsync(where, connectionString);
        }

        public async Task<Tuple<IEnumerable<Module>,int>> GetListAsync<Tkey>(Expression<Func<Module, bool>> where, Func<Module, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string connectionString = null)
        {
            return await moduleRepository.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, connectionString);
        }

        public async Task<IEnumerable<Module>> GetListAsync(string connectionString = null)
        {
            return await moduleRepository.GetListAsync(connectionString);
        }

        public async Task<bool> UpdateAsync(IEnumerable<Module> entityList, string connectionString = null)
        {
            return await moduleRepository.UpdateAsync(entityList, connectionString);
        }

        public async Task<bool> UpdateAsync(Module entity, string connectionString = null)
        {
            return await moduleRepository.UpdateAsync(entity, connectionString);
        }
    }
}
