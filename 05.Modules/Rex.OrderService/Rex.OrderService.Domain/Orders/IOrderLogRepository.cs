using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单记录仓储接口
    /// </summary>
    public interface IOrderLogRepository : IRepository<OrderLog, Guid>
    {
        // ...
    }
}