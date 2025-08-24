using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rex.Service.Permission.GoodServices;

namespace Rex.GoodService.Controllers
{
    /// <summary>
    /// 页面设计控制器
    /// </summary>
    [Route("api/good/pageDesign")]
    [Authorize(GoodServicePermissions.PageDesigns.Default)]
    public class PageDesignController : GoodServiceController
    {
        private readonly ILogger<PageDesignController> _logger;

        public PageDesignController(ILogger<PageDesignController> logger)
        {
            _logger = logger;
        }
    }
}