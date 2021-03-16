using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeChat.Core.Api.Log;
using WeChat.Core.Common.Redis;
using WeChat.Core.IService.ERP;
using WeChat.Core.Model.ViewModel.ERP;

namespace WeChat.Core.Api.Jobs
{
    public class DynamicsDataJob:IJob
    {
        private const string CACHE_KEY_ORDERS = "DynamicsOrdersData";

        private const string CACHE_KEY_SALES = "DynamicsSalesData";

        private readonly IVW_OrderVolume1Service _vwOrderVolume1Service;
        private readonly IVW_OrderVolume2Service _vwOrderVolume2Service;
        private readonly IVW_OrderVolume3Service _vwOrderVolume3Service;

        private readonly IVW_SaleVolume1Service _vwSaleVolume1Service;
        private readonly IVW_SaleVolume2Service _vwSaleVolume2Service;
        private readonly IVW_SaleVolume3Service _vwSaleVolume3Service;

        private readonly ILoggerHelper _logger;
        private readonly IRedisCacheManager _redisCache;


        public DynamicsDataJob(IVW_OrderVolume1Service vwOrderVolume1Service,
            IVW_OrderVolume2Service vwOrderVolume2Service,
            IVW_OrderVolume3Service vwOrderVolume3Service,
            IVW_SaleVolume1Service vwSaleVolume1Service,
            IVW_SaleVolume2Service vwSaleVolume2Service,
            IVW_SaleVolume3Service vwSaleVolume3Service,
            IRedisCacheManager redisCache,
            ILoggerHelper logger)
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
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            
            
            var orderVolume1 = await _vwOrderVolume1Service.Query();
            var orderVolume2 = await _vwOrderVolume2Service.Query();
            var orderVolume3 = await _vwOrderVolume3Service.Query();


            List<PlatOrdersModel> plist = new List<PlatOrdersModel>();
            foreach (var item in orderVolume2.Select(x => x.Plat).Distinct())
            {
                PlatOrdersModel p = new PlatOrdersModel();
                int todayorders = orderVolume2.First(x => x.dtype == "today" && x.Plat == item).orders;
                int yesderdayorders = orderVolume2.First(x => x.dtype == "yesderday" && x.Plat == item).orders;
                int thisyearorders = orderVolume2.First(x => x.dtype == "thisyear" && x.Plat == item).orders;
                p.platname = item;
                p.todayorders = todayorders;
                p.yesderdayorders = yesderdayorders;
                p.thisyearorders = thisyearorders;
                plist.Add(p);
            }

            string[] montharrcategory = orderVolume3.Where(x => x.dtype == "month").OrderBy(x => x.datet).Select(x => x.datet.Substring(2)).ToArray();
            int[] montharrorders = orderVolume3.Where(x => x.dtype == "month").OrderBy(x => x.datet).Select(x => x.orders).ToArray();

            string[] dayarrcategory = orderVolume3.Where(x => x.dtype == "day").OrderBy(x => x.datet).Select(x => x.datet.Substring(2)).ToArray();
            int[] dayarrorders = orderVolume3.Where(x => x.dtype == "day").OrderBy(x => x.datet).Select(x => x.orders).ToArray();

            var orderDynamics = new OrderDynamics()
            {
                v1 = orderVolume1.First(),
                v2 = plist,
                montharrcategory = montharrcategory,
                montharrorders = montharrorders,
                dayarrcategory = dayarrcategory,
                dayarrorders = dayarrorders,
                GetNowTime = DateTime.Now
            };

            _redisCache.Set(CACHE_KEY_ORDERS, orderDynamics, TimeSpan.FromMinutes(10));
            _logger.Debug(nameof(DynamicsDataJob), $"更新动态订单:{DateTime.Now}");

            
            
            var saleVolume1 = await _vwSaleVolume1Service.Query();
            var saleVolume2 = await _vwSaleVolume2Service.Query();
            var saleVolume3 = await _vwSaleVolume3Service.Query();
         

            decimal[] montharrsales = saleVolume2.Where(x => x.dtype == "month").OrderBy(x => x.Dates).Select(x => x.SaleAmount).ToArray();
            decimal[] dayarrsales = saleVolume2.Where(x => x.dtype == "day").OrderBy(x => x.Dates).Select(x => x.SaleAmount).ToArray();

            List<PlatSaleModel> plist2 = new List<PlatSaleModel>();
            foreach (var item in saleVolume3.Select(x => x.PlatformName).Distinct())
            {
                PlatSaleModel p = new PlatSaleModel();
                decimal monthsales = 0;
                var m = saleVolume3.FirstOrDefault(x => x.datet == "month" && x.PlatformName == item);
                if (m != null)
                {
                    monthsales = m.SaleAmount;
                }

                string daystring = DateTime.Now.ToString("yyyy-MM-dd");
                decimal todayorders = 0;
                var t = saleVolume3.FirstOrDefault(x => x.datet == "day" && x.PlatformName == item && x.Dates == daystring);
                if (t != null)
                {
                    todayorders = t.SaleAmount;
                }

                decimal yesderdayorders = 0;
                string yesderdaystring = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                var d = saleVolume3.FirstOrDefault(x => x.datet == "day" && x.PlatformName == item && x.Dates == yesderdaystring);
                if (d != null)
                {
                    yesderdayorders = d.SaleAmount;
                }
                p.platname = item;
                p.todaysales = todayorders;
                p.yesderdaysales = yesderdayorders;
                p.monthsales = monthsales;
                plist2.Add(p);
            }

            var saleDynamics = new SalesDynamics
            {
                f1 = saleVolume1.First(),
                montharrcategory = montharrcategory,
                dayarrcategory = dayarrcategory,
                montharrorders = montharrorders,
                dayarrorders = dayarrorders,
                montharrsales = montharrsales,
                dayarrsales = dayarrsales,
                f3 = plist2,
                GetNowTime=DateTime.Now
            };

            _redisCache.Set(CACHE_KEY_SALES, saleDynamics, TimeSpan.FromMinutes(10));
            _logger.Debug(nameof(DynamicsDataJob), $"更新动态销售额:{DateTime.Now}");
        }
    }
}
