using SqlSugar;

namespace WeChat.Core.Model.EntityModel.ERP
{
    public class StockOperateRecord
    {
        /// <summary>
        /// 仓储操作记录
        /// </summary>
        public StockOperateRecord()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.String _Pno;
        /// <summary>
        /// 批号
        /// </summary>
        public System.String Pno { get { return this._Pno; } set { this._Pno = value?.Trim(); } }

        private System.String _OrderId;
        /// <summary>
        /// 订单号
        /// </summary>
        public System.String OrderId { get { return this._OrderId; } set { this._OrderId = value?.Trim(); } }

        private System.String _TrackingNo;
        /// <summary>
        /// 跟踪号
        /// </summary>
        public System.String TrackingNo { get { return this._TrackingNo; } set { this._TrackingNo = value?.Trim(); } }

        private System.String _Sku;
        /// <summary>
        /// 
        /// </summary>
        public System.String Sku { get { return this._Sku; } set { this._Sku = value?.Trim(); } }

        private System.String _Poa;
        /// <summary>
        /// 
        /// </summary>
        public System.String Poa { get { return this._Poa; } set { this._Poa = value?.Trim(); } }

        private System.String _OperateUser;
        /// <summary>
        /// 操作人
        /// </summary>
        public System.String OperateUser { get { return this._OperateUser; } set { this._OperateUser = value?.Trim(); } }

        private System.DateTime? _OperateTime;
        /// <summary>
        /// 操作时间
        /// </summary>
        public System.DateTime? OperateTime { get { return this._OperateTime; } set { this._OperateTime = value ?? default(System.DateTime); } }

        private System.Int32? _OperateType;
        /// <summary>
        /// 操作类型：1.打印面单 2.扫单 3.采购入库 4.出库扫描 5.条码打印 6.退件入库 7.FB采购入库 8.拣货 9.装箱 10.封箱 11.移库 12.入库管理打印记录 13.修改库存
        /// </summary>
        public System.Int32? OperateType { get { return this._OperateType; } set { this._OperateType = value ?? default(System.Int32); } }

        private System.String _OperateTable;
        /// <summary>
        /// 操作的表
        /// </summary>
        public System.String OperateTable { get { return this._OperateTable; } set { this._OperateTable = value?.Trim(); } }

        private System.Int32? _OperateTableId;
        /// <summary>
        /// 操作的表Id
        /// </summary>
        public System.Int32? OperateTableId { get { return this._OperateTableId; } set { this._OperateTableId = value ?? default(System.Int32); } }

        private System.String _OperateMenu;
        /// <summary>
        /// 操作菜单
        /// </summary>
        public System.String OperateMenu { get { return this._OperateMenu; } set { this._OperateMenu = value?.Trim(); } }

        private System.String _Memo;
        /// <summary>
        /// 
        /// </summary>
        public System.String Memo { get { return this._Memo; } set { this._Memo = value?.Trim(); } }
    }
}