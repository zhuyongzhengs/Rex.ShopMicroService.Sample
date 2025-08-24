using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 支付方式表
    /// </summary>
    public class Payment : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 支付类型名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 支付类型编码
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 是否线上支付
        /// </summary>
        [Required]
        public bool IsOnline { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public string? Parameters { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 方式描述
        /// </summary>
        [StringLength(200)]
        public string? Memo { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}
