using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Brands
{
    /// <summary>
    /// 品牌
    /// </summary>
    public partial class Brand : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 品牌LOGO
        /// </summary>
        [StringLength(255)]
        public string LogoImageUrl { get; set; }

        /// <summary>
        /// 品牌排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        [Required]
        public bool IsShow { get; set; }
    }
}