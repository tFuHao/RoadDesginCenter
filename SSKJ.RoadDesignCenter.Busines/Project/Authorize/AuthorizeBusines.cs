using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SSKJ.RoadDesignCenter.IBusines.Project.Authorize;
using SSKJ.RoadDesignCenter.IRepository.Project.Authorize;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.Busines.Project.Authorize
{
    public class AuthorizeBusines : IAuthorizeBusines
    {
        private readonly IAuthorizeRepository AuthorizeRepo;

        public AuthorizeBusines(IAuthorizeRepository authorizeRepo)
        {
            AuthorizeRepo = authorizeRepo;
        }

        public async Task<bool> CreateAsync(Models.ProjectModel.Authorize entity, string dataBaseName = null)
        {
            return await AuthorizeRepo.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<Models.ProjectModel.Authorize> entityList, string dataBaseName = null)
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

        public async Task<bool> DeleteAsync(Models.ProjectModel.Authorize entity, string dataBaseName = null)
        {
            return await AuthorizeRepo.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<Models.ProjectModel.Authorize> entityList, string dataBaseName = null)
        {
            return await AuthorizeRepo.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<Models.ProjectModel.Authorize> GetEntityAsync(Expression<Func<Models.ProjectModel.Authorize, bool>> where, string dataBaseName = null)
        {
            return await AuthorizeRepo.GetEntityAsync(where, dataBaseName);
        }

        public async Task<Models.ProjectModel.Authorize> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await AuthorizeRepo.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<Models.ProjectModel.Authorize>> GetListAsync(Expression<Func<Models.ProjectModel.Authorize, bool>> where, string dataBaseName = null)
        {
            return await AuthorizeRepo.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<Models.ProjectModel.Authorize>, int>> GetListAsync<Tkey>(Expression<Func<Models.ProjectModel.Authorize, bool>> where, Func<Models.ProjectModel.Authorize, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await AuthorizeRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<Models.ProjectModel.Authorize>> GetListAsync(string dataBaseName = null)
        {
            return await AuthorizeRepo.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(Models.ProjectModel.Authorize entity, string dataBaseName = null)
        {
            return await AuthorizeRepo.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<Models.ProjectModel.Authorize> entityList, string dataBaseName = null)
        {
            return await AuthorizeRepo.UpdateAsync(entityList, dataBaseName);
        }
    }
}