using SqlSugar;

namespace WeChat.Core.Model.EntityModel.ERP
{
    /// <summary>
    /// 超级买家订单表
    /// </summary>
    public class SuperBuyerOrders
    {
        /// <summary>
        /// 超级买家订单表
        /// </summary>
        public SuperBuyerOrders()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.String _OrderNo;
        /// <summary>
        /// 单号
        /// </summary>
        public System.String OrderNo { get { return this._OrderNo; } set { this._OrderNo = value?.Trim(); } }

        private System.Decimal? _DiscountAmount;
        /// <summary>
        /// 折扣金额
        /// </summary>
        public System.Decimal? DiscountAmount { get { return this._DiscountAmount; } set { this._DiscountAmount = value ?? default(System.Decimal); } }

        private System.Decimal? _TotalAmount;
        /// <summary>
        /// 总金额
        /// </summary>
        public System.Decimal? TotalAmount { get { return this._TotalAmount; } set { this._TotalAmount = value ?? default(System.Decimal); } }

        private System.Decimal? _ShippingFee;
        /// <summary>
        /// 运费
        /// </summary>
        public System.Decimal? ShippingFee { get { return this._ShippingFee; } set { this._ShippingFee = value ?? default(System.Decimal); } }

        private System.String _Status;
        /// <summary>
        /// 状态
        /// </summary>
        public System.String Status { get { return this._Status; } set { this._Status = value?.Trim(); } }

        private System.Decimal? _Refund;
        /// <summary>
        /// 退款金额
        /// </summary>
        public System.Decimal? Refund { get { return this._Refund; } set { this._Refund = value ?? default(System.Decimal); } }

        private System.DateTime? _OrderCreatTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? OrderCreatTime { get { return this._OrderCreatTime; } set { this._OrderCreatTime = value ?? default(System.DateTime); } }

        private System.String _OrderRemark;
        /// <summary>
        /// 订单备注
        /// </summary>
        public System.String OrderRemark { get { return this._OrderRemark; } set { this._OrderRemark = value?.Trim(); } }

        private System.String _SellerName;
        /// <summary>
        /// 卖家联系人
        /// </summary>
        public System.String SellerName { get { return this._SellerName; } set { this._SellerName = value?.Trim(); } }

        private System.String _SellerCompanyName;
        /// <summary>
        /// 卖家公司名称
        /// </summary>
        public System.String SellerCompanyName { get { return this._SellerCompanyName; } set { this._SellerCompanyName = value?.Trim(); } }

        private System.String _SellerMobile;
        /// <summary>
        /// 移动电话
        /// </summary>
        public System.String SellerMobile { get { return this._SellerMobile; } set { this._SellerMobile = value?.Trim(); } }

        private System.String _SellerPhone;
        /// <summary>
        /// 固定电话
        /// </summary>
        public System.String SellerPhone { get { return this._SellerPhone; } set { this._SellerPhone = value?.Trim(); } }

        private System.String _SellerEmail;
        /// <summary>
        /// 卖家邮箱
        /// </summary>
        public System.String SellerEmail { get { return this._SellerEmail; } set { this._SellerEmail = value?.Trim(); } }

        private System.Decimal? _SumProductPayment;
        /// <summary>
        /// 产品总金额
        /// </summary>
        public System.Decimal? SumProductPayment { get { return this._SumProductPayment; } set { this._SumProductPayment = value ?? default(System.Decimal); } }

        private System.String _ReceiverName;
        /// <summary>
        /// 收件人
        /// </summary>
        public System.String ReceiverName { get { return this._ReceiverName; } set { this._ReceiverName = value?.Trim(); } }

        private System.String _CreateUserName;
        /// <summary>
        /// 创建人
        /// </summary>
        public System.String CreateUserName { get { return this._CreateUserName; } set { this._CreateUserName = value?.Trim(); } }

        private System.DateTime? _CreatTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? CreatTime { get { return this._CreatTime; } set { this._CreatTime = value ?? default(System.DateTime); } }

