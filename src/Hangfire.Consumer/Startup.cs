using DotBPE.Plugin.AspNetGateway;
using DotBPE.Protobuf;
using DotBPE.Protocol.Amp;
using DotBPE.Rpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hangfire.Consumer
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
            
            //添加路由信息
            services.AddRoutes();

            //添加默认AspNetGateWay相关依赖
            services.AddSingleton<IProtobufObjectFactory, ProtobufObjectFactory>();
            services.AddSingleton<IMessageParser<AmpMessage>, MessageParser>();
            services.AddSingleton<IGateService, DefaultGateService<AmpMessage>>();


            //添加服务端支持
            services.AddDotBPE();

            services.AddServiceActors<AmpMessage>((actors) =>
            {
                actors.Add<TaskConsumerService>(); //注册服务
            });

            //添加AuditLogger相关
            services.AddSingleton<IAuditLoggerFormat<AmpMessage>, AuditLoggerFormat>();

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
