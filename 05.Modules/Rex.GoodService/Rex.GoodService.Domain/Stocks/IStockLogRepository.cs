using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.Stocks
{
    /// <summary>
    /// 库存记录仓储接口
    /// </summary>
    public interface IStockLogRepository : IRepository<StockLog, Guid>
    {
        // ...
    }
}