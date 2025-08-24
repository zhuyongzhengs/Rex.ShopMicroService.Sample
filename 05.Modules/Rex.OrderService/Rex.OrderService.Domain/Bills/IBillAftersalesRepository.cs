using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 售后单仓储接口
    /// </summary>
    public interface IBillAftersalesRepository : IRepository<BillAftersales, Guid>
    {
        /// <summary>
        /// 获取用户(分页) 订单列表总数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="no">订单号</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="type">类型</param>
        /// <param name="status">审核状态</param>
        /// <returns></returns>
        Task<long> GetUserPageCountAsync(
            Guid userId,
            string? no,
            string? orderNo,
            int? type,
            int? status,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 获取用户(分页) 订单列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="userId">用户ID</param>
        /// <param name="no">订单号</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="type">类型</param>
        /// <param name="status">审核状态</param>
        /// <returns></returns>
        Task<List<BillAftersales>> GetUserPageListAsync(
            int skipCount,
            int maxResultCount,
            string? sorting,
            Guid userId,
            string? no,
            string? orderNo,
            int? type,
            int? status,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 查询售后单信息
        /// </summary>
        /// <param name="id">售后单ID</param>
        /// <returns></returns>
        Task<BillAftersales> GetBillAftersalesByIdAsync(Guid id, bool includeDetails = false);

        /// <summary>
        /// 查询售后单信息
        /// </summary>
        /// <param name="no">售后单号</param>
        /// <returns></returns>
        Task<BillAftersales> GetBillAftersalesByNoAsync(string no, bool includeDetails = false);
    }
}