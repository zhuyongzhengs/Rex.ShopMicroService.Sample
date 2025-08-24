using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.OrderService.Controllers
{
    /// <summary>
    /// 订单日志控制器
    /// </summary>
    [Route("api/order/log")]
    [Authorize]
    public class OrderLogController : OrderServiceController
    {
        private readonly ILogger<OrderLogController> _logger;

        public OrderLogController(ILogger<OrderLogController> logger)
        {
            _logger = logger;
        }
    }
}