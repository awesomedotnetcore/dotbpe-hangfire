using DotBPE.AspNetGateway;
using DotBPE.Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangfire.ProducerNode
{
    public static class AspNetGatewayExtension
    {
        /// <summary>
        /// 扩展路由配置信息
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IServiceCollection AddRoutes(this IServiceCollection services)
        {
            services.Configure<HttpRouterOption>(opt =>
            {
                if (opt.Items == null)
                {
                    opt.Items = new List<HttpRouterOptionItem>();
                }

                foreach (var item in HttpApiRouterOptions.GetList())
                {
                    opt.Items.Add(new HttpRouterOptionItem()
                    {
                        ServiceId = item.ServiceId,
                        MessageId = item.MessageId,
                        Method = item.Method,
                        Path = item.Path,
                        Description = item.Description,
                        PluginName = item.Plugin,
                        TimeOut = item.Timeout
                    });
                }
            });
            return services;
        }
    }
}
