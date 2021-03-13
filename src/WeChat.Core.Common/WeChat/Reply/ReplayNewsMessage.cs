using System.Text;

namespace WeChat.Core.Common.WeChat.Reply
{
    public class ReplayNewsMessage:BaseReply
    {
        /// <summary>
        /// 图文消息数量
        /// </summary>
        public int Count { get; set; } = 1; // 如果是多条就创建多个item 暂时只回复1条

        public string Title { get; set; }

        public string Description { get; set; }

        public string PicUrl { get; set; }

        public string Url { get; set; }
        public override string ToXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            sb.Append("<ToUserName><![CDATA[" + this.ToUserName + "]]></ToUserName>");
            sb.Append("<FromUserName><![CDATA[" + this.FromUserName + "]]></FromUserName>");
            sb.Append("<CreateTime>" + this.CreateTime + "</CreateTime>");
            sb.Append("<MsgType><![CDATA[news]]></MsgType>");
            sb.Append($"<ArticleCount>{this.Count}</ArticleCount>");
            sb.Append("<Articles>");
            sb.Append("<item>");
            sb.Append($"<Title><![CDATA[{Title}]]></Title>");
            sb.Append($"<Description><![CDATA[{Description}]]></Description>");
            sb.Append($"<PicUrl><![CDATA[{PicUrl}]]></PicUrl>");
            sb.Append($"<Url><![CDATA[{Url}]]></Url>");
            sb.Append("</item>");
            sb.Append("</Articles>");
            sb.Append("</xml>");
            return sb.ToString();
        }
    }
}