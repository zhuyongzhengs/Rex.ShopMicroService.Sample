using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.OrderService.Logisticss
{
    /// <summary>
    /// 物流表
    /// </summary>
    public class Logistics : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 物流公司编码
        /// </summary>
        [Required]
        [StringLength(50)]
        public string LogiCode { get; set; }

        /// <summary>
        /// 物流公司名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string LogiName { get; set; }

        /// <summary>
        /// 物流logo
        /// </summary>
        [StringLength(500)]
        public string? ImgUrl { get; set; }

        /// <summary>
        /// 物流电话
        /// </summary>
        [StringLength(100)]
        public string? Phone { get; set; }

        /// <summary>
        /// 物流网址
        /// </summary>
        [StringLength(240)]
        public string? Url { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}