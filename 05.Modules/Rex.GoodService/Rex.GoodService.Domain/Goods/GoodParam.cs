using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品参数表
    /// </summary>
    public class GoodParam : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        [StringLength(10)]
        public string Type { get; set; }
    }
}