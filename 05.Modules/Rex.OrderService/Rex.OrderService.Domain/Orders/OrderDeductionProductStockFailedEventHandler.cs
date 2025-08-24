using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using Rex.Service.Core.Events.Orders;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// [订单]扣减库存失败 事件处理
    /// </summary>
    /// <remarks>
    /// 订阅事件
    /// </remarks>
    [Dependency(ServiceLifetime.Scoped)]
    public class OrderDeductionProductStockFailedEventHandler : ICapSubscribe
    {
        private readonly IOrderRepository _orderRepository;

        public OrderDeductionProductStockFailedEventHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [CapSubscribe(OrderDeductionProductStockFailedEto.EventName)]
        public async Task OrderDeductionProductStockFailedAsync(OrderDeductionProductStockFailedEto eventData)
        {
            Order order = await _orderRepository.GetAsync(eventData.Id, false);
            if (order == null) return;
            await _orderRepository.CancelOrderAsync(order.Id, [order.UserId], eventData.FailMsg);
        }
    }
}