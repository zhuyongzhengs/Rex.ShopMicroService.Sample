using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.BaseService.Controllers
{
    /// <summary>
    /// 角色菜单控制器
    /// </summary>
    [Route("api/base/userOrganizationUnit")]
    public class UserOrganizationUnitController : BaseServiceController
    {
        private readonly ILogger<UserOrganizationUnitController> _logger;

        public UserOrganizationUnitController(ILogger<UserOrganizationUnitController> logger)
        {
            _logger = logger;
        }
    }
}