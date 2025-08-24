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
    /// 库存记录(创建)事件处理
    /// </summary>
    /// <remarks>
    /// 订阅事件
    /// </remarks>
    [Dependency(ServiceLifetime.Scoped)]
    public class StockLogCreateEventHandler : ICapSubscribe
    {
        private readonly IStockLogRepository _stockLogRepository;

        public StockLogCreateEventHandler(IStockLogRepository stockLogRepository)
        {
            _stockLogRepository = stockLogRepository;
        }

        [CapSubscribe(StockLogCreateEto.EventName)]
        public async Task StockLogCreateAsync(StockLogCreateEto eventData)
        {
            if (eventData == null || eventData.StockLogDetails.Count == 0)
                throw new UserFriendlyException($"库存记录信息不能为空！", SystemStatusCode.Fail.ToGoodServicePrefix());
            List<StockLog> stockLogs = eventData.StockLogDetails.Select(x => new StockLog()
            {
                SourceId = x.SourceId,
                GoodId = x.GoodId,
                ProductId = x.ProductId,
                Nums = x.Nums,
                Sn = x.Sn,
                Bn = x.Bn,
                GoodName = x.GoodName,
                SpesDesc = x.SpesDesc
            }).ToList();
            await _stockLogRepository.InsertManyAsync(stockLogs);
            Debug.WriteLine($"[库存记录]创建成功！");
        }
    }
}