using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.GoodService.Controllers
{
    /// <summary>
    /// 页面设计项控制器
    /// </summary>
    [Route("api/good/pageDesignItem")]
    public class PageDesignItemController : GoodServiceController
    {
        private readonly ILogger<PageDesignItemController> _logger;

        public PageDesignItemController(ILogger<PageDesignItemController> logger)
        {
            _logger = logger;
        }
    }
}