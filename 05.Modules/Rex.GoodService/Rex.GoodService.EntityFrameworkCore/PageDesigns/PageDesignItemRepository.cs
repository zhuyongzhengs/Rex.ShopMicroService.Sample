using Rex.GoodService.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.PageDesigns
{
    /// <summary>
    /// 页面设计项仓储
    /// </summary>
    public class PageDesignItemRepository : EfCoreRepository<GoodServiceDbContext, PageDesignItem, Guid>, IPageDesignItemRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public PageDesignItemRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}