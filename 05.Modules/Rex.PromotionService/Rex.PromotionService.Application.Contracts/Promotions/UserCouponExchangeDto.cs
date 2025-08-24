using System;
using Volo.Abp.Application.Dtos;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 用户(可用)优惠卷Dto
    /// </summary>
    public class UserCouponExchangeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 是否使用优惠卷
        /// </summary>
        public bool IsUseCoupon { get; set; }

        /// <summary>
        /// 优惠卷编码
        /// </summary>
        public string CouponCode { get; set; }

        /// <summary>
        /// 优惠卷名称
        /// </summary>
        public string CouponName { get; set; }

        /// <summary>
        /// 促销条件编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 支付配置参数序列号存储
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// 优惠卷抵扣的金额
        /// </summary>
        public decimal Money { get; set; }

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