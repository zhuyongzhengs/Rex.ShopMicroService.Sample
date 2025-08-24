using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.BaseService.Controllers
{
    /// <summary>
    /// 审计日志控制器
    /// </summary>
    [Route("api/base/auditLogging")]
    public class AuditLoggingController : BaseServiceController
    {
        private readonly ILogger<AuditLoggingController> _logger;

        public AuditLoggingController(ILogger<AuditLoggingController> logger)
        {
            _logger = logger;
        }
    }
}