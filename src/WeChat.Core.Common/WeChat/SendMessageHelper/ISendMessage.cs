using System.Threading.Tasks;
using WeChat.Core.Common.WeChat.Model;

namespace WeChat.Core.Common.WeChat.SendMessageHelper
{
    /// <summary>
    /// 发送模板消息通知
    /// </summary>
    public interface ISendMessage
    {
        /// <summary>
        /// 发送待审核通知
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="name"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        Task<SendMsgResponse> ToBeReviewed(string openId, string name, string mobile);

        /// <summary>
        /// 审核通过通知
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="name"></param>
        /// <param name="mobile"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<SendMsgResponse> Approved(string openId, string name, string mobile, string code);

        /// <summary>
        /// 提交审核通知
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="name"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        Task<SendMsgResponse> Submitted(string openId, string name, string mobile);
    }
}