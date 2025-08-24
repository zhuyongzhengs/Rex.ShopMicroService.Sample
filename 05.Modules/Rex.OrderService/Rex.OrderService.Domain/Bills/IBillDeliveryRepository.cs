using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 发货单仓储接口
    /// </summary>
    public interface IBillDeliveryRepository : IRepository<BillDelivery, Guid>
    {
        /// <summary>
        /// 订单发货
        /// </summary>
        /// <param name="input">发货信息</param>
        /// <returns></returns>
        Task CreateOrderDeliveryAsync(BillDelivery input);

        /// <summary>
        /// 查询发货单信息
        /// </summary>
        /// <param name="id">发货单ID</param>
        /// <returns></returns>
        Task<BillDelivery> GetBillDeliveryByIdAsync(Guid id, bool includeDetails = false);
    }
}