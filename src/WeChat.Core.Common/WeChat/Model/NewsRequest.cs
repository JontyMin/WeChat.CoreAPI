using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeChat.Core.Common.WeChat.Model
{
    /// <summary>
    /// 获取图文列表请求
    /// </summary>
    public class NewsRequest
    {
        public string type { get; set; } = "news";
        public int offset { get; set; } = default(int);
        public int count { get; set; } = 10;
    }

    public class NewsResponse
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        [JsonProperty("item_count")]
        public int ItemCount { get; set; }
        [JsonProperty("item")]
        public List<NewItem> NewItems { get; set; }
    }

    public class NewItem
    {
        [JsonProperty("media_id")]
        public string MediaId { get; set; }

        [JsonProperty("content")]
        public Content Content { get; set; }
        
        [JsonProperty("update_time")]
        public long UpdateTime { get; set; }
    }

    public class Content
    {
        [JsonProperty("news_item")]
        public List<NewsItem> List { get; set; }

    }

    public class NewsItem
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}