using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using Rex.Service.Core.Events.Orders;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单状态变更事件处理
    /// </summary>
    /// <remarks>
    /// 订阅事件
    /// </remarks>
    [Dependency(ServiceLifetime.Scoped)]
    public class OrderStatusEventHandler : ICapSubscribe
    {
        private readonly IOrderRepository _orderRepository;

        public OrderStatusEventHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [CapSubscribe(OrderChangeStatusEto.EventName)]
        public async Task OrderStatusAsync(OrderChangeStatusEto eventData)
        {
            await _orderRepository.UpdateOrderStatusAsync(eventData.OrderIds, eventData.PaymentCode, eventData.Description);
            Task.CompletedTask.Wait();
        }
    }
}