using SqlSugar;

namespace WeChat.Core.Model.EntityModel.ERP
{
    public class ScanningCheckTmp
    {
        /// <summary>
        /// 收件管理临时表
        /// </summary>
        public ScanningCheckTmp()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.String _OrderNo;
        /// <summary>
        /// 
        /// </summary>
        public System.String OrderNo { get { return this._OrderNo; } set { this._OrderNo = value?.Trim(); } }

        private System.String _PurchaseTrackNo;
        /// <summary>
        /// 
        /// </summary>
        public System.String PurchaseTrackNo { get { return this._PurchaseTrackNo; } set { this._PurchaseTrackNo = value?.Trim(); } }

        private System.DateTime? _AddTime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? AddTime { get { return this._AddTime; } set { this._AddTime = value ?? default(System.DateTime); } }

        private System.Boolean? _IsCheck;
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? IsCheck { get { return this._IsCheck; } set { this._IsCheck = value ?? default(System.Boolean); } }

        private System.String _CheckText;
        /// <summary>
        /// 
        /// </summary>
        public System.String CheckText { get { return this._CheckText; } set { this._CheckText = value?.Trim(); } }

        private System.DateTime? _ScanDate;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? ScanDate { get { return this._ScanDate; } set { this._ScanDate = value ?? default(System.DateTime); } }
    }
}