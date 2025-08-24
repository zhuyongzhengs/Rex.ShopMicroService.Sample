using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.OrderService.Controllers
{
    /// <summary>
    /// 售后单图片关联控制器
    /// </summary>
    [Route("api/order/billAftersalesImages")]
    public class BillAftersalesImagesController : OrderServiceController
    {
        private readonly ILogger<BillAftersalesImagesController> _logger;

        public BillAftersalesImagesController(ILogger<BillAftersalesImagesController> logger)
        {
            _logger = logger;
        }
    }
}