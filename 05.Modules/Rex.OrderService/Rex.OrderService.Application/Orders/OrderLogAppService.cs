using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单日志服务
    /// </summary>
    [RemoteService]
    [Authorize]
    public class OrderLogAppService : ApplicationService, IOrderLogAppService
    {
        private readonly IOrderLogRepository _orderLogRepository;

        public OrderLogAppService(IOrderLogRepository orderLogRepository)
        {
            _orderLogRepository = orderLogRepository;
        }

        /// <summary>
        /// 根据[订单ID]获取优惠劵
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public async Task<List<OrderLogDto>> GetOrderLogByOrderIdAsync(Guid orderId)
        {
            List<OrderLog> orderLogs = await _orderLogRepository.GetListAsync(x => x.OrderId == orderId);
            return ObjectMapper.Map<List<OrderLog>, List<OrderLogDto>>(orderLogs);
        }
    }
}