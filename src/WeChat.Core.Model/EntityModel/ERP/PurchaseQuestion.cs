using SqlSugar;

namespace WeChat.Core.Model.EntityModel.ERP
{
    /// <summary>
    /// 采购问题记录
    /// </summary>
    public class PurchaseQuestion
    {
        /// <summary>
        /// 
        /// </summary>
        public PurchaseQuestion()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.Int32? _PurchaseProductId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? PurchaseProductId { get { return this._PurchaseProductId; } set { this._PurchaseProductId = value ?? default(System.Int32); } }

        private System.Int32? _PurchaseOrderId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? PurchaseOrderId { get { return this._PurchaseOrderId; } set { this._PurchaseOrderId = value ?? default(System.Int32); } }

        private System.Int32? _PlatformNo;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? PlatformNo { get { return this._PlatformNo; } set { this._PlatformNo = value ?? default(System.Int32); } }

        private System.Int32? _ShopId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? ShopId { get { return this._ShopId; } set { this._ShopId = value ?? default(System.Int32); } }

        private System.String _SKU;
        /// <summary>
        /// 
        /// </summary>
        public System.String SKU { get { return this._SKU; } set { this._SKU = value?.Trim(); } }

        private System.String _POA;
        /// <summary>
        /// 
        /// </summary>
        public System.String POA { get { return this._POA; } set { this._POA = value?.Trim(); } }

        private System.String _OrderId;
        /// <summary>
        /// 订单号
        /// </summary>
        public System.String OrderId { get { return this._OrderId; } set { this._OrderId = value?.Trim(); } }

        private System.String _SellerSKU;
        /// <summary>
        /// 标签
        /// </summary>
        public System.String SellerSKU { get { return this._SellerSKU; } set { this._SellerSKU = value?.Trim(); } }

        private System.String _ItemUrl;
        /// <summary>
        /// 标签链接
        /// </summary>
        public System.String ItemUrl { get { return this._ItemUrl; } set { this._ItemUrl = value?.Trim(); } }

        private System.String _ItemImage;
        /// <summary>
        /// 
        /// </summary>
        public System.String ItemImage { get { return this._ItemImage; } set { this._ItemImage = value?.Trim(); } }

        private System.Int32? _ItemsCount;
        /// <summary>
        /// 出单数量
        /// </summary>
        public System.Int32? ItemsCount { get { return this._ItemsCount; } set { this._ItemsCount = value ?? default(System.Int32); } }

        private System.String _QuestionType;
        /// <summary>
        /// 问题类型：无同款、缺货、停产、低利润、亏本、违禁品、重复问题
        /// </summary>
        public System.String QuestionType { get { return this._QuestionType; } set { this._QuestionType = value?.Trim(); } }

        private System.String _Advise;
        /// <summary>
        /// 需求：下架、改价、修改主图继续卖、关联错误、修改技术指标、修改主图详情
        /// </summary>
        public System.String Advise { get { return this._Advise; } set { this._Advise = value?.Trim(); } }

        private System.Int32? _Result;
        /// <summary>
        /// 采购处理结果：0.未采购 1.已采购
        /// </summary>
        public System.Int32? Result { get { return this._Result; } set { this._Result = value ?? default(System.Int32); } }

        private System.String _Memo;
        /// <summary>
        /// 备注
        /// </summary>
        public System.String Memo { get { return this._Memo; } set { this._Memo = value?.Trim(); } }

        private System.String _ProductUrl;
        /// <summary>
        /// 相似款链接
        /// </summary>
        public System.String ProductUrl { get { return this._ProductUrl; } set { this._ProductUrl = value?.Trim(); } }

        private System.String _CompanyUrl;
        /// <summary>
        /// 供应商链接
        /// </summary>
        public System.String CompanyUrl { get { return this._CompanyUrl; } set { this._CompanyUrl = value?.Trim(); } }

        private System.Decimal? _VolumeLength;
        /// <summary>
        /// 长（cm）
        /// </summary>
        public System.Decimal? VolumeLength { get { return this._VolumeLength; } set { this._VolumeLength = value ?? default(System.Decimal); } }

        private System.Decimal? _VolumeWidth;
        /// <summary>
        /// 宽（cm）
        /// </summary>
        public System.Decimal? VolumeWidth { get { return this._VolumeWidth; } set { this._VolumeWidth = value ?? default(System.Decimal); } }

        private System.Decimal? _VolumeHeight;
        /// <summary>
        /// 高（cm）
        /// </summary>
        public System.Decimal? VolumeHeight { get { return this._VolumeHeight; } set { this._VolumeHeight = value ?? default(System.Decimal); } }

        private System.Decimal? _VolumeWeight;
        /// <summary>
        /// 体积重（kg）
        /// </summary>
        public System.Decimal? VolumeWeight { get { return this._VolumeWeight; } set { this._VolumeWeight = value ?? default(System.Decimal); } }

        private System.Decimal? _Weight;
        /// <summary>
        /// 重量（kg）
        /// </summary>
        public System.Decimal? Weight { get { return this._Weight; } set { this._Weight = value ?? default(System.Decimal); } }

        private System.Decimal? _CostPrice;
        /// <summary>
        /// 成本价/拿货价（RMB）
        /// </summary>
        public System.Decimal? CostPrice { get { return this._CostPrice; } set { this._CostPrice = value ?? default(System.Decimal); } }

        private System.Decimal? _ShippingPrice;
        /// <summary>
        /// 运费
        /// </summary>
        public System.Decimal? ShippingPrice { get { return this._ShippingPrice; } set { this._ShippingPrice = value ?? default(System.Decimal); } }

