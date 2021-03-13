using System.Collections.Generic;

namespace WeChat.Core.Common.WeChat.Model
{
    /// <summary>
    /// 批量设置用户标签(通过设置标签控制菜单权限)
    /// </summary>
    public class TagSetting
    {
        /// <summary>
        /// 粉丝openid列表
        /// </summary>
        public List<string> openid_list { get; set; }
        /// <summary>
        /// 标签id(获取已创建的标签查看)
        /// </summary>
        public int tagid { get; set; }  
    }
}