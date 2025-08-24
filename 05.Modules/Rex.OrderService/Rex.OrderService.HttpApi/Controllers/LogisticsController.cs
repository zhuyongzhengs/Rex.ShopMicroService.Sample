using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.OrderService.Controllers
{
    /// <summary>
    /// 物流控制器
    /// </summary>
    [Route("api/order/logistics")]
    public class LogisticsController : OrderServiceController
    {
        private readonly ILogger<LogisticsController> _logger;

        public LogisticsController(ILogger<LogisticsController> logger)
        {
            _logger = logger;
        }
    }
}