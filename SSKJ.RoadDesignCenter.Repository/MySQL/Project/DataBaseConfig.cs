using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.Repository.MySQL.Project
{
    /// <summary>
    /// MySQL的数据库配置
    /// </summary>
    public static class DataBaseConfig
    {
        /// <summary>
        /// 默认的MYSQL的链接字符串
        /// </summary>
        private const string DefaultProjectConnectionString = "server=139.224.200.194;port=3306;database={database};user id=root;password=SSKJ*147258369";

        public static async Task<ProjectContext> CreateContext(string dataBaseName = null)
        {
            var connectionString = "";
            if (string.IsNullOrWhiteSpace(dataBaseName))
                connectionString = DefaultProjectConnectionString.Replace("{database}", "road");
            else
                connectionString = DefaultProjectConnectionString.Replace("{database}", dataBaseName);

            var optionBuilder = new DbContextOptionsBuilder<ProjectContext>();

            optionBuilder.UseMySql(connectionString, m => { });

            var context = new ProjectContext(optionBuilder.Options);
            await context.Database.EnsureCreatedAsync();

            return context;
        }
    }
}
