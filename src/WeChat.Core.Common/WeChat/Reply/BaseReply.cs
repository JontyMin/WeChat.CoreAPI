namespace WeChat.Core.Common.WeChat.Reply
{
    public class BaseReply:BaseMessage
    {
        //没有新内容
        public virtual string ToXml()
        {
            return string.Empty;
        }
    }
}