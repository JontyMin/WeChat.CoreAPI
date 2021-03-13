using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeChat.Core.Common.WeChat.Model
{
    /// <summary>
    /// 子菜单想
    /// </summary>
    public class Sub_buttonItem
    {
        /// <summary>
        /// 响应动作类型，view表示网页类型，click表示点击类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 子菜单名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 菜单项
    /// </summary>
    public class ButtonItem
    {

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string name { get; set; }

        
        /// <summary>
        /// 子菜单项
        /// </summary>
        public List<Sub_buttonItem> sub_button { get; set; }
    }

    /// <summary>
    /// 创建自定义菜单
    /// </summary>
    public class CreateMenuTree
    {
        /// <summary>
        /// 根
        /// </summary>
        public List<ButtonItem> button { get; set; }

        /// <summary>
        /// 个性化标签
        /// </summary>
        public Matchrule matchrule { get; set; }
    }

    /// <summary>
    /// matchrule共七个字段，均可为空，但不能全部为空，至少要有一个匹配信息是不为空的
    /// </summary>
    public class Matchrule
    {
        /// <summary>
        /// 用户标签的id，可通过用户标签管理接口获取
        /// </summary>
        public string tag_id { get; set; }

        /// <summary>
        /// 性别：男（1）女（2）
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 国家信息，是用户在微信中设置的地区
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// 省份信息，是用户在微信中设置的地区
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 城市信息，是用户在微信中设置的地区
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 客户端版本，当前只具体到系统型号：IOS(1), Android(2),Others(3)
        /// </summary>
        public string client_platform_type { get; set; }

        /// <summary>
        /// 语言信息，是用户在微信中设置的语言
        /// 简体中文 "zh_CN" 2、繁体中文TW "zh_TW" 3、繁体中文HK "zh_HK" 4、英文 "en"
        /// </summary>
        public string language { get; set; }

    }
}