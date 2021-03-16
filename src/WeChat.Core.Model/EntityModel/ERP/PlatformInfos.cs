using SqlSugar;

namespace WeChat.Core.Model.EntityModel.ERP
{
    /// <summary>
    /// 平台信息表
    /// </summary>
    public class PlatformInfos
    {
        /// <summary>
        /// 
        /// </summary>
        public PlatformInfos()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.Int32 _PlatformNo;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 PlatformNo { get { return this._PlatformNo; } set { this._PlatformNo = value; } }

        private System.String _PlatformName;
        /// <summary>
        /// 
        /// </summary>
        public System.String PlatformName { get { return this._PlatformName; } set { this._PlatformName = value?.Trim(); } }

        private System.DateTime _CreateTime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime CreateTime { get { return this._CreateTime; } set { this._CreateTime = value; } }
    }
}