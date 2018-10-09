using Microsoft.Extensions.DependencyInjection;
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
            services.AddSingleton<ibProject.ProjectInfo.IPrjInfoBusines, bProject.ProjectInfo.PrjInfoBusines>();
            services.AddSingleton<ibProject.RouteElement.IBrokenChainageBusines, bProject.RouteElement.BrokenChainageBusines>();
            services.AddSingleton<ibProject.RouteElement.IFlatCurve_CurveElementBusines, bProject.RouteElement.FlatCurve_CurveElementBusines>();
            services.AddSingleton<ibProject.RouteElement.IFlatCurve_IntersectionBusines, bProject.RouteElement.FlatCurve_IntersectionBusines>();
            services.AddSingleton<ibProject.RouteElement.IVerticalCurve_GradeChangePointBusines, bProject.RouteElement.VerticalCurve_GradeChangePointBusines>();
            services.AddSingleton<ibProject.RouteElement.IVerticalSectionGroundLineBusines, bProject.RouteElement.VerticalSectionGroundLineBusines>();
            services.AddSingleton<ibProject.RouteElement.ICrossSectionGroundLineBusines, bProject.RouteElement.CrossSectionGroundLineBusines>();
            services.AddSingleton<ibProject.RouteElement.IAddStakeBusines, bProject.RouteElement.AddStakeBusines>();
            services.AddSingleton<ibProject.RouteElement.ISampleLineBusines, bProject.RouteElement.SampleLineBusines>();
            services.AddSingleton<ibProject.RouteElement.IStakeBusines, bProject.RouteElement.StakeBusines>();
            services.AddSingleton<ibProject.RouteElement.IRouteBusines, bProject.RouteElement.RouteBusines>();
            services.AddSingleton<ibProject.IRoleBusines, bProject.RoleBusines>();
        }
    }
}
