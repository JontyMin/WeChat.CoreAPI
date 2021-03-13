using SqlSugar;

namespace WeChat.Core.Model.EntityModel
{
    /// <summary>
    /// 
    /// </summary>
    public class WeChatSupplier
    {
        /// <summary>
        /// 供应商
        /// </summary>
        public WeChatSupplier()
        {
        }

        private System.Int32 _Id;
        /// <summary>
        /// 供应商Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true,IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.String _LoginName;
        /// <summary>
        /// 登录用户名
        /// </summary>
        public System.String LoginName { get { return this._LoginName; } set { this._LoginName = value?.Trim(); } }

        private System.String _LoginPwd;
        /// <summary>
        /// 登录密码
        /// </summary>
        public System.String LoginPwd { get { return this._LoginPwd; } set { this._LoginPwd = value?.Trim(); } }

        private System.String _RealName;
        /// <summary>
        /// 联系人真实姓名
        /// </summary>
        public System.String RealName { get { return this._RealName; } set { this._RealName = value?.Trim(); } }

        private System.String _CompanyName;
        /// <summary>
        /// 供应商公司
        /// </summary>
        public System.String CompanyName { get { return this._CompanyName; } set { this._CompanyName = value?.Trim(); } }

        private System.String _CreditCode;
        /// <summary>
        /// 统一社会信用代码
        /// </summary>
        public System.String CreditCode { get { return this._CreditCode; } set { this._CreditCode = value?.Trim(); } }

        private System.String _MobilePhone;
        /// <summary>
        /// 移动电话
        /// </summary>
        public System.String MobilePhone { get { return this._MobilePhone; } set { this._MobilePhone = value?.Trim(); } }

        private System.String _FixPhone;
        /// <summary>
        /// 公司固话
        /// </summary>
        public System.String FixPhone { get { return this._FixPhone; } set { this._FixPhone = value?.Trim(); } }

        private System.String _Remark;
        /// <summary>
        /// 供应商备注
        /// </summary>
        public System.String Remark { get { return this._Remark; } set { this._Remark = value?.Trim(); } }

        private System.Int32? _State;
        /// <summary>
        /// 供应商状态 0：未审核，1：入驻中，2：已禁用
        /// </summary>
        public System.Int32? State { get { return this._State; } set { this._State = value ?? default(System.Int32); } }

        private System.DateTime? _PassTime;
        /// <summary>
        /// 审核通过时间
        /// </summary>
        public System.DateTime? PassTime { get { return this._PassTime; } set { this._PassTime = value ?? default(System.DateTime); } }

        private System.String _InvitationCode;
        /// <summary>
        /// 邀请码
        /// </summary>
        public System.String InvitationCode { get { return this._InvitationCode; } set { this._InvitationCode = value?.Trim(); } }

        private System.DateTime? _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? CreateTime { get { return this._CreateTime; } set { this._CreateTime = value ?? default(System.DateTime); } }

        private System.DateTime? _UpdateTime;
        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime? UpdateTime { get { return this._UpdateTime; } set { this._UpdateTime = value ?? default(System.DateTime); } }

        private System.String _UpdateUser;
        /// <summary>
        /// 更新人
        /// </summary>
        public System.String UpdateUser { get { return this._UpdateUser; } set { this._UpdateUser = value?.Trim(); } }

        private System.Boolean? _IsReadAgreement;
        /// <summary>
        /// 协议是否已读
        /// </summary>
        public System.Boolean? IsReadAgreement { get { return this._IsReadAgreement; } set { this._IsReadAgreement = value ?? default(System.Boolean); } }

        private System.String _OpenId;
        /// <summary>
        /// 微信openId(唯一)
        /// </summary>
        public System.String OpenId { get { return this._OpenId; } set { this._OpenId = value?.Trim(); } }

        private System.String _TagIds;
        /// <summary>
        /// 标签Id
        /// </summary>
        public System.String TagIds { get { return this._TagIds;} set { this._TagIds = value?.Trim(); } }
    }
}