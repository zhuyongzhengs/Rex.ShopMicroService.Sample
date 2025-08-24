using Rex.GoodService.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.Brands
{
    /// <summary>
    /// 品牌仓储
    /// </summary>
    public class BrandRepository : EfCoreRepository<GoodServiceDbContext, Brand, Guid>, IBrandRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public BrandRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}