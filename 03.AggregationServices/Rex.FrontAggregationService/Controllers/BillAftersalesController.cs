using Microsoft.AspNetCore.Mvc;
using Rex.FrontAggregationService.Core.Mappers;
using Rex.FrontAggregationService.Orders;
using Rex.OrderService.Bills;
using Rex.PaymentService.Payments;

namespace Rex.FrontAggregationService.Controllers
{
    /// <summary>
    /// 售后单控制器
    /// </summary>
    [Route("api/front/aggregation/billAftersales")]
    [ApiController]
    public class BillAftersalesController : ControllerBase
    {
        private readonly IBillAftersalesAppService _billAftersalesAppService;
        private readonly IBillRefundAppService _billRefundAppService;
        private readonly IBillReshipAppService _billReshipAppService;

        public BillAftersalesController(IBillAftersalesAppService billAftersalesAppService, IBillRefundAppService billRefundAppService, IBillReshipAppService billReshipAppService)
        {
            _billAftersalesAppService = billAftersalesAppService;
            _billRefundAppService = billRefundAppService;
            _billReshipAppService = billReshipAppService;
        }

        /// <summary>
        /// 查询售后单信息
        /// </summary>
        /// <param name="id">售后单ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillAftersalesByIdAsync([FromRoute] Guid id)
        {
            BillAftersalesDto billAftersales = await _billAftersalesAppService.GetAsync(id, true);
            if (billAftersales == null) return BadRequest();
            BillAftersalesDetailDto bAftersalesDetail = ObjectMapper.Map<BillAftersalesDto, BillAftersalesDetailDto>(billAftersales);
            // 查询退款单
            bAftersalesDetail.BillRefund = await _billRefundAppService.GetBillRefundByAftersalesIdAsync(billAftersales.Id);
            // 查询退货单
            bAftersalesDetail.BillReship = await _billReshipAppService.GetBillReshipByAftersalesIdAsync(billAftersales.Id);
            return Ok(bAftersalesDetail);
        }
    }
}