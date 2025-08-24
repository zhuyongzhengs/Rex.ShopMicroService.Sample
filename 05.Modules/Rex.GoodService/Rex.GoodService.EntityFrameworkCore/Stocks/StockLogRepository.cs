using Rex.GoodService.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.Stocks
{
    /// <summary>
    /// 库存记录仓储
    /// </summary>
    public class StockLogRepository : EfCoreRepository<GoodServiceDbContext, StockLog, Guid>, IStockLogRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public StockLogRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}