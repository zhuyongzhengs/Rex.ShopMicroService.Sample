using Microsoft.Extensions.DependencyInjection;
using Rex.OrderService.EntityFrameworkCore;
using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EventBus.Distributed;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 发货单明细仓储
    /// </summary>
    public class BillDeliveryItemRepository : EfCoreRepository<OrderServiceDbContext, BillDeliveryItem, Guid>, IBillDeliveryItemRepository
    {
        public OrderServiceDbContext oServiceDbContext { get; set; }

        private readonly IDistributedEventBus _distributedEventBus;

        public BillDeliveryItemRepository(IDbContextProvider<OrderServiceDbContext> dbContextProvider, IDistributedEventBus distributedEventBus) : base(dbContextProvider)
        {
            _distributedEventBus = distributedEventBus;
        }
    }
}