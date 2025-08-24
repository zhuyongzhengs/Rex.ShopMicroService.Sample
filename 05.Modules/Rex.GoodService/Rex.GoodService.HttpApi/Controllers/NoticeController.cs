using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.GoodService.Controllers
{
    /// <summary>
    /// 公告控制器
    /// </summary>
    [Route("api/good/notice")]
    public class NoticeController : GoodServiceController
    {
        private readonly ILogger<NoticeController> _logger;

        public NoticeController(ILogger<NoticeController> logger)
        {
            _logger = logger;
        }
    }
}