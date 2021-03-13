using AutoMapper;
using WeChat.Core.Model.EntityModel;
using WeChat.Core.Model.EntityModel.ERP;
using WeChat.Core.Model.ViewModel;
using WeChat.Core.Model.ViewModel.ERP;

namespace WeChat.Core.Api.AutoMapper
{
    public class CustomProfile:Profile
    {
        /// <summary>
        /// 配置构造函数，用于创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<WeChatSupplier, ApplySettledViewModel>();
            CreateMap<ApplySettledViewModel, WeChatSupplier>();
            
            CreateMap<SupplyProductViewModel, WeChatSupplyProduct>();
            CreateMap<WeChatSupplyProduct, SupplyProductViewModel>();

            CreateMap<ImageViewModel, WeChatSupplyProductImg>();
            CreateMap<WeChatSupplyProductImg, ImageViewModel>();

            CreateMap<PurchaseOrderViewModel, PurchaseOrders>();
            CreateMap<PurchaseOrders, PurchaseOrderViewModel>();

        }
    }
}