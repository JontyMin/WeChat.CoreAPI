using System;
using System.Collections.Generic;

namespace WeChat.Core.Model.ViewModel
{
    /// <summary>
    /// 供应产品模型
    /// </summary>
    public class SupplyProductViewModel
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 中文名称
        /// </summary>
        public string Cname { get; set; } = "产品中文描述";

        /// <summary>
        /// 英文名称
        /// </summary>
        public string Uname { get; set; } = "产品英文描述";

        /// <summary>
        /// 属性
        /// </summary>
        public string Property { get; set; } = "产品属性";
        /// <summary>
        /// 重量
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public double Length { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// 报价
        /// </summary>
        public decimal Quote { get; set; }
        /// <summary>
        /// 产品链接
        /// </summary>
        public string ProductLink { get; set; }
        /// <summary>
        /// 店铺链接
        /// </summary>
        public string ShopLink { get; set; }
        /// <summary>
        /// 中文描述
        /// </summary>
        public string Cdescribe { get; set; }
        /// <summary>
        /// 英文描述
        /// </summary>
        public string Udescribe { get; set; }

        /// <summary>
        /// 产品状态(默认上架)状态：0：删除，1:上架，2：下架  
        /// </summary>
        public int State { get; set; } = (int)ProductState.Active;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 图片链接
        /// </summary>
        public List<ImageViewModel> Images { get; set; }
        /// <summary>
        /// 类目名称
        /// </summary>
        public string CategoryName { get; set; }
    }
}