using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using Rex.Service.Core.Events.Payments;
using System.Diagnostics;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 退款单(创建)处理事件
    /// </summary>
    [Dependency(ServiceLifetime.Scoped)]
    public class BillRefundCreateEventHandler : ICapSubscribe
    {
        private readonly IBillRefundRepository _billRefundRepository;

        public BillRefundCreateEventHandler(IBillRefundRepository billRefundRepository)
        {
            _billRefundRepository = billRefundRepository;
        }

        [CapSubscribe(BillRefundCreateEto.EventName)]
        public async Task BillRefundCreateAsync(BillRefundCreateEto eventData)
        {
            await _billRefundRepository.CreateBillRefundAsync(eventData);
            Debug.WriteLine("退货单生成成功！");
            Task.CompletedTask.Wait();
        }
    }
}