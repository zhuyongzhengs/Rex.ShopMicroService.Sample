using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rex.Service.Permission.GoodServices;

namespace Rex.GoodService.Controllers
{
    /// <summary>
    /// 商品类型规格控制器
    /// </summary>
    [Route("api/good/typeSpec")]
    [Authorize(GoodServicePermissions.GoodTypeSpecs.Default)]
    public class GoodTypeSpecController : GoodServiceController
    {
        private readonly ILogger<GoodTypeSpecController> _logger;

        public GoodTypeSpecController(ILogger<GoodTypeSpecController> logger)
        {
            _logger = logger;
        }
    }
}