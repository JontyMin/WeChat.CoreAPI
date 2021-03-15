using SqlSugar;

namespace WeChat.Core.Model.EntityModel
{
    /// <summary>
    /// 供应产品
    /// </summary>
    public class WeChatSupplyProduct
    {
        /// <summary>
        /// 
        /// </summary>
        public WeChatSupplyProduct()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.Int32 _Sid;
        /// <summary>
        /// 所属供应商Id
        /// </summary>
        public System.Int32 Sid { get { return this._Sid; } set { this._Sid = value; } }

        private System.String _Cname;
        /// <summary>
        /// 中文名称
        /// </summary>
        public System.String Cname { get { return this._Cname; } set { this._Cname = value?.Trim(); } }

        private System.String _Uname;
        /// <summary>
        /// 英文名称
        /// </summary>
        public System.String Uname { get { return this._Uname; } set { this._Uname = value?.Trim(); } }

        private System.String _Property;
        /// <summary>
        /// 属性
        /// </summary>
        public System.String Property { get { return this._Property; } set { this._Property = value?.Trim(); } }

        private System.Double _Weight;
        /// <summary>
        /// 重量
        /// </summary>
        public System.Double Weight { get { return this._Weight; } set { this._Weight = value; } }

        private System.Double _Length;
        /// <summary>
        /// 长度
        /// </summary>
        public System.Double Length { get { return this._Length; } set { this._Length = value; } }

        private System.Double _Width;
        /// <summary>
        /// 宽度
        /// </summary>
        public System.Double Width { get { return this._Width; } set { this._Width = value; } }

        private System.Double _Height;
        /// <summary>
        /// 高度
        /// </summary>
        public System.Double Height { get { return this._Height; } set { this._Height = value; } }

        private System.Decimal _Quote;
        /// <summary>
        /// 报价
        /// </summary>
        public System.Decimal Quote { get { return this._Quote; } set { this._Quote = value; } }

        private System.String _ProductLink;
        /// <summary>
        /// 产品链接
        /// </summary>
        public System.String ProductLink { get { return this._ProductLink; } set { this._ProductLink = value?.Trim(); } }

        private System.String _ShopLink;
        /// <summary>
        /// 店铺链接
        /// </summary>
        public System.String ShopLink { get { return this._ShopLink; } set { this._ShopLink = value?.Trim(); } }

        private System.String _Cdescribe;
        /// <summary>
        /// 中文描述
        /// </summary>
        public System.String Cdescribe { get { return this._Cdescribe; } set { this._Cdescribe = value?.Trim(); } }

        private System.String _Udescribe;
        /// <summary>
        /// 英文描述
        /// </summary>
        public System.String Udescribe { get { return this._Udescribe; } set { this._Udescribe = value?.Trim(); } }
        
        private System.Int32? _State;
        /// <summary>
        /// 状态：0：删除，1:上架，2：下架  
        /// </summary>
        public System.Int32? State { get { return this._State; } set { this._State = value ?? default(System.Int32); } }

        private System.DateTime _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime { get { return this._CreateTime; } set { this._CreateTime = value; } }

        private System.DateTime? _UpdateTime;
        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime? UpdateTime { get { return this._UpdateTime; } set { this._UpdateTime = value ?? default(System.DateTime); } }


        private System.String _CategoryName;
        /// <summary>
        /// 类目名称
        /// </summary>
        public System.String CategoryName
        {
            get
            {
                return this._CategoryName;
            }
            set
            {
                this._CategoryName = value?.Trim();
            }
        }
    }
}