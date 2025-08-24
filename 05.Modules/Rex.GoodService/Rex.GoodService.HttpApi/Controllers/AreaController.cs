using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.GoodService.Controllers
{
    /// <summary>
    /// 区域控制器
    /// </summary>
    [Route("api/good/area")]
    public class AreaController : GoodServiceController
    {
        private readonly ILogger<AreaController> _logger;

        public AreaController(ILogger<AreaController> logger)
        {
            _logger = logger;
        }
    }
}