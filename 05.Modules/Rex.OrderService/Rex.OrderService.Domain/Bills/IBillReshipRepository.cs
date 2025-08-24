using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 退货单仓储接口
    /// </summary>
    public interface IBillReshipRepository : IRepository<BillReship, Guid>
    {
        /// <summary>
        /// 获取(分页) 退货单列表总数
        /// </summary>
        /// <param name="no">退货单号</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="aftersalesNo">售后单号</param>
        /// <param name="logiNo">物料单号</param>
        /// <param name="status">状态</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        Task<long> GetPageCountAsync(
            string? no,
            string? orderNo,
            string? aftersalesNo,
            string? logiNo,
            int? status,
            DateTime? beginTime,
            DateTime? endTime,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 获取(分页) 退货单列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="no">退货单号</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="aftersalesNo">售后单号</param>
        /// <param name="logiNo">物料单号</param>
        /// <param name="status">状态</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        Task<List<BillReship>> GetPageListAsync(
            int skipCount,
            int maxResultCount,
            string? sorting,
            string? no,
            string? orderNo,
            string? aftersalesNo,
            string? logiNo,
            int? status,
            DateTime? beginTime,
            DateTime? endTime,
            bool includeDetails = true,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 获取退货单详情
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<BillReship> GetBillReshipByIdAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = default);
    }
}