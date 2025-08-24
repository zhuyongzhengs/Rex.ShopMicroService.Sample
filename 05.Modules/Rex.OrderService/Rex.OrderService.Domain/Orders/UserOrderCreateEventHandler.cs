using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using Rex.Service.Core.Events.Orders;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 创建用户订单事件处理
    /// </summary>
    /// <remarks>
    /// 订阅事件
    /// </remarks>
    [Dependency(ServiceLifetime.Scoped)]
    public class UserOrderCreateEventHandler : ICapSubscribe
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IObjectMapper _objectMapper;

        public UserOrderCreateEventHandler(IOrderRepository orderRepository, IObjectMapper objectMapper)
        {
            _orderRepository = orderRepository;
            _objectMapper = objectMapper;
        }

        [CapSubscribe(OrderCreateEto.EventName)]
        public async Task UserOrderCreateAsync(OrderCreateEto eventData)
        {
            Order order = _objectMapper.Map<OrderCreateEto, Order>(eventData);
            await _orderRepository.CreateUserOrderAsync(order, eventData.IsUsePoint, eventData.CartIds);
            Task.CompletedTask.Wait();
        }
    }
}