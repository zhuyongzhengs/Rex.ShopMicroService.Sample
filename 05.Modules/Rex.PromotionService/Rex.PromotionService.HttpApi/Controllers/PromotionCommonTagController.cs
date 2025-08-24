using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.PromotionService.Controllers
{
    /// <summary>
    /// 常用促销控制器
    /// </summary>
    [Route("api/promotion/common/tag")]
    public class PromotionCommonTagController : PromotionServiceController
    {
        private readonly ILogger<PromotionCommonTagController> _logger;

        public PromotionCommonTagController(ILogger<PromotionCommonTagController> logger)
        {
            _logger = logger;
        }
    }
}