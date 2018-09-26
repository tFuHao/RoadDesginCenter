using Microsoft.Extensions.Options;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RoadDesignCenter.Models.Services
{
   public class DbContextService:IDbContextService
    {
        public DbContextService(IOptions<AppSettings> appSettings)
        {
            ConnectionString = appSettings.Value.ProjectConnection;
        }
        public string ConnectionString { get; }

        public ProjectContext CreateProjectDbContext(string DbBaseName)
        {
            var connectionString = ConnectionString.Replace("{database}", DbBaseName);

            var dbContext = new ProjectContext(connectionString);

            return dbContext;
        }
    }
}
