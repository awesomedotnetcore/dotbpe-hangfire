using DotBPE.Plugin.AspNetGateway;
using DotBPE.Protobuf;
using DotBPE.Protocol.Amp;
using DotBPE.Rpc;
using DotBPE.Rpc.Options;
using Hangfire.Console;
using Hangfire.Jobs;
using Hangfire.Producer.Impl;
using Hangfire.RecurringJobExtensions;
using Hangfire.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hangfire.Producer
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            this.Configuration = config;
        }
           

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var redisString = Configuration.GetValue<string>("Hangfire:Redis");

            /*
             GlobalConfiguration.Configuration
                 .UseRedisStorage(redisString, new RedisStorageOptions() { Prefix = "Hangfire:" })
                  .UseConsole();
            */

            services.AddHangfire(configuration =>
             {
                 configuration.UseRedisStorage(redisString, new RedisStorageOptions() { Prefix = "Hangfire:" });
                 configuration.UseConsole();

                 // NOTE:如果要配置循环任何可以在这里配置,取消注释，并修改recurringjob.json
                 // configuration.UseRecurringJob("recurringjob.json");

             });

          
            services.Configure<RemoteServicesOption>(Configuration.GetSection("remoteServices"));

            //添加路由信息
            services.AddRoutes();

            //添加默认AspNetGateWay相关依赖
            services.AddSingleton<IProtobufObjectFactory, ProtobufObjectFactory>();
            services.AddSingleton<IMessageParser<AmpMessage>, MessageParser>();
            services.AddSingleton<IGateService, DefaultGateService<AmpMessage>>();

            services.AddSingleton<IJobService, Jobs.Impl.JobService>();



            //添加服务端支持
            services.AddDotBPE();

            services.AddServiceActors<AmpMessage>((actors) =>
            {
                actors.Add<TaskProducerService>(); //注册服务
            });

            

            //添加RPC服务
            services.AddSingleton<IHostedService, VirtualRpcHostService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //使用网关
            app.UseGateWay();
        }
    }
}
