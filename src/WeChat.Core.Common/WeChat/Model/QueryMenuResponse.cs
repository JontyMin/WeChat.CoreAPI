using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeChat.Core.Common.WeChat.Model
{
    
    public  class QueryMenuRequest{

        /// <summary>
        /// user_id可以是粉丝的OpenID，也可以是粉丝的微信号。
        /// </summary>
        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }

    /// <summary>
    /// 测试个性化菜单匹配结果
    /// </summary>
    public class QueryMenuResponse
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("button")]
        public List<Btn> Button { get; set; }
    }

    public class Btn
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        public List<SubBtn> SubBtn { get; set; }
    }

    public class SubBtn
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}