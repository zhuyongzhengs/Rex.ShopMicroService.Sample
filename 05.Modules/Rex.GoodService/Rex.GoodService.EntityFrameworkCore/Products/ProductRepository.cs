using Microsoft.EntityFrameworkCore;
using Rex.GoodService.Caching;
using Rex.GoodService.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.GoodService.Products
{
    /// <summary>
    /// 货品仓储
    /// </summary>
    public class ProductRepository : EfCoreRepository<GoodServiceDbContext, Product, Guid>, IProductRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public IDistributedCache<ProductStockRc> ProductStockDistributedCache { get; set; }

        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public ProductRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider, IConnectionMultiplexer connectionMultiplexer) : base(dbContextProvider)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        /// <summary>
        /// 库存变更
        /// </summary>
        /// <param name="productId">货品ID</param>
        /// <param name="type">类型：1[下单]、2[发货]、3[退款]、4[退货]、5[取消订单]、6[完成订单]</param>
        /// <param name="num">数量</param>
        /// <returns></returns>
        public async Task<bool> ChangeStockAsync(Guid productId, int type = 1, int num = 0)
        {
            Product product = await (await GetDbSetAsync()).Where(x => x.Id == productId).FirstOrDefaultAsync();
            if (product == null) return false;
            //int rowsUpdated = 0;
            switch (type)
            {
                // 下单
                case (int)OrderChangeStockType.Order:
                    // TODO
                    break;

                // 发货
                case (int)OrderChangeStockType.Send:
                    // TODO
                    break;

                // 退款
                case (int)OrderChangeStockType.Refund:
                    // TODO
                    break;

                // 退货
                case (int)OrderChangeStockType.Return:
                    ReduceProductFreezeStock(productId, product.GoodId, num);
                    break;

                // 取消订单
                case (int)OrderChangeStockType.Cancel:
                    ReduceProductFreezeStock(productId, product.GoodId, num);
                    break;

                // 完成订单
                case (int)OrderChangeStockType.Complete:
                    // TODO
                    break;

                default:
                    break;
            }
            return true;
        }

        /// <summary>
        /// 取消(还原)库存
        /// </summary>
        /// <remarks>
        /// -FreezeStock
        /// </remarks>
        /// <returns></returns>
        private void ReduceProductFreezeStock(Guid id, Guid goodId, int num)
        {
            string productStockKey = string.Format(ProductStockRc.CacheKey, $"{{{goodId.ToString()}}}", $"{{{id.ToString()}}}");
            IDatabase redisDatabase = _connectionMultiplexer.GetDatabase();
            var setFreezeStockScript = @"
                if redis.call('EXISTS', KEYS[1]) ~= 1 then
                    return 0;
                end
                local pStockValue = tonumber(redis.call('HGET',KEYS[1], 'FreezeStock') or '0');
                local newFreezeStock = pStockValue - tonumber(ARGV[1]);
                redis.call('HSET',KEYS[1], 'FreezeStock', newFreezeStock);
                return 1;
            ";
            redisDatabase.ScriptEvaluate(
                setFreezeStockScript,
                new RedisKey[] { productStockKey },
                new RedisValue[] { num }
            );
        }

        /// <summary>
        /// 扣减库存
        /// </summary>
        /// <remarks>
        /// +FreezeStock
        /// </remarks>
        /// <returns></returns>
        private void ReduceProductStock(Guid id, Guid goodId, int num)
        {
            string productStockKey = string.Format(ProductStockRc.CacheKey, $"{{{goodId.ToString()}}}", $"{{{id.ToString()}}}");
            IDatabase redisDatabase = _connectionMultiplexer.GetDatabase();
            var setFreezeStockScript = @"
                if redis.call('EXISTS', KEYS[1]) ~= 1 then
                    return 0;
                end
                local pStockValue = tonumber(redis.call('HGET',KEYS[1], 'FreezeStock') or '0');
                local newFreezeStock = pStockValue + tonumber(ARGV[1]);
                redis.call('HSET',KEYS[1], 'FreezeStock', newFreezeStock);
                return 1;
            ";
            redisDatabase.ScriptEvaluate(
                setFreezeStockScript,
                new RedisKey[] { productStockKey },
                new RedisValue[] { num }
            );
        }

        /// <summary>
        /// 修改商品冻结库存
        /// </summary>
        /// <param name="freezeStockDic">冻结库存</param>
        /// <returns></returns>
        public async Task UpdateFreezeStockAsync(Dictionary<Guid, int> freezeStockDic)
        {
            foreach (var item in freezeStockDic)
            {
                await (await GetDbSetAsync())
                    .Where(x => x.Id == item.Key)
                    .ExecuteUpdateAsync(x =>
                        x.SetProperty(s => s.FreezeStock, item.Value)
                    );
            }
        }
    }
}