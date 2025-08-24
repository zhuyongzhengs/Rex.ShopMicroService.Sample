using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Areas
{
    /// <summary>
    /// 区域
    /// </summary>
    public class Area : FullAuditedAggregateRoot<long>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        [DefaultValue(0)]
        public long? ParentId { get; set; }

        /// <summary>
        /// 父级区域
        /// </summary>
        [ForeignKey(nameof(ParentId))]
        public Area? ParentArea { get; set; }

        /// <summary>
        /// 地区深度
        /// </summary>
        public int? Depth { get; set; }

        /// <summary>
        /// 地区名称
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [StringLength(10)]
        public string? PostalCode { get; set; }

        /// <summary>
        /// 地区排序
        /// </summary>
        [Required]
        public int Sort { get; set; }
    }
}