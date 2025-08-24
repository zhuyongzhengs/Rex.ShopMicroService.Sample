using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.GoodService.Controllers
{
    /// <summary>
    /// 服务商品控制器
    /// </summary>
    [Route("api/good/serviceGood")]
    public class ServiceGoodController : GoodServiceController
    {
        private readonly ILogger<ServiceGoodController> _logger;

        public ServiceGoodController(ILogger<ServiceGoodController> logger)
        {
            _logger = logger;
        }
    }
}