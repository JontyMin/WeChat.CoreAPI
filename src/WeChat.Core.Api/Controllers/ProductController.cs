using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using SqlSugar;
using WeChat.Core.Api.Log;
using WeChat.Core.Common.Cache;
using WeChat.Core.Common.HttpContextUser;
using WeChat.Core.IService;
using WeChat.Core.Model;
using WeChat.Core.Model.EntityModel;
using WeChat.Core.Model.ViewModel;
using WeChat.Core.Common.Redis;
using WeChat.Core.Model.EntityModel.ERP;
using WeChat.Core.IService.ERP;
using WeChat.Core.Model.ViewModel.ERP;

namespace WeChat.Core.Api.Controllers
{
    /// <summary>
    /// 供应商产品
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "Supplier")]
    public class ProductController : ControllerBase
    {
        private const string PRODUCT_CACHE_KEY = "product";
        private const string CATEGORY_CACHE_KEY = "Category";

        
        private readonly IWeChatSupplyProductService _weChatSupplyProductService;
        private readonly IWeChatSupplyProductImgService _supplyProductImgService;
        private readonly ICategoryFormEbayUKService _categoryFormEbayUkService;
        private readonly IRedisCacheManager _redisCache;
        private IMemoryCacheService _cacheService;
        private readonly IMapper _mapper;
        private ILoggerHelper _logger;
        private readonly IUser _user;
        private readonly IMemoryCache _cache;


        public ProductController(IWeChatSupplyProductService weChatSupplyProductService,
            IWeChatSupplyProductImgService supplyProductImgService,
            IMemoryCacheService cacheService,
            IMemoryCache cache,
            ILoggerHelper logger,
            IMapper mapper,
            IUser user,
            ICategoryFormEbayUKService categoryFormEbayUKService,
            IRedisCacheManager redisCache)
        {
            _weChatSupplyProductService = weChatSupplyProductService ?? throw new ArgumentNullException(nameof(weChatSupplyProductService));
            _supplyProductImgService = supplyProductImgService ?? throw new ArgumentNullException(nameof(supplyProductImgService));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _categoryFormEbayUkService = categoryFormEbayUKService ?? throw new ArgumentNullException(nameof(categoryFormEbayUKService));
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        /// <summary>
        /// 查询用户所有产品
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<SupplyProductViewModel>>> Get()
        {
            var response = new MessageModel<List<SupplyProductViewModel>> {status = 200, msg = "获取成功"};

            var products = _cache.Get<List<SupplyProductViewModel>>($"{_user.Uid}{PRODUCT_CACHE_KEY}");
            if (products == null)
            {
                //当前用户且未删除产品
                var data = await _weChatSupplyProductService.Query(x =>
                    x.Sid == _user.Uid.ObjToInt() && x.State != ProductState.Delete.ObjToInt());
                var productList = new List<SupplyProductViewModel>();
                foreach (var item in data)
                {
                    var imgs = await _supplyProductImgService.Query(x => x.PId == item.Id);
                    var supplyProduct = _mapper.Map<WeChatSupplyProduct, SupplyProductViewModel>(item);
                    supplyProduct.Images = _mapper.Map<List<ImageViewModel>>(imgs);
                    productList.Add(supplyProduct);
                }

                _cache.Set($"{_user.Uid}{PRODUCT_CACHE_KEY}", productList);

                response.success = productList.Count>=0;
                response.response = productList;
                return response;
            }

            response.success = products.Count > 0;
            response.response = products;
            return response;
        }

        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<MessageModel<SupplyProductViewModel>> Get(string id)
        {
            var data = await _weChatSupplyProductService.QueryById(id);
            var images = await _supplyProductImgService.Query(x => x.PId == data.Id);
            var supplyProduct = _mapper.Map<WeChatSupplyProduct, SupplyProductViewModel>(data);
            supplyProduct.Images = _mapper.Map<List<ImageViewModel>>(images);
            return new MessageModel<SupplyProductViewModel>()
            {
                msg = "获取成功",
                success = data != null,
                response = supplyProduct
            };
        }

        /// <summary>
        /// 根据名称查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("like={name}")]
        public async Task<MessageModel<List<SupplyProductViewModel>>> GetProductByName(string name)
        {
            var data = await _weChatSupplyProductService.Query(x =>
                (x.Cname.Contains(name) || x.Uname.Contains(name)) && x.Sid == _user.Uid.ObjToInt() && x.State != ProductState.Delete.ObjToInt());
            var products = new List<SupplyProductViewModel>();
            foreach (var item in data)
            {
                var imgs = await _supplyProductImgService.Query(x => x.PId == item.Id);
                var supplyProduct = _mapper.Map<WeChatSupplyProduct, SupplyProductViewModel>(item);
                supplyProduct.Images = _mapper.Map<List<ImageViewModel>>(imgs);
                products.Add(supplyProduct);
            }

            return new MessageModel<List<SupplyProductViewModel>>()
            {
                msg = "获取成功",
                success = products.Count >= 0,
                response = products
            };
        }

        /// <summary>
        /// 上架新产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Post([FromBody] SupplyProductViewModel model)
        {
            var data = new MessageModel<string>();

            // 返回创建产品Id
            var pId = await _weChatSupplyProductService.CreateProduct(model);

            if (model.Images.Any())
            {
                var images = _mapper.Map<List<WeChatSupplyProductImg>>(model.Images);
                images.ForEach(x =>
                {
                    x.PId = pId;
                });
                data.success = await _supplyProductImgService.Add(images) > 0;
                if (data.success)
                {
                    data.status = 200;
                    data.response = pId.ObjToString();
                    data.msg = "添加成功";
                    
                    _cache.Remove($"{_user.Uid}{PRODUCT_CACHE_KEY}");
                    
                    return data;
                }
            }

            return data;
        }

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> Put([FromBody] SupplyProductViewModel model)
        {
            var data = new MessageModel<string>();
            if (model != null && model.Id > 0)
            {

                data.success = await _weChatSupplyProductService.UpdateProduct(model);
                var imgs = _mapper.Map<List<WeChatSupplyProductImg>>(model.Images);
                // 更新图片链接 不存在则添加 图片状态
                imgs.ForEach(x =>
                {
                    x.PId = model.Id;
                });
                var addImgs = imgs.Where(x => x.Id == 0).ToList();
                if (addImgs.Any()) data.success = await _supplyProductImgService.Add(addImgs) > 0;
                if (data.success)
                {
                    _cache.Remove($"{_user.Uid}{PRODUCT_CACHE_KEY}");
                    data.msg = "产品更新成功";
                    data.response = model?.Id.ObjToString();
                }
            }
            return data;
        }


        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<MessageModel<string>> Delete(int id)
        {
            var data = new MessageModel<string>();
            if (id > 0)
            {
                var model = await _weChatSupplyProductService.QueryById(id);
                model.State = (int)ProductState.Delete;
                data.success = await _weChatSupplyProductService.Update(model);
                if (data.success)
                {
                    data.status = 200;
                    data.msg = "删除成功";
                    data.response = model?.Id.ObjToString();
                    _cache.Remove($"{_user.Uid}{PRODUCT_CACHE_KEY}");
                    return data;
                }
            }
            return data;
        }

        /// <summary>
        /// 产品上架
        /// </summary>
        /// <param name="id">产品Id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("PutSale/{id}")]
        public async Task<MessageModel<string>> PutSale(int id)
        {
            var data = new MessageModel<string>();
            if (id > 0)
            {
                var model = await _weChatSupplyProductService.QueryById(id);
                model.State = (int)ProductState.Active;
                data.success = await _weChatSupplyProductService.Update(model);
                if (data.success)
                {
                    data.status = 200;
                    data.msg = "上架成功";
                    data.response = model?.Id.ObjToString();
                    return data;
                }
            }
            return data;
        }

        /// <summary>
        /// 产品下架
        /// </summary>
        /// <param name="id">产品Id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("OffSale/{id}")]
        public async Task<MessageModel<string>> OffSale(int id)
        {
            var data = new MessageModel<string>();
            if (id > 0)
            {
                var model = await _weChatSupplyProductService.QueryById(id);
                model.State = (int)ProductState.Paused;
                data.success = await _weChatSupplyProductService.Update(model);
                if (data.success)
                {
                    data.status = 200;
                    data.msg = "下架成功";
                    data.response = model?.Id.ObjToString();
                    return data;
                }
            }
            return data;
        }

        /// <summary>
        /// 获取产品类目
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetCategoryTree))]
        public async Task<MessageModel<List<DTOTree>>> GetCategoryTree()
        {
            var categoryData = _redisCache.Get<List<DTOTree>>(CATEGORY_CACHE_KEY);
            if (categoryData != null)
            {
                return new MessageModel<List<DTOTree>>()
                {
                    status = 200,
                    success = true,
                    msg = "获取成功",
                    response = categoryData
                };
            }
            
            var data = new MessageModel<List<DTOTree>>();
            var categoryTreeAll = await _categoryFormEbayUkService.Query();
            List<DTOTree> categoryTree = GetCategoryTree(categoryTreeAll, 0);
            if (categoryTree.Any())
            {
                data.status = 200;
                data.msg = "获取成功";
                data.response = categoryTree;

                _redisCache.Set(CATEGORY_CACHE_KEY, categoryTree, TimeSpan.FromDays(30));

                return data;
            }

            data.status = 404;
            data.success = false;
            return data;

        }


        #region Private Method

        /// <summary>
        /// 递归树
        /// </summary>
        /// <param name="listDataSource"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<DTOTree> GetCategoryTree(List<CategoryFormEbayUK> listDataSource, int parentId)
        {
            var listNew = listDataSource.Where(d => d.ParentID == Convert.ToInt32(parentId)).ToList();
            var listResult = new List<DTOTree>();
            foreach (var item in listNew)
            {
                var tree = new DTOTree {value = item.CategoryID.ToString(), text = item.CNDescription};

                tree.children = GetTreeMod(listDataSource, tree.value);
                listResult.Add(tree);
            }
            return listResult;
        }
        /// <summary>
        /// 递归树
        /// </summary>
        /// <param name="listDataSource"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<DTOTree> GetTreeMod(List<CategoryFormEbayUK> listDataSource, string parentId)
        {
            
            return GetCategoryTree(listDataSource, Convert.ToInt32(parentId));
        }
        
        #endregion
    }
}
