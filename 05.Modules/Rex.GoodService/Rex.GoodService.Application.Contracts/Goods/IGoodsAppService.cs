using Rex.GoodService.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品服务接口
    /// </summary>
    public interface IGoodsAppService : IApplicationService
    {
        /// <summary>
        /// 根据ID查询商品信息
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <returns></returns>
        Task<GoodDetailDto> GetGoodDetailAsync(Guid id);

        /// <summary>
        /// 获取(分页)商品列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<GoodDto>> GetListAsync(GetGoodInput input);

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="input">商品信息</param>
        /// <returns></returns>
        Task CreateAsync(GoodDetailDto input);

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="input">商品信息</param>
        /// <returns></returns>
        Task UpdateAsync(GoodDetailDto input);

        /// <summary>
        /// 修改商品是否上架
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <param name="isMarketable">是否上架</param>
        /// <returns></returns>
        Task UpdateIsMarketableAsync(Guid id, bool isMarketable);

        /// <summary>
        /// 修改商品是否推荐
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <param name="isRecommend">是否推荐</param>
        /// <returns></returns>
        Task UpdateIsRecommendAsync(Guid id, bool isRecommend);

        /// <summary>
        /// 修改商品是否热门
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <param name="isHot">是否热门</param>
        /// <returns></returns>
        Task UpdateIsHotAsync(Guid id, bool isHot);

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 商品价格修改
        /// </summary>
        /// <param name="input">修改价格Dto</param>
        /// <returns></returns>
        Task UpdatePriceAsync(GoodPriceUpdateDto input);

        /// <summary>
        /// 商品库存修改
        /// </summary>
        /// <param name="input">修改库存Dto</param>
        /// <returns></returns>
        Task UpdateStockAsync(GoodStockUpdateDto input);

        /// <summary>
        /// 批量修改商品是否上架
        /// </summary>
        /// <param name="input">修改上架Dto</param>
        /// <returns></returns>
        Task UpdateIsMarketableManyAsync(GoodMarketableUpdateDto input);

        /// <summary>
        /// 批量删除商品
        /// </summary>
        /// <param name="ids">商品ID</param>
        /// <returns></returns>
        Task DeleteManyAsync(List<Guid> ids);

        /// <summary>
        /// 批量打标签
        /// </summary>
        /// <param name="input">商品标签</param>
        /// <returns></returns>
        Task UpdateGoodLabelManyAsync(GoodLabelManyDto input);

        /// <summary>
        /// 根据ID获取商品信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="includeDetails">是否包括明细</param>
        /// <returns></returns>
        Task<List<GoodDto>> GetGoodByIdsAsync(List<Guid> ids, bool includeDetails = true);

        /// <summary>
        /// 更新货品库存信息
        /// </summary>
        /// <param name="input">更新参数</param>
        /// <returns></returns>
        Task UpdateProductStocksAsync(List<ProductDetailUpdateDto> input);
    }
}