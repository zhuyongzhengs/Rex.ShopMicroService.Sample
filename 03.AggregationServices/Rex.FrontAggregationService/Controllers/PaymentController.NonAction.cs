using Microsoft.AspNetCore.Mvc;
using Rex.BaseService.Systems.Commons;
using Rex.FrontAggregationService.Core.Mappers;
using Rex.FrontAggregationService.Payments;
using Rex.OrderService.Orders;
using Rex.PaymentService.Payments;
using Rex.Service.AspNetCore.Extensions;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Helper;
using Rex.Service.Core.Models;
using System.Text.Json;
using Volo.Abp;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.FrontAggregationService.Controllers
{
    /// <summary>
    /// 支付控制器.NonAction
    /// </summary>
    public partial class PaymentController
    {
        /// <summary>
        /// 生成支付单
        /// </summary>
        /// <param name="input">用户支付Dto</param>
        /// <returns></returns>
        [HttpPost("bill/generate")]
        [NonAction]
        public async Task<BillPaymentDto> GenerateBillPaymentAsync([FromBody] UserPayDto input)
        {
            CheckPayDto checkPay = await CheckPayRelAsync(new PaymentRelDto() { SourceIds = input.SourceIds, PaymentType = input.PaymentType, Params = input.Params });
            if (input.PaymentCode.Equals(PaymentType.BalancePay.ToString()))
            {
                // 验证用户余额是否充足
                CurrentSysUserEto currentUser = await _baseCommonAppService.GetCurrentUserAsync();
                if (checkPay.Money > currentUser.Balance)
                {
                    throw new UserFriendlyException($"余额不足，请充值！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
                }
            }

            BillPaymentCreateDto billPaymentCreate = new BillPaymentCreateDto();
            billPaymentCreate.No = CommonHelper.GetSerialNumberType((int)SerialNumberType.PlayCode);
            billPaymentCreate.SourceId = string.Join(",", input.SourceIds);
            billPaymentCreate.Money = checkPay.Money;
            billPaymentCreate.UserId = checkPay.UserId;
            billPaymentCreate.Type = input.PaymentType;
            billPaymentCreate.Status = (int)OrderPayStatus.No;
            billPaymentCreate.PaymentCode = input.PaymentCode;
            billPaymentCreate.Ip = Request.HttpContext.GetRemoteIpAddress();
            billPaymentCreate.Parameters = input.Params != null ? JsonSerializer.Serialize(input.Params) : "";
            BillPaymentDto billPayment = await _paymentCommonAppService.CreateBillPaymentAsync(billPaymentCreate);
            // 判断支付金额是否为0，如果为0，则直接支付成功
            if (billPayment.Money == 0)
            {
                // 已限制商品价格不能为0，价格为“0”的商品看实际场景是否需要...
                await UpdatePaymentedStatusAsync(new PaymentBillUpdateDto()
                {
                    BillPaymentNo = billPayment.No,
                    Status = (int)BillPaymentStatus.Payed,
                    PaymentCode = input.PaymentCode,
                    Money = billPayment.Money,
                    PayedMsg = "金额为0，自动支付成功！"
                });
            }
            return billPayment;
        }

        /// <summary>
        /// 验证支付Rel
        /// </summary>
        /// <param name="input">支付RelDto</param>
        /// <returns></returns>
        [HttpPost("checkPayRel")]
        [NonAction]
        public async Task<CheckPayDto> CheckPayRelAsync([FromBody] PaymentRelDto input)
        {
            CheckPayDto checkPay = new CheckPayDto();
            // 获取当前用户信息
            CurrentSysUserEto currentUser = await _baseCommonAppService.GetCurrentUserAsync();
            checkPay.UserId = currentUser.Id;

            if (input.PaymentType == (int)BillPaymentType.Order)
            {
                // 获取订单状态
                List<OrderDto> orders = await _orderCommonAppService.GetOrderStatusAsync(input.SourceIds, (int)OrderStatus.Normal, (int)OrderPayStatus.No);
                if (orders == null || !orders.Any()) throw new UserFriendlyException("该订单不存在或已完成支付！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
                foreach (var order in orders)
                {
                    checkPay.Rel.Add(new PayRel()
                    {
                        SourceId = order.Id,
                        Money = order.OrderAmount
                    });
                    checkPay.Money += order.OrderAmount;
                }
            }
            else if (input.PaymentType == (int)BillPaymentType.Recharge)
            {
                var payParams = JsonSerializer.Deserialize<RechargeParams>(input.Params.ToString());
                if (payParams == null || payParams.Money < 0)
                {
                    throw new UserFriendlyException("充值参数无效或金额不正确！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
                }
                checkPay.Rel.Add(new PayRel()
                {
                    SourceId = currentUser.Id,
                    Money = payParams.Money
                });
                checkPay.Money = payParams.Money;
            }
            return checkPay;
        }

        /// <summary>
        /// 支付成功之后，更新支付状态
        /// </summary>
        /// <param name="input">支付单</param>
        /// <returns></returns>
        [HttpPost("updateStatus")]
        [NonAction]
        public async Task<IActionResult> UpdatePaymentedStatusAsync([FromBody] PaymentBillUpdateDto input)
        {
            #region 更新支付单状态

            BillPaymentDto billPayment = await _paymentCommonAppService.UpdateBillPaymentStatusAsync(input.BillPaymentNo, new BillPaymentStatusUpdateDto()
            {
                Money = input.Money,
                Status = input.Status,
                PaymentCode = input.PaymentCode,
                PayedMsg = input.PayedMsg,
                TradeNo = input.TradeNo
            });

            #endregion 更新支付单状态

            #region 更新订单状态

            List<Guid> orderIds = new List<Guid>();
            foreach (string sourceId in billPayment.SourceId.Split(','))
            {
                if (Guid.TryParse(sourceId, out Guid orderId)) orderIds.Add(orderId);
            }
            List<OrderDto> orders = await _orderCommonAppService.GetOrderStatusAsync(orderIds.ToArray(), (int)OrderStatus.Normal);
            if (orders == null || !orders.Any()) throw new UserFriendlyException("更新订单[" + billPayment.SourceId + "]状态失败或该订单已失效！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
            foreach (OrderDto order in orders)
            {
                if (order.PayStatus == (int)OrderPayStatus.Yes || order.PayStatus == (int)OrderPayStatus.PartialNo || order.PayStatus == (int)OrderPayStatus.Refunded)
                {
                    throw new UserFriendlyException("订单[" + order.No + "]支付失败，该订单已经支付！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix()).WithData(nameof(order.No), order.No).WithData(nameof(order.PayStatus), order.PayStatus.GetDescription<OrderPayStatus>());
                }
                order.PayedAmount = order.OrderAmount;
                order.PaymentTime = DateTime.Now;
                order.PaymentCode = input.PaymentCode;
                order.PayStatus = (int)OrderPayStatus.Yes;
            }
            List<OrderUpdateDto> orderUpdates = ObjectMapper.Map<List<OrderDto>, List<OrderUpdateDto>>(orders);
            orders = await _orderAppService.UpdateOrderManyAsync(orderUpdates);

            #endregion 更新订单状态

            #region 增加订单记录

            foreach (var order in orders)
            {
                OrderLogCreateDto orderLogCreate = new OrderLogCreateDto();
                orderLogCreate.OrderId = order.Id;
                orderLogCreate.UserId = order.UserId;
                orderLogCreate.Type = (int)OrderLogType.LogTypePay;
                orderLogCreate.Msg = "订单支付成功！";
                orderLogCreate.Data = JsonSerializer.Serialize(order, AspNetCoreExtension.GetJsonSerializerOptions());
                await _orderAppService.CreateOrderLogAsync(orderLogCreate);
            }

            #endregion 增加订单记录

            // TODO: 结佣处理
            // TODO: 易联云打印机打印
            // TODO: 发送支付成功信息，增加发送内容
            // TODO: 用户升级处理

            return NoContent();
        }
    }
}