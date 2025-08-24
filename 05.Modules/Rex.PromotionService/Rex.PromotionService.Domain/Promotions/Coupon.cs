using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 优惠劵表
    /// </summary>
    public class Coupon : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 优惠券编码
        /// </summary>
        [Required]
        [StringLength(20)]
        public string CouponCode { get; set; }

        /// <summary>
        /// (促销)优惠券id
        /// </summary>
        [Required]
        public Guid PromotionId { get; set; }

        /// <summary>
        /// (促销)优惠券
        /// </summary>
        [ForeignKey(nameof(PromotionId))]
        public Promotion? Promotion { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        [Required]
        public bool IsUsed { get; set; }

        /// <summary>
        /// 谁领取了
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// 被谁用了
        /// </summary>
        [StringLength(50)]
        public Guid? UsedId { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(500)]
        public string? Remark { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required]
        public DateTime EndTime { get; set; }
    }
}