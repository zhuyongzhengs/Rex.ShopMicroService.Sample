using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 发货单明细仓储接口
    /// </summary>
    public interface IBillDeliveryItemRepository : IRepository<BillDeliveryItem, Guid>
    {
        // ...
    }
}