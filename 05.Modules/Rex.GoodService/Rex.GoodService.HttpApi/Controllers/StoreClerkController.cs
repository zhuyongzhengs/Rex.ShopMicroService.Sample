using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.GoodService.Controllers
{
    /// <summary>
    /// 店铺店员关联控制器
    /// </summary>
    [Route("api/good/storeClerk")]
    public class StoreClerkController : GoodServiceController
    {
        private readonly ILogger<StoreClerkController> _logger;

        public StoreClerkController(ILogger<StoreClerkController> logger)
        {
            _logger = logger;
        }
    }
}