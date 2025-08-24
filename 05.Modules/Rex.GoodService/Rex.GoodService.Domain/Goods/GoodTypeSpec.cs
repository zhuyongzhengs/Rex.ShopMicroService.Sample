using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品类型规格表
    /// </summary>
    public partial class GoodTypeSpec : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 规格名称
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// 规格排序
        /// </summary>
        [Required]
        public int Sort { get; set; }

        /// <summary>
        /// 子类
        /// </summary>
        public List<GoodTypeSpecValue> SpecValues { get; set; } = new();
    }
}