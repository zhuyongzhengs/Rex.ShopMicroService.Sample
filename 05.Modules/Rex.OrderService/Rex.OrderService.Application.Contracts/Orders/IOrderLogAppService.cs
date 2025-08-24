using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单日志服务接口
    /// </summary>
    public interface IOrderLogAppService : IApplicationService
    {
        /// <summary>
        /// 根据[订单ID]获取优惠劵
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        Task<List<OrderLogDto>> GetOrderLogByOrderIdAsync(Guid orderId);
    }
}