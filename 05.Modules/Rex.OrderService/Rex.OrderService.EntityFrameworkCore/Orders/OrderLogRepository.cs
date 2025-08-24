using Microsoft.Extensions.DependencyInjection;
using Rex.OrderService.EntityFrameworkCore;
using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单日志仓储
    /// </summary>
    public class OrderLogRepository : EfCoreRepository<OrderServiceDbContext, OrderLog, Guid>, IOrderLogRepository
    {
        public OrderServiceDbContext oServiceDbContext { get; set; }

        public OrderLogRepository(IDbContextProvider<OrderServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}