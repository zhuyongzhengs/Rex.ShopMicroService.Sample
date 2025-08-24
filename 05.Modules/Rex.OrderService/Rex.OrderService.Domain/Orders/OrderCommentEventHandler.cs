using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using Rex.OrderService.Orders;
using Rex.Service.Core.Events.Orders;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单评价变更事件处理
    /// </summary>
    /// <remarks>
    /// 订阅事件
    /// </remarks>
    [Dependency(ServiceLifetime.Scoped)]
    public class OrderCommentEventHandler : ICapSubscribe
    {
        private readonly IOrderRepository _orderRepository;

        public OrderCommentEventHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [CapSubscribe(OrderCommentEto.EventName)]
        public async Task OrderCommentAsync(OrderCommentEto eventData)
        {
            // 更新订单评价状态
            await _orderRepository.OrderCommentAsync(eventData.OrderId, eventData.IsComment, eventData.Type);
            Task.CompletedTask.Wait();
        }
    }
}