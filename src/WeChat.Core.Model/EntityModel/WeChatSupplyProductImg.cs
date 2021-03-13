using SqlSugar;

namespace WeChat.Core.Model.EntityModel
{
    /// <summary>
    /// 供应产品图片
    /// </summary>
    public class WeChatSupplyProductImg
    {
        /// <summary>
        /// 
        /// </summary>
        public WeChatSupplyProductImg()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.Int32 _PId;
        /// <summary>
        /// 产品Id
        /// </summary>
        public System.Int32 PId { get { return this._PId; } set { this._PId = value; } }

        private System.String _ImageUrl;
        /// <summary>
        /// 图片路径
        /// </summary>
        public System.String ImageUrl { get { return this._ImageUrl; } set { this._ImageUrl = value?.Trim(); } }
    }
}