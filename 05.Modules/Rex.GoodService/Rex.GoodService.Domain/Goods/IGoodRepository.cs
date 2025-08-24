using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品仓储接口
    /// </summary>
    public interface IGoodRepository : IRepository<Good, Guid>
    {
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
        Task<long> GetPageCountAsync(
            string name = "",
            Guid? goodsCategoryId = null,
            Guid? brandId = null,
            bool? isMarketable = null,
            bool? isStockWarn = null,
            int? goodStockWarn = null,
            CancellationToken cancellationToken = default
            );

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
        Task<List<Good>> GetPageListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string name = "",
            Guid? goodsCategoryId = null,
            Guid? brandId = null,
            bool? isMarketable = null,
            bool? isStockWarn = null,
            int? goodStockWarn = null,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 根据ID获取商品信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="includeDetails">是否包含明细</param>
        /// <returns></returns>
        Task<Good> GetGoodByIdAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据ID获取商品信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="includeDetails">是否包含明细</param>
        /// <returns></returns>
        Task<List<Good>> GetGoodByIdsAsync(List<Guid> ids, bool includeDetails = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取商品选项卡数据
        /// </summary>
        /// <param name="categoryIds">分类ID</param>
        /// <param name="brandId">品牌ID</param>
        /// <param name="limit">数量</param>
        /// <returns></returns>
        Task<List<Good>> GetGoodTabBarListAsync(List<Guid> categoryIds, Guid brandId, int limit = 10, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取商品推荐数据
        /// </summary>
        /// <param name="limit">获取数量</param>
        /// <param name="isRecommend">是否推荐[默认：true]</param>
        /// <returns></returns>
        Task<List<Good>> GetGoodRecommendListAsync(int limit = 10, bool isRecommend = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新货品库存缓存
        /// </summary>
        /// <param name="id">货品ID</param>
        /// <param name="goodId">商品ID</param>
        /// <param name="stock">库存</param>
        /// <returns></returns>
        Task UpdateProductStockCacheAsync(Guid id, Guid goodId, int stock);

        /// <summary>
        /// 删除货品库存缓存
        /// </summary>
        /// <param name="id">货品ID</param>
        /// <param name="goodId">商品ID</param>
        /// <returns></returns>
        Task DeleteProductStockCacheAsync(Guid id, Guid goodId);

        /// <summary>
        /// 获取库存报警数量
        /// </summary>
        /// <param name="warnNums">库存剩余数</param>
        /// <returns></returns>
        Task<int> GetStockWarnCountAsync(int warnNums);
    }
}