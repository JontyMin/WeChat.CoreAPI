using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WeChat.Core.Api.Authorization;
using WeChat.Core.Common.WeChat;
using WeChat.Core.Common.WeChat.Model;
using WeChat.Core.Model;

namespace WeChat.Core.Api.Controllers
{
    /// <summary>
    /// 微信登录授权
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WeChatAuthController : ControllerBase
    {
        private readonly IWeChatSDK _weChatSdk;

        public WeChatAuthController(IWeChatSDK weChatSdk)
        {
            _weChatSdk = weChatSdk ?? throw new ArgumentNullException(nameof(weChatSdk));
        }

        /// <summary>
        /// 获取请求授权地址
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AuthorizeUrl")]
        public MessageModel<string> Get()
        {
            // 随机生成state 放置在缓存中 请求时验证
            var data = new MessageModel<string>
            {
                response = _weChatSdk.GetAuthorizeUrl("1", OAuthScope.snsapi_userinfo),
                msg = "请求成功",
                success = true,
                status = 200
            };
            return data;
        }

        /// <summary>
        /// 获取微信AccessToken
        /// </summary>
        /// <param name="code">请求授权地址返回链接中的Code</param>
        /// <returns><see cref="OAuthAccessTokenResult"/></returns>
        [HttpGet]
        [Route("AccessToken/{code}")]
        public async Task<MessageModel<OAuthAccessTokenResult>> Get(string code)
        {
            var data = new MessageModel<OAuthAccessTokenResult> { status = 200 };
            var result = await _weChatSdk.GetAccessToken(code);
            if (result.access_token != null)
            {
                data.success = true;
                data.msg = "获取成功";
                data.response = result;
                return data;
            }
            // 使用缓存存储起来
            data.success = false;
            data.msg = "获取失败";
            data.response = result;
            return data;
        }

        /// <summary>
        /// 获取微信授权用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="OAuthUserInfoRequest"/></returns>
        [HttpPost]
        [Authorize]
        public async Task<MessageModel<OAuthUserInfo>> Post(OAuthUserInfoRequest model)
        {
            var data = new MessageModel<OAuthUserInfo>();
            if (string.IsNullOrEmpty(model.AccessToken) || string.IsNullOrEmpty(model.OpenId))
            {
                data.status = 400;
                data.success = false;
                data.msg = "请求错误";
                return data;
            }

            var result = await _weChatSdk.GetUserInfo(model.AccessToken, model.OpenId);
            if (result != null)
            {
                data.status = 200;
                data.success = true;
                data.msg = "获取成功";
                data.response = result;
                return data;
            }

            data.status = 200;
            data.success = false;
            data.msg = "获取失败";
            return data;
        }

        /// <summary>
        /// 请求刷新Token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns><see cref="OAuthAccessTokenResult"/></returns>
        [HttpPost("{refreshToken}")]
        [Authorize]
        public async Task<MessageModel<OAuthAccessTokenResult>> Post(string refreshToken)
        {
            var data = new MessageModel<OAuthAccessTokenResult>();
            if (string.IsNullOrEmpty(refreshToken))
            {
                data.status = 400;
                data.success = false;
                data.msg = "错误请求";
            }

            var result = await _weChatSdk.RefreshToken(refreshToken);
            if (result.access_token != null)
            {
                data.success = true;
                data.msg = "获取成功";
                data.response = result;
                return data;
            }
            // 使用缓存存储起来
            data.success = false;
            data.msg = "获取失败";
            data.response = result;
            return data;
        }
    }
}
