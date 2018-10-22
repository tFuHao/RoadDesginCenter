using SSKJ.RoadDesignCenter.IBusines.Project.Authorize;
using SSKJ.RoadDesignCenter.IRepository.Project.Authorize;
using SSKJ.RoadDesignCenter.IRepository.System;
using SSKJ.RoadDesignCenter.Models.SystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.Busines.Project.Authorize
{
    public class AuthorizeBusines : IAuthorizeBusines
    {
        private readonly IAuthorizeRepository authorizeRepo;
        private readonly IModuleRepository moduleRepo;
        private readonly IButtonRepository buttonRepo;
        private readonly IColumnRepository columnRepo;

        public AuthorizeBusines(IAuthorizeRepository authorizeRepo, IModuleRepository moduleRepo, IButtonRepository buttonRepo, IColumnRepository columnRepo)
        {
            this.authorizeRepo = authorizeRepo;
            this.moduleRepo = moduleRepo;
            this.buttonRepo = buttonRepo;
            this.columnRepo = columnRepo;
        }

        /// <summary>
        /// 获取功能权限
        /// </summary>
        /// <param name="category">1用户权限 2角色权限</param>
        /// <param name="objectId">用户ID或角色ID</param>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Module>> GetModuleAuthorizes(int category, string objectId, string dataBaseName)
        {
            var authorizes = await authorizeRepo.GetListAsync(a => a.Category == category && a.ObjectId == objectId && a.ItemType == 1,dataBaseName);
            var modules = await moduleRepo.GetListAsync(m => m.EnabledMark == 1 && authorizes.Any(a => a.ItemId == m.ModuleId));

            //return TreeData.ModuleTreeJson(modules.ToList());
            return modules.OrderBy(o => o.SortCode);
        }

        /// <summary>
        /// 获取功能按钮权限
        /// </summary>
        /// <param name="category">1用户权限 2角色权限</param>
        /// <param name="objectId">用户ID或角色ID</param>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ModuleButton>> GetButtonAuthorizes(int category, string objectId, string dataBaseName)
        {
            var authorizes = await authorizeRepo.GetListAsync(a => a.Category == category && a.ObjectId == objectId && a.ItemType == 2, dataBaseName);
            var buttons = await buttonRepo.GetListAsync(m => authorizes.Any(a => a.ItemId == m.ModuleButtonId));

            //return TreeData.ButtonTreeJson(buttons.OrderBy(o => o.SortCode).ToList());
            return buttons.OrderBy(o => o.SortCode);
        }

        /// <summary>
        /// 获取功能视图权限
        /// </summary>
        /// <param name="category">1用户权限 2角色权限</param>
        /// <param name="objectId">用户ID或角色ID</param>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Models.SystemModel.ModuleColumn>> GetColumnAuthorizes(int category, string objectId, string dataBaseName)
        {
            var authorizes = await authorizeRepo.GetListAsync(a => a.Category == category && a.ObjectId == objectId && a.ItemType == 3, dataBaseName);
            var columns = await columnRepo.GetListAsync(m => authorizes.Any(a => a.ItemId == m.ModuleColumnId));

            return columns.ToList().OrderBy(o => o.SortCode);
        }

        public async Task<bool> CreateAsync(Models.ProjectModel.Authorize entity, string dataBaseName = null)
        {
            return await authorizeRepo.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<Models.ProjectModel.Authorize> entityList, string dataBaseName = null)
        {
            return await authorizeRepo.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await authorizeRepo.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await authorizeRepo.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(Models.ProjectModel.Authorize entity, string dataBaseName = null)
        {
            return await authorizeRepo.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<Models.ProjectModel.Authorize> entityList, string dataBaseName = null)
        {
            return await authorizeRepo.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<Models.ProjectModel.Authorize> GetEntityAsync(Expression<Func<Models.ProjectModel.Authorize, bool>> where, string dataBaseName = null)
        {
            return await authorizeRepo.GetEntityAsync(where, dataBaseName);
        }

        public async Task<Models.ProjectModel.Authorize> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await authorizeRepo.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<Models.ProjectModel.Authorize>> GetListAsync(Expression<Func<Models.ProjectModel.Authorize, bool>> where, string dataBaseName = null)
        {
            return await authorizeRepo.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<Models.ProjectModel.Authorize>, int>> GetListAsync<Tkey>(Expression<Func<Models.ProjectModel.Authorize, bool>> where, Func<Models.ProjectModel.Authorize, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await authorizeRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<Models.ProjectModel.Authorize>> GetListAsync(string dataBaseName = null)
        {
            return await authorizeRepo.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(Models.ProjectModel.Authorize entity, string dataBaseName = null)
        {
            return await authorizeRepo.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<Models.ProjectModel.Authorize> entityList, string dataBaseName = null)
        {
            return await authorizeRepo.UpdateAsync(entityList, dataBaseName);
        }
    }
}
