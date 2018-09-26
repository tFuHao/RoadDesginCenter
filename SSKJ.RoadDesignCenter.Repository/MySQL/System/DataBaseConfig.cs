using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RoadDesignCenter.Repository.MySQL.System
{
    /// <summary>
    /// MySQL的数据库配置
    /// </summary>
    public static class DataBaseConfig
    {
        /// <summary>
        /// 默认的MYSQL的链接字符串
        /// </summary>
        private const string DefaultSystemConnectionString = "server=139.224.200.194;port=3306;database=road_primary;user id=root;password=SSKJ*147258369";

        public static SystemContext CreateContext(string connectionString = null)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = DefaultSystemConnectionString;
            }
            var optionBuilder = new DbContextOptionsBuilder<SystemContext>();

            optionBuilder.UseMySql(connectionString, m => { });

            var context = new SystemContext(optionBuilder.Options);
            context.Database.EnsureCreated();

            return context;
        }
    }
}
