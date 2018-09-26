using SSKJ.RoadDesignCenter.Busines.IBusines.System;
using SSKJ.RoadDesignCenter.Models.SystemModel;
using SSKJ.RoadDesignCenter.Service.IDalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSKJ.RoadDesignCenter.Busines.Busines.System
{
    public class SysModuleBusines : SysBaseBusines<Module>, ISysModuleBusines
    {
        /// <summary>
        /// 用于实例化父级，DBService变量
        /// </summary>
        /// <param name="service"></param>
        public SysModuleBusines(ISystemBaseService<Module> service) : base(service)
        {

        }
        public List<Module> GetList()
        {
            return LoadEntites(c => true).ToList();
        }
    }
}
