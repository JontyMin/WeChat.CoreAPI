using Newtonsoft.Json;

namespace WeChat.Core.Common.WeChat.Model
{
    public class Miniprogram
    {
        /// <summary>
        /// 所需跳转到的小程序appid
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 所需跳转到小程序的具体页面路径，支持带参数
        /// </summary>
        public string pagepath { get; set; }
    }

    /// <summary>
    /// 消息头
    /// </summary>
    public class First
    {
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string color { get; set; }
    }
    /// <summary>
    /// 关键字1
    /// </summary>
    public class Keyword1
    {
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string color { get; set; }
    }

    public class Keyword2
    {
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string color { get; set; }
    }

    public class Keyword3
    {
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string color { get; set; }
    }

    public class Remark
    {
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string color { get; set; }
    }

    /// <summary>
    /// 模板数据(具体关键字根据模板修改)
    /// </summary>
    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public First first { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Keyword1 keyword1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Keyword2 keyword2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Keyword3 keyword3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Remark remark { get; set; }
    }

    /// <summary>
    /// 消息模板实体类
    /// 配置修改见(https://developers.weixin.qq.com/doc/offiaccount/Message_Management/Template_Message_Interface.html)
    /// </summary>
    public class MessageTemplate
    {
        /// <summary>
        /// 接收者openid
        /// </summary>
        public string touser { get; set; }
        /// <summary>
        /// template_id
        /// </summary>
        public string template_id { get; set; }
        /// <summary>
        /// 模板跳转链接
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 跳小程序所需数据，不需跳小程序可不用传该数据
        /// </summary>
        public Miniprogram miniprogram { get; set; }
        /// <summary>
        /// 模板数据
        /// </summary>
        public Data data { get; set; }
    }

    /// <summary>
    /// 发送消息后响应类
    /// </summary>
    public class SendMsgResponse
    {
        [JsonProperty("errcode")]
        public int ErrCode { get; set; }

        [JsonProperty("errmsg")]
        public string ErrMsg { get; set; }

        [JsonProperty("msgid")]
        public long MsgId { get; set; }
    }
}