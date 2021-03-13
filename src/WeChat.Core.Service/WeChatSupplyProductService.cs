using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WeChat.Core.Common.HttpContextUser;
using WeChat.Core.IRepository;
using WeChat.Core.IRepository.BASE;
using WeChat.Core.IService;
using WeChat.Core.Model.EntityModel;
using WeChat.Core.Model.ViewModel;
using WeChat.Core.Service.BASE;

namespace WeChat.Core.Service
{
    /// <summary>
    /// 供应商产品服务
    /// </summary>
    public class WeChatSupplyProductService:BaseService<WeChatSupplyProduct>,IWeChatSupplyProductService
    {
        private readonly IWeChatSupplyProductRepository _weChatSupplyProductRepository;
        //private readonly ISupplyProductImgRepository _supplyProductImgRepository;
        private readonly IMapper _mapper;
        private readonly IUser _user;

        public WeChatSupplyProductService(IBaseRepository<WeChatSupplyProduct> baseRepository,
            IWeChatSupplyProductRepository weChatSupplyProductRepository,
            //ISupplyProductImgRepository supplyProductImgRepository,
            IMapper mapper,
            IUser user) : base(baseRepository)
        {
            _weChatSupplyProductRepository = weChatSupplyProductRepository;
            //_supplyProductImgRepository = supplyProductImgRepository;
            _mapper = mapper;
            _user = user;
        }

        /// <summary>
        /// 创建产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> CreateProduct(SupplyProductViewModel model)
        {
            // autoMapper映射
            var weChatSupplyProduct = _mapper.Map<SupplyProductViewModel, WeChatSupplyProduct>(model);
            weChatSupplyProduct.Sid = int.Parse(_user.Uid);

            // 添加实体
            var id = await _weChatSupplyProductRepository.Add(weChatSupplyProduct);

            return id > 0 ? id : -1;
        }

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateProduct(SupplyProductViewModel model)
        {
            var weChatSupplyProduct = _mapper.Map<SupplyProductViewModel, WeChatSupplyProduct>(model);
            weChatSupplyProduct.Sid= int.Parse(_user.Uid);
            return await _weChatSupplyProductRepository.Update(weChatSupplyProduct);
        }
    }
}