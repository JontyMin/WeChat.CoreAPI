using SqlSugar;

namespace WeChat.Core.Model.EntityModel.ERP
{
    /// <summary>
    /// 采购产品表
    /// </summary>
    public class PurchaseProducts
    {
        /// <summary>
        /// 采购产品表
        /// </summary>
        public PurchaseProducts()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.String _Pno;
        /// <summary>
        /// 批次号
        /// </summary>
        public System.String Pno { get { return this._Pno; } set { this._Pno = value?.Trim(); } }

        private System.String _Sku;
        /// <summary>
        /// Sku
        /// </summary>
        public System.String Sku { get { return this._Sku; } set { this._Sku = value?.Trim(); } }

        private System.String _Poa;
        /// <summary>
        /// Poa
        /// </summary>
        public System.String Poa { get { return this._Poa; } set { this._Poa = value?.Trim(); } }

        private System.Int32? _ProductId;
        /// <summary>
        /// 产品ID
        /// </summary>
        public System.Int32? ProductId { get { return this._ProductId; } set { this._ProductId = value ?? default(System.Int32); } }

        private System.Int32? _PropertyId;
        /// <summary>
        /// 产品属性ID
        /// </summary>
        public System.Int32? PropertyId { get { return this._PropertyId; } set { this._PropertyId = value ?? default(System.Int32); } }

        private System.Int32? _PreparGoodsStatus;
        /// <summary>
        /// 备货状态 1待备货 2已采购 3运送中 4已签收
        /// </summary>
        public System.Int32? PreparGoodsStatus { get { return this._PreparGoodsStatus; } set { this._PreparGoodsStatus = value ?? default(System.Int32); } }

        private System.Boolean? _IsInputPurchaseInfo;
        /// <summary>
        /// 是否已经录入采购信息 1是 0否
        /// </summary>
        public System.Boolean? IsInputPurchaseInfo { get { return this._IsInputPurchaseInfo; } set { this._IsInputPurchaseInfo = value ?? default(System.Boolean); } }

        private System.DateTime? _PurchaseTime;
        /// <summary>
        /// 采购时间
        /// </summary>
        public System.DateTime? PurchaseTime { get { return this._PurchaseTime; } set { this._PurchaseTime = value ?? default(System.DateTime); } }

        private System.String _PurchaseNo;
        /// <summary>
        /// 采购单号
        /// </summary>
        public System.String PurchaseNo { get { return this._PurchaseNo; } set { this._PurchaseNo = value?.Trim(); } }

        private System.String _PurchaseUserName;
        /// <summary>
        /// 采购人
        /// </summary>
        public System.String PurchaseUserName { get { return this._PurchaseUserName; } set { this._PurchaseUserName = value?.Trim(); } }

        private System.String _SupplierName;
        /// <summary>
        /// 供应商名
        /// </summary>
        public System.String SupplierName { get { return this._SupplierName; } set { this._SupplierName = value?.Trim(); } }

        private System.String _SupplierTel;
        /// <summary>
        /// 供应商联系方式
        /// </summary>
        public System.String SupplierTel { get { return this._SupplierTel; } set { this._SupplierTel = value?.Trim(); } }

        private System.String _PurchaseTrackNo;
        /// <summary>
        /// 采购运单号
        /// </summary>
        public System.String PurchaseTrackNo { get { return this._PurchaseTrackNo; } set { this._PurchaseTrackNo = value?.Trim(); } }

        private System.Int32? _CategoryId;
        /// <summary>
        /// 类目ID
        /// </summary>
        public System.Int32? CategoryId { get { return this._CategoryId; } set { this._CategoryId = value ?? default(System.Int32); } }

        private System.String _CategroyName;
        /// <summary>
        /// 类目名称
        /// </summary>
        public System.String CategroyName { get { return this._CategroyName; } set { this._CategroyName = value?.Trim(); } }

