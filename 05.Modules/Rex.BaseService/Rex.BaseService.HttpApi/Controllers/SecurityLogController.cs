using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.BaseService.Controllers
{
    /// <summary>
    /// 安全日志控制器
    /// </summary>
    [Route("api/base/securityLog")]
    public class SecurityLogController : BaseServiceController
    {
        private readonly ILogger<SecurityLogController> _logger;

        public SecurityLogController(ILogger<SecurityLogController> logger)
        {
            _logger = logger;
        }
    }
}