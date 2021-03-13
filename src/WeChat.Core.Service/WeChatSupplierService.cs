using System;
using System.Threading.Tasks;
using AutoMapper;
using WeChat.Core.Common.Helper;
using WeChat.Core.Common.WeChat;
using WeChat.Core.Common.WeChat.Model;
using WeChat.Core.Common.WeChat.SendMessageHelper;
using WeChat.Core.IRepository;
using WeChat.Core.IRepository.BASE;
using WeChat.Core.IService;
using WeChat.Core.Model;
using WeChat.Core.Model.EntityModel;
using WeChat.Core.Model.ViewModel;
using WeChat.Core.Service.BASE;

namespace WeChat.Core.Service
{
    /// <summary>
    /// 供应商业务类
    /// </summary>
    public class WeChatSupplierService:BaseService<WeChatSupplier>,IWeChatSupplierService
    {
        /// <summary>
        /// 审核接收openId(一般是采购总监)
        /// </summary>
        private readonly string Reviewer = AppSettings.app(new string[] {"AppSettings", "WeChat", "Reviewer" });

        private readonly IWeChatSupplierRepository _chatSupplierRepository;
        private readonly ISendMessage _sendMessage;
        private readonly IWeChatSDK _weChat;
        private readonly IMapper _mapper;
        private IWeChatSupplyProductImgRepository _weChatSupplyProductImg;

        public WeChatSupplierService(IBaseRepository<WeChatSupplier> baseRepository,
            IWeChatSupplyProductImgRepository weChatSupplyProductImg,
            IWeChatSupplierRepository chatSupplierRepository, 
            ISendMessage sendMessage,
            IMapper mapper, 
            IWeChatSDK weChat) : base(baseRepository)
        {
            _chatSupplierRepository =
                chatSupplierRepository ?? throw new ArgumentNullException(nameof(chatSupplierRepository));
            _sendMessage = sendMessage ?? throw new ArgumentNullException(nameof(sendMessage));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _weChat = weChat ?? throw new ArgumentNullException(nameof(weChat));
            _weChatSupplyProductImg = weChatSupplyProductImg;
        }

        /// <summary>
        /// 新增供应商
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> CreateSupplier(ApplySettledViewModel model)
        {

            //var imgs = await _weChatSupplyProductImg.Query();

            var supplier = _mapper.Map<ApplySettledViewModel,WeChatSupplier>(model);

            
            /*
            // 获取微信用户信息
            var user =  await _weChat.GetUserInfo(model.AccessToken, model.OpenId);
            if (user != null)
            {
                supplier.OpenId = user.openid;
                supplier.NickName = user.nickname;
                supplier.Sex = user.sex;
                supplier.Address = $"{user.country}/{user.province}/{user.city}";
                supplier.HeadImgUrl = user.headimgurl;
            }
            else
            {
                return -1;
            }
            */
            
            var sId = await _chatSupplierRepository.Add(supplier);

            if (sId > 0)
            {
                // send to 供应商
                await _sendMessage.Submitted(model.OpenId,supplier.RealName,supplier.MobilePhone);
                // send to 采购总监
                await _sendMessage.ToBeReviewed(Reviewer, supplier.RealName, supplier.MobilePhone);

                // 新增之后根据用户Id生成邀请码
                var i = await _chatSupplierRepository.QueryById(sId);
                i.InvitationCode = CodeHelper.Encode(i.Id);
                var state =await _chatSupplierRepository.Update(i);
                if (state) return sId;
            }
            return -1;
        }
    }
}