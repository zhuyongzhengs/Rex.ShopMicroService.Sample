using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rex.OrderService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 售后单明细仓储
    /// </summary>
    public class BillAftersalesItemRepository : EfCoreRepository<OrderServiceDbContext, BillAftersalesItem, Guid>, IBillAftersalesItemRepository
    {
        public OrderServiceDbContext oServiceDbContext { get; set; }

        public BillAftersalesItemRepository(IDbContextProvider<OrderServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 根据订单明细ID获取售后单明细
        /// </summary>
        /// <param name="orderItemIds">订单明细ID</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public async Task<List<BillAftersalesItem>> GetBillAftersalesItemByOrderItemIdsAsync(List<Guid> orderItemIds, List<int>? status = null, bool includeDetails = false)
        {
            return await (await GetDbSetAsync())
                .Where(x => orderItemIds.Contains(x.OrderItemId))
                .WhereIf(status != null && status.Count > 0, x => status.Contains(x.Aftersales.Status))
                .IncludeIf(includeDetails, p => p.Aftersales)
                .ToListAsync();
        }
    }
}