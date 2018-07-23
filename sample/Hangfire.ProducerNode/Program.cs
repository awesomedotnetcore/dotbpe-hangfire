using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Hangfire.ProducerNode
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //已经在项目文件中设置了
            //System.Threading.ThreadPool.SetMinThreads(100, 100);

            //任务系统出错的情况
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            BuildWebHost(args).Run();
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            try
            {
                System.Console.WriteLine(e.Exception.Message + "---------------\r\n" + e.Exception.StackTrace);
            }
            catch
            {
            }
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var currentEnv = System.Environment.GetEnvironmentVariable("DOTBPE_ENVIRONMENT");

            var hostConfig = new ConfigurationBuilder()
             .AddJsonFile("hosting.json")
             .AddJsonFile($"hosting.{currentEnv}.json", optional: true)
             .AddEnvironmentVariables()
             .Build();

            return WebHost.CreateDefaultBuilder(args)
                   .UseConfiguration(hostConfig)
                   .ConfigureAppConfiguration((context, config) =>
                   {
                       config.AddJsonFile("dotbpe.json", optional: true, reloadOnChange: true) //服务相关的配置
                        .AddJsonFile($"dotbpe.{context.HostingEnvironment.EnvironmentName}.json", optional: true);
                       config.AddCommandLine(args);
                   })
                   .UseStartup<Startup>()
                   .Build();
        }
    }
}
