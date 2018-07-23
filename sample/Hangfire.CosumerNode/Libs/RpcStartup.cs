using DotBPE.Protocol.Amp;
using DotBPE.Rpc;
using DotBPE.Rpc.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire.Console;
using Hangfire.Redis;

using Microsoft.Extensions.Hosting;
using DotBPE.Hangfire;

namespace Hangfire.CosumerNode
{
    public static class RpcStartup
    {
        /// <summary>
        /// 配置注入的服务信息
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            string redisString = context.Configuration.GetValue<string>("Hangfire:Redis");
            string prefix = context.Configuration.GetValue<string>("Hangfire:Prefix");
            //Hangfire相关服务
            services.AddHangfire(configuration =>
            {
                configuration.UseRedisStorage(redisString, new RedisStorageOptions() { Prefix = prefix });
                configuration.UseConsole();
            });

            // 使用AMP协议
            services.AddDotBPE();

            //注册服务接收器
            AddServiceActors(services);

            //添加挂载的宿主服务
            services.AddSingleton<IHostedService, RpcHostedService>();
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
                actors.Add<TaskCosumerService>();
            });
        }
    }
}
