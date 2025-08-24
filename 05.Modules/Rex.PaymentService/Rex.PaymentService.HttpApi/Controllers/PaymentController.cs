using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.PaymentService.Controllers
{
    /// <summary>
    /// 支付控制器
    /// </summary>
    [Route("api/payment")]
    public class PaymentController : PaymentServiceController
    {
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(ILogger<PaymentController> logger)
        {
            _logger = logger;
        }
    }
}