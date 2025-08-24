using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品图片关联表
    /// </summary>
    public class GoodImage : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        public Guid GoodId { get; set; }

        /// <summary>
        /// 图片ID
        /// </summary>
        [Required]
        [StringLength(50)]
        public Guid ImageId { get; set; }

        /// <summary>
        /// 图片排序
        /// </summary>
        [Required]
        public int Sort { get; set; }
    }
}