using System;
using Volo.Abp.Caching;

namespace Rex.GoodService.Caching
{
    /// <summary>
    /// 货品库存缓存
    /// </summary>
    [CacheName("ProductStock")]
    [Serializable]
    public class ProductStockRc
    {
        /// <summary>
        /// 缓存前缀
        /// </summary>
        public const string CacheKey = "Rex.GoodService:{0}:Product:{1}";

        /// <summary>
        /// 库存数量
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 冻结库存数量
        /// </summary>
        public int FreezeStock { get; set; }
    }
}