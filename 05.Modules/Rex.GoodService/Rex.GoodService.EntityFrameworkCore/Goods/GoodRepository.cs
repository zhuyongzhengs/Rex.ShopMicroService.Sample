using Microsoft.EntityFrameworkCore;
using Rex.GoodService.Caching;
using Rex.GoodService.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品仓储
    /// </summary>
    public class GoodRepository : EfCoreRepository<GoodServiceDbContext, Good, Guid>, IGoodRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }
        public IDistributedCache<ProductStockRc> ProductStockDistributedCache { get; set; }
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public GoodRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider, IConnectionMultiplexer connectionMultiplexer) : base(dbContextProvider)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        /// <summary>
        /// 获取(分页) 商品列表总数
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <param name="goodsCategoryId">商品分类</param>
        /// <param name="brandId">品牌</param>
        /// <param name="isMarketable">是否上架</param>
        /// <param name="isStockWarn">是否库存报警</param>
        /// <param name="goodStockWarn">商品库存警告数</param>
        /// <returns></returns>
        public async Task<long> GetPageCountAsync(string name = "", Guid? goodsCategoryId = null, Guid? brandId = null, bool? isMarketable = null, bool? isStockWarn = null, int? goodStockWarn = null, CancellationToken cancellationToken = default)
        {
            if (!goodStockWarn.HasValue) goodStockWarn = 0;
            return await (await GetDbSetAsync())
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .WhereIf(goodsCategoryId.HasValue, p => p.GoodCategoryId == goodsCategoryId)
                .WhereIf(brandId.HasValue, p => p.BrandId == brandId)
                .WhereIf(isMarketable.HasValue, p => p.IsMarketable == isMarketable)
                .WhereIf(isStockWarn.HasValue && isStockWarn == true, p => p.Products.Any(x => (x.Stock - x.FreezeStock) <= goodStockWarn.Value))
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取(分页) 商品列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="name">商品名称</param>
        /// <param name="goodsCategoryId">商品分类</param>
        /// <param name="brandId">品牌</param>
        /// <param name="isMarketable">是否上架</param>
        /// <param name="isStockWarn">是否库存报警</param>
        /// <param name="goodStockWarn">商品库存警告数</param>
        /// <returns></returns>
        public async Task<List<Good>> GetPageListAsync(int skipCount, int maxResultCount, string sorting, string name = "", Guid? goodsCategoryId = null, Guid? brandId = null, bool? isMarketable = null, bool? isStockWarn = null,
            int? goodStockWarn = null, CancellationToken cancellationToken = default)
        {
            if (!goodStockWarn.HasValue) goodStockWarn = 0;
            IQueryable<Good> queryable = (await GetDbSetAsync())
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .WhereIf(goodsCategoryId.HasValue, p => p.GoodCategoryId == goodsCategoryId)
                .WhereIf(brandId.HasValue, p => p.BrandId == brandId)
                .WhereIf(isMarketable.HasValue, p => p.IsMarketable == isMarketable)
                .WhereIf(isStockWarn.HasValue && isStockWarn == true, p => p.Products.Any(x => (x.Stock - x.FreezeStock) <= goodStockWarn.Value))
                .Include(p => p.Brand)
                .Include(p => p.GoodCategory)
                .Include(p => p.Products)
                .ThenInclude(product => product.ProductDistribution);
            if (!sorting.IsNullOrEmpty() && sorting.Trim().Equals("Price ASC", StringComparison.OrdinalIgnoreCase))
            {
                queryable = queryable.OrderBy(g => g.Products.Min(p => p.Price));
            }
            else if (!sorting.IsNullOrEmpty() && sorting.Trim().Equals("Price DESC", StringComparison.OrdinalIgnoreCase))
            {
                queryable = queryable.OrderByDescending(g => g.Products.Max(p => p.Price));
            }
            else
            {
                queryable = queryable.OrderByIf<Good, IQueryable<Good>>(!sorting.IsNullOrWhiteSpace(), sorting);
            }
            /*
            List<Good> goods = await (await GetDbSetAsync())
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .WhereIf(goodsCategoryId.HasValue, p => p.GoodCategoryId == goodsCategoryId)
                .WhereIf(brandId.HasValue, p => p.BrandId == brandId)
                .WhereIf(isMarketable.HasValue, p => p.IsMarketable == isMarketable)
                .Include(p => p.Brand)
                .Include(p => p.GoodCategory)
                .Include(p => p.Products)
                .ThenInclude(product => product.ProductDistribution)
                .OrderByIf<Good, IQueryable<Good>>(!sorting.IsNullOrWhiteSpace(), sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
            */
            return await queryable.PageBy(skipCount, maxResultCount).ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 根据ID获取商品信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="includeDetails">是否包含明细</param>
        /// <remarks>
        /// 【AsNoTracking】只做查询，不跟踪实体的变化
        /// </remarks>
        /// <returns></returns>
        public async Task<Good> GetGoodByIdAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            IQueryable<Good> query = (await GetDbSetAsync()).Where(p => p.Id == id);
            if (includeDetails)
            {
                return await query.Include(p => p.Brand)
                .Include(p => p.GoodCategory)
                .Include(p => p.Brand)
                .Include(p => p.Products)
                .ThenInclude(product => product.ProductDistribution)
                .AsNoTracking()
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 根据ID获取商品信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="includeDetails">是否包含明细</param>
        /// <returns></returns>
        public async Task<List<Good>> GetGoodByIdsAsync(List<Guid> ids, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            if (!ids.Any()) return new List<Good>();
            IQueryable<Good> query = (await GetDbSetAsync()).Where(p => ids.Contains(p.Id));
            if (includeDetails)
            {
                return await query.Include(p => p.Brand)
                .Include(p => p.GoodCategory)
                .Include(p => p.Products)
                .ThenInclude(product => product.ProductDistribution)
                .OrderBy(p => p.Sort)
                .ToListAsync(GetCancellationToken(cancellationToken));
            }
            return await query.OrderBy(p => p.Sort).ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取商品选项卡数据
        /// </summary>
        /// <param name="categoryIds">分类ID</param>
        /// <param name="brandId">品牌ID</param>
        /// <param name="limit">数量</param>
        /// <returns></returns>
        public async Task<List<Good>> GetGoodTabBarListAsync(List<Guid> categoryIds, Guid brandId, int limit = 10, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(p => categoryIds.Contains(p.GoodCategoryId))
                .Where(p => p.BrandId == brandId)
                .OrderBy(p => p.Sort)
                .Include(p => p.Brand)
                .Include(p => p.GoodCategory)
                .Include(p => p.Products)
                .ThenInclude(product => product.ProductDistribution)
                .PageBy(0, limit)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取商品推荐数据
        /// </summary>
        /// <param name="limit">获取数量</param>
        /// <param name="isRecommend">是否推荐[默认：true]</param>
        /// <returns></returns>
        public async Task<List<Good>> GetGoodRecommendListAsync(int limit = 10, bool isRecommend = true, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(isRecommend, p => p.IsRecommend)
                .Include(p => p.Brand)
                .Include(p => p.GoodCategory)
                .Include(p => p.Products)
                .ThenInclude(product => product.ProductDistribution)
                .OrderBy(p => p.Sort)
                .PageBy(0, limit)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 更新货品库存缓存
        /// </summary>
        /// <param name="id">货品ID</param>
        /// <param name="goodId">商品ID</param>
        /// <param name="stock">库存</param>
        /// <returns></returns>
        public async Task UpdateProductStockCacheAsync(Guid id, Guid goodId, int stock)
        {
            string cacheKey = string.Format(ProductStockRc.CacheKey, $"{{{goodId.ToString()}}}", $"{{{id.ToString()}}}");
            IDatabase redisDatabase = _connectionMultiplexer.GetDatabase();

            var pStockKeys = new RedisKey[] { cacheKey };
            var pStockValues = new RedisValue[] {
                stock // ARGV[1]
            };
            RedisResult rResult = await redisDatabase.ScriptEvaluateAsync(
                @"
                    local pStockKey = KEYS[1];
	                local pStock = redis.call('HGET',pStockKey,'Stock');
	                if pStock then
		                redis.call('HSET',pStockKey,'Stock',ARGV[1]);
	                    return 1;
	                end
                    return 0;
                ",
                pStockKeys,
                pStockValues
            );
            string execResult = rResult?.ToString();
            if (execResult.IsNullOrWhiteSpace() || execResult.Equals("0"))
                Debug.WriteLine("该货品信息未缓存！");
        }

        /// <summary>
        /// 删除货品库存缓存
        /// </summary>
        /// <param name="id">货品ID</param>
        /// <param name="goodId">商品ID</param>
        /// <returns></returns>
        public async Task DeleteProductStockCacheAsync(Guid id, Guid goodId)
        {
            string cacheKey = string.Format(ProductStockRc.CacheKey, $"{{{goodId.ToString()}}}", $"{{{id.ToString()}}}");
            IDatabase redisDatabase = _connectionMultiplexer.GetDatabase();
            await redisDatabase.KeyDeleteAsync(cacheKey);
        }

        /// <summary>
        /// 获取库存报警数量
        /// </summary>
        /// <param name="warnNums">库存剩余数</param>
        /// <returns></returns>
        public async Task<int> GetStockWarnCountAsync(int warnNums)
        {
            return await (await GetDbSetAsync())
                .Where(p => p.Products.Any(x => (x.Stock - x.FreezeStock) <= warnNums))
                .CountAsync();
        }
    }
}