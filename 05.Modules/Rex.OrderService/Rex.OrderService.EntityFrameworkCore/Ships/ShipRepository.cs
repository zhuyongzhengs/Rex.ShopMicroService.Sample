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

namespace Rex.OrderService.Ships
{
    /// <summary>
    /// 配送方式仓储
    /// </summary>
    public class ShipRepository : EfCoreRepository<OrderServiceDbContext, Ship, Guid>, IShipRepository
    {
        public OrderServiceDbContext oServiceDbContext { get; set; }

        public ShipRepository(IDbContextProvider<OrderServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取(分页) 配送方式列表总数
        /// </summary>
        /// <param name="name">配送方式名称</param>
        /// <param name="logiCode">物料公司名称</param>
        /// <param name="logiName">物料公司名称</param>
        /// <returns></returns>
        public async Task<long> GetPageCountAsync(string name, string logiCode, string logiName, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .WhereIf(!logiCode.IsNullOrWhiteSpace(), p => p.LogiCode == logiCode)
                .WhereIf(!logiName.IsNullOrWhiteSpace(), p => p.LogiName == logiName)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取(分页) 配送方式列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="name">配送方式名称</param>
        /// <param name="logiCode">物料公司名称</param>
        /// <param name="logiName">物料公司名称</param>
        /// <returns></returns>
        public async Task<List<Ship>> GetPageListAsync(int skipCount, int maxResultCount, string sorting, string name, string logiCode, string logiName, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .WhereIf(!logiCode.IsNullOrWhiteSpace(), p => p.LogiCode == logiCode)
                .WhereIf(!logiName.IsNullOrWhiteSpace(), p => p.LogiName == logiName)
                .OrderByIf<Ship, IQueryable<Ship>>(!sorting.IsNullOrWhiteSpace(), sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}