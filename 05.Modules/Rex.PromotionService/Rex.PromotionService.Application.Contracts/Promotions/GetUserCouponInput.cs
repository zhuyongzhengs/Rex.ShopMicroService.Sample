using Volo.Abp.Application.Dtos;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 查询用户优惠劵
    /// </summary>
    public class GetUserCouponInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 优惠券编码
        /// </summary>
        public string? CouponCode { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        /// <remarks>
        /// 1.未使用、2.已使用、3.已失效
        /// </remarks>
        public int? Status { get; set; }
    }
}