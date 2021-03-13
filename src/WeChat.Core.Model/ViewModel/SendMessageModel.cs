namespace WeChat.Core.Model.ViewModel
{
    /// <summary>
    /// 发送消息模型类
    /// </summary>
    public class SendMessageModel
    {
        /// <summary>
        /// 消息类型 1:审核提交通知 2:待审核通知 3:审核通过通知
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 接收者openId
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 邀请码
        /// </summary>
        public string Code { get; set; }
    }
}