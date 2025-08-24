using Microsoft.Extensions.Logging;
using Quartz;
using Rex.GoodService.Products;
using Rex.Service.Core.Helper;
using Rex.Service.Core.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Caching;

namespace Rex.GoodService.BackgroundWorkers
{
    /// <summary>
    /// 同步商品库存到数据库
    /// </summary>
    public class SyncProductStockToDatabaseWorker : QuartzBackgroundWorkerBase
    {
        public IDistributedCache<WorkerStatusRc> WorkerStatusDistributedCache { get; set; }

        private readonly IProductRepository _productRepository;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public SyncProductStockToDatabaseWorker(IProductRepository productRepository, IConnectionMultiplexer connectionMultiplexer)
        {
            _productRepository = productRepository;
            _connectionMultiplexer = connectionMultiplexer;
            JobDetail = JobBuilder.Create<SyncProductStockToDatabaseWorker>().WithIdentity(nameof(SyncProductStockToDatabaseWorker)).Build();
            Trigger = TriggerBuilder.Create()
            .WithIdentity(nameof(SyncProductStockToDatabaseWorker))
            //.StartNow()
            .StartAt(DateBuilder.FutureDate(30, IntervalUnit.Second)) // 30秒后开启(防止项目还未启动就执行调度)
            // 每隔 1 小时执行一次
            .WithSimpleSchedule(s => s
                .WithIntervalInHours(1)
                .RepeatForever())
            //.WithCronSchedule("0 0 * * * ? *")
            .Build();
        }

        public override async Task Execute(IJobExecutionContext context)
        {
            IDatabase redisDatabase = _connectionMultiplexer.GetDatabase();
            string rKey = $"Rex.GoodService:Lock:{{{nameof(SyncProductStockToDatabaseWorker)}}}";
            await DistributedTaskHelper.RunWithRedisLockAsync(
                redisDatabase,
                rKey,
                Logger,
                async () =>
                {
                    await ProductStockToDatabase(redisDatabase);
                });
            await Task.CompletedTask;
        }

        /// <summary>
        /// 商品库存同步到数据库
        /// </summary>
        /// <param name="redisDatabase"></param>
        /// <returns></returns>
        private async Task ProductStockToDatabase(IDatabase redisDatabase)
        {
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

            Dictionary<Guid, int> freezeStockDic = new Dictionary<Guid, int>();
            foreach (var stockKey in stockKeys)
            {
                // 获取 Redis 中的库存值：Rex.GoodService:{xxx}:StockDirtyKey
                string key = stockKey.ToString();

                var keyType = redisDatabase.KeyType(key);
                if (keyType != RedisType.List)
                {
                    Logger.LogWarning($"Key {key} 类型为 {keyType}，不是 List，已跳过。");
                    continue;
                }

                //long listLength = redisDatabase.ListLength(key);
                RedisValue[] productStocckKeyValues = redisDatabase.ListRange(new RedisKey(key));
                if (productStocckKeyValues == null || productStocckKeyValues.Length < 1)
                    continue;

                foreach (var productStocckKeyVal in productStocckKeyValues)
                {
                    // 得到商品库存Key：Rex.GoodService:{xxx}:Product:{xxx}
                    string productStocckKey = productStocckKeyVal.ToString();
                    var productIdStr = productStocckKey.Split(':').Last().Replace("{", "").Replace("}", "");
                    if (!Guid.TryParse(productIdStr, out Guid productId)) continue;

                    var fStockValue = redisDatabase.HashGet(new RedisKey(productStocckKey), new RedisValue("FreezeStock"));
                    if (!int.TryParse(fStockValue, out int fStock)) continue;
                    freezeStockDic.Add(productId, fStock);

                    // 移除数据
                    redisDatabase.ListRemove(stockKey, productStocckKeyVal);
                }
            }
            if (freezeStockDic.Count > 0)
                await _productRepository.UpdateFreezeStockAsync(freezeStockDic);
        }
    }
}