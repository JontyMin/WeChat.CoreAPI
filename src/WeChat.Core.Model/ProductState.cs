namespace WeChat.Core.Model
{
    /// <summary>
    /// 产品状态
    /// </summary>
    public enum ProductState
    {
        /// <summary>
        /// 已删除
        /// </summary>
        Delete=0,
        /// <summary>
        /// 上架中
        /// </summary>
        Active=1,
        /// <summary>
        /// 下架中
        /// </summary>
        Paused=2    
    }
}