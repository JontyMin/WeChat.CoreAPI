namespace WeChat.Core.Common.WeChat.Model
{
    /// <summary>
    /// 获取授权用户信息请求
    /// </summary>
    public class OAuthUserInfoRequest
    {
        /// <summary>
        /// 授权之后返回的token
        /// </summary>
        public string AccessToken { get; set; }
        public string OpenId { get; set; }
        
    }
}