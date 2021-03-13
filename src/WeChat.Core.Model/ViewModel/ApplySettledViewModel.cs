using System;

namespace WeChat.Core.Model.ViewModel
{
    /// <summary>
    /// 供应商入驻模型
    /// </summary>
    public class ApplySettledViewModel
    {
        /// <summary>
        /// 联系人真实姓名
        /// </summary>
        public System.String RealName { get; set; }

        /// <summary>
        /// 供应商公司
        /// </summary>
        public System.String CompanyName { get; set; }

        /// <summary>
        /// 统一社会信用代码
        /// </summary>
        public System.String CreditCode { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        public System.String MobilePhone { get; set; }

        /// <summary>
        /// 公司固话
        /// </summary>
        public System.String FixPhone { get; set; } = "";

        /// <summary>
        /// 供应商状态 0：未审核，1：入驻中，2：已禁用
        /// </summary>
        public System.Int32? State { get; set; } = 0;
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? CreateTime { get; set; }=DateTime.Now;
        
        /// <summary>
        /// 协议是否已读
        /// </summary>
        public System.Boolean? IsReadAgreement { get; set; } = true;

        /// <summary>
        /// 用户OpenId
        /// </summary>
        public System.String OpenId { get; set; }

        /// <summary>
        /// AccessToken
        /// </summary>
        public System.String AccessToken { get; set; }
    }
}