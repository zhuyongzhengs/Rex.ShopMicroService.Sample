using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Stocks
{
    /// <summary>
    /// 库存操作表
    /// </summary>
    public class Stock : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 来源ID
        /// </summary>
        public Guid SourceId { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 操作员ID
        /// </summary>
        public Guid OperatorId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(200)]
        public string? Memo { get; set; }
    }
}