using SqlSugar;

namespace WeChat.Core.Model.EntityModel.ERP
{
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        public User()
        {
        }

        private System.Int32 _UserId;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 UserId { get { return this._UserId; } set { this._UserId = value; } }

        private System.Int32 _CompanyId;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 CompanyId { get { return this._CompanyId; } set { this._CompanyId = value; } }

        private System.Int32 _RoleId;
        /// <summary>
        /// 角色ID
        /// </summary>
        public System.Int32 RoleId { get { return this._RoleId; } set { this._RoleId = value; } }

        private System.String _LoginName;
        /// <summary>
        /// 登录名
        /// </summary>
        public System.String LoginName { get { return this._LoginName; } set { this._LoginName = value?.Trim(); } }

        private System.String _LoginPwd;
        /// <summary>
        /// 登陆密码
        /// </summary>
        public System.String LoginPwd { get { return this._LoginPwd; } set { this._LoginPwd = value?.Trim(); } }

        private System.String _TrueName;
        /// <summary>
        /// 实名
        /// </summary>
        public System.String TrueName { get { return this._TrueName; } set { this._TrueName = value?.Trim(); } }

        private System.String _Mobile;
        /// <summary>
        /// 手机
        /// </summary>
        public System.String Mobile { get { return this._Mobile; } set { this._Mobile = value?.Trim(); } }

        private System.String _Email;
        /// <summary>
        /// 邮箱
        /// </summary>
        public System.String Email { get { return this._Email; } set { this._Email = value?.Trim(); } }

        private System.String _QQ;
        /// <summary>
        /// QQ
        /// </summary>
        public System.String QQ { get { return this._QQ; } set { this._QQ = value?.Trim(); } }

        private System.String _WeChat;
        /// <summary>
        /// 微信
        /// </summary>
        public System.String WeChat { get { return this._WeChat; } set { this._WeChat = value?.Trim(); } }

        private System.Int32? _Sex;
        /// <summary>
        /// 性别(1：男，2：女)
        /// </summary>
        public System.Int32? Sex { get { return this._Sex; } set { this._Sex = value ?? default(System.Int32); } }

        private System.String _HeadUrl;
        /// <summary>
        /// 
        /// </summary>
        public System.String HeadUrl { get { return this._HeadUrl; } set { this._HeadUrl = value?.Trim(); } }

        private System.Int32? _Status;
        /// <summary>
        /// 状态：0试用期，1转正在职，2试用期离职，3转正离职
        /// </summary>
        public System.Int32? Status { get { return this._Status; } set { this._Status = value ?? default(System.Int32); } }

        private System.Int32? _Type;
        /// <summary>
        /// 1职工账号，2行政账号,3主管账号，4经理账号，5财务账号，6总监账号，8股东账号，9董事账号
        /// </summary>
        public System.Int32? Type { get { return this._Type; } set { this._Type = value ?? default(System.Int32); } }

        private System.DateTime? _EntryTime;
        /// <summary>
        /// 入职时间
        /// </summary>
        public System.DateTime? EntryTime { get { return this._EntryTime; } set { this._EntryTime = value ?? default(System.DateTime); } }

        private System.String _NameUrl;
        /// <summary>
        /// 
        /// </summary>
        public System.String NameUrl { get { return this._NameUrl; } set { this._NameUrl = value?.Trim(); } }

        private System.DateTime? _Birthday;
        /// <summary>
        /// 生日
        /// </summary>
        public System.DateTime? Birthday { get { return this._Birthday; } set { this._Birthday = value ?? default(System.DateTime); } }

        private System.Int32? _Wages;
        /// <summary>
        /// 基本工资
        /// </summary>
        public System.Int32? Wages { get { return this._Wages; } set { this._Wages = value ?? default(System.Int32); } }

        private System.String _KpiLevel;
        /// <summary>
        /// KPI等级
        /// </summary>
        public System.String KpiLevel { get { return this._KpiLevel; } set { this._KpiLevel = value?.Trim(); } }

        private System.String _Address;
        /// <summary>
        /// 地址
        /// </summary>
        public System.String Address { get { return this._Address; } set { this._Address = value?.Trim(); } }

        private System.String _IdentityNum;
        /// <summary>
        /// 身份证
        /// </summary>
        public System.String IdentityNum { get { return this._IdentityNum; } set { this._IdentityNum = value?.Trim(); } }

        private System.String _Education;
        /// <summary>
        /// 专业
        /// </summary>
        public System.String Education { get { return this._Education; } set { this._Education = value?.Trim(); } }

        private System.String _BankCard;
        /// <summary>
        /// 工资卡
        /// </summary>
        public System.String BankCard { get { return this._BankCard; } set { this._BankCard = value?.Trim(); } }

        private System.String _Remark;
        /// <summary>
        /// 备注
        /// </summary>
        public System.String Remark { get { return this._Remark; } set { this._Remark = value?.Trim(); } }

