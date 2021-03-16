using SqlSugar;

namespace WeChat.Core.Model.EntityModel.ERP
{
    public class ScanningCheck
    {
        /// <summary>
        /// 收件
        /// </summary>
        public ScanningCheck()
        {
        }

        private System.Int32 _Id;

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id
        {
            get { return this._Id; }
            set { this._Id = value; }
        }

        private System.String _OrderNo;

        /// <summary>
        /// 采购订单号
        /// </summary>
        public System.String OrderNo
        {
            get { return this._OrderNo; }
            set { this._OrderNo = value?.Trim(); }
        }

        private System.String _PurchaseTrackNo;

        /// <summary>
        /// 快递单号
        /// </summary>
        public System.String PurchaseTrackNo
        {
            get { return this._PurchaseTrackNo; }
            set { this._PurchaseTrackNo = value?.Trim(); }
        }

        private System.DateTime? _AddTime;

        /// <summary>
        /// 收件时间
        /// </summary>
        public System.DateTime? AddTime
        {
            get { return this._AddTime; }
            set { this._AddTime = value ?? default(System.DateTime); }
        }

        private System.Boolean? _IsCheck;

        /// <summary>
        /// 是否核实（0：待核实，1：已核实）
        /// </summary>
        public System.Boolean? IsCheck
        {
            get { return this._IsCheck; }
            set { this._IsCheck = value ?? default(System.Boolean); }
        }

        private System.String _CheckText;

        /// <summary>
        /// 待核实/已核实
        /// </summary>
        public System.String CheckText
        {
            get { return this._CheckText; }
            set { this._CheckText = value?.Trim(); }
        }

        private System.DateTime? _ScanDate;

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? ScanDate
        {
            get { return this._ScanDate; }
            set { this._ScanDate = value ?? default(System.DateTime); }
        }
    }
}