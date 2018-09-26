﻿using Microsoft.Extensions.DependencyInjection;
using ibSystem= SSKJ.RoadDesignCenter.IBusines.System;
using bSystem=SSKJ.RoadDesignCenter.Busines.System;
using ibProject = SSKJ.RoadDesignCenter.IBusines.Project;
using bProject=SSKJ.RoadDesignCenter.Busines.Project;

namespace SSKJ.RoadDesignCenter.DependencyInjection
{
    /// <summary>
    /// 注入业务逻辑层
    /// </summary>
    public static class BusinesInjection
    {
        public static void ConfigureBusiness(IServiceCollection services)
        {
            //system
            services.AddSingleton<ibSystem.IUserBusines, bSystem.UserBusines>();
            services.AddSingleton<ibSystem.IModuleBusines, bSystem.ModuleBusines>();
            services.AddSingleton<ibSystem.IUserProjectBusines, bSystem.UserProjectBusines>();

            //project
            services.AddSingleton<ibProject.IUserBusines, bProject.UserBusines>();
        }
    }
}