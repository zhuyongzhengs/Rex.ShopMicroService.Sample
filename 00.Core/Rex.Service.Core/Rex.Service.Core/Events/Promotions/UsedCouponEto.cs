using System;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Rex.Service.Core.Events.Promotions
{
    /// <summary>
    /// 优惠劵使用事件Eto
    /// </summary>
    public class UsedCouponEto : EntityEto
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public const string EventName = "Used.Coupon";

        /// <summary>
        /// 优惠券编码
        /// </summary>
        public string[] CouponCode { get; set; }

        /// <summary>
        /// 使用者
        /// </summary>
        public Guid? UsedId { get; set; }
    }
}