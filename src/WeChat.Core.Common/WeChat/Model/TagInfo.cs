using System.Collections.Generic;

namespace WeChat.Core.Common.WeChat.Model
{
    public class TagsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int count { get; set; }
    }
    /// <summary>
    /// 标签信息
    /// </summary>
    public class TagInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public List<TagsItem> tags { get; set; }
    }
}