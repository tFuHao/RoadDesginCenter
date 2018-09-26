using Microsoft.Extensions.DependencyInjection;
using SSKJ.RoadDesignCenter.Busines.Busines.Project;
using SSKJ.RoadDesignCenter.Busines.Busines.System;
using SSKJ.RoadDesignCenter.Busines.IBusines.Project;
using SSKJ.RoadDesignCenter.Busines.IBusines.System;
using SSKJ.RoadDesignCenter.Service.DalService;
using SSKJ.RoadDesignCenter.Service.IDalService;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RoadDesignCenter.Busines
{
    public class DIBllRegister
    {
        public void DIRegister(IServiceCollection services)
        {
            // 用于实例化DalService对象，获取上下文对象
            services.AddTransient(typeof(IProjectBaseService<>), typeof(ProjectBaseService<>));
            services.AddTransient(typeof(ISystemBaseService<>), typeof(SystemBaseService<>));

            // 配置一个依赖注入映射关系 
            services.AddTransient(typeof(ISysModuleBusines), typeof(SysModuleBusines));
            services.AddTransient(typeof(IPrjUserBusines), typeof(PrjUserBusines));
        }
    }
}