        private System.String _PurchaseAccountName;
        /// <summary>
        /// 采购账号
        /// </summary>
        public System.String PurchaseAccountName { get { return this._PurchaseAccountName; } set { this._PurchaseAccountName = value?.Trim(); } }

        private System.Int32? _PurchaseCount;
        /// <summary>
        /// 采购数据量
        /// </summary>
        public System.Int32? PurchaseCount { get { return this._PurchaseCount; } set { this._PurchaseCount = value ?? default(System.Int32); } }

        private System.Decimal? _PurchaseAmount;
        /// <summary>
        /// 采购金额
        /// </summary>
        public System.Decimal? PurchaseAmount { get { return this._PurchaseAmount; } set { this._PurchaseAmount = value ?? default(System.Decimal); } }

        private System.Decimal? _PurchaseShipping;
        /// <summary>
        /// 采购运费
        /// </summary>
        public System.Decimal? PurchaseShipping { get { return this._PurchaseShipping; } set { this._PurchaseShipping = value ?? default(System.Decimal); } }

        private System.String _Memo;
        /// <summary>
        /// 备注
        /// </summary>
        public System.String Memo { get { return this._Memo; } set { this._Memo = value?.Trim(); } }

        private System.DateTime? _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? CreateTime { get { return this._CreateTime; } set { this._CreateTime = value ?? default(System.DateTime); } }

        private System.Int32? _CreateUserId;
        /// <summary>
        /// 创建人ID
        /// </summary>
        public System.Int32? CreateUserId { get { return this._CreateUserId; } set { this._CreateUserId = value ?? default(System.Int32); } }

        private System.DateTime? _ModifyTime;
        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime? ModifyTime { get { return this._ModifyTime; } set { this._ModifyTime = value ?? default(System.DateTime); } }

        private System.Int32? _MoidfyUserId;
        /// <summary>
        /// 更新人
        /// </summary>
        public System.Int32? MoidfyUserId { get { return this._MoidfyUserId; } set { this._MoidfyUserId = value ?? default(System.Int32); } }

        private System.Boolean? _IsDeleted;
        /// <summary>
        /// 是否删除
        /// </summary>
        public System.Boolean? IsDeleted { get { return this._IsDeleted; } set { this._IsDeleted = value ?? default(System.Boolean); } }

        private System.Int32? _EnterWareHouseCount;
        /// <summary>
        /// 入库数量
        /// </summary>
        public System.Int32? EnterWareHouseCount { get { return this._EnterWareHouseCount; } set { this._EnterWareHouseCount = value ?? default(System.Int32); } }

        private System.String _EnterWarehouseUserName;
        /// <summary>
        /// 入库人
        /// </summary>
        public System.String EnterWarehouseUserName { get { return this._EnterWarehouseUserName; } set { this._EnterWarehouseUserName = value?.Trim(); } }

        private System.DateTime? _EnterWarehouseTime;
        /// <summary>
        /// 入库时间
        /// </summary>
        public System.DateTime? EnterWarehouseTime { get { return this._EnterWarehouseTime; } set { this._EnterWarehouseTime = value ?? default(System.DateTime); } }

        private System.Int32? _WaitPurchaseCount;
        /// <summary>
        /// 当前待备货数量
        /// </summary>
        public System.Int32? WaitPurchaseCount { get { return this._WaitPurchaseCount; } set { this._WaitPurchaseCount = value ?? default(System.Int32); } }

        private System.Decimal? _PurchasePrice;
        /// <summary>
        /// 采购价格
        /// </summary>
        public System.Decimal? PurchasePrice { get { return this._PurchasePrice; } set { this._PurchasePrice = value ?? default(System.Decimal); } }

        private System.String _Examiner;
        /// <summary>
        /// 核定人
        /// </summary>
        public System.String Examiner { get { return this._Examiner; } set { this._Examiner = value?.Trim(); } }

        private System.DateTime? _ExamineTime;
        /// <summary>
        /// 核定时间
        /// </summary>
        public System.DateTime? ExamineTime { get { return this._ExamineTime; } set { this._ExamineTime = value ?? default(System.DateTime); } }

