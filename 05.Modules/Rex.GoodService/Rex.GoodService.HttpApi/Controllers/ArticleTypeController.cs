using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rex.Service.Permission.GoodServices;

namespace Rex.GoodService.Controllers
{
    /// <summary>
    /// 文章分类控制器
    /// </summary>
    [Route("api/good/articleType")]
    [Authorize(GoodServicePermissions.ArticleTypes.Default)]
    public class ArticleTypeController : GoodServiceController
    {
        private readonly ILogger<ArticleTypeController> _logger;

        public ArticleTypeController(ILogger<ArticleTypeController> logger)
        {
            _logger = logger;
        }
    }
}