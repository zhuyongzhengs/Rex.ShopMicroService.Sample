using Microsoft.AspNetCore.Mvc;
using Rex.BackendAggregationService.Core.Mappers;
using Rex.BackendAggregationService.Payments;
using Rex.OrderService.Orders;
using Rex.PaymentService.Payments;
using Rex.Service.AspNetCore.Extensions;
using Rex.Service.Core.Configurations;
using System.Text.Json;
using Volo.Abp;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.BackendAggregationService.Controllers
{
    public partial class BillPaymentController
    {
        /// <summary>
        /// 支付成功之后，更新支付状态
        /// </summary>
        /// <param name="input">支付单</param>
        /// <returns></returns>
        [HttpPost("generateBillPayment")]
        [NonAction]
        public async Task<IActionResult> GenerateBillPaymentAsync([FromBody] List<BillPaymentCreateDto> input)
        {
            foreach (BillPaymentCreateDto bPayment in input)
            {
                BillPaymentDto billPayment = await _paymentCommonAppService.CreateBillPaymentAsync(bPayment);
                // 更新订单状态
                await UpdatePaymentedStatusAsync(new PaymentBillUpdateDto()
                {
                    BillPaymentNo = billPayment.No,
                    Status = (int)BillPaymentStatus.Payed,
                    PaymentCode = bPayment.PaymentCode,
                    Money = billPayment.Money,
                    PayedMsg = "平台[后台]手动支付！"
                });
            }
            return NoContent();
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
            if (orders == null || !orders.Any()) throw new UserFriendlyException("更新订单[" + billPayment.SourceId + "]状态失败或该订单已失效！", SystemStatusCode.Fail.ToBackendAggregationServicePrefix());
            foreach (OrderDto order in orders)
            {
                if (order.PayStatus == (int)OrderPayStatus.Yes || order.PayStatus == (int)OrderPayStatus.PartialNo || order.PayStatus == (int)OrderPayStatus.Refunded)
                {
                    throw new UserFriendlyException("订单[" + order.No + "]支付失败，该订单已经支付！", SystemStatusCode.Fail.ToBackendAggregationServicePrefix()).WithData(nameof(order.No), order.No).WithData(nameof(order.PayStatus), order.PayStatus.GetDescription<OrderPayStatus>());
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