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
   public class ColumnBusines:IColumnBusines
    {
        private readonly IColumnRepository columnRepository;

        public ColumnBusines(IColumnRepository columnRepository)
        {
            this.columnRepository = columnRepository;
        }
        public async Task<bool> CreateAsync(ModuleColumn entity, string dataBaseName = null)
        {
            return await columnRepository.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<ModuleColumn> entityList, string dataBaseName = null)
        {
            return await columnRepository.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await columnRepository.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await columnRepository.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(ModuleColumn entity, string dataBaseName = null)
        {
            return await columnRepository.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<ModuleColumn> entityList, string dataBaseName = null)
        {
            return await columnRepository.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<ModuleColumn> GetEntityAsync(Expression<Func<ModuleColumn, bool>> where, string dataBaseName = null)
        {
            return await columnRepository.GetEntityAsync(where, dataBaseName);
        }

        public async Task<ModuleColumn> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await columnRepository.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<ModuleColumn>> GetListAsync(Expression<Func<ModuleColumn, bool>> where, string dataBaseName = null)
        {
            return await columnRepository.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<ModuleColumn>, int>> GetListAsync<Tkey>(Expression<Func<ModuleColumn, bool>> where, Func<ModuleColumn, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await columnRepository.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<ModuleColumn>> GetListAsync(string dataBaseName = null)
        {
            return await columnRepository.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<ModuleColumn> entityList, string dataBaseName = null)
        {
            return await columnRepository.UpdateAsync(entityList, dataBaseName);
        }

        public async Task<bool> UpdateAsync(ModuleColumn entity, string dataBaseName = null)
        {
            return await columnRepository.UpdateAsync(entity, dataBaseName);
        }
    }
}
