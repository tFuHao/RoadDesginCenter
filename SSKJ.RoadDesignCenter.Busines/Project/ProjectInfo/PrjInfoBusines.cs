using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SSKJ.RoadDesignCenter.IBusines.Project.ProjectInfo;
using SSKJ.RoadDesignCenter.IRepository.Project.ProjectInfo;

namespace SSKJ.RoadDesignCenter.Busines.Project.ProjectInfo
{
   public class PrjInfoBusines:IPrjInfoBusines
    {
        private readonly IPrjInfoRepository prjInfoRepository;

        public PrjInfoBusines(IPrjInfoRepository prjInfoRepository)
        {
            this.prjInfoRepository = prjInfoRepository;
        }
        public async Task<bool> CreateAsync(Models.ProjectModel.ProjectInfo entity, string dataBaseName = null)
        {
            return await prjInfoRepository.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<Models.ProjectModel.ProjectInfo> entityList, string dataBaseName = null)
        {
            return await prjInfoRepository.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await prjInfoRepository.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await prjInfoRepository.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(Models.ProjectModel.ProjectInfo entity, string dataBaseName = null)
        {
            return await prjInfoRepository.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<Models.ProjectModel.ProjectInfo> entityList, string dataBaseName = null)
        {
            return await prjInfoRepository.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<Models.ProjectModel.ProjectInfo> GetEntityAsync(Expression<Func<Models.ProjectModel.ProjectInfo, bool>> where, string dataBaseName = null)
        {
            return await prjInfoRepository.GetEntityAsync(where, dataBaseName);
        }

        public async Task<Models.ProjectModel.ProjectInfo> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await prjInfoRepository.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<Models.ProjectModel.ProjectInfo>> GetListAsync(Expression<Func<Models.ProjectModel.ProjectInfo, bool>> where, string dataBaseName = null)
        {
            return await prjInfoRepository.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<Models.ProjectModel.ProjectInfo>, int>> GetListAsync<Tkey>(Expression<Func<Models.ProjectModel.ProjectInfo, bool>> where, Func<Models.ProjectModel.ProjectInfo, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await prjInfoRepository.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<Models.ProjectModel.ProjectInfo>> GetListAsync(string dataBaseName = null)
        {
            return await prjInfoRepository.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(Models.ProjectModel.ProjectInfo entity, string dataBaseName = null)
        {
            return await prjInfoRepository.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<Models.ProjectModel.ProjectInfo> entityList, string dataBaseName = null)
        {
            return await prjInfoRepository.UpdateAsync(entityList, dataBaseName);
        }
    }
}
