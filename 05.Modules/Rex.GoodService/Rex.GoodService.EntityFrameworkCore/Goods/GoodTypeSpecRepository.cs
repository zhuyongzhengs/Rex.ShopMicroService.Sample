using Microsoft.EntityFrameworkCore;
using Rex.GoodService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品类型规格仓储
    /// </summary>
    public class GoodTypeSpecRepository : EfCoreRepository<GoodServiceDbContext, GoodTypeSpec, Guid>, IGoodTypeSpecRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public GoodTypeSpecRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取(分页) 商品类型规格列表总数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns></returns>
        public async Task<long> GetPageCountAsync(string name = "", CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取(分页) 商品类型规格列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="name">参数名称</param>
        /// <returns></returns>
        public async Task<List<GoodTypeSpec>> GetPageListAsync(int skipCount, int maxResultCount, string sorting, string name = "", CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                    .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                    .Include(p => p.SpecValues)
                    .OrderByIf<GoodTypeSpec, IQueryable<GoodTypeSpec>>(!sorting.IsNullOrWhiteSpace(), sorting)
                    .PageBy(skipCount, maxResultCount)
                    .ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 根据ID获取商品类型规格信息
        /// </summary>
        /// <param name="ids">商品类型规格ID</param>
        /// <returns></returns>
        public async Task<List<GoodTypeSpec>> GetListByIdAsync(List<Guid> ids, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(p => ids.Contains(p.Id))
                .Include(p => p.SpecValues)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}