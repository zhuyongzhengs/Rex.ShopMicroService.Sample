using Microsoft.AspNetCore.Mvc;
using Rex.BackendAggregationService.Core.Mappers;
using Rex.BackendAggregationService.Orders;
using Rex.BaseService.Systems.SysUsers;
using Rex.BaseService.Systems.UserGrades;
using Rex.OrderService.Bills;
using Rex.OrderService.Orders;
using Rex.PaymentService.Payments;
using Rex.PromotionService.Promotions;
using Rex.Service.Core.Configurations;
using IOrderCommonAppService = Rex.OrderService.Commons.ICommonAppService;
using IPaymentCommonAppService = Rex.PaymentService.Commons.ICommonAppService;

namespace Rex.BackendAggregationService.Controllers
{
    /// <summary>
    /// 订单控制器
    /// </summary>
    [Route("api/backend/aggregation/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrdersAppService _orderAppService;
        private readonly ISysUserAppService _sysUserAppService;
        private readonly IUserGradeAppService _userGradeAppService;
        private readonly IPaymentsAppService _paymentAppService;
        private readonly IBillPaymentAppService _billPaymentAppService;
        private readonly IBillRefundAppService _billRefundAppService;
        private readonly IBillDeliveryAppService _billDeliveryAppService;
        private readonly IBillReshipAppService _billReshipAppService;
        private readonly ICouponAppService _couponAppService;
        private readonly IOrderLogAppService _orderLogAppService;
        private readonly IOrderCommonAppService _orderCommonAppService;
        private readonly IPaymentCommonAppService _paymentCommonAppService;
        private readonly IBillAftersalesItemAppService _billAftersalesItemAppService;

        public OrderController(IOrdersAppService orderAppService, ISysUserAppService sysUserAppService, IUserGradeAppService userGradeAppService, IPaymentsAppService paymentAppService, IBillPaymentAppService billPaymentAppService, IBillRefundAppService billRefundAppService, IBillDeliveryAppService billDeliveryAppService, IBillReshipAppService billReshipAppService, ICouponAppService couponAppService, IOrderLogAppService orderLogAppService, IOrderCommonAppService orderCommonAppService, IPaymentCommonAppService paymentCommonAppService, IBillAftersalesItemAppService billAftersalesItemAppService)
        {
            _orderAppService = orderAppService;
            _sysUserAppService = sysUserAppService;
            _userGradeAppService = userGradeAppService;
            _paymentAppService = paymentAppService;
            _billPaymentAppService = billPaymentAppService;
            _billRefundAppService = billRefundAppService;
            _billDeliveryAppService = billDeliveryAppService;
            _billReshipAppService = billReshipAppService;
            _couponAppService = couponAppService;
            _orderLogAppService = orderLogAppService;
            _orderCommonAppService = orderCommonAppService;
            _paymentCommonAppService = paymentCommonAppService;
            _billAftersalesItemAppService = billAftersalesItemAppService;
        }

        /// <summary>
        /// 获取订单详情列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetOrderDetailAsync([FromRoute] Guid id)
        {
            OrderDto order = await _orderAppService.GetAsync(id, true);
            if (order == null) return NotFound();
            OrderDetailDto orderDetail = ObjectMapper.Map<OrderDto, OrderDetailDto>(order);

            #region 查询退货数量

            if (orderDetail.OrderItems != null && orderDetail.OrderItems.Any())
            {
                List<Guid> orderItemIds = orderDetail.OrderItems.Select(x => x.Id).ToList();
                List<BillAftersalesItemDto> bAftersalesItems = await _billAftersalesItemAppService.GetOrderItemIdsAsync(new GetBillAftersalesItemToOrderItemIdInput()
                {
                    OrderItemIds = orderItemIds,
                    Status = new List<int>() { (int)GlobalEnums.BillAftersalesStatus.WaitAudit, (int)GlobalEnums.BillAftersalesStatus.Success }
                });
                if (bAftersalesItems != null && bAftersalesItems.Any())
                {
                    foreach (var orderItem in orderDetail.OrderItems)
                    {
                        BillAftersalesItemDto bAftersalesItem = bAftersalesItems.FirstOrDefault(x => x.OrderItemId == orderItem.Id);
                        if (bAftersalesItem == null) continue;
                        orderItem.ReshipNums = bAftersalesItem.Nums;
                    }
                }
            }

            #endregion 查询退货数量

            #region 获取用户信息

            SysUserDto sysUser = await _sysUserAppService.GetAsync(orderDetail.UserId);
            orderDetail.UserName = sysUser.UserName;
            orderDetail.NickName = sysUser.Name;
            orderDetail.PhoneNumber = sysUser.PhoneNumber;
            if (sysUser.GradeId.HasValue)
            {
                UserGradeDto userGrade = await _userGradeAppService.GetAsync(sysUser.GradeId.Value);
                if (userGrade != null) orderDetail.UserGradeName = userGrade.Title;
            }

            #endregion 获取用户信息

            #region 获取支付单/退款单

            string sourceId = orderDetail.Id.ToString();
            orderDetail.BillPayments = await _billPaymentAppService.GetSourceIdAsync(sourceId);
            orderDetail.BillRefunds = await _billRefundAppService.GetBillRefundBySourceIdAsync(sourceId);

            #endregion 获取支付单/退款单

            #region 获取发货单/退货单

            orderDetail.BillDeliverys = await _billDeliveryAppService.GetOrderIdAsync(orderDetail.Id);
            orderDetail.BillReships = await _billReshipAppService.GetOrderIdAsync(orderDetail.Id);

            #endregion 获取发货单/退货单

            #region 优惠劵

            orderDetail.Coupons = await _couponAppService.GetCouponByUsedIdAsync(orderDetail.Id);

            #endregion 优惠劵

            #region 订单日志

            orderDetail.OrderLogs = await _orderLogAppService.GetOrderLogByOrderIdAsync(orderDetail.Id);

            #endregion 订单日志

            return Ok(orderDetail);
        }

        /// <summary>
        /// 获取订单发货信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("delivery/{id}")]
        public async Task<IActionResult> GetOrderDeliveryAsync([FromRoute] Guid id)
        {
            OrderDto order = await _orderAppService.GetAsync(id, true);
            if (order == null) return NotFound();
            OrderDeliveryDto orderDelivery = ObjectMapper.Map<OrderDto, OrderDeliveryDto>(order);

            #region 获取用户信息

            SysUserDto sysUser = await _sysUserAppService.GetAsync(orderDelivery.UserId);
            orderDelivery.UserName = sysUser.UserName;
            orderDelivery.NickName = sysUser.Name;
            orderDelivery.PhoneNumber = sysUser.PhoneNumber;
            if (sysUser.GradeId.HasValue)
            {
                UserGradeDto userGrade = await _userGradeAppService.GetAsync(sysUser.GradeId.Value);
                if (userGrade != null) orderDelivery.UserGradeName = userGrade.Title;
            }

            #endregion 获取用户信息

            return Ok(orderDelivery);
        }
    }
}