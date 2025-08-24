using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using Rex.Service.Core.Events.Goods;
using Rex.Service.Core.Events.Orders;
using System.Diagnostics;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.GoodService.Products
{
    /// <summary>
    /// [订单]扣减库存订单 事件处理
    /// </summary>
    /// <remarks>
    /// 订阅事件
    /// </remarks>
    [Dependency(ServiceLifetime.Scoped)]
    public class OrderDeductionProductStockHandler : ICapSubscribe
    {
        private readonly IProductRepository _productRepository;
        private readonly ICapPublisher _capEventBus;

        public OrderDeductionProductStockHandler(IProductRepository productRepository, ICapPublisher capEventBus)
        {
            _productRepository = productRepository;
            _capEventBus = capEventBus;
        }

        [CapSubscribe(OrderDeductionProductStockEto.EventName)]
        public async Task OrderDeductionProductStockAsync(OrderDeductionProductStockEto eventData)
        {
            if (eventData.StockDatas == null || eventData.StockDatas.Count == 0)
            {
                Debug.WriteLine($"未传递扣减库存信息，请检查！");
                return;
            }
            bool deductionResult = true;
            string failMsg = string.Empty;
            foreach (var stockData in eventData.StockDatas)
            {
                deductionResult = await _productRepository.ChangeStockAsync(stockData.ProductId, (int)OrderChangeStockType.Order, stockData.Nums);
                if (deductionResult) continue;
                Product product = await _productRepository.GetAsync(stockData.ProductId, false);
                if (product != null) failMsg = $"货品编码：{product.Sn}；选择的商品规格【{product.SpesDesc}】库存不足，请重新下单。";
                break;
            }
            if (!deductionResult)
            {
                // 库存不足，订单取消
                await _capEventBus.PublishAsync(OrderDeductionProductStockFailedEto.EventName, new OrderDeductionProductStockFailedEto()
                {
                    Id = eventData.Id,
                    FailMsg = failMsg
                });
            }
        }
    }
}