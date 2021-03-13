using Newtonsoft.Json;

namespace WeChat.Core.Common.WeChat.Model
{
    
    /// <summary>
    /// 公众号后台AccessToken响应
    /// </summary>
    public class AccessTokenResponse
    {
        /// <summary>
        /// Token
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }
    }

    /// <summary>
    /// 错误响应
    /// </summary>
    public class ResponseError  
    {
        [JsonProperty("errcode")]
        public int ErrorCode { get; set; }

        [JsonProperty("errmsg")]
        public string ErrorMessage { get; set; }    
    }

    /// <summary>
    /// 创建个性菜单响应
    /// </summary>
    public class MenuResponse   
    {
        [JsonProperty("menuid")]
        public string MenuId { get; set; }
    }
}