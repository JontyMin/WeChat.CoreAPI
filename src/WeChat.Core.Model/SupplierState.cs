namespace WeChat.Core.Model
{
    /// <summary>
    /// 供应商状态
    /// </summary>
    public enum SupplierState
    {
        /// <summary>
        /// 待审核
        /// </summary>
        Pending,
        /// <summary>
        /// 入驻中
        /// </summary>
        Settled,
        /// <summary>
        /// 已禁用
        /// </summary>
        Disabled
    }
}