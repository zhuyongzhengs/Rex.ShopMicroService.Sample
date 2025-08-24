using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Events.Bases;
using Rex.Service.Core.Events.Orders;
using Rex.Service.Core.Events.Payments;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 用户余额支付 处理事件
    /// </summary>
    [Dependency(ServiceLifetime.Scoped)]
    public class UserBalancePaymentEventHandler : ICapSubscribe
    {
        private readonly ICapPublisher _capEventBus;
        private readonly IBillPaymentRepository _billPaymentRepository;

        public UserBalancePaymentEventHandler(ICapPublisher capEventBus, IBillPaymentRepository billPaymentRepository)
        {
            _capEventBus = capEventBus;
            _billPaymentRepository = billPaymentRepository;
        }

        [CapSubscribe(UserBalancePaymentEto.EventName)]
        public async Task UserBalancePaymentAsync(UserBalancePaymentEto eventData)
        {
            string paymentCode = PaymentType.BalancePay.ToString();

            #region 更新订单状态

            BillPayment billPayment = await _billPaymentRepository.GetBillPaymentByNoAsync(eventData.BillPaymentNo);
            if (billPayment == null)
                throw new UserFriendlyException($"支付单号：{eventData.BillPaymentNo} 不存在！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix()).WithData(nameof(eventData.BillPaymentNo), eventData.BillPaymentNo);

            if (billPayment.Type == (int)BillPaymentType.Order)
            {
                // 更新订单状态
                List<Guid> orderIds = new List<Guid>();
                foreach (var sourceId in billPayment.SourceId.Split(','))
                {
                    if (Guid.TryParse(sourceId, out Guid orderId))
                    {
                        orderIds.Add(orderId);
                    }
                }
                await _capEventBus.PublishAsync(OrderChangeStatusEto.EventName, new OrderChangeStatusEto()
                {
                    OrderIds = orderIds.ToArray(),
                    PaymentCode = paymentCode,
                    Description = "余额支付成功！",
                });
            }

            #endregion 更新订单状态

            #region 扣减余额

            await _capEventBus.PublishAsync(ChangeUserBalanceEto.EventName, new ChangeUserBalanceEto()
            {
                UserId = billPayment.UserId,
                Type = (int)UserBalanceType.Pay,
                Money = billPayment.Money,
                SourceId = billPayment.Id.ToString()
            });

            #endregion 扣减余额

            #region 更新支付单状态

            await _billPaymentRepository.UpdateBillPaymentStatusAsync(billPayment.Id, (int)BillPaymentStatus.Payed, paymentCode);

            #endregion 更新支付单状态

            Debug.WriteLine("用户余额支付成功！");
            Task.CompletedTask.Wait();
        }
    }
}