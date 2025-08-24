using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.GoodService.Controllers
{
    /// <summary>
    /// 商品参数[模型]控制器
    /// </summary>
    [Route("api/good/param")]
    public class GoodParamController : GoodServiceController
    {
        private readonly ILogger<GoodParamController> _logger;

        public GoodParamController(ILogger<GoodParamController> logger)
        {
            _logger = logger;
        }
    }
}