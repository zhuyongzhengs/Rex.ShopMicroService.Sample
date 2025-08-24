using Microsoft.EntityFrameworkCore;
using Rex.GoodService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.StoreClerks
{
    /// <summary>
    /// 店铺店员关联仓储
    /// </summary>
    public class StoreClerkRepository : EfCoreRepository<GoodServiceDbContext, StoreClerk, Guid>, IStoreClerkRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public StoreClerkRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取(分页) 店铺店员关联列表总数
        /// </summary>
        /// <param name="storeId">店铺ID</param>
        /// <param name="userIds">用户ID(多个用“,”隔开)</param>
        /// <returns></returns>
        public async Task<long> GetPageCountAsync(Guid? storeId, string userIds, CancellationToken cancellationToken = default)
        {
            List<Guid> sysUserIds = new List<Guid>();
            if (!userIds.IsNullOrWhiteSpace())
            {
                foreach (var userId in userIds.Split(','))
                {
                    sysUserIds.Add(Guid.Parse(userId));
                }
            }
            return await (await GetDbSetAsync())
                .WhereIf(sysUserIds.Any(), p => sysUserIds.Contains(p.UserId))
                .WhereIf(storeId.HasValue, p => p.StoreId == storeId)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取(分页) 店铺店员关联列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="storeId">店铺ID</param>
        /// <param name="userIds">用户ID(多个用“,”隔开)</param>
        /// <returns></returns>
        public async Task<List<StoreClerk>> GetPageListAsync(int skipCount, int maxResultCount, string sorting, Guid? storeId, string userIds, CancellationToken cancellationToken = default)
        {
            List<Guid> sysUserIds = new List<Guid>();
            if (!userIds.IsNullOrWhiteSpace())
            {
                foreach (var userId in userIds.Split(','))
                {
                    sysUserIds.Add(Guid.Parse(userId));
                }
            }
            return await (await GetDbSetAsync())
                .WhereIf(sysUserIds.Any(), p => sysUserIds.Contains(p.UserId))
                .WhereIf(storeId.HasValue, p => p.StoreId == storeId)
                .Include(p => p.Store)
                .OrderByIf<StoreClerk, IQueryable<StoreClerk>>(!sorting.IsNullOrWhiteSpace(), sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}