using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.OrderService.Controllers
{
    /// <summary>
    /// 售后单控制器
    /// </summary>
    [Route("api/order/billAftersales")]
    public class BillAftersalesController : OrderServiceController
    {
        private readonly ILogger<BillAftersalesController> _logger;

        public BillAftersalesController(ILogger<BillAftersalesController> logger)
        {
            _logger = logger;
        }
    }
}