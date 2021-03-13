using System;
using System.Collections.Generic;
using AspNetCoreRateLimit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WeChat.Core.Api.SetUpService
{
    public static class IpPolicyRateLimitSetup
    {
        public static void AddIpPolicyRateLimitSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services==null) throw new ArgumentNullException(nameof(services));

            // 规则缓存
            services.AddMemoryCache();

            // 加载配置文件
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));


            // 注册规则
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }
    }
}