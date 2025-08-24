using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;
using static Rex.Service.Permission.PromotionServices.PromotionServicePermissions;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 拼团商品Dto
    /// </summary>
    public partial class PinTuanGoodDto : EntityDto<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 规则表ID
        /// </summary>
        public Guid RuleId { get; set; }

        /// <summary>
        /// 拼团规则
        /// </summary>
        public PinTuanRuleDto? PinTuanRule { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public Guid GoodId { get; set; }

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