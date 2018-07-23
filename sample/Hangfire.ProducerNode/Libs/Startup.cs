using DotBPE.AspNetGateway;
using DotBPE.Hangfire;
using DotBPE.Protobuf;
using DotBPE.Protocol.Amp;
using DotBPE.Rpc;
using DotBPE.Rpc.Hosting;
using Hangfire.Console;
using Hangfire.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hangfire.ProducerNode
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
            // 添加网关相关依赖
            services.AddRoutes();

            services.AddSingleton<IProtobufDescriptorFactory, ProtobufDescriptorFactory>();
            services.AddSingleton<IMessageParser<AmpMessage>, MessageParser>();
            services.AddSingleton<IAuditLoggerFormat<AmpMessage>, AuditLoggerFormat>();   //添加AuditLogger相关

            services.AddProtocolPipe<AmpMessage>();

            string redisString = Configuration.GetValue<string>("Hangfire:Redis");
            string prefix = Configuration.GetValue<string>("Hangfire:Prefix");

            //Hangfire相关服务
            services.AddHangfire(configuration =>
            {
                configuration.UseRedisStorage(redisString, new RedisStorageOptions() { Prefix = prefix });
                configuration.UseConsole();
            });

            services.AddDotBPE();

            AddServiceActors(services);

            //添加挂载的宿主服务
            services.AddSingleton<IHostedService, RpcHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            DotBPE.Rpc.Environment.SetServiceProvider(app.ApplicationServices);
            DotBPE.Rpc.Environment.SetLoggerFactory(loggerFactory);

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            //使用网关
            app.UseGateway();
        }

        /// <summary>
        /// 注册服务接收器
        /// </summary>
        /// <param name="services"></param>
        private static void AddServiceActors(IServiceCollection services)
        {
            // 添加本地加载的服务项
            services.AddServiceActors<AmpMessage>((actors) =>
            {
                //任务和队列
                actors.Add<TaskProducerService>();
            });
        }
    }
}
