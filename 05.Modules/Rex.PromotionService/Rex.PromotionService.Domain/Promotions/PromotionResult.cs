using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 促销结果表
    /// </summary>
    public class PromotionResult : Entity<Guid>, IMultiTenant, IHasConcurrencyStamp
    {
        /// <summary>
        /// 促销结果
        /// </summary>
        public PromotionResult() { }

        /// <summary>
        /// 促销结果表
        /// </summary>
        /// <param name="id">ID</param>
        public PromotionResult(Guid id)
        { 
            Id = id;
        }

        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 促销ID
        /// </summary>
        public Guid? PromotionId { get; set; }

        /// <summary>
        /// 促销
        /// </summary>
        [ForeignKey(nameof(PromotionId))]
        public Promotion? Promotion { get; set; }

        /// <summary>
        /// 促销条件编码
        /// </summary>
        [StringLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// 支付配置参数序列号存储
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}