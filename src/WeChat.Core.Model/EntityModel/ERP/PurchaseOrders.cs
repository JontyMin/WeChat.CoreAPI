using SqlSugar;

namespace WeChat.Core.Model.EntityModel.ERP
{
    public class PurchaseOrders
    {
        /// <summary>
        /// 采购订单信息
        /// </summary>
        public PurchaseOrders()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.String _Pno;
        /// <summary>
        /// 批次号
        /// </summary>
        public System.String Pno { get { return this._Pno; } set { this._Pno = value?.Trim(); } }

        private System.Int32 _PlatformNo;
        /// <summary>
        /// 平台编号
        /// </summary>
        public System.Int32 PlatformNo { get { return this._PlatformNo; } set { this._PlatformNo = value; } }

        private System.Int32 _ShopId;
        /// <summary>
        /// 店铺ID
        /// </summary>
        public System.Int32 ShopId { get { return this._ShopId; } set { this._ShopId = value; } }

        private System.String _OrderId;
        /// <summary>
        /// 订单号
        /// </summary>
        public System.String OrderId { get { return this._OrderId; } set { this._OrderId = value?.Trim(); } }

        private System.DateTime _OrderTime;
        /// <summary>
        /// 订单时间
        /// </summary>
        public System.DateTime OrderTime { get { return this._OrderTime; } set { this._OrderTime = value; } }

        private System.String _ItemId;
        /// <summary>
        /// 链接ID
        /// </summary>
        public System.String ItemId { get { return this._ItemId; } set { this._ItemId = value?.Trim(); } }

        private System.String _Sku;
        /// <summary>
        /// SKU
        /// </summary>
        public System.String Sku { get { return this._Sku; } set { this._Sku = value?.Trim(); } }

        private System.String _Poa;
        /// <summary>
        /// POA
        /// </summary>
        public System.String Poa { get { return this._Poa; } set { this._Poa = value?.Trim(); } }

        private System.Int32 _ProductId;
        /// <summary>
        /// 产品ID
        /// </summary>
        public System.Int32 ProductId { get { return this._ProductId; } set { this._ProductId = value; } }

        private System.Int32 _PropertyId;
        /// <summary>
        /// 属性ID
        /// </summary>
        public System.Int32 PropertyId { get { return this._PropertyId; } set { this._PropertyId = value; } }

        private System.Int32? _Quantity;
        /// <summary>
        /// 订单里面的产品数量
        /// </summary>
        public System.Int32? Quantity { get { return this._Quantity; } set { this._Quantity = value ?? default(System.Int32); } }

        private System.Decimal? _OrderPrice;
        /// <summary>
        /// 订单价格
        /// </summary>
        public System.Decimal? OrderPrice { get { return this._OrderPrice; } set { this._OrderPrice = value ?? default(System.Decimal); } }

        private System.Decimal? _OrderAmount;
        /// <summary>
        /// 订单金额
        /// </summary>
        public System.Decimal? OrderAmount { get { return this._OrderAmount; } set { this._OrderAmount = value ?? default(System.Decimal); } }

        private System.DateTime? _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? CreateTime { get { return this._CreateTime; } set { this._CreateTime = value ?? default(System.DateTime); } }

        private System.String _VariationId;
        /// <summary>
        /// 
        /// </summary>
        public System.String VariationId { get { return this._VariationId; } set { this._VariationId = value?.Trim(); } }

        private System.String _ItemUrl;
        /// <summary>
        /// 链接Url
        /// </summary>
        public System.String ItemUrl { get { return this._ItemUrl; } set { this._ItemUrl = value?.Trim(); } }

        private System.String _ItemImage;
        /// <summary>
        /// 链接图片
        /// </summary>
        public System.String ItemImage { get { return this._ItemImage; } set { this._ItemImage = value?.Trim(); } }

        private System.String _ShippingOption;
        /// <summary>
        /// 物流方式
        /// </summary>
        public System.String ShippingOption { get { return this._ShippingOption; } set { this._ShippingOption = value?.Trim(); } }

