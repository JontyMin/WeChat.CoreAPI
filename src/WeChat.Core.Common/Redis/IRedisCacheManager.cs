using System;

namespace WeChat.Core.Common.Redis
{
    public interface IRedisCacheManager
    {
        void Clear();

        bool Get(string key);

        string GetValue(string key);

        TEntity Get<TEntity>(string key);

        void Remove(string key);

        void Set(string key, object value, TimeSpan cacheTime);

        bool SetValue(string key, byte[] value);
    }
}