using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.BaseService.Controllers
{
    /// <summary>
    /// 微信用户控制器
    /// </summary>
    [Route("api/base/userWeChat")]
    public class UserWeChatController : BaseServiceController
    {
        private readonly ILogger<UserWeChatController> _logger;

        public UserWeChatController(ILogger<UserWeChatController> logger)
        {
            _logger = logger;
        }
    }
}