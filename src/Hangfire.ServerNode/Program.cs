using DotBPE.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Hangfire.Jobs;
using Hangfire.Console;
using DotBPE.Rpc.Options;
using DotBPE.Protocol.Amp;
using DotBPE.Rpc.Netty;

namespace Hangfire.ServerNode
{
    class Program
    {
        private static void Main(string[] args)
        {
            //任务系统出错的情况
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;


            // 创建Host ，这里可以进一步简化
            var host = new HostBuilder()               
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true)                      
                      .AddEnvironmentVariables(prefix: "DOTBPE_");
                })
                .ConfigureLogging((context, builder) =>
                {
                    builder
                       .AddConfiguration(context.Configuration.GetSection("Logging"))
                       .AddFilter("Microsoft", LogLevel.Warning)
                       .AddFilter("System", LogLevel.Warning)
                       .AddFilter("Hangfire", LogLevel.Information)                    
                       .AddConsole();
                   
                })
                .ConfigureServices((context, services) =>
                {

                    services.AddSingleton<IJobService, Jobs.Impl.JobService>();

                    var option = new BackgroundJobServerOptions
                    {
                        //wait all jobs performed when BackgroundJobServer shutdown.
                        ShutdownTimeout = TimeSpan.FromMinutes(30),
                        Queues = new string[] {"default","order" },
                        WorkerCount = Math.Max(Environment.ProcessorCount, 20)
                    };
                                       
                    services.AddSingleton(option);

                    //TODO:其他的配置信息
                    string redisString = context.Configuration.GetValue<string>("Hangfire:Redis");
                    services.AddHangfire(configuration =>
                    {                        
                        configuration.UseRedisStorage(redisString,new Redis.RedisStorageOptions() { Prefix= "Hangfire:" });
                        //configuration.UseColouredConsoleLogProvider();
                        configuration.UseConsole();
                    });

                    //内部服务地址，也可以使用服务发现的方式，这里使用本地配置
                    services.Configure<RemoteServicesOption>(context.Configuration.GetSection("remoteServices"));

                    //让服务拥有调用DotBPE接口的能力
                    services.AddGatewayClient().AddNettyClient<AmpMessage>();

                    //注册服务
                    services.AddSingleton<IHostedService, HangfireHostService>();
                });

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
