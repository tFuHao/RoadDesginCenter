using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using SSKJ.RoadDesignCenter.DependencyInjection;
using AutoMapper;
using SSKJ.RoadDesignCenter.API.Areas.AuthorizeManage.Data;
using SSKJ.RoadDesignCenter.API.Areas.RouteManage_RouteElement.Models;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using SSKJ.RoadDesignCenter.Models.SystemModel;

namespace SSKJ.RoadDesignCenter.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RepositoryInjection.ConfigureRepository(services);
            BusinesInjection.ConfigureBusiness(services);

            services.AddHttpContextAccessor();
            services.AddCors();
            services.AddMvc().AddJsonOptions(config =>
            {
                config.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            services.AddMemoryCache();

            services.AddSession();

            //自动映射初始化
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Route, RouteDto>();
                cfg.CreateMap<Module, AuthorizeModuleDto>();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseSession();

            app.UseStaticFiles();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseMvc();
        }
    }
}
