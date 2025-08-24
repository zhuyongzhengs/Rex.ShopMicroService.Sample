using Rex.GoodService.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.PageDesigns
{
    /// <summary>
    /// 页面设计仓储
    /// </summary>
    public class PageDesignRepository : EfCoreRepository<GoodServiceDbContext, PageDesign, Guid>, IPageDesignRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public PageDesignRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}