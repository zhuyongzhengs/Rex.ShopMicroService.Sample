using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.BaseService.Controllers
{
    /// <summary>
    /// 菜单控制器
    /// </summary>
    [Route("api/base/menu")]
    public class MenuController : BaseServiceController
    {
        private readonly ILogger<MenuController> _logger;

        public MenuController(ILogger<MenuController> logger)
        {
            _logger = logger;
        }
    }
}