using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 退款单服务接口
    /// </summary>
    public interface IBillRefundAppService : IApplicationService
    {
        /// <summary>
        /// 获取(分页)退款单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<BillRefundDto>> GetListAsync(GetBillRefundInput input);

        /// <summary>
        /// 根据[售后单]获取退款单
        /// </summary>
        /// <param name="aftersalesId">售后ID</param>
        /// <returns></returns>
        Task<BillRefundDto> GetBillRefundByAftersalesIdAsync(Guid aftersalesId);

        /// <summary>
        /// 根据[资源编号]获取退款单
        /// </summary>
        /// <param name="sourceId">资源编号</param>
        /// <returns></returns>
        Task<List<BillRefundDto>> GetBillRefundBySourceIdAsync(string sourceId);

        /// <summary>
        /// 查询退款单类型
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetBillRefundTypeAsync();

        /// <summary>
        /// 查询退款单状态
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetBillRefundStatusAsync();

        /// <summary>
        /// 审核退款单
        /// </summary>
        /// <param name="id">退款单ID</param>
        /// <param name="input">退款参数</param>
        /// <returns></returns>
        Task UpdateAuditAsync(Guid id, AuditRefundDto input);

        /// <summary>
        /// 支付退款
        /// </summary>
        /// <param name="id">退款单ID</param>
        /// <returns></returns>
        Task UpdatePaymentRefundAsync(Guid id);
    }
}