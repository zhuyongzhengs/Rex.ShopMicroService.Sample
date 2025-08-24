using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using Rex.GoodService.Stocks;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Events.Goods;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Rex.GoodService.Products
{
    /// <summary>
    /// 库存操作(创建)事件处理
    /// </summary>
    /// <remarks>
    /// 订阅事件
    /// </remarks>
    [Dependency(ServiceLifetime.Scoped)]
    public class StockCreateEventHandler : ICapSubscribe
    {
        private readonly IStockRepository _stockRepository;

        public StockCreateEventHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [CapSubscribe(StockCreateEto.EventName)]
        public async Task StockCreateAsync(StockCreateEto eventData)
        {
            if (eventData.StockDetails == null || eventData.StockDetails.Count == 0)
                throw new UserFriendlyException($"库存操作信息不能为空！", SystemStatusCode.Fail.ToGoodServicePrefix());
            List<Stock> stocks = eventData.StockDetails.Select(x => new Stock()
            {
                SourceId = x.SourceId,
                Type = x.Type,
                OperatorId = x.OperatorId,
                Memo = x.Memo
            }).ToList();
            await _stockRepository.InsertManyAsync(stocks);
            Debug.WriteLine($"[库存操作]创建成功！");
        }
    }
}