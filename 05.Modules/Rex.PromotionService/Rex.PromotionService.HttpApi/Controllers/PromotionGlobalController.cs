using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.PromotionService.Controllers
{
    /// <summary>
    /// 全局促销控制器
    /// </summary>
    [Route("api/promotion/global")]
    public class PromotionGlobalController : PromotionServiceController
    {
        private readonly ILogger<PromotionGlobalController> _logger;

        public PromotionGlobalController(ILogger<PromotionGlobalController> logger)
        {
            _logger = logger;
        }
    }
}