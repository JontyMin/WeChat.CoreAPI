using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using RestSharp;
using WeChat.Core.Common.Helper;
using WeChat.Core.Common.Http;
using WeChat.Core.Common.Redis;
using WeChat.Core.Common.WeChat.Model;

namespace WeChat.Core.Common.WeChat
{
    /// <summary>
    /// 微信SDK
    /// </summary>
    public class WeChatSDK:IWeChatSDK
    {
        private readonly string AppId = AppSettings.app(new string[] {"AppSettings", "WeChat", "AppId" });
        private readonly string AppSecret = AppSettings.app(new string[] {"AppSettings", "WeChat", "AppSecret" });
        private readonly string RedirectUrl = AppSettings.app(new string[] {"AppSettings", "WeChat", "RedirectUrl" });
        private readonly string ApiDomain = AppSettings.app(new string[] {"AppSettings", "WeChat", "ApiDomain" });
        private readonly IRedisCacheManager _redisCache;

        private readonly string CACHE_KEY = $"{nameof(WeChatSDK)}-AccessToken";

        /*
         * 微信公众号官方文档
         * https://developers.weixin.qq.com/doc/offiaccount/Getting_Started/Overview.html
         */

        public WeChatSDK(IRedisCacheManager redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        #region OAuth 用户授权

        /// <summary>
        /// 获取验证地址
        /// </summary>
        /// <param name="state"></param>
        /// <param name="scope"></param>
        /// <param name="responseType"></param>
        /// <returns></returns>
        public string GetAuthorizeUrl(string state, OAuthScope scope,
            string responseType = "code")
        {

            /* 这一步发送之后，客户会得到授权页面，无论同意或拒绝，都会返回redirectUrl页面。
             * 如果用户同意授权，页面将跳转至 redirect_uri/?code=CODE&state=STATE。这里的code用于换取access_token（和通用接口的access_token不通用）
             * 若用户禁止授权，则重定向后不会带上code参数，仅会带上state参数redirect_uri?state=STATE
             */
            var url =
                $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={AppId}&redirect_uri={UrlEncode(RedirectUrl)}&response_type={responseType}&scope={scope}&state={state}#wechat_redirect";
            return url;
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code">code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。</param>
        /// <param name="grantType"></param>
        /// <returns></returns>
        public async Task<OAuthAccessTokenResult> GetAccessToken(string code, string grantType = "authorization_code")
        {
            var url =
                $"https://api.weixin.qq.com/sns/oauth2/access_token?appid={AppId}&secret={AppSecret}&code={code}&grant_type={grantType}";
            var client = new RestSharpClient(url);
            var response = await client.ExecuteAsync(new RestRequest(Method.GET));

            Console.WriteLine(response.Content);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            return JsonConvert.DeserializeObject<OAuthAccessTokenResult>(response.Content);
        }

        /// <summary>
        /// 刷新access_token（如果需要）
        /// </summary>
        /// <param name="refreshToken">填写通过access_token获取到的refresh_token参数</param>
        /// <param name="grantType"></param>
        /// <returns></returns>
        public async Task<OAuthAccessTokenResult> RefreshToken(string refreshToken, string grantType = "refresh_token")
        {
            var url =
                $"https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={AppId}&grant_type={grantType}&refresh_token={refreshToken}";
            var client = new RestSharpClient(url);
            var response = await client.ExecuteAsync(new RestRequest(Method.GET));

            Console.WriteLine(response.Content);

            if (response.ErrorException != null) throw response.ErrorException;

            return JsonConvert.DeserializeObject<OAuthAccessTokenResult>(response.Content);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public async Task<OAuthUserInfo> GetUserInfo(string accessToken, string openId)
        {
            var url = $"https://api.weixin.qq.com/sns/userinfo?access_token={accessToken}&openid={openId}";
            var client = new RestSharpClient(url);
            var response = await client.ExecuteAsync(new RestRequest(Method.GET));

            Console.WriteLine(response.Content);

            if (response.ErrorException != null) throw response.ErrorException;
            return JsonConvert.DeserializeObject<OAuthUserInfo>(response.Content); ;
        }

        #endregion

        #region OpenAPI开放平台

        /// <summary>
        /// 公众号后台接口API(2h)
        /// </summary>
        /// <returns></returns>
        public async Task<AccessTokenResponse> GetAccessToken()
        {
            var url =
                $"{ApiDomain}cgi-bin/token?grant_type=client_credential&appid={AppId}&secret={AppSecret}";
            var client = new RestSharpClient(url);
            var response = await client.ExecuteAsync(new RestRequest(Method.GET));
            Console.WriteLine($"{nameof(GetAccessToken)}-{response.Content}");
            if (response.ErrorException != null) throw response.ErrorException;
            return JsonConvert.DeserializeObject<AccessTokenResponse>(response.Content);
        }

        #region User用户管理

        /// <summary>
        /// 获取用户OpenIds
        /// </summary>
        /// <param name="nextOpenId"></param>
        /// <returns></returns>
        public async Task<UserOpenIds> GetUserOpenIds(string nextOpenId)
        {
            var token = await GetToken();
            var url = $"{ApiDomain}cgi-bin/user/get?access_token={token}&next_openid={nextOpenId}";
            var client = new RestSharpClient(url);
            var response = await client.ExecuteAsync(new RestRequest(Method.GET));
            Console.WriteLine($"{nameof(GetTagInfos)}-{response.Content}");
            if (response.ErrorException != null) throw response.ErrorException;
            return JsonConvert.DeserializeObject<UserOpenIds>(response.Content);
        }

        /// <summary>
        /// 根据用户OpenId获取用户信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public async Task<UserInfo> GetUserInfoByOpenId(string openId)
        {
            var token = await GetToken();
            var url = $"{ApiDomain}cgi-bin/user/info?access_token={token}&openid={openId}&lang=zh_CN";
            var client = new RestSharpClient(url);
            var response = await client.ExecuteAsync(new RestRequest(Method.GET));
            Console.WriteLine($"{nameof(GetTagInfos)}-{response.Content}");
            if (response.ErrorException != null) throw response.ErrorException;
            return JsonConvert.DeserializeObject<UserInfo>(response.Content);
        }

        /// <summary>
        /// 批量获取用户信息
        /// </summary>
        /// <param name="openIds"></param>
        /// <returns></returns>
        public async Task<UserInfos> BatchGetUserInfo(OpenIds openIds)
        {
            var token = await GetToken();
            var url = $"{ApiDomain}cgi-bin/user/info/batchget?access_token={token}";
            var response = await RestResponse(openIds, url);
            Console.WriteLine($"{nameof(GetTagInfos)}-{response.Content}");
            if (response.ErrorException != null) throw response.ErrorException;
            return JsonConvert.DeserializeObject<UserInfos>(response.Content);
        }

        #endregion

        #region Menu菜单管理

        /// <summary>
        /// 创建自定义菜单
        /// https://developers.weixin.qq.com/doc/offiaccount/Custom_Menus/Creating_Custom-Defined_Menu.html
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        public async Task<ResponseError> CreateMenu(CreateMenuTree tree)
        {
            var token = await GetToken();
            var url = $"{ApiDomain}cgi-bin/menu/create?access_token={token}";
            Console.WriteLine(JsonConvert.SerializeObject(tree));
            var response = await RestResponse(tree, url);
            Console.WriteLine($"{nameof(CreatePersonalizedMenu)}-{response.Content}");
            if (response.ErrorException!=null) throw response.ErrorException;
            return JsonConvert.DeserializeObject<ResponseError>(response.Content);
        }

        /// <summary>
        /// 创建个性化菜单
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        public async Task<MenuResponse> CreatePersonalizedMenu(CreateMenuTree tree)
        {
            var token = await GetToken();
            var url = $"{ApiDomain}cgi-bin/menu/addconditional?access_token={token}";
            Console.WriteLine(JsonConvert.SerializeObject(tree));
            var response = await RestResponse(tree, url);
            Console.WriteLine($"{nameof(CreatePersonalizedMenu)}-{response.Content}");
            if (response.ErrorException != null) throw response.ErrorException;
            return JsonConvert.DeserializeObject<MenuResponse>(response.Content);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseError> DeleteMenu()
        {
            var token = await GetToken();
            var url = $"{ApiDomain}cgi-bin/menu/delete?access_token={token}";
            var client = new RestSharpClient(url);
            var response = await client.ExecuteAsync(new RestRequest(Method.GET));
            if (response.ErrorException != null) throw response.ErrorException;
            return JsonConvert.DeserializeObject<ResponseError>(response.Content);
        }

        /// <summary>
        /// 删除定义菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<ResponseError> DeletePersonalizedMenu(MenuResponse menuId)
        {
            var token = await GetToken();
            var url = $"{ApiDomain}cgi-bin/menu/delconditional?access_token={token}";
            var client = new RestSharpClient(url);
            var request = new RestRequest(Method.POST);
            request.AddParameter("application/json", menuId, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            if (response.ErrorException != null) throw response.ErrorException;
            return JsonConvert.DeserializeObject<ResponseError>(response.Content);

        }

        /// <summary>
        /// 测试个性化菜单匹配结果
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<QueryMenuResponse> QueryPersonalizedMenu(QueryMenuRequest model)
        {
            var token = await GetToken();
            var url = $"{ApiDomain}cgi-bin/menu/trymatch?access_token={token}";
            var response = await RestResponse(model, url);
            Console.WriteLine($"{nameof(CreatePersonalizedMenu)}-{response.Content}");
            if (response.ErrorException != null) throw response.ErrorException;
            return JsonConvert.DeserializeObject<QueryMenuResponse>(response.Content);
        }

        

        #endregion

        #region Message消息管理

        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="message"><see cref="MessageTemplate"/></param>
        /// <returns><see cref="SendMsgResponse"/></returns>
        public async Task<SendMsgResponse> SendMessage(MessageTemplate message)
        {
            var token = await GetToken();
            var url = $"{ApiDomain}cgi-bin/message/template/send?access_token={token}";
            var response = await RestResponse(message, url);
            Console.WriteLine($"{nameof(SendMessage)}-{response.Content}");
            if (response.ErrorException != null) throw response.ErrorException;
            return JsonConvert.DeserializeObject<SendMsgResponse>(response.Content);
        }

        #endregion

        #region Tag标签

        /// <summary>
        /// 获取标签信息
        /// </summary>
        /// <returns><see cref="TagInfo"/></returns>
        public async Task<TagInfo> GetTagInfos()
        {
            var token = await GetToken();
            var url = $"{ApiDomain}cgi-bin/tags/get?access_token={token}";
            Console.WriteLine(url);
            var client = new RestSharpClient(url);
            var response = await client.ExecuteAsync(new RestRequest(Method.GET));
            Console.WriteLine($"{nameof(GetTagInfos)}-{response.Content}");
            if (response.ErrorException != null) throw response.ErrorException;
            return JsonConvert.DeserializeObject<TagInfo>(response.Content);
        }

        /// <summary>
        /// 批量设置粉丝标签
        /// </summary>
        /// <param name="model"><see cref="TagSetting"/></param>
        /// <returns><see cref="ResponseError"/></returns>
        public async Task<ResponseError> BatchTagging(TagSetting model)
        {
            
            var token = await GetToken();
            var url = $"{ApiDomain}cgi-bin/tags/members/batchtagging?access_token={token}";
            var client = new RestSharpClient(url);
            var response = await RestResponse(model, url);
            Console.WriteLine($"{nameof(SendMessage)}-{response.Content}");
            if (response.ErrorException != null) throw response.ErrorException;
            return JsonConvert.DeserializeObject<ResponseError>(response.Content);

        }
        
        /// <summary>
        /// 批量取消标签
        /// </summary>
        /// <param name="model"><see cref="TagSetting"/></param>
        /// <returns><see cref="ResponseError"/></returns>
        public async Task<ResponseError> BatchUnTagging(TagSetting model)
        {
            var token = await GetToken();
            var url = $"{ApiDomain}cgi-bin/tags/members/batchuntagging?access_token={token}";
            var client = new RestSharpClient(url);
            var response = await RestResponse(model, url);
            Console.WriteLine($"{nameof(SendMessage)}-{response.Content}");
            if (response.ErrorException != null) throw response.ErrorException;
            return JsonConvert.DeserializeObject<ResponseError>(response.Content);
        }

        #endregion

        #region 图文消息

        /// <summary>
        /// 获取图文列表(未完善)
        /// </summary>
        /// <returns></returns>
        public async Task<NewsResponse> GetNewsInfos(NewsRequest model)
        {
            var token = await GetToken();
            var url = $"{ApiDomain}cgi-bin/material/batchget_material?access_token={token}";
            Console.WriteLine(url);
            var response = await RestResponse(model, url);
            Console.WriteLine($"{nameof(GetTagInfos)}-{response.Content}");
            if (response.ErrorException != null) throw response.ErrorException;
            return JsonConvert.DeserializeObject<NewsResponse>(response.Content);
        }



        #endregion

        #endregion

        #region PeivateMethod

        /// <summary>
        /// POST请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"><see cref="T"/></param>
        /// <param name="url">请求url</param>
        /// <returns><see cref="IRestResponse"/></returns>
        private static async Task<IRestResponse> RestResponse<T>(T model, string url)
        {
            var client = new RestSharpClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(model),
                ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            return response;
        }

        
        /// <summary>
        /// 获取缓存token
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetToken()
        {

            var token = _redisCache.Get<string>(CACHE_KEY);
            if (string.IsNullOrEmpty(token))
            {
                var access = await GetAccessToken();
                token = access.AccessToken;
                _redisCache.Set(CACHE_KEY, token, TimeSpan.FromHours(2));
            }
            return token;
        }


        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }
            return (sb.ToString());
        }

        #endregion


    }
}