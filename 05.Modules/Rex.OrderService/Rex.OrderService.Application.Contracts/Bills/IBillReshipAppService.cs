using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 退货单服务接口
    /// </summary>
    public interface IBillReshipAppService : IApplicationService
    {
        /// <summary>
        /// 获取(分页)退货单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<BillReshipDto>> GetListAsync(GetBillReshipInput input);

        /// <summary>
        /// 查询退货单状态
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetStatusAsync();

        /// <summary>
        /// 根据[订单ID]获取退货单
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        Task<List<BillReshipDto>> GetOrderIdAsync(Guid orderId);

        /// <summary>
        /// 售后单生成退货单
        /// </summary>
        /// <param name="input">生成退货单参数</param>
        /// <returns></returns>
        Task CreateBillAftersalesToReshipAsync(BillAftersalesToReshipCreateDto input);

        /// <summary>
        /// 根据[售后单]获取退货单
        /// </summary>
        /// <param name="aftersalesId">售后ID</param>
        /// <returns></returns>
        Task<BillReshipDto> GetBillReshipByAftersalesIdAsync(Guid aftersalesId);

        /// <summary>
        /// 更新退货单物流信息
        /// </summary>
        /// <param name="id">退货单ID</param>
        /// <param name="input">物流信息</param>
        /// <returns></returns>
        Task UpdateLogisticsAsync(Guid id, BillReshipLogiUpdateDto input);

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="id">退货单ID</param>
        /// <returns></returns>
        Task UpdateConfirmDeliveryAsync(Guid id);
    }
}