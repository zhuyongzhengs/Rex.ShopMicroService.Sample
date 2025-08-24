using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 售后单明细服务
    /// </summary>
    [RemoteService]
    [Authorize]
    public class BillAftersalesItemAppService : ApplicationService, IBillAftersalesItemAppService
    {
        private readonly IBillAftersalesItemRepository _billAftersalesItemRepository;

        public BillAftersalesItemAppService(IBillAftersalesItemRepository billAftersalesItemRepository)
        {
            _billAftersalesItemRepository = billAftersalesItemRepository;
        }

        /// <summary>
        /// 根据订单明细ID获取售后单明细
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        public async Task<List<BillAftersalesItemDto>> GetOrderItemIdsAsync(GetBillAftersalesItemToOrderItemIdInput input, bool includeDetails = false)
        {
            List<BillAftersalesItem> billAftersalesItem = await _billAftersalesItemRepository.GetBillAftersalesItemByOrderItemIdsAsync(input.OrderItemIds, input.Status, includeDetails);
            return ObjectMapper.Map<List<BillAftersalesItem>, List<BillAftersalesItemDto>>(billAftersalesItem);
        }
    }
}