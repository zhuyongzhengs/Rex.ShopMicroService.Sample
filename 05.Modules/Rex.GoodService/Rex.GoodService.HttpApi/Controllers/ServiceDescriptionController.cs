using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.GoodService.Controllers
{
    /// <summary>
    /// 商城服务描述控制器
    /// </summary>
    [Route("api/good/serviceDescription")]
    public class ServiceDescriptionController : GoodServiceController
    {
        private readonly ILogger<ServiceDescriptionController> _logger;

        public ServiceDescriptionController(ILogger<ServiceDescriptionController> logger)
        {
            _logger = logger;
        }
    }
}