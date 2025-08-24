using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 拼团记录表
    /// </summary>
    public partial class PinTuanRecord : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 团序列
        /// </summary>
        [Required]
        public Guid TeamId { get; set; }

        /// <summary>
        /// 用户序列
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// 规则表序列
        /// </summary>
        [Required]
        public Guid RuleId { get; set; }

        /// <summary>
        /// 商品序列
        /// </summary>
        [Required]
        public Guid GoodId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public int Status { get; set; }

        /// <summary>
        /// 订单序列
        /// </summary>
        [Required]
        [StringLength(20)]
        public string OrderId { get; set; }

        /// <summary>
        /// 拼团人数Json
        /// </summary>
        [Display(Name = "拼团人数Json")]
        [StringLength(1500)]
        public string? Parameters { get; set; }

        /// <summary>
        /// 关闭时间
        /// </summary>
        [Required]
        public DateTime CloseTime { get; set; }
    }
}