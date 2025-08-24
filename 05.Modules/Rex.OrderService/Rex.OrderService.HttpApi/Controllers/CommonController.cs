using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.OrderService.Controllers
{
    /// <summary>
    /// 公共控制器
    /// </summary>
    [Route("api/order/common")]
    public class CommonController : OrderServiceController
    {
        private readonly ILogger<CommonController> _logger;

        public CommonController(ILogger<CommonController> logger)
        {
            _logger = logger;
        }
    }
}