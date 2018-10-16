using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SSKJ.RoadDesignCenter.IBusines.Project.Authorize;
using SSKJ.RoadDesignCenter.IRepository.Project.Authorize;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.Busines.Project.Authorize
{
    public class AuthorizeDataBusines : IAuthorizeDataBusines
    {
        private readonly IAuthorizeDataRepository AuthorizeRepo;

        public AuthorizeDataBusines(IAuthorizeDataRepository authorizeRepo)
        {
            AuthorizeRepo = authorizeRepo;
        }

        public async Task<bool> CreateAsync(Models.ProjectModel.AuthorizeData entity, string dataBaseName = null)
        {
            return await AuthorizeRepo.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<Models.ProjectModel.AuthorizeData> entityList, string dataBaseName = null)
        {
            return await AuthorizeRepo.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await AuthorizeRepo.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await AuthorizeRepo.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(Models.ProjectModel.AuthorizeData entity, string dataBaseName = null)
        {
            return await AuthorizeRepo.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<Models.ProjectModel.AuthorizeData> entityList, string dataBaseName = null)
        {
            return await AuthorizeRepo.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<Models.ProjectModel.AuthorizeData> GetEntityAsync(Expression<Func<Models.ProjectModel.AuthorizeData, bool>> where, string dataBaseName = null)
        {
            return await AuthorizeRepo.GetEntityAsync(where, dataBaseName);
        }

        public async Task<Models.ProjectModel.AuthorizeData> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await AuthorizeRepo.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<Models.ProjectModel.AuthorizeData>> GetListAsync(Expression<Func<Models.ProjectModel.AuthorizeData, bool>> where, string dataBaseName = null)
        {
            return await AuthorizeRepo.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<Models.ProjectModel.AuthorizeData>, int>> GetListAsync<Tkey>(Expression<Func<Models.ProjectModel.AuthorizeData, bool>> where, Func<Models.ProjectModel.AuthorizeData, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await AuthorizeRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<Models.ProjectModel.AuthorizeData>> GetListAsync(string dataBaseName = null)
        {
            return await AuthorizeRepo.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(Models.ProjectModel.AuthorizeData entity, string dataBaseName = null)
        {
            return await AuthorizeRepo.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<Models.ProjectModel.AuthorizeData> entityList, string dataBaseName = null)
        {
            return await AuthorizeRepo.UpdateAsync(entityList, dataBaseName);
        }
    }
}