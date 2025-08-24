using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.BaseService.Controllers
{
    /// <summary>
    /// 平台设置控制器
    /// </summary>
    [Route("api/base/platformSetting")]
    public class PlatformSettingController : BaseServiceController
    {
        private readonly ILogger<PlatformSettingController> _logger;

        public PlatformSettingController(ILogger<PlatformSettingController> logger)
        {
            _logger = logger;
        }
    }
}