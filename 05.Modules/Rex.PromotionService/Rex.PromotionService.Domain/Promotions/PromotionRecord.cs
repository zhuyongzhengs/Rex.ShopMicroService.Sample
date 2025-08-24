using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 促销活动记录表
    /// </summary>
    public class PromotionRecord : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 促销序列
        /// </summary>
        [Required]
        public Guid PromotionId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// 商品id
        /// </summary>
        [Required]
        public Guid GoodId { get; set; }

        /// <summary>
        /// 货品id
        /// </summary>
        [Required]
        public Guid ProductId { get; set; }

        /// <summary>
        /// 订单id
        /// </summary>
        [Required]
        public Guid OrderId { get; set; }

        /// <summary>
        /// 3团购/4促销
        /// </summary>
        [Required]
        public int Type { get; set; }
    }
}