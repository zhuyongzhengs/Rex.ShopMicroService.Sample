using Microsoft.Extensions.DependencyInjection;
using Rex.OrderService.EntityFrameworkCore;
using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 退货单明细仓储
    /// </summary>
    public class BillReshipItemRepository : EfCoreRepository<OrderServiceDbContext, BillReshipItem, Guid>, IBillReshipItemRepository
    {
        public OrderServiceDbContext oServiceDbContext { get; set; }

        public BillReshipItemRepository(IDbContextProvider<OrderServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}