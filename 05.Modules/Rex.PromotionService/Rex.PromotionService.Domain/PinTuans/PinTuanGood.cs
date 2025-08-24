using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 拼团商品表
    /// </summary>
    public partial class PinTuanGood : Entity<Guid>, IMultiTenant, IHasConcurrencyStamp
    {
        public PinTuanGood()
        { }

        public PinTuanGood(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 规则表序列
        /// </summary>
        [Required]
        public Guid RuleId { get; set; }

        /// <summary>
        /// 拼团规则
        /// </summary>
        [ForeignKey(nameof(RuleId))]
        public PinTuanRule? PinTuanRule { get; set; }

        /// <summary>
        /// 商品序列
        /// </summary>
        [Required]
        public Guid GoodId { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}