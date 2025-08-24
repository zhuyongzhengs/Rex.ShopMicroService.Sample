using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 查询优惠劵
    /// </summary>
    public class GetCouponInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 优惠券id
        /// </summary>
        public Guid? PromotionId { get; set; }

        /// <summary>
        /// 优惠券编码
        /// </summary>
        public string? CouponCode { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool? IsUsed { get; set; }

        /// <summary>
        /// 起止时间
        /// </summary>
        public List<DateTime> StartAndEndTime { get; set; } = new();
    }
}