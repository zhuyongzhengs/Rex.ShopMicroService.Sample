using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.GoodService.Controllers
{
    /// <summary>
    /// 品牌控制器
    /// </summary>
    [Route("api/good/brand")]
    public class BrandController : GoodServiceController
    {
        private readonly ILogger<BrandController> _logger;

        public BrandController(ILogger<BrandController> logger)
        {
            _logger = logger;
        }
    }
}