        private System.String _TrackingNo;
        /// <summary>
        /// 跟踪号
        /// </summary>
        public System.String TrackingNo { get { return this._TrackingNo; } set { this._TrackingNo = value?.Trim(); } }

        private System.Int32? _OrderDeliveryStatus;
        /// <summary>
        /// 订单发货状态(IsPurchase=1表示缺货状态 其他:待发货 = 1, 已经发货 = 2, 问题订单 = 3)
        /// </summary>
        public System.Int32? OrderDeliveryStatus { get { return this._OrderDeliveryStatus; } set { this._OrderDeliveryStatus = value ?? default(System.Int32); } }

        private System.Boolean? _IsPurchase;
        /// <summary>
        /// 是否需要采购
        /// </summary>
        public System.Boolean? IsPurchase { get { return this._IsPurchase; } set { this._IsPurchase = value ?? default(System.Boolean); } }

        private System.String _NoPurchaseReason;
        /// <summary>
        /// 不采购原因
        /// </summary>
        public System.String NoPurchaseReason { get { return this._NoPurchaseReason; } set { this._NoPurchaseReason = value?.Trim(); } }

        private System.String _OrderStatus;
        /// <summary>
        /// 订单状态
        /// </summary>
        public System.String OrderStatus { get { return this._OrderStatus; } set { this._OrderStatus = value?.Trim(); } }

        private System.Boolean? _IsPrint;
        /// <summary>
        /// 是否打印
        /// </summary>
        public System.Boolean? IsPrint { get { return this._IsPrint; } set { this._IsPrint = value ?? default(System.Boolean); } }

        private System.Boolean? _IsRts;
        /// <summary>
        /// 是否已经RTS
        /// </summary>
        public System.Boolean? IsRts { get { return this._IsRts; } set { this._IsRts = value ?? default(System.Boolean); } }

        private System.DateTime? _PrintTime;
        /// <summary>
        /// 打印时间
        /// </summary>
        public System.DateTime? PrintTime { get { return this._PrintTime; } set { this._PrintTime = value ?? default(System.DateTime); } }

        private System.Int32? _PlatformOrderId;
        /// <summary>
        /// 平台订单表ID
        /// </summary>
        public System.Int32? PlatformOrderId { get { return this._PlatformOrderId; } set { this._PlatformOrderId = value ?? default(System.Int32); } }

        private System.Boolean? _IsDeliveryScan;
        /// <summary>
        /// 是否发货扫描
        /// </summary>
        public System.Boolean? IsDeliveryScan { get { return this._IsDeliveryScan; } set { this._IsDeliveryScan = value ?? default(System.Boolean); } }

        private System.DateTime? _DeliveryScanTime;
        /// <summary>
        /// 发货扫描时间
        /// </summary>
        public System.DateTime? DeliveryScanTime { get { return this._DeliveryScanTime; } set { this._DeliveryScanTime = value ?? default(System.DateTime); } }

        private System.String _ChannelTrackingNo;
        /// <summary>
        /// 渠道跟踪号(特殊情况下保存临时跟踪号)
        /// </summary>
        public System.String ChannelTrackingNo { get { return this._ChannelTrackingNo; } set { this._ChannelTrackingNo = value?.Trim(); } }

        private System.Boolean? _IsClickFarm;
        /// <summary>
        /// 是否刷单
        /// </summary>
        public System.Boolean? IsClickFarm { get { return this._IsClickFarm; } set { this._IsClickFarm = value ?? default(System.Boolean); } }

        private System.String _Memo;
        /// <summary>
        /// 备注
        /// </summary>
        public System.String Memo { get { return this._Memo; } set { this._Memo = value?.Trim(); } }

        private System.String _LogisticsChannel;
        /// <summary>
        /// 物流渠道备注
        /// </summary>
        public System.String LogisticsChannel { get { return this._LogisticsChannel; } set { this._LogisticsChannel = value?.Trim(); } }

