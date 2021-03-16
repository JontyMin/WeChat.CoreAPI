﻿#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeChat.Core.Common.Helper;
using WeChat.Core.Common.HttpContextUser;
using WeChat.Core.Common.Redis;
using WeChat.Core.Common.WeChat.Model;
using WeChat.Core.IService.ERP;
using WeChat.Core.Model;
using WeChat.Core.Model.EntityModel.ERP;
using WeChat.Core.Model.Enums;
using WeChat.Core.Model.ViewModel.ERP;

namespace WeChat.Core.Api.Controllers
{

    /// <summary>
    /// 仓储Uni-App API
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "UniApp")]
    public class DepotController : ControllerBase
    {
        private const string PLATCACHE_KEY = "PlatFormInfo";
        
        private readonly string AccessKey = AppSettings.app(new string[] {"AppSettings", "Express", "AccessKey" });
        private readonly string SecretKey = AppSettings.app(new string[] {"AppSettings", "Express", "SecretKey" });


        private readonly IStockOperateRecordService _stockOperateRecordService;
        private readonly IScanningCheckTmpService _scanningCheckTmpService;
        private readonly IPurchaseProductsService _purchaseProductsService;
        private readonly ISuperBuyerOrdersService _superBuyerOrdersService;
        private readonly IPurchaseOrdersService _purchaseOrdersService;
        private readonly IPlatformInfoService _platformInfoService;
        private readonly IRedisCacheManager _redisCache;
        private readonly IUserService _userService;
        private readonly IUser _user;

        public DepotController(IPurchaseOrdersService purchaseOrdersService,
            IStockOperateRecordService stockOperateRecordService,
            IPurchaseProductsService purchaseProductsService,
            IScanningCheckTmpService scanningCheckTmpService,
            ISuperBuyerOrdersService superBuyerOrdersService,
            IPlatformInfoService platformInfoService,
            IRedisCacheManager redisCache,
            IUserService userService,
            IUser user)
        {
            _purchaseOrdersService =
                purchaseOrdersService ?? throw new ArgumentNullException(nameof(purchaseOrdersService));
            _stockOperateRecordService = stockOperateRecordService ??
                                         throw new ArgumentNullException(nameof(stockOperateRecordService));
            _scanningCheckTmpService =
                scanningCheckTmpService ?? throw new ArgumentException(nameof(scanningCheckTmpService));
            _purchaseProductsService =
                purchaseProductsService ?? throw new ArgumentException(nameof(purchaseProductsService));
            _superBuyerOrdersService =
                superBuyerOrdersService ?? throw new ArgumentException(nameof(superBuyerOrdersService));
            _platformInfoService = platformInfoService ?? throw new ArgumentException(nameof(platformInfoService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _redisCache = redisCache ?? throw new ArgumentException(nameof(redisCache));
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }



        /// <summary>
        /// 查询订单扫描信息
        /// </summary>
        /// <param name="trackNo"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{trackNo}")]
        public async Task<MessageModel<PurchaseOrderViewModel>> Get(string trackNo)
        {
            // 统计
            var data = new MessageModel<PurchaseOrderViewModel>();

            if (string.IsNullOrEmpty(trackNo))
            {
                data.msg = "跟踪号不能为空";
                return data;
            }

            // 此跟踪号下的订单
            var purchaseOrder = await _purchaseOrdersService.Query(x => x.TrackingNo == trackNo);

            if (purchaseOrder.Any())
            {
                
                if (purchaseOrder.Any(x => x.IsDeliveryScan == true))
                {
                    data.msg = "订单已扫描";
                }

                
                if (purchaseOrder.Any(x => x.IsPurchase == false && x.OrderDeliveryStatus == 1))
                {
                    data.msg = "订单为待发货状态，请打印面单";
                    return data;
                }

                if (purchaseOrder.Any(x=>x.OrderDeliveryStatus==3))
                {
                    data.msg = "问题订单提示不发货";
                    return data;
                }

                //var orderIds = purchaseOrder.Select(t => t.OrderId).ToArray();
                //var purchaseQuestion = await _purchaseQuestionService.Query(x => orderIds.Contains(x.OrderId));
                //if (purchaseQuestion.Any())
                //{
                //    data.msg = "问题记录提示订单不发货";
                //    return data;
                //}

                var purchase = purchaseOrder.First();
                var purchaseOrderView = new PurchaseOrderViewModel()
                {
                    TrackingNo = purchase.TrackingNo,
                    PlatformName = ((ErpPlatform)purchase.PlatformNo).ToString(),
                    Num = purchaseOrder.Count,
                    ScanWeight = purchase.ScanWeight is null ? "0" : purchase.ScanWeight.ToString(),
                    IsDeliveryScan = purchase.IsDeliveryScan == true ? "已扫描" : "未扫描",
                    DeliveryScanName = purchase.DeliveryScanName ?? "-",
                    DeliveryScanTime = purchase.DeliveryScanTime is null
                        ? ""
                        : Convert.ToDateTime(purchase.DeliveryScanTime).ToString("yyyy/MM/dd HH:mm"),
                    OrderId = purchase.OrderId,
                    OrderTime = purchase.OrderTime.ToString("yyyy/MM/dd HH:mm"),
                    PrintTime = purchase.PrintTime is null
                        ? ""
                        : Convert.ToDateTime(purchase.PrintTime).ToString("yyyy/MM/dd HH:mm")
                };

                data.msg = "获取成功";
                data.status = 200;
                data.success = true;
                data.response = purchaseOrderView;
                return data;
            }

            data.msg = "未找到任何一条关于该跟踪号的数据";
            return data;
        }


        /// <summary>
        /// 查询订单扫描信息
        /// </summary>
        /// <param name="trackNo"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{trackNo}/{platform}")]
        public async Task<MessageModel<PurchaseOrderViewModel>> Get(string trackNo, string platform)
        {
            // 统计
            var data = new MessageModel<PurchaseOrderViewModel>();

            if (string.IsNullOrEmpty(trackNo))
            {
                data.msg = "跟踪号不能为空";
                return data;
            }

            // 此跟踪号下的订单
            var purchaseOrder = await _purchaseOrdersService.Query(x => x.TrackingNo == trackNo);

            if (!string.IsNullOrEmpty(platform))
            {


                var platInfo = _redisCache.Get<List<PlatformInfos>>(PLATCACHE_KEY);
                if (platInfo is null)
                {
                    platInfo = await _platformInfoService.Query();
                    _redisCache.Set(PLATCACHE_KEY, platInfo, TimeSpan.MaxValue);
                }

                var platformNo = platInfo.Find(x => x.PlatformName == platform.Trim())!.PlatformNo;
                if (purchaseOrder.All(x => x.PlatformNo != platformNo))
                {
                    data.status = 404;
                    data.msg = $"跟踪号不属于平台{platform}";
                    return data;
                }
            }

            if (purchaseOrder.Any())
            {

                if (purchaseOrder.Any(x => x.IsDeliveryScan == true))
                {
                    data.msg = "订单已扫描";
                }


                if (purchaseOrder.Any(x => x.IsPurchase == false && x.OrderDeliveryStatus == 1))
                {
                    data.msg = "订单为待发货状态，请打印面单";
                    return data;
                }

                if (purchaseOrder.Any(x => x.OrderDeliveryStatus == 3))
                {
                    data.msg = "问题订单提示不发货";
                    return data;
                }

                //var orderIds = purchaseOrder.Select(t => t.OrderId).ToArray();
                //var purchaseQuestion = await _purchaseQuestionService.Query(x => orderIds.Contains(x.OrderId));
                //if (purchaseQuestion.Any())
                //{
                //    data.msg = "问题记录提示订单不发货";
                //    return data;
                //}

                var purchase = purchaseOrder.First();
                var purchaseOrderView = new PurchaseOrderViewModel()
                {
                    TrackingNo = purchase.TrackingNo,
                    PlatformName = ((ErpPlatform)purchase.PlatformNo).ToString(),
                    Num = purchaseOrder.Count,
                    ScanWeight = purchase.ScanWeight is null ? "0" : purchase.ScanWeight.ToString(),
                    IsDeliveryScan = purchase.IsDeliveryScan == true ? "已扫描" : "未扫描",
                    DeliveryScanName = purchase.DeliveryScanName ?? "-",
                    DeliveryScanTime = purchase.DeliveryScanTime is null
                        ? ""
                        : Convert.ToDateTime(purchase.DeliveryScanTime).ToString("yyyy/MM/dd HH:mm"),
                    OrderId = purchase.OrderId,
                    OrderTime = purchase.OrderTime.ToString("yyyy/MM/dd HH:mm"),
                    PrintTime = purchase.PrintTime is null
                        ? ""
                        : Convert.ToDateTime(purchase.PrintTime).ToString("yyyy/MM/dd HH:mm")
                };

                data.msg = "获取成功";
                data.status = 200;
                data.success = true;
                data.response = purchaseOrderView;
                return data;
            }
            data.msg = "未找到任何一条关于该跟踪号的数据";
            return data;
        }

        /// <summary>
        /// 出库扫描称重
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<MessageModel<PurchaseOrderViewModel>> Post(ScanWeightViewModel model)
        {
            var data = new MessageModel<PurchaseOrderViewModel>();

            if (string.IsNullOrEmpty(model.Weight)) return data;
            var user =await _userService.QueryById(_user.Uid);

            if (user==null)
            {
                data.msg = "获取用户信息失败";
                data.status = 401;
                return data;
            }
            decimal.TryParse(model.Weight.Replace("kg", "").Trim(), out var weight);

            var purchaseOrder = await _purchaseOrdersService.Query(x=>x.TrackingNo==model.TrackNo);
            
            if (purchaseOrder.Any())
            {

                purchaseOrder.ForEach(x =>
                {
                    x.DeliveryScanName = user.TrueName;
                    x.DeliveryScanTime=DateTime.Now;
                    x.ScanWeight = weight;
                    x.IsDeliveryScan = true;
                });
                
                // 修改跟踪号下所有订单
                var count = await _purchaseOrdersService.Update(purchaseOrder);
                
                if (count>0)
                {
                    var purchase = purchaseOrder.First();

                    // 添加操作记录
                    var stockOperateRecord = new StockOperateRecord()
                    {
                        Pno = purchase.Pno,
                        OrderId = purchase.OrderId,
                        TrackingNo = purchase.TrackingNo,
                        Sku = purchase.Sku,
                        Poa = purchase.Poa,
                        OperateUser = user.TrueName,
                        OperateTime = DateTime.Now,
                        OperateType = 4, // 出库扫描
                        OperateTable = "PurchaseOrders",
                        OperateTableId = purchase.Id,
                        OperateMenu = "出库扫描",
                        Memo = "uni-app操作记录"
                    };
                    await _stockOperateRecordService.Add(stockOperateRecord);

                    
                    var purchaseOrderView = new PurchaseOrderViewModel()
                    {
                        TrackingNo = purchase.TrackingNo,
                        PlatformName = ((ErpPlatform)purchase.PlatformNo).ToString(),
                        Num = purchaseOrder.Count,
                        ScanWeight = model.Weight,
                        IsDeliveryScan = "已扫描",
                        DeliveryScanName = user.TrueName,
                        DeliveryScanTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm"),
                        OrderId = purchase.OrderId,
                        OrderTime = purchase.OrderTime.ToString("yyyy/MM/dd HH:mm"),
                        PrintTime = purchase.PrintTime is null
                            ? ""
                            : Convert.ToDateTime(purchase.PrintTime).ToString("yyyy/MM/dd HH:mm")
                    };
                    data.msg = "扫描完成";
                    data.status = 200;
                    data.success = true;
                    data.response = purchaseOrderView;
                    return data;
                }
            }

            return data;
        }

        
        /// <summary>
        /// 加载请求
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<string>> Get(string appid,string sign,string nonce,string timespan)
        {
            Console.WriteLine($"Sign:{sign},nonce:{nonce},timespan:{timespan}");
            
            var data = new MessageModel<string>() { status = 400, msg = "错误请求" };

            if (appid!= AccessKey)
            {
                return data;
            }

            if (SignCheck.ValidateSignature(sign, timespan, nonce, SecretKey))
            {
                //await _scanningCheckTmpService.Deleteable(x => x.Id != 0);
                var scanningCheckTmps = await _scanningCheckTmpService.Query();

                data.status = 200;
                data.success = true;
                data.msg = "已清空";
                return data;
            }

            return data;
        }

        /// <summary>
        /// 跟踪号请求
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="sign"></param>
        /// <param name="nonce"></param>
        /// <param name="timespan"></param>
        /// <param name="trackNo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetScan))]
        public async Task<MessageModel<ScanStatus>> GetScan(string appid, string sign, string nonce, string timespan,
            string trackNo)
        {

            var data = new MessageModel<ScanStatus> {status = 400, msg = "错误请求"};
            if (appid != AccessKey)
            {
                return data;
            }

            if (SignCheck.ValidateSignature(sign, timespan, nonce, SecretKey))
            {
                var scanStatus = new ScanStatus();
                var purchaseProductsList =await _purchaseProductsService.Query(x => x.PurchaseTrackNo.Contains(trackNo));
                scanStatus.PurchaseNo =
                    purchaseProductsList.Any() ? purchaseProductsList.FirstOrDefault()?.PurchaseNo : "";
                scanStatus.Verify = purchaseProductsList.Any();
                if (scanStatus.Verify)
                {
                    //await _superBuyerOrdersService.
                    //scanStatus.LogisticsCompanyName =
                }
                
                   
                data.status = 200;
                data.success = true;
                data.response = scanStatus;
                data.msg = "获取成功";
                return data;
            }

            return data;
        }

        
        
        #region Private Method
        
        #endregion
    }
}
