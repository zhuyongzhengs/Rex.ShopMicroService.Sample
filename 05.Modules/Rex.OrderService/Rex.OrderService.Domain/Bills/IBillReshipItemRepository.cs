using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 退货单明细仓储接口
    /// </summary>
    public interface IBillReshipItemRepository : IRepository<BillReshipItem, Guid>
    {
        // ...
    }
}