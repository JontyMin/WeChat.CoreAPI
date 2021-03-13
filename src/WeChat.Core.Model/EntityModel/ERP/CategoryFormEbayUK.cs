using SqlSugar;
using System;

namespace WeChat.Core.Model.EntityModel.ERP
{
    /// <summary>
    /// [供应链_OA_产品资料_江忠林]分类表(同步自EbayHK)
    /// </summary>
    public class CategoryFormEbayUK
    {
        /// <summary>
        /// [供应链_OA_产品资料_江忠林]分类表(同步自EbayHK)
        /// </summary>
        public CategoryFormEbayUK()
        {
        }

        /// <summary>
        /// ID
        /// </summary>
        public System.Int32 ID { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public System.Int32 CategoryID { get; set; }

        /// <summary>
        /// 分类父ID
        /// </summary>
        public System.Int32 ParentID { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public System.String Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public System.Int32? SortOrder { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public System.String UpdateUser { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime? UpdateDatetime { get; set; }

        /// <summary>
        /// 中文描述
        /// </summary>
        public System.String CNDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int64? LastSyncTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? CreatorUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? DeleterUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? DeletionTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? IsDeleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte[] ModifyTimeStamp { get; set; }

        /// <summary>
        /// 根类目ID
        /// </summary>
        public System.Int32? RootCategoryId { get; set; }

        /// <summary>
        /// 根类目名称
        /// </summary>
        public System.String RootCategoryName { get; set; }

        /// <summary>
        /// 是否存在父节点
        /// </summary>
        public System.Boolean? IsHasChildren { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String DutyUser { get; set; }
    }
}
