using Microsoft.EntityFrameworkCore;
using Rex.AreaService.Areas;
using Rex.GoodService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.Areas
{
    /// <summary>
    /// 区域仓储
    /// </summary>
    public class AreaRepository : EfCoreRepository<GoodServiceDbContext, Area, long>, IAreaRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public AreaRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取(分页) 区域列表总数
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <param name="name">区域名称</param>
        /// <returns></returns>
        public async Task<long> GetPageCountAsync(long? parentId, string name, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(parentId.HasValue, p => p.ParentId == parentId)
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取(分页) 区域列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="parentId">父级ID</param>
        /// <param name="name">区域名称</param>
        /// <returns></returns>
        public async Task<List<Area>> GetPageListAsync(int skipCount, int maxResultCount, string sorting, long? parentId, string name, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(parentId.HasValue, p => p.ParentId == parentId)
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .Include(p => p.ParentArea)
                .OrderByIf<Area, IQueryable<Area>>(!sorting.IsNullOrWhiteSpace(), sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}