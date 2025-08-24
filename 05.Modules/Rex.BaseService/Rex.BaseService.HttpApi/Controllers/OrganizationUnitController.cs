using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.BaseService.Controllers
{
    /// <summary>
    /// 组织单元控制器
    /// </summary>
    [Route("api/base/organizationUnit")]
    public class OrganizationUnitController : BaseServiceController
    {
        private readonly ILogger<OrganizationUnitController> _logger;

        public OrganizationUnitController(ILogger<OrganizationUnitController> logger)
        {
            _logger = logger;
        }
    }
}