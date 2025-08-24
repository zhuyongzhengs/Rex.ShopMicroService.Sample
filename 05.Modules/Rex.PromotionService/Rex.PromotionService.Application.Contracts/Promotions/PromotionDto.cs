using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 促销Dto
    /// </summary>
    public class PromotionDto : EntityDto<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 促销名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 权重
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// 其它参数
        /// </summary>
        public string? Parameters { get; set; }

        /// <summary>
        /// 每人限购数量
        /// </summary>
        public int MaxNums { get; set; }

        /// <summary>
        /// 每个商品活动数量
        /// </summary>
        public int MaxGoodNums { get; set; }

        /// <summary>
        /// 最大领取数量
        /// </summary>
        public int MaxRecevieNums { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 是否排他
        /// </summary>
        public bool IsExclusive { get; set; }

        /// <summary>
        /// 是否自动领取
        /// </summary>
        public bool IsAutoReceive { get; set; }

        /// <summary>
        /// 是否开启
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 有效天数
        /// </summary>
        public int EffectiveDays { get; set; }

        /// <summary>
        /// 有效小时
        /// </summary>
        public int EffectiveHours { get; set; }

        /// <summary>
        /// 促销条件
        /// </summary>
        public List<PromotionConditionDto> PromotionConditions { get; set; } = new();

        /// <summary>
        /// 促销结果
        /// </summary>
        public List<PromotionResultDto> PromotionResults { get; set; } = new();

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreationTime { get; set; }
    }
}