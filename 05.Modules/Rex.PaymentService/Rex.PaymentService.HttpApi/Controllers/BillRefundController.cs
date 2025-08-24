using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.PaymentService.Controllers
{
    /// <summary>
    /// 退款单控制器
    /// </summary>
    [Route("api/payment/billRefund")]
    public class BillRefundController : PaymentServiceController
    {
        private readonly ILogger<BillRefundController> _logger;

        public BillRefundController(ILogger<BillRefundController> logger)
        {
            _logger = logger;
        }
    }
}