using Rex.GoodService.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.ServiceGoods
{
    /// <summary>
    /// 服务商品仓储
    /// </summary>
    public class ServiceGoodRepository : EfCoreRepository<GoodServiceDbContext, ServiceGood, Guid>, IServiceGoodRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public ServiceGoodRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}