        private System.DateTime? _LastLoginTime;
        /// <summary>
        /// 最后一次登陆时间
        /// </summary>
        public System.DateTime? LastLoginTime { get { return this._LastLoginTime; } set { this._LastLoginTime = value ?? default(System.DateTime); } }

        private System.DateTime? _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? CreateTime { get { return this._CreateTime; } set { this._CreateTime = value ?? default(System.DateTime); } }

        private System.String _PurchaseAccount;
        /// <summary>
        /// 采购默认账号
        /// </summary>
        public System.String PurchaseAccount { get { return this._PurchaseAccount; } set { this._PurchaseAccount = value?.Trim(); } }

        private System.Int32? _Bonus;
        /// <summary>
        /// 绩效奖金
        /// </summary>
        public System.Int32? Bonus { get { return this._Bonus; } set { this._Bonus = value ?? default(System.Int32); } }

        private System.Boolean? _IsTestUser;
        /// <summary>
        /// 是否测试用户
        /// </summary>
        public System.Boolean? IsTestUser { get { return this._IsTestUser; } set { this._IsTestUser = value ?? default(System.Boolean); } }

        private System.String _GraduateSchool;
        /// <summary>
        /// 毕业院校
        /// </summary>
        public System.String GraduateSchool { get { return this._GraduateSchool; } set { this._GraduateSchool = value?.Trim(); } }

        private System.DateTime? _GraduateTime;
        /// <summary>
        /// 毕业时间
        /// </summary>
        public System.DateTime? GraduateTime { get { return this._GraduateTime; } set { this._GraduateTime = value ?? default(System.DateTime); } }

        private System.String _ResidenceAddress;
        /// <summary>
        /// 户籍地址
        /// </summary>
        public System.String ResidenceAddress { get { return this._ResidenceAddress; } set { this._ResidenceAddress = value?.Trim(); } }

        private System.String _CurrentAddress;
        /// <summary>
        /// 现居地址
        /// </summary>
        public System.String CurrentAddress { get { return this._CurrentAddress; } set { this._CurrentAddress = value?.Trim(); } }

        private System.String _EmergencyContact;
        /// <summary>
        /// 紧急联系人
        /// </summary>
        public System.String EmergencyContact { get { return this._EmergencyContact; } set { this._EmergencyContact = value?.Trim(); } }

        private System.String _EmergencyContactTel;
        /// <summary>
        /// 紧急联系人电话
        /// </summary>
        public System.String EmergencyContactTel { get { return this._EmergencyContactTel; } set { this._EmergencyContactTel = value?.Trim(); } }

        private System.String _CreateUser;
        /// <summary>
        /// 创建人
        /// </summary>
        public System.String CreateUser { get { return this._CreateUser; } set { this._CreateUser = value?.Trim(); } }

        private System.String _UpdateUser;
        /// <summary>
        /// 更新人
        /// </summary>
        public System.String UpdateUser { get { return this._UpdateUser; } set { this._UpdateUser = value?.Trim(); } }

        private System.DateTime? _UpdateTime;
        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime? UpdateTime { get { return this._UpdateTime; } set { this._UpdateTime = value ?? default(System.DateTime); } }

        private System.String _CurrentEconomize;
        /// <summary>
        /// 现居省
        /// </summary>
        public System.String CurrentEconomize { get { return this._CurrentEconomize; } set { this._CurrentEconomize = value?.Trim(); } }

        private System.String _CurrentCity;
        /// <summary>
        /// 现居市
        /// </summary>
        public System.String CurrentCity { get { return this._CurrentCity; } set { this._CurrentCity = value?.Trim(); } }

        private System.String _CurrentDistrict;
        /// <summary>
        /// 现居区
        /// </summary>
        public System.String CurrentDistrict { get { return this._CurrentDistrict; } set { this._CurrentDistrict = value?.Trim(); } }

        private System.String _ResidenceEconomize;
        /// <summary>
        /// 户籍省
        /// </summary>
        public System.String ResidenceEconomize { get { return this._ResidenceEconomize; } set { this._ResidenceEconomize = value?.Trim(); } }

        private System.String _ResidenceCity;
        /// <summary>
        /// 户籍市
        /// </summary>
        public System.String ResidenceCity { get { return this._ResidenceCity; } set { this._ResidenceCity = value?.Trim(); } }

        private System.String _ResidenceDistrict;
        /// <summary>
        /// 户籍区
        /// </summary>
        public System.String ResidenceDistrict { get { return this._ResidenceDistrict; } set { this._ResidenceDistrict = value?.Trim(); } }

        private System.String _WxUserId;
        /// <summary>
        /// 企业微信UserId
        /// </summary>
        public System.String WxUserId { get { return this._WxUserId; } set { this._WxUserId = value?.Trim(); } }
    }
}