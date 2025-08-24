using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.GoodService.Controllers
{
    /// <summary>
    /// 商品控制器
    /// </summary>
    [Route("api/good/goods")]
    public class GoodsController : GoodServiceController
    {
        private readonly ILogger<GoodsController> _logger;

        public GoodsController(ILogger<GoodsController> logger)
        {
            _logger = logger;
        }
    }
}