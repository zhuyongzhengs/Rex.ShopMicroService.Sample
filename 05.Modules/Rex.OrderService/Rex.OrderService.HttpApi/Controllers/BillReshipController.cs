using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.OrderService.Controllers
{
    /// <summary>
    /// 退货单控制器
    /// </summary>
    [Route("api/order/billReship")]
    public class BillReshipController : OrderServiceController
    {
        private readonly ILogger<BillReshipController> _logger;

        public BillReshipController(ILogger<BillReshipController> logger)
        {
            _logger = logger;
        }
    }
}