        private System.Boolean? _IsReminder;
        /// <summary>
        /// 是否已催单
        /// </summary>
        public System.Boolean? IsReminder { get { return this._IsReminder; } set { this._IsReminder = value ?? default(System.Boolean); } }

        private System.DateTime? _ReminderTime;
        /// <summary>
        /// 催单时间
        /// </summary>
        public System.DateTime? ReminderTime { get { return this._ReminderTime; } set { this._ReminderTime = value ?? default(System.DateTime); } }

        private System.String _ReminderMemo;
        /// <summary>
        /// 催单备注
        /// </summary>
        public System.String ReminderMemo { get { return this._ReminderMemo; } set { this._ReminderMemo = value?.Trim(); } }

        private System.Boolean? _IsCancel;
        /// <summary>
        /// 是否废除
        /// </summary>
        public System.Boolean? IsCancel { get { return this._IsCancel; } set { this._IsCancel = value ?? default(System.Boolean); } }

        private System.String _CancelReason;
        /// <summary>
        /// 废除原因
        /// </summary>
        public System.String CancelReason { get { return this._CancelReason; } set { this._CancelReason = value?.Trim(); } }

        private System.String _CancelUserName;
        /// <summary>
        /// 废除人
        /// </summary>
        public System.String CancelUserName { get { return this._CancelUserName; } set { this._CancelUserName = value?.Trim(); } }

        private System.Int32? _SurplusCount;
        /// <summary>
        /// 剩余数量
        /// </summary>
        public System.Int32? SurplusCount { get { return this._SurplusCount; } set { this._SurplusCount = value ?? default(System.Int32); } }

        private System.Boolean? _IsRetrieval;
        /// <summary>
        /// 是否已出库
        /// </summary>
        public System.Boolean? IsRetrieval { get { return this._IsRetrieval; } set { this._IsRetrieval = value ?? default(System.Boolean); } }

        private System.Boolean? _IsTraceStatus;
        /// <summary>
        /// 是否快递签收
        /// </summary>
        public System.Boolean? IsTraceStatus { get { return this._IsTraceStatus; } set { this._IsTraceStatus = value ?? default(System.Boolean); } }

        private System.DateTime? _TraceStatusTime;
        /// <summary>
        /// 快递签收时间
        /// </summary>
        public System.DateTime? TraceStatusTime { get { return this._TraceStatusTime; } set { this._TraceStatusTime = value ?? default(System.DateTime); } }

        private System.Boolean? _IsReplenish;
        /// <summary>
        /// 是否补货
        /// </summary>
        public System.Boolean? IsReplenish { get { return this._IsReplenish; } set { this._IsReplenish = value ?? default(System.Boolean); } }

        private System.String _CreateUserName;
        /// <summary>
        /// 创建人
        /// </summary>
        public System.String CreateUserName { get { return this._CreateUserName; } set { this._CreateUserName = value?.Trim(); } }

        private System.DateTime? _RecordTime;
        /// <summary>
        /// 记录时间(临时用)
        /// </summary>
        public System.DateTime? RecordTime { get { return this._RecordTime; } set { this._RecordTime = value ?? default(System.DateTime); } }

        private System.DateTime? _PayAmountTime;
        /// <summary>
        /// 超级买家付款时间
        /// </summary>
        public System.DateTime? PayAmountTime { get { return this._PayAmountTime; } set { this._PayAmountTime = value ?? default(System.DateTime); } }

        private System.Int32? _MarkOrderCount;
        /// <summary>
        /// 标记出单数量
        /// </summary>
        public System.Int32? MarkOrderCount { get { return this._MarkOrderCount; } set { this._MarkOrderCount = value ?? default(System.Int32); } }

        private System.Int32? _GoodsIdentifying;
        /// <summary>
        /// 货物标识(0:正常，2:货物错误，3:破损件，4:未核定)
        /// </summary>
        public System.Int32? GoodsIdentifying { get { return this._GoodsIdentifying; } set { this._GoodsIdentifying = value ?? default(System.Int32); } }

