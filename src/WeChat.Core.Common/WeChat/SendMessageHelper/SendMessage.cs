using System;
using System.Threading.Tasks;
using WeChat.Core.Common.Helper;
using WeChat.Core.Common.WeChat.Model;

namespace WeChat.Core.Common.WeChat.SendMessageHelper
{
    
    /// <summary>
    /// 发送模板消息通知
    /// </summary>
    public class SendMessage:ISendMessage
    {
        /// <summary>
        /// 待审核消息模板Id
        /// </summary>
        private readonly string ToBeReviewedTemplateId = AppSettings.app(new string[] {"AppSettings", "WeChat", "ToBeReviewed" });

        /// <summary>
        /// 审核通过模板Id
        /// </summary>
        private readonly string ApprovedTemplateId = AppSettings.app(new string[] {"AppSettings", "WeChat", "Approved"});
        
        /// <summary>
        /// 审核提交通知模板Id
        /// </summary>
        private readonly string TemplateID = AppSettings.app(new string[] {"AppSettings", "WeChat", "TemplateID" });


        private readonly IWeChatSDK _weChatSdk;

        public SendMessage(IWeChatSDK weChatSdk)
        {
            _weChatSdk = weChatSdk ?? throw new ArgumentNullException(nameof(weChatSdk));
        }

        /// <summary>
        /// 发送待审核通知
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="name"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public async Task<SendMsgResponse> ToBeReviewed(string openId,string name,string mobile)
        {
            MessageTemplate message = new MessageTemplate
            {
                touser = openId,
                template_id = ToBeReviewedTemplateId,
                url = "https://lianzhoukuajing.com",
                miniprogram = null
            };

            Data msgData = new Data();
            var first = new First() { value = "你好，有新的供应商入驻申请待审核" };
            msgData.first = first;
            var keyword1 = new Keyword1() { value = name };
            msgData.keyword1 = keyword1;
            var keyword2 = new Keyword2() { value = mobile };
            msgData.keyword2 = keyword2;
            var remark = new Remark() { value = "请前往ERP处理" };
            msgData.remark = remark;
            message.data = msgData;
            
            return await _weChatSdk.SendMessage(message);

        }

        /// <summary>
        /// 发送审核通过消息
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="name"></param>
        /// <param name="mobile"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<SendMsgResponse> Approved(string openId, string name, string mobile,string code)
        {
            MessageTemplate message = new MessageTemplate
            {
                touser = openId,
                template_id = ApprovedTemplateId,
                url = "https://wx.lianzhoukuajing.com",
                miniprogram = null
            };

            Data msgData = new Data();
            var first = new First() { value = "你好，贵公司的入驻申请已通过" };
            msgData.first = first;
            var keyword1 = new Keyword1() { value = name };
            msgData.keyword1 = keyword1;
            var keyword2 = new Keyword2() { value = mobile };
            msgData.keyword2 = keyword2;
            var keyword3 = new Keyword3() { value = code };
            msgData.keyword3 = keyword3;
            var remark = new Remark() { value = "点击前往注册" };
            msgData.remark = remark;
            message.data = msgData;

            return await _weChatSdk.SendMessage(message);
        }


        /// <summary>
        /// 审核提交通知
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="name"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public async Task<SendMsgResponse> Submitted(string openId, string name, string mobile)
        {
            

            MessageTemplate message = new MessageTemplate
            {
                touser = openId,
                template_id = TemplateID,
                url = "https://wx.lianzhoukuajing.com/#/Apply",
                miniprogram = null
            };

            Data msgData = new Data();
            var first = new First() { value = "入驻申请已提交，请等待联洲审核" };
            msgData.first = first;
            var keyword1 = new Keyword1() { value = name };
            msgData.keyword1 = keyword1;
            var keyword2 = new Keyword2() { value = mobile };
            msgData.keyword2 = keyword2;
            var keyword3 = new Keyword3() { value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
            msgData.keyword3 = keyword3;
            var remark = new Remark() { value = "感谢你的使用" };
            msgData.remark = remark;
            message.data = msgData;

            return await _weChatSdk.SendMessage(message);
        }
    }
}