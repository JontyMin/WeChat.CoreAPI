using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WeChat.Core.Api.Authorization;
using WeChat.Core.Api.Log;
using WeChat.Core.Common.Helper;
using WeChat.Core.Common.HttpContextUser;
using WeChat.Core.Common.Redis;
using WeChat.Core.Common.WeChat;
using WeChat.Core.IService;
using WeChat.Core.IService.ERP;
using WeChat.Core.Model;
using WeChat.Core.Model.ViewModel;

namespace WeChat.Core.Api.Controllers
{
    /// <summary>
    /// 供应商入驻(无权限)
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class AccountController : ControllerBase
    {
        private readonly IWeChatSupplierService _weChatSupplierService;
        private readonly IRedisCacheManager _redisCache;
        private readonly IUserService _userService;
        private readonly IWeChatSDK _weChatOAuth;
        private readonly ILoggerHelper _logger;
        private readonly IUser _user;
        

        public AccountController(IWeChatSupplierService weChatSupplierService,
            IUserService userService,
            IRedisCacheManager redisCache,
            IWeChatSDK weChatOAuth,
            ILoggerHelper logger,
            IUser user
            )
        {
            _weChatSupplierService = weChatSupplierService ?? throw new ArgumentNullException(nameof(weChatSupplierService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _weChatOAuth = weChatOAuth ?? throw new ArgumentNullException(nameof(weChatOAuth));
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }
        
        /// <summary>
        /// 供应商申请入驻
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ApplySettled")]
        public async Task<MessageModel<int>> ApplySettled(ApplySettledViewModel model)
        {
            var data = new MessageModel<int>();
            
            int sId = await _weChatSupplierService.CreateSupplier(model);
            if (sId > 0)
            {
                data.status = 200;
                data.success = true;
                data.msg = "您的入驻申请已提交,供应商资质审核通过后将发送邀请码";
                data.response = sId;
                return data;
            }

            data.status = 400;
            return data;
        }

        /// <summary>
        /// 供应商注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public async Task<MessageModel<string>> Registered(RegisterViewModel model)
        {
            var data = new MessageModel<string>();

            var suppliers = await _weChatSupplierService.Query(x => x.InvitationCode == model.InvitationCode);
            if (!suppliers.Any())
            {
                data.msg = "无效邀请码";
                data.status = 404;
                return data;
            }

            var userNames = await _weChatSupplierService.Query(x => x.LoginName == model.LoginName);
            if (userNames.Any())
            {
                data.msg = "用户名已存在";
                data.status = 400;
                return data;
            }
            
            // 邀请码=>用户Id
            var sId = CodeHelper.Decode(model.InvitationCode);
            var supplier = await _weChatSupplierService.QueryById(sId);
            if (supplier!=null)
            {
                if (string.IsNullOrEmpty(supplier.LoginName) && string.IsNullOrEmpty(supplier.LoginPwd))
                {
                    supplier.LoginName = model.LoginName;
                    supplier.LoginPwd = MD5Helper.MD5Encrypt32(model.LoginPwd);
                    var state = await _weChatSupplierService.Update(supplier);
                    data.status = 200;
                    data.success = state;
                    data.msg = "注册成功";
                    return data;
                }

                data.status = 400;
                data.msg = "邀请码错误";
            }
            return data;
        }

        /// <summary>
        /// 供应商登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("JwtToken")]
        public async Task<IActionResult> GetToken(LoginViewModel model)
        {
            
            if (string.IsNullOrEmpty(model.userName) || string.IsNullOrEmpty(model.passWord))
                return new JsonResult(new
                {
                    status = false,
                    message = "用户名或密码不能为空"
                });

            // 密码加密
            model.passWord = MD5Helper.MD5Encrypt32(model.passWord);
            var supplier =
                await _weChatSupplierService.Query(x => x.LoginName == model.userName && x.LoginPwd == model.passWord);

            // 查无此人 => 账号密码错误 
            if (!supplier.Any())
                return new JsonResult(new
                {
                    status = false,
                    message = "账号或密码错误"
                });

            // 禁用状态
            if (supplier.First().State == (int)SupplierState.Disabled)
                return new JsonResult(new
                {
                    status = false,
                    message = "账号已停用,请联系管理员"
                });

            if (supplier.Count > 0)
            {
                var tokenModel = new TokenModel {sId = supplier.FirstOrDefault().Id};
                var jwtStr = JwtHelper.IssueJwt(tokenModel);
                //_redisCache.Set("Token",jwtStr, TimeSpan.FromHours(2));
                return new JsonResult(jwtStr);
            }

            return new JsonResult(new
            {
                success = false,
                message = "认证失败"
            });
        }

        
        /// <summary>
        /// ERP登录授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ErpToken")] 
        public async Task<IActionResult> JwtToken(LoginViewModel model)
        {
            //StringBuilder builder = new StringBuilder("L#g^p");
            //string pass = builder.Append(model.passWord).ToString();
            model.passWord = MD5Helper.MD5Entry(model.passWord);

            var user = await _userService.Query(x => x.LoginName == model.userName&& x.LoginPwd == model.passWord);
            if (user.Any())
            {
                var tokenModel = new TokenModel()
                {
                    sId = user.FirstOrDefault().UserId
                };
                var jwtStr = JwtHelper.IssueJwt(tokenModel);
                return new JsonResult(jwtStr);
            }

            return new JsonResult(new
            {
                success = false,
                message = "登录失败"
            });
        }
        
        
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RefreshToken/{token}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult RefreshToken(string token = "")
        {
            // 需要截取Bearer
            //var tokenHeader = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return new JsonResult(new
                {
                    Status = false,
                    message = "请重新登录一下吧"
                });
            }
            var tokenModel = JwtHelper.SerializeJwt(token);
            if (tokenModel != null && tokenModel.sId > 0)
            {
                var refreshToken = JwtHelper.IssueJwt(tokenModel);
                return new JsonResult(refreshToken);
            }

            return new JsonResult(new
            {
                success = false,
                message = "登录超时"
            });
        }

        /// <summary>
        /// 测试 MD5 加密字符串
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Md5Password")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public string Md5Password(string password = "")
        {
            return MD5Helper.MD5Encrypt32(password);
        }

    }
}
