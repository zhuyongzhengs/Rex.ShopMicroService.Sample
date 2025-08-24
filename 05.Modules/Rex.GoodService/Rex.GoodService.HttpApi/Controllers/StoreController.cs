using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.GoodService.Controllers
{
    /// <summary>
    /// 门店控制器
    /// </summary>
    [Route("api/good/store")]
    public class StoreController : GoodServiceController
    {
        private readonly ILogger<StoreController> _logger;

        public StoreController(ILogger<StoreController> logger)
        {
            _logger = logger;
        }
    }
}