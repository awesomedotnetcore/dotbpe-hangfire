using DotBPE.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Hangfire.Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {

            var currentEnv = System.Environment.GetEnvironmentVariable("DOTBPE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("hosting.json")
                .AddJsonFile($"hosting.{currentEnv}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
                      

            return WebHost.CreateDefaultBuilder(args)
                   .UseConfiguration(configuration)
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
