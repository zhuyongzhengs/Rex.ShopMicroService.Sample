using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.Pay.RequestHandling;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Models;
using Rex.PaymentService.Payments;
using Rex.Service.AspNetCore.Extensions;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rex.PaymentService.EntityFrameworkCore.Events
{
    /// <summary>
    /// 微信退款事件处理器
    /// </summary>
    public class RefundWeChatPayEventHandler : WeChatPayRefundEventHandlerBase
    {
        private readonly IBillRefundRepository _billRefundRepository;

        public RefundWeChatPayEventHandler(IBillRefundRepository billRefundRepository)
        {
            _billRefundRepository = billRefundRepository;
        }

        public override async Task<WeChatRequestHandlingResult> HandleAsync(WeChatPayEventModel<WeChatPayRefundEventModel> model)
        {
            bool refundResult = model.Resource.RefundStatus.Equals("SUCCESS", StringComparison.OrdinalIgnoreCase);
            if (refundResult)
            {
                string jsonData = JsonSerializer.Serialize(model.Resource, options: AspNetCoreExtension.GetJsonSerializerOptions());
                await _billRefundRepository.UpdateMemoByNoAsync(model.Resource.OutRefundNo, jsonData);
            }
            return await Task.FromResult(new WeChatRequestHandlingResult(refundResult));
        }
    }
}