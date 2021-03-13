using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeChat.Core.Common.WeChat.Model
{
    public class UserInfo
    {
        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        /// </summary>
        [JsonProperty("subscribe")]
        public int Subscribe { get; set; }

        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        [JsonProperty("openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// 户的昵称
        /// </summary>
        [JsonProperty("nickname")]
        public string NickName { get; set; }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        [JsonProperty("sex")]
        public int Sex { get; set; }

        /// <summary>
        /// 用户所在国家
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// 用户所在省份
        /// </summary>
        [JsonProperty("province")]
        public string Province { get; set; }

        /// <summary>
        /// 用户所在城市
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// 用户的语言，简体中文为zh_CN
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        [JsonProperty("headimgurl")]
        public string HeadImgUrl { get; set; }

        /// <summary>
        /// 用户关注时间戳
        /// </summary>
        [JsonProperty("subscribe_time")]
        public long SubscribeTime { get; set; }

        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。
        /// </summary>
        [JsonProperty("unionid")]
        public string UnionId { get; set; }

        /// <summary>
        /// 公众号运营者对粉丝的备注
        /// </summary>
        [JsonProperty("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 用户所在的分组ID
        /// </summary>
        [JsonProperty("groupid")]
        public int GroupId { get; set; }

        /// <summary>
        /// 用户被打上的标签ID列表
        /// </summary>
        [JsonProperty("tagid_list")]
        public List<int> TagIdList { get; set; }

        /// <summary>
        /// 返回用户关注的渠道来源
        /// </summary>
        [JsonProperty("subscribe_scene")]
        public string SubscribeScene { get; set; }
        
        
        
    }

    public class OpenIds
    {
        public List<OpenIdList> user_list { get; set; }
    }

    public class OpenIdList
    {
        public string openid { get; set; }
        public string lang { get; set; }
    }
    
    public class UserInfos{
        public List<UserInfo> user_info_list { get; set; }
    }
}