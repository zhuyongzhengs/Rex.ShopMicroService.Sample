using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.GoodService.Controllers
{
    /// <summary>
    /// 库存控制器
    /// </summary>
    [Route("api/good/stockLog")]
    [Authorize]
    public class StockLogController : GoodServiceController
    {
        private readonly ILogger<StockLogController> _logger;

        public StockLogController(ILogger<StockLogController> logger)
        {
            _logger = logger;
        }
    }
}