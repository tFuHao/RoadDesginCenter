using SSKJ.RoadDesignCenter.IBusines.System;
using SSKJ.RoadDesignCenter.IRepository.System;
using SSKJ.RoadDesignCenter.Models.SystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> CreateAsync(Module entity, string dataBaseName = null)
        {
            return await moduleRepository.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<Module> entityList, string dataBaseName = null)
        {
            return await moduleRepository.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await moduleRepository.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await moduleRepository.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(Module entity, string dataBaseName = null)
        {
            return await moduleRepository.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<Module> entityList, string dataBaseName = null)
        {
            return await moduleRepository.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<Module> GetEntityAsync(Expression<Func<Module, bool>> where, string dataBaseName = null)
        {
            return await moduleRepository.GetEntityAsync(where, dataBaseName);
        }

        public async Task<Module> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await moduleRepository.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<Module>> GetListAsync(Expression<Func<Module, bool>> where, string dataBaseName = null)
        {
            return await moduleRepository.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<Module>,int>> GetListAsync<Tkey>(Expression<Func<Module, bool>> where, Func<Module, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await moduleRepository.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<Module>> GetListAsync(string dataBaseName = null)
        {
            return await moduleRepository.GetListAsync(dataBaseName);
        }

        public async Task<string> GetTreeListAsync(Expression<Func<Module, bool>> where, string dataBaseName = null)
        {
            var data = await moduleRepository.GetListAsync(where);

            return TreeData.ModuleTreeJson(data.ToList().OrderBy(o => o.SortCode).ToList());
        }

        public async Task<bool> UpdateAsync(IEnumerable<Module> entityList, string dataBaseName = null)
        {
            return await moduleRepository.UpdateAsync(entityList, dataBaseName);
        }

        public async Task<bool> UpdateAsync(Module entity, string dataBaseName = null)
        {
            return await moduleRepository.UpdateAsync(entity, dataBaseName);
        }
    }
}