        private System.String _ModifyUserName;
        /// <summary>
        /// 更新人
        /// </summary>
        public System.String ModifyUserName { get { return this._ModifyUserName; } set { this._ModifyUserName = value?.Trim(); } }

        private System.DateTime? _ModifyTime;
        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime? ModifyTime { get { return this._ModifyTime; } set { this._ModifyTime = value ?? default(System.DateTime); } }

        private System.String _TradePayWayDesc;
        /// <summary>
        /// 支付方式
        /// </summary>
        public System.String TradePayWayDesc { get { return this._TradePayWayDesc; } set { this._TradePayWayDesc = value?.Trim(); } }

        private System.String _TradePayStatusDesc;
        /// <summary>
        /// 支付状态
        /// </summary>
        public System.String TradePayStatusDesc { get { return this._TradePayStatusDesc; } set { this._TradePayStatusDesc = value?.Trim(); } }

        private System.Decimal? _TradePhasAmount;
        /// <summary>
        /// 支付金额
        /// </summary>
        public System.Decimal? TradePhasAmount { get { return this._TradePhasAmount; } set { this._TradePhasAmount = value ?? default(System.Decimal); } }

        private System.String _LogisticsCompanyName;
        /// <summary>
        /// 物流公司
        /// </summary>
        public System.String LogisticsCompanyName { get { return this._LogisticsCompanyName; } set { this._LogisticsCompanyName = value?.Trim(); } }

        private System.String _LogisticsBillNo;
        /// <summary>
        /// 物流编号
        /// </summary>
        public System.String LogisticsBillNo { get { return this._LogisticsBillNo; } set { this._LogisticsBillNo = value?.Trim(); } }

        private System.DateTime? _TradePayTime;
        /// <summary>
        /// 支付时间
        /// </summary>
        public System.DateTime? TradePayTime { get { return this._TradePayTime; } set { this._TradePayTime = value ?? default(System.DateTime); } }

        private System.String _SenderAddress;
        /// <summary>
        /// 发货地址
        /// </summary>
        public System.String SenderAddress { get { return this._SenderAddress; } set { this._SenderAddress = value?.Trim(); } }

        private System.String _TraceStatus;
        /// <summary>
        /// 物流跟踪状态(WAITACCEPT:未受理;CANCEL:已撤销;ACCEPT:已受理;TRANSPORT:运输中;NOGET:揽件失败;SIGN:已签收;UNSIGN:签收异常)
        /// </summary>
        public System.String TraceStatus { get { return this._TraceStatus; } set { this._TraceStatus = value?.Trim(); } }

        private System.String _TradeType;
        /// <summary>
        /// 交易类型
        /// </summary>
        public System.String TradeType { get { return this._TradeType; } set { this._TradeType = value?.Trim(); } }

        private System.Boolean? _IsCreditOrder;
        /// <summary>
        /// 是否诚e赊交易方式
        /// </summary>
        public System.Boolean? IsCreditOrder { get { return this._IsCreditOrder; } set { this._IsCreditOrder = value ?? default(System.Boolean); } }

        private System.Int32? _IsHaste;
        /// <summary>
        /// 是否催件
        /// </summary>
        public System.Int32? IsHaste { get { return this._IsHaste; } set { this._IsHaste = value ?? default(System.Int32); } }

        private System.String _Memo;
        /// <summary>
        /// 备注
        /// </summary>
        public System.String Memo { get { return this._Memo; } set { this._Memo = value?.Trim(); } }

        private System.String _HasteUser;
        /// <summary>
        /// 催件人
        /// </summary>
        public System.String HasteUser { get { return this._HasteUser; } set { this._HasteUser = value?.Trim(); } }

        private System.DateTime? _HasteTime;
        /// <summary>
        /// 催件时间
        /// </summary>
        public System.DateTime? HasteTime { get { return this._HasteTime; } set { this._HasteTime = value ?? default(System.DateTime); } }
    }
}