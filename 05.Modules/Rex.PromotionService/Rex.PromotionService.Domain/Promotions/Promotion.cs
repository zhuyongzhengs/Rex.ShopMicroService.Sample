using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 促销表
    /// </summary>
    public class Promotion : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 促销名称
        /// </summary>
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public int Type { get; set; }

        /// <summary>
        /// 权重
        /// </summary>
        [Required]
        public int Weight { get; set; }

        /// <summary>
        /// 其它参数
        /// </summary>
        public string? Parameters { get; set; }

        /// <summary>
        /// 每人限购数量
        /// </summary>
        [Required]
        public int MaxNums { get; set; }

        /// <summary>
        /// 每个商品活动数量
        /// </summary>
        [Required]
        public int MaxGoodNums { get; set; }

        /// <summary>
        /// 最大领取数量
        /// </summary>
        [Required]
        public int MaxRecevieNums { get; set; }

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

        /// <summary>
        /// 是否排他
        /// </summary>
        [Required]
        public bool IsExclusive { get; set; }

        /// <summary>
        /// 是否自动领取
        /// </summary>
        [Required]
        public bool IsAutoReceive { get; set; }

        /// <summary>
        /// 是否开启
        /// </summary>
        [Required]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 有效天数
        /// </summary>
        [Required]
        public int EffectiveDays { get; set; }

        /// <summary>
        /// 有效小时
        /// </summary>
        [Required]
        public int EffectiveHours { get; set; }

        /// <summary>
        /// 促销条件
        /// </summary>
        public List<PromotionCondition> PromotionConditions { get; set; } = new();

        /// <summary>
        /// 促销结果
        /// </summary>
        public List<PromotionResult> PromotionResults { get; set; } = new();
    }
}