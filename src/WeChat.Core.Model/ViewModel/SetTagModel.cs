namespace WeChat.Core.Model.ViewModel
{
    /// <summary>
    /// 设置标签模型
    /// </summary>
    public class SetTagModel
    {
        public string OpenId { get; set; }
        public int TagId { get; set; }

        public int Type { get; set; }
    }
}