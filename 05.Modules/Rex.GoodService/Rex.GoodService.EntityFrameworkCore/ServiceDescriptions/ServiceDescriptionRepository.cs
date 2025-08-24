using Rex.GoodService.EntityFrameworkCore;
using Rex.GoodService.ServiceDescriptions;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.PageDesigns
{
    /// <summary>
    /// 商城服务描述仓储
    /// </summary>
    public class ServiceDescriptionRepository : EfCoreRepository<GoodServiceDbContext, ServiceDescription, Guid>, IServiceDescriptionRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public ServiceDescriptionRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}