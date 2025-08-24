using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Rex.FrontAggregationService.Core.Mappers;
using Rex.FrontAggregationService.Payments;
using Rex.OrderService.Orders;
using Rex.PaymentService.Commons;
using Rex.PaymentService.Payments;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Events.Payments;
using Rex.Service.Core.Models;
using Volo.Abp;
using Volo.Abp.EventBus.Distributed;
using static Rex.Service.Core.Configurations.GlobalEnums;
using IBaseCommonAppService = Rex.BaseService.Systems.Commons.ICommonAppService;
using IOrderCommonAppService = Rex.OrderService.Commons.ICommonAppService;
using IPaymentCommonAppService = Rex.PaymentService.Commons.ICommonAppService;

namespace Rex.FrontAggregationService.Controllers
{
    /// <summary>
    /// 支付控制器
    /// </summary>
    [Route("api/front/aggregation/payment")]
    [ApiController]
    public partial class PaymentController : ControllerBase
    {
        private readonly IBaseCommonAppService _baseCommonAppService;
        private readonly IBillPaymentAppService _billPaymentAppService;
        private readonly IOrderCommonAppService _orderCommonAppService;
        private readonly IPaymentCommonAppService _paymentCommonAppService;
        private readonly IOrdersAppService _orderAppService;
        private readonly ICapPublisher _capEventBus;

        public PaymentController(IBaseCommonAppService baseCommonAppService, IBillPaymentAppService billPaymentAppService, IOrderCommonAppService orderCommonAppService, IPaymentCommonAppService paymentCommonAppService, IOrdersAppService orderAppService, ICapPublisher capEventBus)
        {
            _baseCommonAppService = baseCommonAppService;
            _billPaymentAppService = billPaymentAppService;
            _orderCommonAppService = orderCommonAppService;
            _paymentCommonAppService = paymentCommonAppService;
            _orderAppService = orderAppService;
            _capEventBus = capEventBus;
        }

        /// <summary>
        /// 微信小程序支付
        /// </summary>
        /// <param name="input">支付单</param>
        /// <returns></returns>
        [HttpPost("wxmp")]
        public async Task<IActionResult> WxmpPaymentAsync([FromBody] UserPayDto input)
        {
            // 验证支付编码
            if (!input.PaymentCode.Equals(PaymentType.WechatPay.ToString()))
            {
                throw new UserFriendlyException($"支付编码不正确，微信小程序的支付编码应为[{PaymentType.WechatPay}]！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix()).WithData(nameof(input.PaymentCode), input.PaymentCode);
            }

            // 获取当前用户信息
            CurrentSysUserEto currentUser = await _baseCommonAppService.GetCurrentUserAsync();

            // 生成支付单
            BillPaymentDto billPayment = await GenerateBillPaymentAsync(input);
            if (billPayment.Money == 0)
            {
                return Ok(new WeChatPayParameterResultDto
                {
                    Detail = ObjectMapper.Map<BillPaymentDto, BillPaymentDetailDto>(billPayment),
                    PayParameter = new JsSdkWeChatPayParameterDto()
                });
            }

            // 发起支付
            WeChatPayParameterResultDto weChatPayParameterResult = await _paymentCommonAppService.PubWxmpPayAsync(currentUser.OpenId, billPayment);
            return Ok(weChatPayParameterResult);
        }

        /// <summary>
        /// 余额支付
        /// </summary>
        /// <param name="input">支付单</param>
        /// <returns></returns>
        [HttpPost("balance")]
        public async Task<IActionResult> BalancePaymentAsync([FromBody] UserPayDto input)
        {
            // 验证支付编码
            if (!input.PaymentCode.Equals(PaymentType.BalancePay.ToString()))
            {
                throw new UserFriendlyException($"支付编码不正确，余额的支付编码应为[{PaymentType.BalancePay}]！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix()).WithData(nameof(input.PaymentCode), input.PaymentCode);
            }
            if (input.PaymentCode.Equals(PaymentType.BalancePay.ToString()) && input.PaymentType == (int)BillPaymentType.Recharge)
            {
                throw new UserFriendlyException($"充值不能选择[余额支付]，请检查！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
            }

            // 生成支付单
            BillPaymentDto billPayment = await GenerateBillPaymentAsync(input);
            BillPaymentDetailDto bPaymentDetail = ObjectMapper.Map<BillPaymentDto, BillPaymentDetailDto>(billPayment);
            if (bPaymentDetail.Money == 0) return Ok(bPaymentDetail);

            // 扣减余额
            await _capEventBus.PublishAsync(UserBalancePaymentEto.EventName, new UserBalancePaymentEto()
            {
                BillPaymentNo = bPaymentDetail.No,
                Remark = "余额支付。"
            });

            return Ok(bPaymentDetail);
        }

        /// <summary>
        /// 查询支付订单详情
        /// </summary>
        /// <param name="billPaymentNo">支付单号</param>
        /// <returns></returns>
        [HttpGet("detail/{billPaymentNo}")]
        public async Task<IActionResult> GetPaymentOrderDetailAsync([FromRoute] string billPaymentNo)
        {
            // 查询支付单信息
            BillPaymentDto billPayment = await _billPaymentAppService.GetUserBillPaymentAsync(billPaymentNo);
            PaymentOrderDetailDto paymentOrderDetail = new PaymentOrderDetailDto();
            if (billPayment.Type == (int)BillPaymentType.Order)
            {
                // 查询订单信息
                Guid.TryParse(billPayment.SourceId, out Guid orderId);
                UserOrderDto userOrder = await _orderAppService.GetUserOrderAsync(orderId);
                // 得到支付订单详情
                paymentOrderDetail.OrderId = userOrder.Id;
                paymentOrderDetail.OrderNo = userOrder.No;
                paymentOrderDetail.OrderPayStatus = userOrder.PayStatus;
                paymentOrderDetail.OrderShipStatus = userOrder.ShipStatus;
                paymentOrderDetail.OrderStatus = userOrder.Status;
            }
            paymentOrderDetail.PaymentId = billPayment.Id;
            paymentOrderDetail.PaymentNo = billPayment.No;
            paymentOrderDetail.Money = billPayment.Money;
            paymentOrderDetail.PaymentCode = billPayment.PaymentCode;
            paymentOrderDetail.Parameters = billPayment.Parameters;
            paymentOrderDetail.PayedMsg = billPayment.PayedMsg;
            paymentOrderDetail.TradeNo = billPayment.TradeNo;
            paymentOrderDetail.PaymentType = billPayment.Type;
            paymentOrderDetail.PaymentStatus = billPayment.Status;
            paymentOrderDetail.CreationTime = billPayment.CreationTime;
            return Ok(paymentOrderDetail);
        }
    }
}