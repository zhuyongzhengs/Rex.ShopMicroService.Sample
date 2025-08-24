using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.PromotionService.Controllers
{
    /// <summary>
    /// 拼团记录控制器
    /// </summary>
    [Route("api/promotion/pinTuanRecord")]
    public class PinTuanRecordController : PromotionServiceController
    {
        private readonly ILogger<PinTuanRecordController> _logger;

        public PinTuanRecordController(ILogger<PinTuanRecordController> logger)
        {
            _logger = logger;
        }
    }
}