        private System.String _ErrorCode;
        /// <summary>
        /// 错误编号
        /// </summary>
        public System.String ErrorCode { get { return this._ErrorCode; } set { this._ErrorCode = value?.Trim(); } }

        private System.String _LeadingCadre;
        /// <summary>
        /// 责任人
        /// </summary>
        public System.String LeadingCadre { get { return this._LeadingCadre; } set { this._LeadingCadre = value?.Trim(); } }

        private System.String _ErrorImageUrl;
        /// <summary>
        /// 错误标识图片
        /// </summary>
        public System.String ErrorImageUrl { get { return this._ErrorImageUrl; } set { this._ErrorImageUrl = value?.Trim(); } }

        private System.DateTime? _IdentifyingTime;
        /// <summary>
        /// 标识时间
        /// </summary>
        public System.DateTime? IdentifyingTime { get { return this._IdentifyingTime; } set { this._IdentifyingTime = value ?? default(System.DateTime); } }

        private System.String _IdentifyingUserName;
        /// <summary>
        /// 标识人
        /// </summary>
        public System.String IdentifyingUserName { get { return this._IdentifyingUserName; } set { this._IdentifyingUserName = value?.Trim(); } }

        private System.DateTime? _PrintCodeTime;
        /// <summary>
        /// 条码打印时间
        /// </summary>
        public System.DateTime? PrintCodeTime { get { return this._PrintCodeTime; } set { this._PrintCodeTime = value ?? default(System.DateTime); } }

        private System.Int32? _IsMeasureSize;
        /// <summary>
        /// 是否需要量尺寸：0.不需要  1.需要
        /// </summary>
        public System.Int32? IsMeasureSize { get { return this._IsMeasureSize; } set { this._IsMeasureSize = value ?? default(System.Int32); } }

        private System.Decimal? _VolumeLength;
        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? VolumeLength { get { return this._VolumeLength; } set { this._VolumeLength = value ?? default(System.Decimal); } }

        private System.Decimal? _VolumeWidth;
        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? VolumeWidth { get { return this._VolumeWidth; } set { this._VolumeWidth = value ?? default(System.Decimal); } }

        private System.Decimal? _VolumeHeight;
        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? VolumeHeight { get { return this._VolumeHeight; } set { this._VolumeHeight = value ?? default(System.Decimal); } }

        private System.Decimal? _StockWeight;
        /// <summary>
        /// 入库时填写的重量(kg)
        /// </summary>
        public System.Decimal? StockWeight { get { return this._StockWeight; } set { this._StockWeight = value ?? default(System.Decimal); } }

        private System.String _ChoiceUserName;
        /// <summary>
        /// 备货人(出库管理拉到待备货以及拣货拉到备货管理)
        /// </summary>
        public System.String ChoiceUserName { get { return this._ChoiceUserName; } set { this._ChoiceUserName = value?.Trim(); } }

        private System.DateTime? _ChoiceTime;
        /// <summary>
        /// 备货时间(出库管理拉到待备货以及拣货拉到备货管理)
        /// </summary>
        public System.DateTime? ChoiceTime { get { return this._ChoiceTime; } set { this._ChoiceTime = value ?? default(System.DateTime); } }

        private System.Boolean? _IsAgainDelivery;
        /// <summary>
        /// 是否重新发货(针对已经发货到分拣中心后丢件得需要重新发货)
        /// </summary>
        public System.Boolean? IsAgainDelivery { get { return this._IsAgainDelivery; } set { this._IsAgainDelivery = value ?? default(System.Boolean); } }

        private System.Boolean? _IsAccessories;
        /// <summary>
        /// 是否补充信息
        /// </summary>
        public System.Boolean? IsAccessories { get { return this._IsAccessories; } set { this._IsAccessories = value ?? default(System.Boolean); } }
    }
}