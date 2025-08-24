using Rex.GoodService.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品分类仓储
    /// </summary>
    public class GoodCategoryRepository : EfCoreRepository<GoodServiceDbContext, GoodCategory, Guid>, IGoodCategoryRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public GoodCategoryRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}