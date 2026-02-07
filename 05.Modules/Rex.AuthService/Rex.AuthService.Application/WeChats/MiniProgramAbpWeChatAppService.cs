using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;

namespace Rex.AuthService.WeChats
{
    /// <summary>
    /// 微信小程序应用服务接口
    /// </summary>
    [RemoteService]
    public class MiniProgramAbpWeChatAppService : ApplicationService, IMiniProgramAbpWeChatAppService
    {
        private readonly IDistributedCache<string> _cache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public MiniProgramAbpWeChatAppService(IDistributedCache<string> cache, IConnectionMultiplexer multiplexer)
        {
            _cache = cache;
            _connectionMultiplexer = multiplexer;
        }

        /// <summary>
        /// 测试分布式Redis缓存
        /// </summary>
        /// <param name="testValue">测试值</param>
        /// <returns></returns>
        public async Task TestDistributedRedisCache(string testValue)
        {
            await _cache.SetAsync("D_TEST", testValue);

            string productStockKey = "Rex.GoodService:{GD001}:Product:{P001}";
            var db = _connectionMultiplexer.GetDatabase();
            var getStockScript = @"
                local stockVal = tostring(redis.call('HGET',KEYS[1], 'Stock') or '');
                local freezeStockVal = tostring(redis.call('HGET',KEYS[1], 'FreezeStock') or '');
                if string.len(stockVal) == 0 or string.len(freezeStockVal) == 0 then
                    return nil;
                end
                return { stockVal, freezeStockVal };
            ";
            var stock = await db.ScriptEvaluateAsync(
                getStockScript,
                new RedisKey[] { productStockKey }
            );
            if (!stock.IsNull)
            {
                Console.WriteLine(((RedisValue[]?)stock)[0]);
                Console.WriteLine(((RedisValue[]?)stock)[1]);
            }
            var setStockScript = @"
                redis.call('HSET',KEYS[1], 'Stock', ARGV[1]);
                redis.call('HSET',KEYS[1], 'FreezeStock', ARGV[2]);
                return {ARGV[1], ARGV[2]};
            ";
            var result = await db.ScriptEvaluateAsync(
                setStockScript,
                new RedisKey[] { productStockKey },
                new RedisValue[] { 100, 5 }
            );

            if (result.Type == ResultType.MultiBulk)
            {
                //var results = result.ToArray();
                //Console.WriteLine($"第一个值: {results[0]}"); // 输出: 100
                //Console.WriteLine($"第二个值: {results[1]}"); // 输出: 5
            }
            else
            {
                Console.WriteLine($"单值: {result}");
            }
            Debug.WriteLine(result);

            HashSet<string> stockKeys = new HashSet<string>();
            EndPoint[] endPoints = _connectionMultiplexer.GetEndPoints();
            foreach (var endPoint in endPoints)
            {
                IServer server = _connectionMultiplexer.GetServer(endPoint);
                var allKeys = server.Keys(pattern: "Rex.GoodService:{*}:StockDirtyKey").ToArray();
                foreach (var key in allKeys)
                {
                    stockKeys.Add(key.ToString());
                }
            }
            if (stockKeys.Count < 1) return;
            IDatabase redisDatabase = _connectionMultiplexer.GetDatabase();

            Dictionary<Guid, int> freezeStockDic = new Dictionary<Guid, int>();
            foreach (var stockKey in stockKeys)
            {
                // 获取 Redis 中的库存值：Rex.GoodService:{xxx}:StockDirtyKey
                string key = stockKey.ToString();

                //long listLength = redisDatabase.ListLength(key);
                RedisValue[] productStocckKeyValues = redisDatabase.ListRange(key);
                foreach (var productStocckKeyVal in productStocckKeyValues)
                {
                    // 得到商品库存Key：Rex.GoodService:{xxx}:Product:{xxx}
                    string productStocckKey = productStocckKeyVal.ToString();
                    var productIdStr = productStocckKey.Split(':').Last().Replace("{", "").Replace("}", "");
                    if (!Guid.TryParse(productIdStr, out Guid productId)) continue;
                    RedisValue fStockValue = await redisDatabase.HashGetAsync(new RedisKey(productStocckKey), new RedisValue("FreezeStock"));
                    if (!int.TryParse(fStockValue.ToString(), out int fStock)) continue;
                    freezeStockDic.Add(productId, fStock);
                }
            }
        }
    }
}