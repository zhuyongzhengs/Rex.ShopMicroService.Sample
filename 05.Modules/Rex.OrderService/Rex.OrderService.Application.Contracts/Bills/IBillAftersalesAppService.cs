using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 售后单服务接口
    /// </summary>
    public interface IBillAftersalesAppService : IApplicationService
    {
        /// <summary>
        /// 查询售后单收货类型
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetReceiveTypesAsync();

        /// <summary>
        /// 查询售后单状态
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetStatusAsync();

        /// <summary>
        /// 获取(分页)售后单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<BillAftersalesDto>> GetListAsync(GetBillAftersalesInput input);

        /// <summary>
        /// 查询售后单信息
        /// </summary>
        /// <param name="id">售后单ID</param>
        /// <returns></returns>
        Task<BillAftersalesDto> GetAsync(Guid id, bool includeDetails = false);

        /// <summary>
        /// 查询售后单信息
        /// </summary>
        /// <param name="no">售后单号</param>
        /// <returns></returns>
        Task<BillAftersalesDto> GetNoAsync(string no, bool includeDetails = false);

        /// <summary>
        /// 根据订单ID获取售后单状态
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="status">售后单状态</param>
        /// <returns></returns>
        Task<BillAftersalesDto> GetOrderStatusAsync(Guid orderId, int status);

        /// <summary>
        /// 创建售后单
        /// </summary>
        /// <param name="input">售后单参数</param>
        /// <returns></returns>
        Task CreateAsync(BillAftersalesCreateDto input);

        /// <summary>
        /// 审核售后单
        /// </summary>
        /// <param name="id">售后单ID</param>
        /// <param name="input">售后单参数</param>
        /// <returns></returns>
        Task UpdateAuditAsync(Guid id, BillAftersalesAuditDto input);

        /// <summary>
        /// 获取用户(分页)售后单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<BillAftersalesDto>> GetUserAsync(GetUserBillAftersalesInput input);

        /// <summary>
        /// 查询批量售后单信息
        /// </summary>
        /// <param name="ids">售后单ID</param>
        /// <returns></returns>
        Task<List<BillAftersalesDto>> GetManyAsync(Guid[] ids, bool includeDetails = false);
    }
}