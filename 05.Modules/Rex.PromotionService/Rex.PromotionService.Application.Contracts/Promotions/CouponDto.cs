using System;
using Volo.Abp.Application.Dtos;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 优惠劵Dto
    /// </summary>
    public class CouponDto : EntityDto<Guid>
    {
        /// <summary>
        /// 优惠券编码
        /// </summary>
        public string CouponCode { get; set; }

        /// <summary>
        /// (促销)优惠券id
        /// </summary>
        public Guid PromotionId { get; set; }

        /// <summary>
        /// (促销)优惠券
        /// </summary>
        public PromotionDto? Promotion { get; set; }

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

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? LastModificationTime { get; set; }
    }
}