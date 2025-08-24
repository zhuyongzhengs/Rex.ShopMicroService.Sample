using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using Rex.Service.Core.Events;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 测试【OrderToGoodEto】事件处理器
    /// </summary>
    /// <remarks>
    /// 订阅事件
    /// </remarks>
    [Dependency(ServiceLifetime.Scoped)]
    public class OrderToGoodEventHandler : ICapSubscribe
    {
        private readonly IGoodRepository _goodRepository;

        public OrderToGoodEventHandler(IGoodRepository goodRepository)
        {
            _goodRepository = goodRepository;
        }

        /// <summary>
        /// 测试消息
        /// </summary>
        /// <returns></returns>
        [CapSubscribe(OrderToGoodEto.EventName)]
        public async Task<object> TestOrderToGoodMsgAsync(OrderToGoodEto eventData)
        {
            if (eventData.IsException)
                throw new UserFriendlyException("模拟异常！");
            Console.WriteLine($"商品服务已收到：{eventData.OrderMsg}");
            return await Task.FromResult(eventData);
        }
    }
}