namespace Rex.FrontAggregationService.Luas
{
    /// <summary>
    /// 下单Lua脚本服务扩展
    /// </summary>
    public static class PlaceOrderServiceCollectionExtensions
    {
        /// <summary>
        /// 添加下单Lua脚本服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddPlaceOrderLua(this IServiceCollection services)
        {
            // 1.本地缓存
            services.AddMemoryCache();

            // 2.Lua脚本加载
            services.AddHostedService<FrontAggregationHostedService>();
            return services;
        }
    }
}