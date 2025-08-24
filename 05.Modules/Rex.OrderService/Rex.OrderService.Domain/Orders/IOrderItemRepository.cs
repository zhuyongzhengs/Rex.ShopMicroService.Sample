using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单明细仓储接口
    /// </summary>
    public interface IOrderItemRepository : IRepository<OrderItem, Guid>
    {
        /// <summary>
        /// 更新订单明细的发货数量
        /// </summary>
        /// <param name="id">订单明细ID</param>
        /// <param name="sendNum">发货数量</param>
        Task UpdateSendNumAsync(Guid id, int sendNum);
    }
}