        private System.Int32? _PickStatus;
        /// <summary>
        /// 拣货状态(0:未拣货，1:已拣货)
        /// </summary>
        public System.Int32? PickStatus { get { return this._PickStatus; } set { this._PickStatus = value ?? default(System.Int32); } }

        private System.String _PickUserName;
        /// <summary>
        /// 拣货人
        /// </summary>
        public System.String PickUserName { get { return this._PickUserName; } set { this._PickUserName = value?.Trim(); } }

        private System.DateTime? _PickTime;
        /// <summary>
        /// 拣货时间
        /// </summary>
        public System.DateTime? PickTime { get { return this._PickTime; } set { this._PickTime = value ?? default(System.DateTime); } }

        private System.String _DeliveryScanName;
        /// <summary>
        /// 扫描人
        /// </summary>
        public System.String DeliveryScanName { get { return this._DeliveryScanName; } set { this._DeliveryScanName = value?.Trim(); } }

        private System.Decimal? _ScanWeight;
        /// <summary>
        /// 出库扫描重量(kg)
        /// </summary>
        public System.Decimal? ScanWeight { get { return this._ScanWeight; } set { this._ScanWeight = value ?? default(System.Decimal); } }

        private System.Boolean? _IsUnsalable;
        /// <summary>
        /// 是否滞销
        /// </summary>
        public System.Boolean? IsUnsalable { get { return this._IsUnsalable; } set { this._IsUnsalable = value ?? default(System.Boolean); } }

        private System.Int32? _UnsalableSubsidyNum;
        /// <summary>
        /// 滞销补贴数量
        /// </summary>
        public System.Int32? UnsalableSubsidyNum { get { return this._UnsalableSubsidyNum; } set { this._UnsalableSubsidyNum = value ?? default(System.Int32); } }

        private System.Decimal? _SubsidyCostPrice;
        /// <summary>
        /// 补贴成本价
        /// </summary>
        public System.Decimal? SubsidyCostPrice { get { return this._SubsidyCostPrice; } set { this._SubsidyCostPrice = value ?? default(System.Decimal); } }

        private System.Decimal? _UnsalableCostPrice;
        /// <summary>
        /// 滞销成本价
        /// </summary>
        public System.Decimal? UnsalableCostPrice { get { return this._UnsalableCostPrice; } set { this._UnsalableCostPrice = value ?? default(System.Decimal); } }

        private System.String _CancelTheReason;
        /// <summary>
        /// 取消原因
        /// </summary>
        public System.String CancelTheReason { get { return this._CancelTheReason; } set { this._CancelTheReason = value?.Trim(); } }

        private System.Boolean? _IsPersonalTab;
        /// <summary>
        /// 是否人为标记问题单
        /// </summary>
        public System.Boolean? IsPersonalTab { get { return this._IsPersonalTab; } set { this._IsPersonalTab = value ?? default(System.Boolean); } }

        private System.String _OrderType;
        /// <summary>
        /// 订单类型
        /// </summary>
        public System.String OrderType { get { return this._OrderType; } set { this._OrderType = value?.Trim(); } }

        private System.Boolean? _IsAgainDelivery;
        /// <summary>
        /// 是否重新发货(针对已经发货到分拣中心后丢件得需要重新发货)
        /// </summary>
        public System.Boolean? IsAgainDelivery { get { return this._IsAgainDelivery; } set { this._IsAgainDelivery = value ?? default(System.Boolean); } }

        private System.String _DocumentUrl;
        /// <summary>
        /// 
        /// </summary>
        public System.String DocumentUrl { get { return this._DocumentUrl; } set { this._DocumentUrl = value?.Trim(); } }

        private System.String _PrintUserName;
        /// <summary>
        /// 打印人
        /// </summary>
        public System.String PrintUserName { get { return this._PrintUserName; } set { this._PrintUserName = value?.Trim(); } }
    }
}