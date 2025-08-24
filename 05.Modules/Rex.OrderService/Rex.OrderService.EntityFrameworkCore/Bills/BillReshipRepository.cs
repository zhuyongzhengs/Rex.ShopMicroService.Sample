using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rex.OrderService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 退货单仓储
    /// </summary>
    public class BillReshipRepository : EfCoreRepository<OrderServiceDbContext, BillReship, Guid>, IBillReshipRepository
    {
        public OrderServiceDbContext oServiceDbContext { get; set; }

        public BillReshipRepository(IDbContextProvider<OrderServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

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
        public async Task<long> GetPageCountAsync(string no, string orderNo, string aftersalesNo, string logiNo, int? status, DateTime? beginTime, DateTime? endTime, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!no.IsNullOrWhiteSpace(), p => p.No == no)
                .WhereIf(!orderNo.IsNullOrWhiteSpace(), p => p.Order.No == orderNo)
                .WhereIf(!aftersalesNo.IsNullOrWhiteSpace(), p => p.Aftersales.No == aftersalesNo)
                .WhereIf(!logiNo.IsNullOrWhiteSpace(), p => p.LogiNo == logiNo)
                .WhereIf(status.HasValue, p => p.Status == status)
                .WhereIf(beginTime.HasValue, p => p.CreationTime >= beginTime)
                .WhereIf(endTime.HasValue, p => p.CreationTime <= endTime)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

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
        public async Task<List<BillReship>> GetPageListAsync(int skipCount, int maxResultCount, string sorting, string no, string orderNo, string aftersalesNo, string logiNo, int? status, DateTime? beginTime, DateTime? endTime, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!no.IsNullOrWhiteSpace(), p => p.No == no)
                .WhereIf(!orderNo.IsNullOrWhiteSpace(), p => p.Order.No == orderNo)
                .WhereIf(!aftersalesNo.IsNullOrWhiteSpace(), p => p.Aftersales.No == aftersalesNo)
                .WhereIf(!logiNo.IsNullOrWhiteSpace(), p => p.LogiNo == logiNo)
                .WhereIf(status.HasValue, p => p.Status == status)
                .WhereIf(beginTime.HasValue, p => p.CreationTime >= beginTime)
                .WhereIf(endTime.HasValue, p => p.CreationTime <= endTime)
                .IncludeIf(includeDetails, p => p.Order)
                .IncludeIf(includeDetails, p => p.Aftersales)
                .IncludeIf(includeDetails, p => p.BillReshipItems)
                .OrderByIf<BillReship, IQueryable<BillReship>>(!sorting.IsNullOrWhiteSpace(), sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取退货单详情
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public async Task<BillReship> GetBillReshipByIdAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(p => p.Id == id)
                .IncludeIf(includeDetails, p => p.Order)
                .IncludeIf(includeDetails, p => p.Aftersales)
                .IncludeIf(includeDetails, p => p.BillReshipItems)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }
    }
}