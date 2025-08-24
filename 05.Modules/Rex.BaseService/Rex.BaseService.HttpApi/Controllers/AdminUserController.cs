using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.BaseService.Controllers
{
    /// <summary>
    /// 管理员用户控制器
    /// </summary>
    [Route("api/base/adminUser")]
    public class AdminUserController : BaseServiceController
    {
        private readonly ILogger<AdminUserController> _logger;

        public AdminUserController(ILogger<AdminUserController> logger)
        {
            _logger = logger;
        }
    }
}