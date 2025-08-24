using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品分类
    /// </summary>
    public partial class GoodCategory : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 上级分类id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 类型ID 关联 goods_type.id
        /// </summary>
        [Required]
        public Guid TypeId { get; set; }

        /// <summary>
        /// 分类排序
        /// </summary>
        [Required]
        public int Sort { get; set; }

        /// <summary>
        /// 分类图片
        /// </summary>
        [StringLength(255)]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        [Required]
        public bool IsShow { get; set; }
    }
}