using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Hangfire.CosumerNode
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // 设置线程， 实际项目中可以在项目文件中配置
            //System.Threading.ThreadPool.SetMinThreads(100, 100);

            //任务系统出错的情况
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            // 创建Host ，这里可以进一步简化
            var host = new HostBuilder()
                .ConfigureHostConfiguration(builder =>
                {
                    builder.AddJsonFile("hosting.json", optional: true)
                   .AddCommandLine(args)
                   .AddEnvironmentVariables(prefix: "DOTBPE_");
                })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddJsonFile("dotbpe.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"dotbpe.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true)
                      .AddEnvironmentVariables(prefix: "DOTBPE_");
                })
                 .ConfigureLogging((context, builder) =>
                 {
                     builder.AddConsole();
                 })
                .ConfigureServices(RpcStartup.ConfigureServices);

            host.RunConsoleAsync().Wait();
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            try
            {
                System.Console.WriteLine(e.Exception.ToString());
            }
            catch
            {
            }
        }
    }
}
