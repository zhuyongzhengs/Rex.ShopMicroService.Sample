using Rex.OrderService.Orders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 发货单服务接口
    /// </summary>
    public interface IBillDeliveryAppService : IApplicationService
    {
        /// <summary>
        /// 获取(分页)发货单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<BillDeliveryDto>> GetListAsync(GetBillDeliveryInput input);

        /// <summary>
        /// 根据[订单ID]获取发货单
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        Task<List<BillDeliveryDto>> GetOrderIdAsync(Guid orderId);

        /// <summary>
        /// 订单发货
        /// </summary>
        /// <param name="input">发货信息</param>
        /// <returns></returns>
        Task CreateOrderDeliveryAsync(OrderDeliveryDto input);

        /// <summary>
        /// 更新发货单信息
        /// </summary>
        /// <param name="id">发货单ID</param>
        /// <param name="input">发货信息</param>
        /// <returns></returns>
        Task UpdateOrderDeliveryAsync(Guid id, BillDeliveryUpdateDto input);

        /// <summary>
        /// 查询发货单信息
        /// </summary>
        /// <param name="id">发货单ID</param>
        /// <returns></returns>
        Task<BillDeliveryDto> GetAsync(Guid id, bool includeDetails = false);
    }
}