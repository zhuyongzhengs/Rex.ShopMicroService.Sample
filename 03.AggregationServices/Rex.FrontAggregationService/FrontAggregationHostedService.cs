using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;

namespace Rex.FrontAggregationService
{
    /// <summary>
    /// 服务启动加载Lua文件
    /// </summary>
    public class FrontAggregationHostedService : IHostedService
    {
        /// <summary>
        /// 正常下单Lua执行脚本缓存Key
        /// </summary>
        public const string PlaceOrderKey = "PLACE_ORDER_LUA";

        /// <summary>
        /// [回滚]正常下单Lua执行脚本缓存Key
        /// </summary>
        public const string RollbackPlaceOrderKey = "ROLLBACK_PLACE_ORDER_LUA";

        /// <summary>
        /// 订单秒杀Lua脚本缓存Key
        /// </summary>
        public const string SeckillOrderLuaKey = "SECKILL_ORDER_LUA";

        /// <summary>
        /// 订单回滚有效性缓存Key
        /// </summary>
        public const string PlaceOrderCallbackKey = "PLACE_ORDER_CALLBACK_LUA";

        /// <summary>
        /// 订单回滚Lua脚本缓存Key
        /// </summary>
        public const string SeckillOrderCallbackLuaKey = "SECKILL_ORDER_CALLBACK_LUA";

        private readonly IMemoryCache _memoryCache;

        public FrontAggregationHostedService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// 加载Lua脚本文件缓存
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // 加载正常下单Lua脚本
            FileStream fileStreamPlaceOrder = new FileStream(@"Luas/PlaceOrder.lua", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStreamPlaceOrder))
            {
                string script = reader.ReadToEnd();
                LuaScript vPlaceOrderScript = LuaScript.Prepare(script);
                _memoryCache.Set(PlaceOrderKey, vPlaceOrderScript.ExecutableScript);
            }

            // 加载[回滚]正常下单Lua脚本
            FileStream fileStreamPlaceOrderRollback = new FileStream(@"Luas/PlaceOrderRollback.lua", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStreamPlaceOrderRollback))
            {
                string script = reader.ReadToEnd();
                LuaScript vPlaceOrderScript = LuaScript.Prepare(script);
                _memoryCache.Set(RollbackPlaceOrderKey, vPlaceOrderScript.ExecutableScript);
            }

            // 加载SeckillOrder脚本
            FileStream fileStream = new FileStream(@"Luas/SeckillOrder.lua", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string script = reader.ReadToEnd();
                LuaScript sOrderScript = LuaScript.Prepare(script);
                _memoryCache.Set(SeckillOrderLuaKey, sOrderScript.ExecutableScript);
            }

            // 加载PlaceOrder回滚脚本
            FileStream fileStreamPlaceOrderCallback = new FileStream(@"Luas/PlaceOrderCallback.lua", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStreamPlaceOrderCallback))
            {
                string script = reader.ReadToEnd();
                LuaScript vPlaceOrderScript = LuaScript.Prepare(script);
                _memoryCache.Set(PlaceOrderCallbackKey, vPlaceOrderScript.ExecutableScript);
            }

            // 加载SeckillOrder回滚脚本
            FileStream fileStreamSeckillOrder = new FileStream(@"Luas/SeckillOrderCallback.lua", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStreamSeckillOrder))
            {
                string script = reader.ReadToEnd();
                LuaScript sOrderScript = LuaScript.Prepare(script);
                _memoryCache.Set(SeckillOrderCallbackLuaKey, sOrderScript.ExecutableScript);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}