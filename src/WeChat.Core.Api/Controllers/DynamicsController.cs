using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeChat.Core.Common.Redis;
using WeChat.Core.IService.ERP;
using WeChat.Core.Model;
using WeChat.Core.Model.EntityModel.ERP;
using WeChat.Core.Model.ViewModel.ERP;

namespace WeChat.Core.Api.Controllers
{
    /// <summary>
    /// 微信公众号 动态订单和销量报表
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicsController : ControllerBase
    {

        private const string CACHE_KEY_ORDERS = "DynamicsOrdersData";

        private const string CACHE_KEY_SALES = "DynamicsSalesData";

        private readonly IVW_OrderVolume1Service _vwOrderVolume1Service;
        private readonly IVW_OrderVolume2Service _vwOrderVolume2Service;
        private readonly IVW_OrderVolume3Service _vwOrderVolume3Service;

        private readonly IVW_SaleVolume1Service _vwSaleVolume1Service;
        private readonly IVW_SaleVolume2Service _vwSaleVolume2Service;
        private readonly IVW_SaleVolume3Service _vwSaleVolume3Service;

        private readonly IRedisCacheManager _redisCache;


        public DynamicsController(IVW_OrderVolume1Service vwOrderVolume1Service,
            IVW_OrderVolume2Service vwOrderVolume2Service,
            IVW_OrderVolume3Service vwOrderVolume3Service,
            IVW_SaleVolume1Service vwSaleVolume1Service,
            IVW_SaleVolume2Service vwSaleVolume2Service,
            IVW_SaleVolume3Service vwSaleVolume3Service,
               IRedisCacheManager redisCache)
        {
            _vwOrderVolume1Service =
                vwOrderVolume1Service ?? throw new ArgumentNullException(nameof(vwOrderVolume1Service));
            _vwOrderVolume2Service =
               vwOrderVolume2Service ?? throw new ArgumentNullException(nameof(vwOrderVolume2Service));
            _vwOrderVolume3Service =
               vwOrderVolume3Service ?? throw new ArgumentNullException(nameof(vwOrderVolume3Service));
            _vwSaleVolume1Service =
                vwSaleVolume1Service ?? throw new ArgumentNullException(nameof(vwSaleVolume1Service));
            _vwSaleVolume2Service =
               vwSaleVolume2Service ?? throw new ArgumentNullException(nameof(vwSaleVolume2Service));
            _vwSaleVolume3Service =
               vwSaleVolume3Service ?? throw new ArgumentNullException(nameof(vwSaleVolume3Service));
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        /// <summary>
        /// 查询动态订单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetOrderDynamics))]
        public async Task<MessageModel<OrderDynamics>> GetOrderDynamics()
        {
            var orderData=_redisCache.Get<OrderDynamics>(CACHE_KEY_ORDERS);
            if (orderData != null)
            {
                return new MessageModel<OrderDynamics>() { 
                status=200,
                success=true,
                msg= "获取成功",
                response=orderData};
            }

            var data = new MessageModel<OrderDynamics>();
            var orderVolume1 = await _vwOrderVolume1Service.Query();
            var orderVolume2 = await _vwOrderVolume2Service.Query();
            var orderVolume3 = await _vwOrderVolume3Service.Query();

            List<PlatOrdersModel> plist = new List<PlatOrdersModel>();
            foreach (var item in orderVolume2.Select(x=>x.Plat).Distinct())
            {
                PlatOrdersModel p = new PlatOrdersModel();
                int todayorders = orderVolume2.First(x=>x.dtype == "today" && x.Plat==item).orders;
                int yesderdayorders = orderVolume2.First(x => x.dtype == "yesderday" && x.Plat == item).orders;
                int thisyearorders = orderVolume2.First(x => x.dtype == "thisyear" && x.Plat == item).orders;
                p.platname = item;
                p.todayorders = todayorders;
                p.yesderdayorders = yesderdayorders;
                p.thisyearorders = thisyearorders;
                plist.Add(p);
            }

            string[] montharrcategory = orderVolume3.Where(x => x.dtype == "month").OrderBy(x=>x.datet).Select(x => x.datet.Substring(2)).ToArray();
            int[] montharrorders = orderVolume3.Where(x => x.dtype == "month").OrderBy(x => x.datet).Select(x => x.orders).ToArray();

            string[] dayarrcategory = orderVolume3.Where(x => x.dtype == "day").OrderBy(x => x.datet).Select(x => x.datet.Substring(2)).ToArray();
            int[] dayarrorders = orderVolume3.Where(x => x.dtype == "day").OrderBy(x => x.datet).Select(x => x.orders).ToArray();

            var orderDynamics = new OrderDynamics() {
                v1 = orderVolume1.First(),
                v2 = plist,
                montharrcategory = montharrcategory,
                montharrorders = montharrorders,
                dayarrcategory = dayarrcategory,
                dayarrorders = dayarrorders,
                GetNowTime = DateTime.Now
            };

            _redisCache.Set(CACHE_KEY_ORDERS, orderDynamics, TimeSpan.FromMinutes(10));

            data.msg = "获取成功";
            data.status = 200;
            data.success = true;
            data.response = orderDynamics;
            return data;
        }

        /// <summary>
        /// 查询动态销量信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetSalesDynamics))]
        public async Task<MessageModel<SalesDynamics>> GetSalesDynamics()
        {
            var saleData = _redisCache.Get<SalesDynamics>(CACHE_KEY_SALES);
            if (saleData != null)
            {
                return new MessageModel<SalesDynamics>()
                {
                    status = 200,
                    success = true,
                    msg = "获取成功",
                    response = saleData
                };
            }
            var data = new MessageModel<SalesDynamics>();



            var saleVolume1 = await _vwSaleVolume1Service.Query();
            var saleVolume2 = await _vwSaleVolume2Service.Query();
            var saleVolume3 = await _vwSaleVolume3Service.Query();
            var orderVolume3 = await _vwOrderVolume3Service.Query();

            string[] montharrcategory = orderVolume3.Where(x => x.dtype == "month").OrderBy(x => x.datet).Select(x => x.datet.Substring(2)).ToArray();
            int[] montharrorders = orderVolume3.Where(x => x.dtype == "month").OrderBy(x => x.datet).Select(x => x.orders).ToArray();

            string[] dayarrcategory = orderVolume3.Where(x => x.dtype == "day").OrderBy(x => x.datet).Select(x => x.datet.Substring(2)).ToArray();
            int[] dayarrorders = orderVolume3.Where(x => x.dtype == "day").OrderBy(x => x.datet).Select(x => x.orders).ToArray();

            decimal [] montharrsales = saleVolume2.Where(x => x.dtype == "month").OrderBy(x => x.Dates).Select(x => x.SaleAmount).ToArray();
            decimal[] dayarrsales = saleVolume2.Where(x => x.dtype == "day").OrderBy(x => x.Dates).Select(x => x.SaleAmount).ToArray();

            List<PlatSaleModel> plist = new List<PlatSaleModel>();
            foreach (var item in saleVolume3.Select(x => x.PlatformName).Distinct())
            {
                PlatSaleModel p = new PlatSaleModel();
                decimal monthsales = saleVolume3.First(x => x.datet == "month" && x.PlatformName == item).SaleAmount;
                decimal todayorders = saleVolume3.First(x => x.datet == "day" && x.PlatformName == item && x.Sort==1).SaleAmount;
                decimal yesderdayorders = 0;
                var d = saleVolume3.FirstOrDefault(x => x.datet == "day" && x.PlatformName == item && x.Sort == 2);
                if (d != null)
                {
                     yesderdayorders = saleVolume3.First(x => x.datet == "day" && x.PlatformName == item && x.Sort == 2).SaleAmount;
                }
                p.platname = item;
                p.todaysales = todayorders;
                p.yesderdaysales = yesderdayorders;
                p.monthsales = monthsales;
                plist.Add(p);
            }

            var saleDynamics = new SalesDynamics()
            {
                f1 = saleVolume1.First(),
                montharrcategory= montharrcategory,
                dayarrcategory= dayarrcategory,
                montharrorders= montharrorders,
                dayarrorders= dayarrorders,
                montharrsales= montharrsales,
                dayarrsales= dayarrsales,
                f3 = plist,
                GetNowTime = DateTime.Now
            };

            _redisCache.Set(CACHE_KEY_SALES, saleDynamics, TimeSpan.FromMinutes(10));

            data.msg = "获取成功";
            data.status = 200;
            data.success = true;
            data.response = saleDynamics;
            return data;
        }

    }
}
