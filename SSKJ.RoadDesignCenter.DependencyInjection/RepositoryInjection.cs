using Microsoft.Extensions.DependencyInjection;
using irSystem = SSKJ.RoadDesignCenter.IRepository.System;
using rSystem = SSKJ.RoadDesignCenter.Repository.MySQL.System;
using irProject = SSKJ.RoadDesignCenter.IRepository.Project;
using rProject = SSKJ.RoadDesignCenter.Repository.MySQL.Project;

namespace SSKJ.RoadDesignCenter.DependencyInjection
{
    /// <summary>
    /// 注入仓储层
    /// </summary>
    public static class RepositoryInjection
    {
        public static void ConfigureRepository(IServiceCollection services)
        {
            //system
            services.AddSingleton<irSystem.IModuleRepository, rSystem.ModuleRepository>();
            services.AddSingleton<irSystem.IUserRepository, rSystem.UserRepository>();
            services.AddSingleton<irSystem.IUserProjectRepository, rSystem.UserProjectRepository>();

            //project
            services.AddSingleton<irProject.IUserRepository, rProject.UserRepository>();
            services.AddSingleton<irProject.RouteElement.IBrokenChainRepository, rProject.RouteElement.BrokenChainRepository>();
            services.AddSingleton<irProject.RouteElement.IFlatCurve_CurveElementRepository, rProject.RouteElement.FlatCurve_CurveElementRepository>();
            services.AddSingleton<irProject.RouteElement.IFlatCurve_IntersectionRepository, rProject.RouteElement.FlatCurve_IntersectionRepository>();
            services.AddSingleton<irProject.RouteElement.IVerticalSectionGroundLineRepository, rProject.RouteElement.VerticalSectionGroundLineRepository>();
        }
    }
}
