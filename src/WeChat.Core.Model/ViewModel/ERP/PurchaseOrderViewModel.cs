using System;

namespace WeChat.Core.Model.ViewModel.ERP
{
    
    public class PurchaseOrderViewModel
    {
       

        /// <summary>
        /// 平台
        /// </summary>
        public string PlatformName { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 订单下单时间
        /// </summary>
        public string OrderTime { get; set; }

        /// <summary>
        /// 打印时间
        /// </summary>
        public string PrintTime { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 跟踪号
        /// </summary>
        public string TrackingNo { get; set; }

        /// <summary>
        /// 是否出库扫描
        /// </summary>
        public string IsDeliveryScan { get; set; }

        /// <summary>
        /// 扫描人
        /// </summary>
        public string DeliveryScanName { get; set; }

        /// <summary>
        /// 扫描重量
        /// </summary>
        public string ScanWeight { get; set; }

        /// <summary>
        /// 扫描时间
        /// </summary>
        public string DeliveryScanTime { get; set; }
    }
}