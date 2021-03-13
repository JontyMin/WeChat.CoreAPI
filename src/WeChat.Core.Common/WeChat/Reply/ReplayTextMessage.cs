using System.Text;
using WeChat.Core.Common.WeChat.ChatRobot;

namespace WeChat.Core.Common.WeChat.Reply
{
    /// <summary>
    /// 回复文本内容
    /// </summary>
    public class ReplayTextMessage:BaseReply
    {
        private Robot _robot;
        public string _query;
        public ReplayTextMessage(string query)
        {
            _robot = new Robot();
            _query = query;
            Content = _robot.ChatBot(_query);
        }

        public ReplayTextMessage()
        {
            
        }

        //回复文本内容
        public string Content { get; set; }

        //<xml>
        //<ToUserName><![CDATA[toUser]]></ToUserName>
        //<FromUserName><![CDATA[fromUser]]></FromUserName>
        //<CreateTime>12345678</CreateTime>
        //<MsgType><![CDATA[text]]></MsgType>
        //<Content><![CDATA[你好]]></Content>
        //</xml>
        public override string ToXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            sb.Append("<ToUserName><![CDATA[" + this.ToUserName + "]]></ToUserName>");
            sb.Append("<FromUserName><![CDATA[" + this.FromUserName + "]]></FromUserName>");
            sb.Append("<CreateTime>" + this.CreateTime + "</CreateTime>");
            sb.Append("<MsgType><![CDATA[text]]></MsgType>");
            sb.Append("<Content><![CDATA[" + this.Content + "]]></Content>");
            sb.Append("</xml>");
            return sb.ToString();
        }
    }
}