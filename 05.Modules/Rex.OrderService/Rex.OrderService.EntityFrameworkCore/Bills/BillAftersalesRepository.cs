using Microsoft.EntityFrameworkCore;
using Rex.OrderService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 售后单仓储
    /// </summary>
    public class BillAftersalesRepository : EfCoreRepository<OrderServiceDbContext, BillAftersales, Guid>, IBillAftersalesRepository
    {
        public OrderServiceDbContext oServiceDbContext { get; set; }

        public BillAftersalesRepository(IDbContextProvider<OrderServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取用户(分页) 订单列表总数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="no">订单号</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="type">类型</param>
        /// <param name="status">审核状态</param>
        /// <returns></returns>
        public async Task<long> GetUserPageCountAsync(Guid userId, string? no, string? orderNo, int? type, int? status, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(p => p.UserId == userId)
                .WhereIf(!no.IsNullOrWhiteSpace(), p => p.No == no)
                .WhereIf(!orderNo.IsNullOrWhiteSpace(), p => p.Order.No == orderNo)
                .WhereIf(type.HasValue && type > 0, p => p.Type == type)
                .WhereIf(status.HasValue && status > 0, p => p.Status == status)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

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
        public async Task<List<BillAftersales>> GetUserPageListAsync(int skipCount, int maxResultCount, string? sorting, Guid userId, string? no, string? orderNo, int? type, int? status, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(p => p.UserId == userId)
                .WhereIf(!no.IsNullOrWhiteSpace(), p => p.No == no)
                .WhereIf(!orderNo.IsNullOrWhiteSpace(), p => p.Order.No == orderNo)
                .WhereIf(type.HasValue && type > 0, p => p.Type == type)
                .WhereIf(status.HasValue && status > 0, p => p.Status == status)
                .IncludeIf(includeDetails, p => p.Order)
                .IncludeIf(includeDetails, p => p.BillAftersalesItems)
                .IncludeIf(includeDetails, p => p.BillAftersalesImagess)
                .OrderByIf<BillAftersales, IQueryable<BillAftersales>>(!sorting.IsNullOrWhiteSpace(), sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 查询售后单信息
        /// </summary>
        /// <param name="id">售后单ID</param>
        /// <returns></returns>
        public async Task<BillAftersales> GetBillAftersalesByIdAsync(Guid id, bool includeDetails = false)
        {
            return await (await GetDbSetAsync())
                .Where(p => p.Id == id)
                .IncludeIf(includeDetails, p => p.Order)
                .IncludeIf(includeDetails, p => p.BillAftersalesItems)
                .IncludeIf(includeDetails, p => p.BillAftersalesImagess)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 查询售后单信息
        /// </summary>
        /// <param name="no">售后单号</param>
        /// <returns></returns>
        public async Task<BillAftersales> GetBillAftersalesByNoAsync(string no, bool includeDetails = false)
        {
            return await (await GetDbSetAsync())
                .Where(p => p.No == no)
                .IncludeIf(includeDetails, p => p.Order)
                .IncludeIf(includeDetails, p => p.BillAftersalesItems)
                .IncludeIf(includeDetails, p => p.BillAftersalesImagess)
                .FirstOrDefaultAsync();
        }
    }
}