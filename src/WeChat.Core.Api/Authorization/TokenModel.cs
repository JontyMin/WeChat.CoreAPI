namespace WeChat.Core.Api.Authorization
{
    public class TokenModel
    {
        /// <summary>
        /// 供应商Id
        /// </summary>
        public int sId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; } = "supplier";

    }

    /// <summary>
    /// Token信息
    /// </summary>
    public class TokenInfoViewModel
    {
        public bool success { get; set; }
        public string token { get; set; }
        public double expires_in { get; set; }
        public string token_type { get; set; }
    }
}