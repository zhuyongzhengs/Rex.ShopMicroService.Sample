using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.BaseService.Controllers
{
    /// <summary>
    /// 角色菜单控制器
    /// </summary>
    [Route("api/base/roleMenu")]
    public class RoleMenuController : BaseServiceController
    {
        private readonly ILogger<RoleMenuController> _logger;

        public RoleMenuController(ILogger<RoleMenuController> logger)
        {
            _logger = logger;
        }
    }
}