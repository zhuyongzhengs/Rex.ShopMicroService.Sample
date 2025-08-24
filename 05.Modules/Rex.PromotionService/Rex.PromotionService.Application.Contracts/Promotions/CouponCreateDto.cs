using System;
using Volo.Abp.Application.Dtos;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 创建优惠劵Dto
    /// </summary>
    public class CouponCreateDto : EntityDto
    {
        /// <summary>
        /// 优惠券编码
        /// </summary>
        public string CouponCode { get; set; }

        /// <summary>
        /// 优惠券id
        /// </summary>
        public Guid PromotionId { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// 谁领取了
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 被谁用了
        /// </summary>
        public Guid? UsedId { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}