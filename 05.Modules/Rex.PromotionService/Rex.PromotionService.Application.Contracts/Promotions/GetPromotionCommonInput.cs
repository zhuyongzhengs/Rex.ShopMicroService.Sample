using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 查询常用促销
    /// </summary>
    public class GetPromotionCommonInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 促销名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 是否排他
        /// </summary>
        public bool? IsExclusive { get; set; }

        /// <summary>
        /// 起止时间
        /// </summary>
        public List<DateTime> StartAndEndTime { get; set; } = new();
    }
}