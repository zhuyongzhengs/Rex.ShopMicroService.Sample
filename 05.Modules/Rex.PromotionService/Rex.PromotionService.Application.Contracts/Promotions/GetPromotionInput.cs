using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 查询促销
    /// </summary>
    public class GetPromotionInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 类型
        /// </summary>
        public int[] Types { get; set; } = [];

        /// <summary>
        /// 促销名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 是否开启
        /// </summary>
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 是否排他
        /// </summary>
        public bool? IsExclusive { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 起止时间
        /// </summary>
        public List<DateTime> StartAndEndTime { get; set; } = new();
    }
}