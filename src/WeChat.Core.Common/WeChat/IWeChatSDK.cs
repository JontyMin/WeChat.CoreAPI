using System.Threading.Tasks;
using WeChat.Core.Common.WeChat.Model;

namespace WeChat.Core.Common.WeChat
{
    /// <summary>
    /// 微信SDK
    /// </summary>
    public interface IWeChatSDK
    {

        #region OAuth
        
        /// <summary>
        /// 获取授权url
        /// </summary>
        /// <param name="state"></param>
        /// <param name="scope"></param>
        /// <param name="responseType"></param>
        /// <returns></returns>
        string GetAuthorizeUrl(string state, OAuthScope scope,
            string responseType = "code");

        /// <summary>
        /// 获取用户授权AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <param name="grantType"></param>
        /// <returns></returns>
        Task<OAuthAccessTokenResult> GetAccessToken(string code, string grantType= "authorization_code");

        /// <summary>
        /// 刷新AccessToken
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="grantType"></param>
        /// <returns></returns>
        Task<OAuthAccessTokenResult> RefreshToken(string refreshToken, string grantType = "refresh_token");

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        Task<OAuthUserInfo> GetUserInfo(string accessToken, string openId);

        #endregion

        #region Platform

        /// <summary>
        /// 获取开放平台AccessToken
        /// </summary>
        /// <returns></returns>
        Task<AccessTokenResponse> GetAccessToken();

        

        #region Custom menu

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        Task<ResponseError> CreateMenu(CreateMenuTree tree);

        /// <summary>
        /// 创建个性菜单
        /// (添加标签)
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        Task<MenuResponse> CreatePersonalizedMenu(CreateMenuTree tree);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <returns></returns>
        Task<ResponseError> DeleteMenu();

        /// <summary>
        /// 删除个性化菜单
        /// </summary>
        /// <param name="menuId">自定义菜单Id</param>
        /// <returns></returns>
        Task<ResponseError> DeletePersonalizedMenu(MenuResponse menuId);

        /// <summary>
        /// 测试个性化菜单匹配结果
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<QueryMenuResponse> QueryPersonalizedMenu(QueryMenuRequest model);

        #endregion


        #region Message

        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<SendMsgResponse> SendMessage(MessageTemplate message);


        #endregion


        #region Tag

        /// <summary>
        /// 获取标签信息
        /// </summary>
        /// <returns></returns>
        Task<TagInfo> GetTagInfos();


        /// <summary>
        /// 批量打标签
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ResponseError> BatchTagging(TagSetting model);

        /// <summary>
        /// 批量取消标签
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ResponseError> BatchUnTagging(TagSetting model);
        
        #endregion

        /// <summary>
        /// 获取图文列表
        /// </summary>
        /// <returns></returns>
        Task<NewsResponse> GetNewsInfos(NewsRequest model);


        #region Message

        #region User

        /// <summary>
        /// 获取用户openIds
        /// </summary>
        /// <param name="nextOpenId">拉取列表的最后一个用户的OPENID</param>
        /// <returns><see cref="UserOpenIds"/></returns>
        Task<UserOpenIds> GetUserOpenIds(string nextOpenId);

        /// <summary>
        /// 根据openId获取用户信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        Task<UserInfo> GetUserInfoByOpenId(string openId);

        /// <summary>
        /// 批量获取用户信息
        /// </summary>
        /// <param name="openIds"></param>
        /// <returns></returns>
        Task<UserInfos> BatchGetUserInfo(OpenIds openIds);

        #endregion

        #endregion

        #endregion
    }
}