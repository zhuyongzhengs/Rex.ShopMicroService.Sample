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

namespace Rex.OrderService.Logisticss
{
    /// <summary>
    /// 物流仓储
    /// </summary>
    public class LogisticsRepository : EfCoreRepository<OrderServiceDbContext, Logistics, Guid>, ILogisticsRepository
    {
        public OrderServiceDbContext oServiceDbContext { get; set; }

        public LogisticsRepository(IDbContextProvider<OrderServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取(分页) 物流列表总数
        /// </summary>
        /// <param name="logiCode">物流公司编码</param>
        /// <param name="logiName">物流公司名称</param>
        /// <param name="phone">物流电话</param>
        /// <returns></returns>
        public async Task<long> GetPageCountAsync(string logiCode, string logiName, string phone, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!logiCode.IsNullOrWhiteSpace(), p => p.LogiCode == logiCode)
                .WhereIf(!logiName.IsNullOrWhiteSpace(), p => p.LogiName.Contains(logiName))
                .WhereIf(!phone.IsNullOrWhiteSpace(), p => p.Phone == phone)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取(分页) 物流列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="logiCode">物流公司编码</param>
        /// <param name="logiName">物流公司名称</param>
        /// <param name="phone">物流电话</param>
        /// <returns></returns>
        public async Task<List<Logistics>> GetPageListAsync(int skipCount, int maxResultCount, string sorting, string logiCode, string logiName, string phone, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!logiCode.IsNullOrWhiteSpace(), p => p.LogiCode == logiCode)
                .WhereIf(!logiName.IsNullOrWhiteSpace(), p => p.LogiName.Contains(logiName))
                .WhereIf(!phone.IsNullOrWhiteSpace(), p => p.Phone == phone)
                .OrderByIf<Logistics, IQueryable<Logistics>>(!sorting.IsNullOrWhiteSpace(), sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}