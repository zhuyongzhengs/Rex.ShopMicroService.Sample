using DotNetCore.CAP;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.Pay.RequestHandling;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Models;
using Rex.PaymentService.Payments;
using Rex.Service.Core.Events.Bases;
using Rex.Service.Core.Events.Orders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.PaymentService.EntityFrameworkCore.Events
{
    /// <summary>
    /// 微信支付事件处理器
    /// </summary>
    public class PaidWeChatPayEventHandler : WeChatPayPaidEventHandlerBase
    {
        private readonly ICapPublisher _capEventBus;
        private readonly IBillPaymentRepository _billPaymentRepository;

        public PaidWeChatPayEventHandler(ICapPublisher capEventBus, IBillPaymentRepository billPaymentRepository)
        {
            _capEventBus = capEventBus;
            _billPaymentRepository = billPaymentRepository;
        }

        public override async Task<WeChatRequestHandlingResult> HandleAsync(WeChatPayEventModel<WeChatPayPaidEventModel> model)
        {
            #region 更新订单状态

            BillPayment billPayment = await _billPaymentRepository.GetBillPaymentByNoAsync(model.Resource.OutTradeNo);
            if (billPayment == null) return await Task.FromResult(new WeChatRequestHandlingResult(false, $"支付单号：{model.Resource.OutTradeNo} 不存在！"));
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
                    PaymentCode = PaymentType.WechatPay.ToString(),
                    Description = "微信支付成功！",
                });
            }
            else if (billPayment.Type == (int)BillPaymentType.Recharge)
            {
                // 余额充值
                await _capEventBus.PublishAsync(ChangeUserBalanceEto.EventName, new ChangeUserBalanceEto()
                {
                    UserId = billPayment.UserId,
                    Type = (int)UserBalanceType.Recharge,
                    Money = billPayment.Money,
                    SourceId = billPayment.Id.ToString()
                });
            }

            #endregion 更新订单状态

            #region 更新支付单状态

            await _billPaymentRepository.UpdateBillPaymentStatusAsync(billPayment.Id, (int)BillPaymentStatus.Payed, PaymentType.WechatPay.ToString(), model.Resource.TradeStateDesc, model.Resource.TransactionId);

            #endregion 更新支付单状态

            return await Task.FromResult(new WeChatRequestHandlingResult(true));
        }
    }
}