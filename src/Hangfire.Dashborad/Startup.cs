using Hangfire.Console;
using Hangfire.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;

namespace Hangfire.Dashborad
{
    public class Startup
    {       
        private static string RedisConnectionString;
        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            HostingEnvironment = env;
            Configuration = config;

            RedisConnectionString = Configuration.GetValue<string>("Hangfire:Redis");
        }

        private IHostingEnvironment HostingEnvironment { get; }
        private IConfiguration Configuration { get; }
              
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthenticationCore();
            //services.AddScoped<Jobs.IJobService, Jobs.Impl.JobService>();

            services.AddHangfire(configuration =>
            {
                configuration.UseRedisStorage(RedisConnectionString, new RedisStorageOptions() { Prefix = "Hangfire:" });
                configuration.UseConsole();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //TODO:实现认证的方式
            var options = new DashboardOptions
            {
                Authorization = new[] { new BasicDashboardAuthorizationFilter() }
            };

            //使用Hangfire Dashboard
            app.UseHangfireDashboard("/hangfire", options);
        }
    }
}
