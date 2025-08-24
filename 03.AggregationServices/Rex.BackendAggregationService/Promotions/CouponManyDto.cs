using Rex.PromotionService.Promotions;

namespace Rex.BackendAggregationService.Promotions
{
    /// <summary>
    /// 优惠卷Dto
    /// </summary>
    public class CouponManyDto : CouponDto
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
    }
}