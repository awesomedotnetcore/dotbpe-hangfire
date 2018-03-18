using DotBPE.Plugin.AspNetGateway;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Hangfire.Consumer
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
                opt.CookieMode = CookieMode.Manual;

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
