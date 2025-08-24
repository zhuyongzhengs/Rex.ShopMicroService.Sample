using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.OrderService.Controllers
{
    /// <summary>
    /// 配送方式控制器
    /// </summary>
    [Route("api/order/ship")]
    public class ShipController : OrderServiceController
    {
        private readonly ILogger<ShipController> _logger;

        public ShipController(ILogger<ShipController> logger)
        {
            _logger = logger;
        }
    }
}