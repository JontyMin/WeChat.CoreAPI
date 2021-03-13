using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using WeChat.Core.Api.Log;
using WeChat.Core.Common.MessageEncryption;
using WeChat.Core.Common.WeChat;
using WeChat.Core.Common.WeChat.Model;
using WeChat.Core.Common.WeChat.Reply;
using WeChat.Core.Common.WeChat.SendMessageHelper;
using WeChat.Core.Model;
using WeChat.Core.Model.ViewModel;

namespace WeChat.Core.Api.Controllers
{
    /// <summary>
    /// 微信后台API
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "WeChat")]
    public class WeChatAPIController : ControllerBase
    {
        private readonly IWeChatSDK _weChatSdk;
        private readonly ISendMessage _sendMessage;
        private readonly ILoggerHelper _logger;
        private WXBizMsgCrypt _wxBizMsgCrypt;

        public WeChatAPIController(IWeChatSDK weChatSdk,ISendMessage sendMessage,ILoggerHelper logger,WXBizMsgCrypt wxBizMsgCrypt)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _weChatSdk = weChatSdk ?? throw new ArgumentNullException(nameof(weChatSdk));
            _sendMessage = sendMessage ?? throw new ArgumentNullException(nameof(sendMessage));
            _wxBizMsgCrypt = wxBizMsgCrypt ?? throw new ArgumentNullException(nameof(wxBizMsgCrypt));
        }
        
        /// <summary>
        /// 公众号校验
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public string Get(string signature, string timestamp, string nonce, string echostr)
        {   
            
            var check = new CheckSignature().Check(signature, timestamp, nonce);
            if (check)
            {
                return echostr;
            }
            return String.Empty;
        }
        
        /// <summary>
        /// 公众号接收消息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<string> Post(string signature, string timestamp, string nonce, string echostr)
        {
            var check = new CheckSignature().Check(signature, timestamp, nonce);
            if (!check)
            {
                // 校验失败,不会对此作任何处理
                return "";
            }

            //var notifyContent = Request.Body.ReadAsync();
            string notifyContent;
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                notifyContent = await reader.ReadToEndAsync();
            }

            return ReplayMessage.ParseMessage(notifyContent);
        }

        /// <summary>
        /// 获取开放平台API_Token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetAccessToken))]
        public async Task<MessageModel<AccessTokenResponse>> GetAccessToken()
        {
            var data = new MessageModel<AccessTokenResponse>();
            var response =await _weChatSdk.GetAccessToken();
            if (!string.IsNullOrEmpty(response.AccessToken))
            {
                data.msg = "获取成功";
                data.status = 200;
                data.success = true;
                data.response = response;
                return data;
            }
            data.status = 400;
            data.msg = "获取失败,请重试";
            return data;
        }

        /// <summary>
        /// 获取用户标签信息
        /// </summary>
        /// <returns><see cref="TagInfo"/></returns>
        [HttpGet]
        [Route(nameof(GetTagInfo))]
        public async Task<MessageModel<TagInfo>> GetTagInfo()
        {
            var data = new MessageModel<TagInfo>();
            var response = await _weChatSdk.GetTagInfos();
            if (response.tags.Count > 0)
            {
                data.msg = "获取成功";
                data.status = 200;
                data.success = true;
                data.response = response;
                return data;
            }
            data.status = 400;
            data.msg = "获取失败,请重试";
            return data;
        }

        /// <summary>
        /// 获取图文信息
        /// </summary>
        /// <returns><see cref="NewsResponse"/></returns>
        [HttpPost]
        [Route(nameof(GetNewsInfo))]
        public async Task<MessageModel<NewsResponse>> GetNewsInfo(NewsRequest news)
        {
            var data = new MessageModel<NewsResponse>();
            var response = await _weChatSdk.GetNewsInfos(news);
            if (response.ItemCount > 0)
            {
                data.msg = "获取成功";
                data.status = 200;
                data.success = true;
                data.response = response;
                return data;
            }
            data.status = 400;
            data.msg = "获取失败,请重试";
            return data;
        }


        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(CreateMenu))]
        public async Task<MessageModel<ResponseError>> CreateMenu(CreateMenuTree tree)
        {
            var data = new MessageModel<ResponseError>();
            var response = await _weChatSdk.CreateMenu(tree);
            if (response.ErrorMessage == "ok")
            {
                data.msg = "创建成功";
                data.status = 200;
                data.success = true;
                data.response = response;
                return data;
            }

            data.status = 400;
            data.msg = "创建失败,请重试";
            return data;
        }

        /// <summary>
        /// 创建自定义菜单
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(CreatePersonalizedMenu))]
        public async Task<MessageModel<MenuResponse>> CreatePersonalizedMenu(CreateMenuTree tree)
        {
            var data = new MessageModel<MenuResponse>();
            var response =await _weChatSdk.CreatePersonalizedMenu(tree);
            if (response.MenuId!=null)
            {
                data.msg = "创建成功";
                data.status = 200;
                data.success = true;
                data.response = response;
                return data;
            }

            data.status = 400;
            data.msg = "创建失败,请重试";
            return data;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route(nameof(DeleteMenu))]
        public async Task<MessageModel<ResponseError>> DeleteMenu()
        {
            var data = new MessageModel<ResponseError>();
            var response = await _weChatSdk.DeleteMenu();
            if (response.ErrorMessage=="ok")
            {
                data.msg = "删除完成";
                data.status = 200;
                data.success = true;
                data.response = response;
                return data;
            }
            data.status = 400;
            data.msg = "删除失败,请重试";
            return data;
        }

        /// <summary>
        /// 删除自定义菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(nameof(DeletePersonalizedMenu))]
        public async Task<MessageModel<ResponseError>> DeletePersonalizedMenu(MenuResponse model)
        {
            var data = new MessageModel<ResponseError>();
            var response = await _weChatSdk.DeletePersonalizedMenu(model);
            if (response.ErrorMessage == "ok")
            {
                data.msg = "删除完成";
                data.status = 200;
                data.success = true;
                data.response = response;
                return data;
            }
            data.status = 400;
            data.msg = "删除失败,请重试";
            return data;
        }

        
        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(SendMessage))]
        [AllowAnonymous]
        public async Task<MessageModel<SendMsgResponse>> SendMessage(SendMessageModel model)
        {
            var data = new MessageModel<SendMsgResponse>();

            var response = new SendMsgResponse();
            switch (model.Type)
            {
                case 1:
                    response = await _sendMessage.Submitted(model.OpenId, model.Name, model.Phone);
                    break;
                case 2:
                    response = await _sendMessage.ToBeReviewed(model.OpenId, model.Name, model.Phone);
                    break;
                case 3:
                    response = await _sendMessage.Approved(model.OpenId, model.Name, model.Phone, model.Code);
                    break;
            }
            
            if (response.ErrMsg=="ok")
            {
                data.msg = "发送成功";
                data.status = 200;
                data.success = true;
                data.response = response;
                return data;
            }
            data.status = 400;
            data.msg = "发送失败,请重试";
            data.response = response;
            return data;
        }

        /// <summary>
        /// 设置用户标签
        /// </summary>
        /// <param name="setTag"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(Tagging))]
        [AllowAnonymous]
        public async Task<MessageModel<ResponseError>> Tagging(SetTagModel setTag)
        {

            TagSetting model = new TagSetting();
            List<string> openidList = new List<string> { setTag.OpenId};
            model.openid_list = openidList;
            model.tagid = setTag.TagId;

            ResponseError response = new ResponseError();
            switch (setTag.Type)
            {
                case 1001: // 设置标签
                    response = await _weChatSdk.BatchTagging(model);
                    break;
                case 1002: // 取消标签
                    response = await _weChatSdk.BatchUnTagging(model);
                    break;
            }

            var data = new MessageModel<ResponseError>();
            
            if (response.ErrorMessage=="ok")
            {
                data.status = 200;
                data.success = true;
                data.response = response;
                data.msg = "设置成功";
                return data;
            }
            data.status = 400;
            data.msg = "设置失败,请重试";
            return data;
        }
    }
}
