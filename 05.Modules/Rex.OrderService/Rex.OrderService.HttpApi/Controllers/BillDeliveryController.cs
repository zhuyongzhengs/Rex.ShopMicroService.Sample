using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.OrderService.Controllers
{
    /// <summary>
    /// 发货单控制器
    /// </summary>
    [Route("api/order/billDelivery")]
    public class BillDeliveryController : OrderServiceController
    {
        private readonly ILogger<BillDeliveryController> _logger;

        public BillDeliveryController(ILogger<BillDeliveryController> logger)
        {
            _logger = logger;
        }
    }
}