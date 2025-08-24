using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.OrderService.Carts
{
    /// <summary>
    /// 购物车仓储接口
    /// </summary>
    public interface ICartRepository : IRepository<Cart, Guid>
    {
        // ...
    }
}