using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.BaseService.Controllers
{
    /// <summary>
    /// 用户(收货)地址控制器
    /// </summary>
    [Route("api/base/userShip")]
    public class UserShipController : BaseServiceController
    {
        private readonly ILogger<UserShipController> _logger;

        public UserShipController(ILogger<UserShipController> logger)
        {
            _logger = logger;
        }
    }
}