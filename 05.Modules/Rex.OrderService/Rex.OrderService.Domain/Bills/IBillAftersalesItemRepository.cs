using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 售后单明细仓储接口
    /// </summary>
    public interface IBillAftersalesItemRepository : IRepository<BillAftersalesItem, Guid>
    {
        /// <summary>
        /// 根据订单明细ID获取售后单明细
        /// </summary>
        /// <param name="orderItemIds">订单明细ID</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        Task<List<BillAftersalesItem>> GetBillAftersalesItemByOrderItemIdsAsync(List<Guid> orderItemIds, List<int>? status = null, bool includeDetails = false);
    }
}