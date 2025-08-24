using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 拼团规则表
    /// </summary>
    public partial class PinTuanRule : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

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
        /// 人数2-10人
        /// </summary>
        [Required]
        public int PeopleNumber { get; set; }

        /// <summary>
        /// 单位分钟
        /// </summary>
        [Required]
        public int SignificantInterval { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        [Required]
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 每人限购数量
        /// </summary>
        [Required]
        public int MaxNums { get; set; }

        /// <summary>
        /// 每个商品活动数量
        /// </summary>
        [Required]
        public int MaxGoodsNums { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int Sort { get; set; }

        /// <summary>
        /// 是否开启
        /// </summary>
        [Required]
        public bool IsStatusOpen { get; set; }

        /// <summary>
        /// 拼团商品
        /// </summary>
        public List<PinTuanGood> PinTuanGoods { get; set; } = new();
    }
}