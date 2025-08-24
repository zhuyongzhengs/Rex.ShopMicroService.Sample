using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rex.OrderService.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单明细仓储
    /// </summary>
    public class OrderItemRepository : EfCoreRepository<OrderServiceDbContext, OrderItem, Guid>, IOrderItemRepository
    {
        public OrderServiceDbContext oServiceDbContext { get; set; }

        public OrderItemRepository(IDbContextProvider<OrderServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 更新订单明细的发货数量
        /// </summary>
        /// <param name="id">订单明细ID</param>
        /// <param name="sendNum">发货数量</param>
        public async Task UpdateSendNumAsync(Guid id, int sendNum)
        {
            await (await GetDbSetAsync())
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x.SetProperty(s => s.SendNums, sendNum));
        }
    }
}