using Microsoft.Extensions.DependencyInjection;
using Rex.OrderService.EntityFrameworkCore;
using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.OrderService.Carts
{
    /// <summary>
    /// 购物车仓储
    /// </summary>
    public class CartRepository : EfCoreRepository<OrderServiceDbContext, Cart, Guid>, ICartRepository
    {
        public OrderServiceDbContext oServiceDbContext { get; set; }

        public CartRepository(IDbContextProvider<OrderServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}