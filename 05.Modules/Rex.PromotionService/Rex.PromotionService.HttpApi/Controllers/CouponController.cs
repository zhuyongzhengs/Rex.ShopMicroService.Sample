using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.PromotionService.Controllers
{
    /// <summary>
    /// 优惠劵控制器
    /// </summary>
    [Route("api/promotion/coupon")]
    [Authorize]
    public class CouponController : PromotionServiceController
    {
        private readonly ILogger<CouponController> _logger;

        public CouponController(ILogger<CouponController> logger)
        {
            _logger = logger;
        }
    }
}