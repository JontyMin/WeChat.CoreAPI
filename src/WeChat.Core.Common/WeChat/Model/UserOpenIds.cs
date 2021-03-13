using System.Collections.Generic;

namespace WeChat.Core.Common.WeChat.Model
{
    public class OpenId
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> openid { get; set; }
    }

    public class UserOpenIds
    {
        /// <summary>
        /// 关注该公众账号的总用户数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 拉取的OPENID个数，最大值为10000
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 列表数据，OPENID的列表
        /// </summary>
        public OpenId data { get; set; }
        /// <summary>
        /// 拉取列表的最后一个用户的OPENID
        /// </summary>
        public string next_openid { get; set; }
    }
   
}