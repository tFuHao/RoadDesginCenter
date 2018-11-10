using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SSKJ.RoadDesignCenter.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //TryLoadAssembly();
            BuildWebHost(args).Run();
        }

        // Tools will use this to get application services
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        //private static void TryLoadAssembly()
        //{
        //    Assembly entry = Assembly.GetEntryAssembly();
        //    //找到当前执行文件所在路径
        //    string dir = Path.GetDirectoryName(entry.Location);
        //    string entryName = entry.GetName().Name;
        //    //获取执行文件同一目录下的其他dll
        //    foreach (string dll in Directory.GetFiles(dir, "*.dll"))
        //    {
        //        if (entryName.Equals(Path.GetFileNameWithoutExtension(dll))) { continue; }
        //        //非程序集类型的关联load时会报错
        //        try
        //        {
        //            AssemblyLoadContext.Default.LoadFromAssemblyPath(dll);
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }
        //}
    }
}
