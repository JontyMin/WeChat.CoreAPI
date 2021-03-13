using WeChat.Core.IRepository.BASE;
using WeChat.Core.IService;
using WeChat.Core.Model.EntityModel;
using WeChat.Core.Service.BASE;

namespace WeChat.Core.Service
{
    /// <summary>
    /// 供应商产品图业务类
    /// </summary>
    public class WeChatSupplyProductImgService:BaseService<WeChatSupplyProductImg>,IWeChatSupplyProductImgService
    {
        public WeChatSupplyProductImgService(IBaseRepository<WeChatSupplyProductImg> baseRepository) : base(baseRepository)
        {
        }
    }
}