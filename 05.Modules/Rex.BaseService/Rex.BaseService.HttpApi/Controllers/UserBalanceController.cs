using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.BaseService.Controllers
{
    /// <summary>
    /// 用户余额控制器
    /// </summary>
    [Route("api/base/userBalance")]
    public class UserBalanceController : BaseServiceController
    {
        private readonly ILogger<UserBalanceController> _logger;

        public UserBalanceController(ILogger<UserBalanceController> logger)
        {
            _logger = logger;
        }
    }
}