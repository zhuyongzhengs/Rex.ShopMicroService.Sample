using Rex.GoodService.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品参数仓储
    /// </summary>
    public class GoodParamRepository : EfCoreRepository<GoodServiceDbContext, GoodParam, Guid>, IGoodParamRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public GoodParamRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}