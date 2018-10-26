using SSKJ.RoadDesignCenter.Models.SystemModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.IBusines.System
{
    public interface IButtonBusines : IBaseBusines<ModuleButton>
    {
        /// <summary>
        /// 根据条件where获取数据
        /// </summary>
        /// <param name="where">条件where</param>
        /// <param name="dataBaseName">数据库名称</param>
        /// <returns></returns>
        Task<string> GetTreeListAsync(Expression<Func<ModuleButton, bool>> where, string dataBaseName = null);

        string ButtonListToTree(List<ModuleButton> list, string dataBaseName = null);
    }
}
