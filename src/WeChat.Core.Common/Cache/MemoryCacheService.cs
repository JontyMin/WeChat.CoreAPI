using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Logging;

namespace WeChat.Core.Common.Cache
{
    public class MemoryCacheService: IMemoryCacheService
    {
    private readonly IMemoryCache _cache;

    public MemoryCacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public bool Add<T>(string key, T value, int expirationTime = 7200)
    {
        if (!string.IsNullOrEmpty(key))
        {
            MemoryCacheEntryOptions cacheEntityOps = new MemoryCacheEntryOptions()
            {
                //滑动过期时间 20秒没有访问则清除
                //SlidingExpiration = TimeSpan.FromSeconds(expirationTime),

                // 绝对过期
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(expirationTime),
                //设置份数
                Size = 1,
                //优先级
                Priority = CacheItemPriority.Low,
            };
            //过期回掉
            cacheEntityOps.RegisterPostEvictionCallback((keyInfo, valueInfo, reason, state) =>
            {
                Console.WriteLine($"回调函数输出【键:{keyInfo},值:{valueInfo},被清除的原因:{reason}】");

            });
            _cache.Set<T>(key, value, cacheEntityOps);
        }

        return true;
    }

    public T GetValue<T>(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return default(T);
        }

        if (Exists(key))
        {
            return _cache.Get<T>(key);
        }

        return default(T);
    }

    public bool Exists(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return false;
        }

        object cache;
        return _cache.TryGetValue(key, out cache);
    }

    public bool Remove(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return false;
        }

        if (Exists(key))
        {
            _cache.Remove(key);
            return true;
        }

        return false;
    }
    }
}