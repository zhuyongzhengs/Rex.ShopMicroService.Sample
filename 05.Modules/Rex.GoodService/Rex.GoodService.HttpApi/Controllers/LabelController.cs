using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.GoodService.Controllers
{
    /// <summary>
    /// 标签控制器
    /// </summary>
    [Route("api/good/label")]
    public class LabelController : GoodServiceController
    {
        private readonly ILogger<LabelController> _logger;

        public LabelController(ILogger<LabelController> logger)
        {
            _logger = logger;
        }
    }
}