        private System.String _ImageUrl;
        /// <summary>
        /// 图片Url
        /// </summary>
        public System.String ImageUrl { get { return this._ImageUrl; } set { this._ImageUrl = value?.Trim(); } }

        private System.Int32? _IsUpdate;
        /// <summary>
        /// 是否修改: 0. 未修改  1. 已修改
        /// </summary>
        public System.Int32? IsUpdate { get { return this._IsUpdate; } set { this._IsUpdate = value ?? default(System.Int32); } }

        private System.Int32? _LookIsAgree;
        /// <summary>
        /// 抽查结果：1. 同意  2.不同意
        /// </summary>
        public System.Int32? LookIsAgree { get { return this._LookIsAgree; } set { this._LookIsAgree = value ?? default(System.Int32); } }

        private System.String _LookVoucher;
        /// <summary>
        /// 抽查凭证
        /// </summary>
        public System.String LookVoucher { get { return this._LookVoucher; } set { this._LookVoucher = value?.Trim(); } }

        private System.String _LookMemo;
        /// <summary>
        /// 抽查备注
        /// </summary>
        public System.String LookMemo { get { return this._LookMemo; } set { this._LookMemo = value?.Trim(); } }

        private System.String _LookUser;
        /// <summary>
        /// 抽查人
        /// </summary>
        public System.String LookUser { get { return this._LookUser; } set { this._LookUser = value?.Trim(); } }

        private System.DateTime? _LookDate;
        /// <summary>
        /// 抽查时间
        /// </summary>
        public System.DateTime? LookDate { get { return this._LookDate; } set { this._LookDate = value ?? default(System.DateTime); } }

        private System.Int32? _FeedbackIsPurchase;
        /// <summary>
        /// 反馈是否采购： 1. 采购  2.不采购
        /// </summary>
        public System.Int32? FeedbackIsPurchase { get { return this._FeedbackIsPurchase; } set { this._FeedbackIsPurchase = value ?? default(System.Int32); } }

        private System.String _FeedbackMemo;
        /// <summary>
        /// 反馈备注
        /// </summary>
        public System.String FeedbackMemo { get { return this._FeedbackMemo; } set { this._FeedbackMemo = value?.Trim(); } }

        private System.String _FeedbackUser;
        /// <summary>
        /// 反馈人
        /// </summary>
        public System.String FeedbackUser { get { return this._FeedbackUser; } set { this._FeedbackUser = value?.Trim(); } }

        private System.DateTime? _FeedbackDate;
        /// <summary>
        /// 反馈时间
        /// </summary>
        public System.DateTime? FeedbackDate { get { return this._FeedbackDate; } set { this._FeedbackDate = value ?? default(System.DateTime); } }

        private System.String _CreateUser;
        /// <summary>
        /// 问题提出人
        /// </summary>
        public System.String CreateUser { get { return this._CreateUser; } set { this._CreateUser = value?.Trim(); } }

        private System.DateTime? _CreateDate;
        /// <summary>
        /// 问题提出时间
        /// </summary>
        public System.DateTime? CreateDate { get { return this._CreateDate; } set { this._CreateDate = value ?? default(System.DateTime); } }

        private System.String _UpdateUser;
        /// <summary>
        /// 修改人
        /// </summary>
        public System.String UpdateUser { get { return this._UpdateUser; } set { this._UpdateUser = value?.Trim(); } }

        private System.DateTime? _UpdateDate;
        /// <summary>
        /// 修改时间
        /// </summary>
        public System.DateTime? UpdateDate { get { return this._UpdateDate; } set { this._UpdateDate = value ?? default(System.DateTime); } }

        private System.String _LinkPlatform;
        /// <summary>
        /// 关联平台
        /// </summary>
        public System.String LinkPlatform { get { return this._LinkPlatform; } set { this._LinkPlatform = value?.Trim(); } }

        private System.String _FinishPlatform;
        /// <summary>
        /// 应处理平台
        /// </summary>
        public System.String FinishPlatform { get { return this._FinishPlatform; } set { this._FinishPlatform = value?.Trim(); } }

        private System.Int32? _HandleType;
        /// <summary>
        /// 处理状态：0. 未处理  1. 处理中  2. 已处理
        /// </summary>
        public System.Int32? HandleType { get { return this._HandleType; } set { this._HandleType = value ?? default(System.Int32); } }

        private System.String _LinkStoreManager;
        /// <summary>
        /// 关联店铺负责人
        /// </summary>
        public System.String LinkStoreManager { get { return this._LinkStoreManager; } set { this._LinkStoreManager = value?.Trim(); } }

        private System.String _FinishStoreManager;
        /// <summary>
        /// 应处理负责人
        /// </summary>
        public System.String FinishStoreManager { get { return this._FinishStoreManager; } set { this._FinishStoreManager = value?.Trim(); } }

        private System.String _LinkPoa;
        /// <summary>
        /// 
        /// </summary>
        public System.String LinkPoa { get { return this._LinkPoa; } set { this._LinkPoa = value?.Trim(); } }

        private System.Int32? _NoDeliver;
        /// <summary>
        /// 0：正常 1：不发货
        /// </summary>
        public System.Int32? NoDeliver { get { return this._NoDeliver; } set { this._NoDeliver = value ?? default(System.Int32); } }

        private System.String _NoDeliverReason;
        /// <summary>
        /// 不发货原因
        /// </summary>
        public System.String NoDeliverReason { get { return this._NoDeliverReason; } set { this._NoDeliverReason = value?.Trim(); } }

        private System.String _Memo2;
        /// <summary>
        /// 
        /// </summary>
        public System.String Memo2 { get { return this._Memo2; } set { this._Memo2 = value?.Trim(); } }
    }
}
