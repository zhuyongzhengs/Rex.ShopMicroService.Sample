using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Labels
{
    /// <summary>
    /// 标签表
    /// </summary>
    public partial class Label : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 标签样式
        /// </summary>
        [StringLength(20)]
        public string Style { get; set; }
    }
}