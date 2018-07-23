using Hangfire.Console;
using Hangfire.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hangfire.Dashborad
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            HostingEnvironment = env;
            Configuration = config;
        }

        private IHostingEnvironment HostingEnvironment { get; }
        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string redisString = Configuration.GetValue<string>("Hangfire:Redis");
            string prefix = Configuration.GetValue<string>("Hangfire:Prefix");

            services.AddAuthenticationCore();
            //services.AddScoped<Jobs.IJobService, Jobs.Impl.JobService>();

            services.AddHangfire(configuration =>
            {
                configuration.UseRedisStorage(redisString, new RedisStorageOptions() { Prefix = prefix });
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
