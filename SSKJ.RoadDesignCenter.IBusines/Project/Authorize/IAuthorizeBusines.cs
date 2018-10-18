using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.IBusines.Project.Authorize
{
   public interface IAuthorizeBusines : IBaseBusines<Models.ProjectModel.Authorize>
    {
        // <summary>
        /// 获取功能权限
        /// </summary>
        /// <param name="category">1用户权限 2角色权限</param>
        /// <param name="objectId">用户ID或角色ID</param>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        Task<string> GetModuleAuthorizes(int category, string objectId, string dataBaseName);

        /// <summary>
        /// 获取功能按钮权限
        /// </summary>
        /// <param name="category">1用户权限 2角色权限</param>
        /// <param name="objectId">用户ID或角色ID</param>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        Task<string> GetButtonAuthorizes(int category, string objectId, string dataBaseName);

        /// <summary>
        /// 获取功能视图权限
        /// </summary>
        /// <param name="category">1用户权限 2角色权限</param>
        /// <param name="objectId">用户ID或角色ID</param>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        Task<IEnumerable<Models.SystemModel.ModuleColumn>> GetColumnAuthorizes(int category, string objectId, string dataBaseName);
    }
}
