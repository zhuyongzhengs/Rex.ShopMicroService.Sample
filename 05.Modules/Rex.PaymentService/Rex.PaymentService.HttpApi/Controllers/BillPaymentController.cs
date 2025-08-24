using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.PaymentService.Controllers
{
    /// <summary>
    /// 支付单控制器
    /// </summary>
    [Route("api/payment/bill")]
    public class BillPaymentController : PaymentServiceController
    {
        private readonly ILogger<BillPaymentController> _logger;

        public BillPaymentController(ILogger<BillPaymentController> logger)
        {
            _logger = logger;
        }
    }
}