using Rex.GoodService.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.Stocks
{
    /// <summary>
    /// 库存操作仓储
    /// </summary>
    public class StockRepository : EfCoreRepository<GoodServiceDbContext, Stock, Guid>, IStockRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public StockRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}