using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.GoodService.Controllers
{
    /// <summary>
    /// 商品评价控制器
    /// </summary>
    [Route("api/good/comment")]
    public class GoodCommentController : GoodServiceController
    {
        private readonly ILogger<GoodCommentController> _logger;

        public GoodCommentController(ILogger<GoodCommentController> logger)
        {
            _logger = logger;
        }
    }
}