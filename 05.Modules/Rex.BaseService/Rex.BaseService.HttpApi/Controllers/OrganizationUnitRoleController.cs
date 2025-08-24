using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.BaseService.Controllers
{
    /// <summary>
    /// 角色组织单元控制器
    /// </summary>
    [Route("api/base/organizationUnitRole")]
    public class OrganizationUnitRoleController : BaseServiceController
    {
        private readonly ILogger<OrganizationUnitRoleController> _logger;

        public OrganizationUnitRoleController(ILogger<OrganizationUnitRoleController> logger)
        {
            _logger = logger;
        }
    }
}