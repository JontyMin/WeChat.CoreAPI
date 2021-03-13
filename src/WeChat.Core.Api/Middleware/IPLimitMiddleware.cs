using System.Threading.Tasks;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WeChat.Core.Api.Middleware
{
    public class IPLimitMiddleware: IpRateLimitMiddleware
    {
        public IPLimitMiddleware(RequestDelegate next, IOptions<IpRateLimitOptions> options,
            IRateLimitCounterStore counterStore, IIpPolicyStore policyStore, IRateLimitConfiguration config,
            ILogger<IpRateLimitMiddleware> logger) : base(next, options, counterStore, policyStore, config, logger)
        {
        }

        public override Task ReturnQuotaExceededResponse(HttpContext httpContext, RateLimitRule rule, string retryAfter)
        {
            httpContext.Response.Headers.Append("Access-Control-Allow-Origin", "*");
            return base.ReturnQuotaExceededResponse(httpContext, rule, retryAfter);
        }
    }
}