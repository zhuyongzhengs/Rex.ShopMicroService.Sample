using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.PromotionService.Controllers
{
    /// <summary>
    /// 常用促销控制器
    /// </summary>
    [Route("api/promotion/common")]
    public class CommonController : PromotionServiceController
    {
        private readonly ILogger<CommonController> _logger;

        public CommonController(ILogger<CommonController> logger)
        {
            _logger = logger;
        }
    }
}