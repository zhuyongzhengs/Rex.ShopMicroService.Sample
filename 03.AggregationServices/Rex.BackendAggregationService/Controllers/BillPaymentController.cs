using Microsoft.AspNetCore.Mvc;
using Rex.BackendAggregationService.Payments;
using Rex.BaseService.Systems.SysUsers;
using Rex.OrderService.Orders;
using Rex.PaymentService.Payments;
using Rex.Service.AspNetCore.Extensions;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Helper;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using static Rex.Service.Core.Configurations.GlobalEnums;
using IOrderCommonAppService = Rex.OrderService.Commons.ICommonAppService;
using IPaymentCommonAppService = Rex.PaymentService.Commons.ICommonAppService;

namespace Rex.BackendAggregationService.Controllers
{
    /// <summary>
    /// 支付单控制器
    /// </summary>
    [Route("api/backend/aggregation/billPayment")]
    [ApiController]
    public partial class BillPaymentController : ControllerBase
    {
        private readonly IOrdersAppService _orderAppService;
        private readonly IBillPaymentAppService _billPaymentAppService;
        private readonly ISysUserAppService _sysUserAppService;
        private readonly IOrderCommonAppService _orderCommonAppService;
        private readonly IPaymentCommonAppService _paymentCommonAppService;

        public BillPaymentController(IOrdersAppService orderAppService, IBillPaymentAppService billPaymentAppService, ISysUserAppService sysUserAppService, IPaymentCommonAppService paymentCommonAppService, IOrderCommonAppService orderCommonAppService)
        {
            _orderAppService = orderAppService;
            _billPaymentAppService = billPaymentAppService;
            _sysUserAppService = sysUserAppService;
            _paymentCommonAppService = paymentCommonAppService;
            _orderCommonAppService = orderCommonAppService;
        }

        /// <summary>
        /// 获取(分页)支付单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetBillPaymentPageListAsync([FromQuery] GetBillPaymentInput input)
        {
            PagedResultDto<BillPaymentDto> billPaymentResult = await _billPaymentAppService.GetListAsync(input);
            if (billPaymentResult.Items != null && billPaymentResult.Items.Any())
            {
                Guid[] userIds = billPaymentResult.Items.Select(x => x.UserId).Distinct().ToArray();
                List<SysUserDto> sysUsers = await _sysUserAppService.GetManyAsync(userIds);
                foreach (var billPayment in billPaymentResult.Items)
                {
                    SysUserDto sysUser = sysUsers.FirstOrDefault(x => x.Id == billPayment.UserId);
                    if (sysUser == null) continue;
                    billPayment.UserName = sysUser.Name;
                }
            }
            return Ok(billPaymentResult);
        }

        /// <summary>
        /// 订单支付
        /// </summary>
        /// <param name="input">支付Dto</param>
        /// <returns></returns>
        [HttpPost("orderPay")]
        public async Task<IActionResult> OrderPay([FromBody] OrderPayDto input)
        {
            // 验证支付编码
            bool isPaymentType = false;
            foreach (var paymentType in Enum.GetValues(typeof(PaymentType)))
            {
                string pType = paymentType.ToString();
                if (pType.Equals(input.PaymentCode))
                {
                    isPaymentType = true;
                    break;
                }
            }
            if (!isPaymentType)
            {
                throw new UserFriendlyException($"支付编码不正确，请核对！", SystemStatusCode.Fail.ToBackendAggregationServicePrefix()).WithData(nameof(input.PaymentCode), input.PaymentCode);
            }

            // 验证订单状态
            List<OrderDto> orders = new List<OrderDto>();
            foreach (Guid orderId in input.OrderIds)
            {
                OrderDto order = await _orderCommonAppService.GetOrderStatusAsync(orderId, (int)OrderStatus.Normal, (int)OrderPayStatus.No);
                if (order == null) throw new UserFriendlyException("选择的订单不存在或已完成支付！", SystemStatusCode.Fail.ToBackendAggregationServicePrefix());
                orders.Add(order);
            }

            // 生成支付单
            List<BillPaymentCreateDto> billPaymentCreates = new List<BillPaymentCreateDto>();
            foreach (OrderDto order in orders)
            {
                BillPaymentCreateDto billPaymentCreate = new BillPaymentCreateDto();
                billPaymentCreate.No = CommonHelper.GetSerialNumberType((int)SerialNumberType.PlayCode);
                billPaymentCreate.SourceId = order.Id.ToString();
                billPaymentCreate.Money = order.OrderAmount;
                billPaymentCreate.UserId = order.UserId;
                billPaymentCreate.Type = input.PaymentType;
                billPaymentCreate.Status = (int)OrderPayStatus.No;
                billPaymentCreate.PaymentCode = input.PaymentCode;
                billPaymentCreate.Ip = Request.HttpContext.GetRemoteIpAddress();
                billPaymentCreate.Parameters = "";
                billPaymentCreates.Add(billPaymentCreate);
            }
            return await GenerateBillPaymentAsync(billPaymentCreates);
        }
    }
}