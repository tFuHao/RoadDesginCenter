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
        public async Task<bool> CreateAsync(Models.ProjectModel.ProjectInfo entity, string connectionString = null)
        {
            return await prjInfoRepository.CreateAsync(entity, connectionString);
        }

        public async Task<bool> CreateAsync(IEnumerable<Models.ProjectModel.ProjectInfo> entityList, string connectionString = null)
        {
            return await prjInfoRepository.CreateAsync(entityList, connectionString);
        }

        public async Task<bool> DeleteAsync(string keyValue, string connectionString = null)
        {
            return await prjInfoRepository.DeleteAsync(keyValue, connectionString);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string connectionString = null)
        {
            return await prjInfoRepository.DeleteAsync(keyValues, connectionString);
        }

        public async Task<bool> DeleteAsync(Models.ProjectModel.ProjectInfo entity, string connectionString = null)
        {
            return await prjInfoRepository.DeleteAsync(entity, connectionString);
        }

        public async Task<bool> DeleteAsync(IEnumerable<Models.ProjectModel.ProjectInfo> entityList, string connectionString = null)
        {
            return await prjInfoRepository.DeleteAsync(entityList, connectionString);
        }

        public async Task<Models.ProjectModel.ProjectInfo> GetEntityAsync(Expression<Func<Models.ProjectModel.ProjectInfo, bool>> where, string connectionString = null)
        {
            return await prjInfoRepository.GetEntityAsync(where, connectionString);
        }

        public async Task<Models.ProjectModel.ProjectInfo> GetEntityAsync(string keyValue, string connectionString = null)
        {
            return await prjInfoRepository.GetEntityAsync(keyValue, connectionString);
        }

        public async Task<IEnumerable<Models.ProjectModel.ProjectInfo>> GetListAsync(Expression<Func<Models.ProjectModel.ProjectInfo, bool>> where, string connectionString = null)
        {
            return await prjInfoRepository.GetListAsync(where, connectionString);
        }

        public async Task<Tuple<IEnumerable<Models.ProjectModel.ProjectInfo>, int>> GetListAsync<Tkey>(Expression<Func<Models.ProjectModel.ProjectInfo, bool>> where, Func<Models.ProjectModel.ProjectInfo, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string connectionString = null)
        {
            return await prjInfoRepository.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, connectionString);
        }

        public async Task<IEnumerable<Models.ProjectModel.ProjectInfo>> GetListAsync(string connectionString = null)
        {
            return await prjInfoRepository.GetListAsync(connectionString);
        }

        public async Task<bool> UpdateAsync(Models.ProjectModel.ProjectInfo entity, string connectionString = null)
        {
            return await prjInfoRepository.UpdateAsync(entity, connectionString);
        }

        public async Task<bool> UpdateAsync(IEnumerable<Models.ProjectModel.ProjectInfo> entityList, string connectionString = null)
        {
            return await prjInfoRepository.UpdateAsync(entityList, connectionString);
        }
    }
}
