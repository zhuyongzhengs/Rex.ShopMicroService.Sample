using System;
using Volo.Abp.Application.Dtos;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 查询拼团规则
    /// </summary>
    public class GetPinTuanRuleInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 拼团规则名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 是否开启
        /// </summary>
        public bool? IsStatusOpen { get; set; }
    }
}