using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.Stocks
{
    /// <summary>
    /// 库存操作仓储接口
    /// </summary>
    public interface IStockRepository : IRepository<Stock, Guid>
    {
        // ...
    }
}