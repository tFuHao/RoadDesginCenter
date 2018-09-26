using SSKJ.RoadDesignCenter.Busines.IBusines.Project;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using SSKJ.RoadDesignCenter.Service.IDalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSKJ.RoadDesignCenter.Busines.Busines.Project
{
    public class PrjUserBusines : PrjBaseBusines<User>, IPrjUserBusines
    {
        /// <summary>
        /// 用于实例化父级，DBService变量
        /// </summary>
        /// <param name="service"></param>
        public PrjUserBusines(IProjectBaseService<User> service) : base(service)
        {

        }
        public List<User> GetList()
        {
            return LoadEntites(c => true).ToList();
        }
    }
}
