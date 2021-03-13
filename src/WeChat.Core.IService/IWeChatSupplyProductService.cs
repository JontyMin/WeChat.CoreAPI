﻿using System.Threading.Tasks;
using WeChat.Core.IService.BASE;
using WeChat.Core.Model.EntityModel;
using WeChat.Core.Model.ViewModel;

namespace WeChat.Core.IService
{
    public interface IWeChatSupplyProductService:IBaseService<WeChatSupplyProduct>
    {
        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> CreateProduct(SupplyProductViewModel model);

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateProduct(SupplyProductViewModel model);
    }
}