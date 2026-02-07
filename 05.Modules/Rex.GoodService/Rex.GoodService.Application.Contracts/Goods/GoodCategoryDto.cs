using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品分类Dto
    /// </summary>
    public class GoodCategoryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 上级分类id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型ID 关联 goods_type.id
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// 分类排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 分类图片ID
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreationTime { get; set; }
    }
}