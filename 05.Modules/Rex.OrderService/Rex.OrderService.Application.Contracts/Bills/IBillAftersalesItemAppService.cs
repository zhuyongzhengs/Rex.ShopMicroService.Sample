using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 售后单明细服务接口
    /// </summary>
    public interface IBillAftersalesItemAppService : IApplicationService
    {
        /// <summary>
        /// 根据订单明细ID获取售后单明细
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        Task<List<BillAftersalesItemDto>> GetOrderItemIdsAsync(GetBillAftersalesItemToOrderItemIdInput input, bool includeDetails = false);
    }
}