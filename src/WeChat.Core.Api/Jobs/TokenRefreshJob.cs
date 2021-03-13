using System;
using System.Threading.Tasks;
using Quartz;
using WeChat.Core.Api.Log;
using WeChat.Core.Common.Redis;
using WeChat.Core.Common.WeChat;

namespace WeChat.Core.Api.Jobs
{
    /// <summary>
    /// token更新任务
    /// </summary>
    public class TokenRefreshJob:IJob
    {
        private const string CACHE_KEY = "AccessToken";

        private readonly IWeChatSDK _weChat;
        private readonly ILoggerHelper _logger;
        private readonly IRedisCacheManager _redisCache;

        public TokenRefreshJob(IWeChatSDK weChat,IRedisCacheManager redisCache,ILoggerHelper logger)
        {
            _weChat = weChat ?? throw new ArgumentNullException(nameof(weChat));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }
        /// <summary>
        /// 定时实现接口
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            
            var token = _redisCache.Get<string>(CACHE_KEY);
            if (string.IsNullOrEmpty(token))
            {
                var access = await _weChat.GetAccessToken();
                token = access.AccessToken;
                _redisCache.Set(CACHE_KEY, token, TimeSpan.FromHours(2));
                _logger.Debug(nameof(TokenRefreshJob), $"更新Token:{access.ExpiresIn}");
                
            }
            _logger.Debug(nameof(TokenRefreshJob), $"获取缓存{token}");
        }
    }
}