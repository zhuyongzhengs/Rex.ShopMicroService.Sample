using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.OrderService.Controllers
{
    /// <summary>
    /// 售后单明细控制器
    /// </summary>
    [Route("api/order/billAftersalesItem")]
    public class BillAftersalesItemController : OrderServiceController
    {
        private readonly ILogger<BillAftersalesItemController> _logger;

        public BillAftersalesItemController(ILogger<BillAftersalesItemController> logger)
        {
            _logger = logger;
        }
    }
}