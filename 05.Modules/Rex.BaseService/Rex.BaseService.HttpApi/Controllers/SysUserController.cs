using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.BaseService.Controllers
{
    /// <summary>
    /// [注册]用户控制器
    /// </summary>
    [Route("api/base/sysUser")]
    public class SysUserController : BaseServiceController
    {
        private readonly ILogger<SysUserController> _logger;

        public SysUserController(ILogger<SysUserController> logger)
        {
            _logger = logger;
        }
    }
}