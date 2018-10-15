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
   public class ButtonBusines:IButtonBusines
    {
        private readonly IButtonRepository buttonRepository;
        public ButtonBusines(IButtonRepository buttonRepository)
        {
            this.buttonRepository = buttonRepository;
        }

        public async Task<bool> CreateAsync(ModuleButton entity, string dataBaseName = null)
        {
            return await buttonRepository.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<ModuleButton> entityList, string dataBaseName = null)
        {
            return await buttonRepository.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await buttonRepository.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await buttonRepository.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(ModuleButton entity, string dataBaseName = null)
        {
            return await buttonRepository.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<ModuleButton> entityList, string dataBaseName = null)
        {
            return await buttonRepository.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<ModuleButton> GetEntityAsync(Expression<Func<ModuleButton, bool>> where, string dataBaseName = null)
        {
            return await buttonRepository.GetEntityAsync(where, dataBaseName);
        }

        public async Task<ModuleButton> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await buttonRepository.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<ModuleButton>> GetListAsync(Expression<Func<ModuleButton, bool>> where, string dataBaseName = null)
        {
            return await buttonRepository.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<ModuleButton>, int>> GetListAsync<Tkey>(Expression<Func<ModuleButton, bool>> where, Func<ModuleButton, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await buttonRepository.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<ModuleButton>> GetListAsync(string dataBaseName = null)
        {
            return await buttonRepository.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<ModuleButton> entityList, string dataBaseName = null)
        {
            return await buttonRepository.UpdateAsync(entityList, dataBaseName);
        }

        public async Task<bool> UpdateAsync(ModuleButton entity, string dataBaseName = null)
        {
            return await buttonRepository.UpdateAsync(entity, dataBaseName);
        }
    }
}
