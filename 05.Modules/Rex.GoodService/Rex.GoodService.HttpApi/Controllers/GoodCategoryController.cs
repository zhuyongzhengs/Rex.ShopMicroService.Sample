using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.GoodService.Controllers
{
    /// <summary>
    /// 商品分类控制器
    /// </summary>
    [Route("api/good/category")]
    public class GoodCategoryController : GoodServiceController
    {
        private readonly ILogger<GoodCategoryController> _logger;

        public GoodCategoryController(ILogger<GoodCategoryController> logger)
        {
            _logger = logger;
        }
    }
}