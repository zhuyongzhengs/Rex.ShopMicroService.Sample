using Rex.GoodService.Areas;
using Rex.GoodService.Articles;
using Rex.GoodService.Brands;
using Rex.GoodService.Caching;
using Rex.GoodService.Goods;
using Rex.GoodService.Notices;
using Rex.GoodService.PageDesigns;
using Rex.GoodService.Products;
using Rex.GoodService.ServiceDescriptions;
using Rex.GoodService.ServiceGoods;
using Rex.GoodService.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Commons
{
    /// <summary>
    /// 公共服务接口
    /// </summary>
    public interface ICommonAppService : IApplicationService
    {
        /// <summary>
        /// 设置当前租户值
        /// </summary>
        /// <param name="name">Key</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        Task UpdateSettingCurrentTenantAsync(string name, string? value);

        /// <summary>
        /// 获取库存报警数量
        /// </summary>
        /// <param name="warnNums">库存剩余数</param>
        /// <returns></returns>
        Task<int> GetStockWarnCountAsync(int warnNums);

        /// <summary>
        /// 获取(分页)商品列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<GoodDto>> GetGoodPageListAsync(GetGoodInput input);

        /// <summary>
        /// 根据ID获取商品信息
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <param name="includeDetails">是否包括明细</param>
        /// <returns></returns>
        Task<GoodDto> GetGoodByIdAsync(Guid id, bool includeDetails = true);

        /// <summary>
        /// 根据ID获取商品信息
        /// </summary>
        /// <param name="ids">商品ID</param>
        /// <param name="includeDetails">是否包括明细</param>
        /// <returns></returns>
        Task<List<GoodDto>> GetGoodByIdsAsync(List<Guid> ids, bool includeDetails = true);

        /// <summary>
        /// 获取商品选项卡数据
        /// </summary>
        /// <param name="categoryIds">分类ID</param>
        /// <param name="brandId">品牌ID</param>
        /// <param name="limit">数量</param>
        /// <returns></returns>
        Task<List<GoodDto>> GetGoodTabBarListAsync(List<Guid> categoryIds, Guid brandId, int limit = 10);

        /// <summary>
        /// 根据设计编码查询页面设计
        /// </summary>
        /// <param name="code">设计编码</param>
        /// <returns></returns>
        Task<PageDesignDto> GetPageDesignByCodeAsync(string code);

        /// <summary>
        /// 根据设计编码查询页面设计
        /// </summary>
        /// <param name="layout">布局样式：1、移动端  2、PC端</param>
        /// <param name="code">设计编码(为空则获取默认)</param>
        /// <returns></returns>
        Task<PageDesignDto> GetPageDesignByLayoutCodeAsync(int layout, string code = "");

        /// <summary>
        /// 根据设计编码查询页面设计项
        /// </summary>
        /// <param name="pageCode">设计编码</param>
        /// <returns></returns>
        Task<List<PageDesignItemDto>> GetPageDesignItemByPageCodeAsync(string pageCode);

        /// <summary>
        /// 分页获取通知列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <returns></returns>
        Task<List<NoticeDto>> GetNoticePagedListAsync(int skipCount, int maxResultCount, string sorting);

        /// <summary>
        /// 根据ID获取通知信息
        /// </summary>
        /// <param name="ids">通知ID</param>
        /// <returns></returns>
        Task<List<NoticeDto>> GetNoticeByIdsAsync(List<Guid> ids);

        /// <summary>
        /// 获取商品分类中的子分类
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <returns></returns>
        Task<List<GoodCategoryDto>> GetGoodCategoryByParentIdAsync(Guid parentId);

        /// <summary>
        /// 根据ID获取商品品牌
        /// </summary>
        /// <param name="ids">品牌ID</param>
        /// <returns></returns>
        Task<List<BrandDto>> GetBrandByIdsAsync(List<Guid> ids);

        /// <summary>
        /// 根据文章分类查询文章信息
        /// </summary>
        /// <param name="typeId">分类ID</param>
        /// <param name="isPub">是否发布</param>
        /// <param name="limit">数量</param>
        /// <returns></returns>
        Task<List<ArticleDto>> GetArticleByClassifyIdAsync(Guid typeId, bool? isPub = null, int limit = 10);

        /// <summary>
        /// 根据ID获取服务商品
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<ServiceGoodDto> GetServiceGoodByIdAsync(Guid id);

        /// <summary>
        /// 获取树形商品分类
        /// </summary>
        /// <param name="parentId">上级分类ID</param>
        /// <returns></returns>
        Task<List<GoodCategoryTreeDto>> GetGoodCategorysTreeAsync(Guid? parentId = null);

        /// <summary>
        /// 更新商品浏览次数
        /// </summary>
        /// <param name="goodId">商品ID</param>
        /// <returns></returns>
        Task UpdateGoodViewCountAsync(Guid goodId);

        /// <summary>
        /// 查询商城服务描述
        /// </summary>
        /// <returns></returns>
        Task<List<ServiceDescriptionDto>> GetServiceDescriptionAllAsync();

        /// <summary>
        /// 更新商城服务描述缓存
        /// </summary>
        /// <returns></returns>
        Task UpdateServiceDescriptionCacheAsync();

        /// <summary>
        /// 获取商品推荐数据
        /// </summary>
        /// <param name="limit">获取数量</param>
        /// <param name="isRecommend">是否推荐[默认：true]</param>
        /// <returns></returns>
        Task<List<GoodDto>> GetGoodRecommendListAsync(int limit = 10, bool isRecommend = true);

        /// <summary>
        /// 根据参数ID查询商品参数
        /// </summary>
        /// <param name="paramIds">参数ID</param>
        /// <returns></returns>
        Task<List<GoodParamDto>> GetGoodParamByIdsAsync(List<Guid> paramIds);

        /// <summary>
        /// 获取货品信息
        /// </summary>
        /// <param name="ids">货品ID</param>
        /// <returns></returns>
        Task<List<ProductDto>> GetProductByIdsAsync(List<Guid> ids);

        /// <summary>
        /// 根据ID获取货品信息
        /// </summary>
        /// <param name="id">货品ID</param>
        /// <returns></returns>
        Task<ProductDto> GetProductByIdAsync(Guid id);

        /// <summary>
        /// 获取(树形)区域
        /// </summary>
        /// <returns></returns>
        Task<List<AreaTreeDto>> GetAreaTreeAsync();

        /// <summary>
        /// 根据ID获取门店信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<StoreDto> GetStoreByIdAsync(Guid id);

        /// <summary>
        /// 获取商品会员折扣价格
        /// </summary>
        /// <param name="goodId">商品ID</param>
        /// <returns></returns>
        Task<List<GoodGradeDto>> GetGoodGradeByGoodIdAsync(Guid goodId);

        /// <summary>
        /// 验证用户是否收藏商品
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="goodId">商品ID</param>
        /// <returns></returns>
        Task<bool> GetIsUserGoodCollectionAsync(Guid userId, Guid goodId);

        /// <summary>
        /// 库存变更
        /// </summary>
        /// <param name="productId">货品ID</param>
        /// <param name="type">类型：1[下单]、2[发货]、3[退款]、4[退货]、5[取消订单]、6[完成订单]</param>
        /// <param name="num">数量</param>
        /// <returns></returns>
        Task<bool> ChangeStockAsync(Guid productId, int type = 1, int num = 0);

        /// <summary>
        /// 获取货品库存信息
        /// </summary>
        /// <param name="id">货品ID</param>
        /// <param name="goodId">商品ID</param>
        /// <returns></returns>
        Task<ProductStockRc> GetProductStockAsync(Guid id, Guid goodId);

        /// <summary>
        /// 获取货品库存信息
        /// </summary>
        /// <param name="goodId">商品ID</param>
        /// <returns></returns>
        Task<List<ProductStockRc>> GetProductStocksAsync(Guid goodId);

        /// <summary>
        /// 根据商品ID获取货品信息
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <returns></returns>
        Task<List<ProductDto>> GetProductByGoodIdsAsync(Guid id);

        /// <summary>
        /// 根据坐标获取门店
        /// </summary>
        /// <param name="storeName">门店名称</param>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="latitude">纬度</param>
        /// <param name="longitude">精度</param>
        /// <returns></returns>
        Task<PagedResultDto<StoreDto>> GetStoreCoordinateListAsync(string? storeName, int skipCount, int maxResultCount, string sorting, decimal latitude = 0, decimal longitude = 0);

        /// <summary>
        /// 商品收藏
        /// </summary>
        /// <param name="goodId">商品ID</param>
        /// <remarks>
        /// 存在：删除、不存在：新增
        /// </remarks>
        /// <returns></returns>
        Task CreateGoodCollectionAsync(Guid goodId);

        /// <summary>
        /// 查询(用户)商品收藏
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        Task<PagedResultDto<GoodCollectionDto>> GetUserGoodCollectionAsync(GetGoodCollectionInput input);

        /// <summary>
        /// 商品浏览记录
        /// </summary>
        /// <param name="goodId">商品ID</param>
        /// <remarks>
        /// 存在：删除、不存在：新增
        /// </remarks>
        /// <returns></returns>
        Task UserGoodBrowsingByGoodIdAsync(Guid goodId);

        /// <summary>
        /// 查询(用户)浏览记录
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        Task<PagedResultDto<GoodBrowsingDto>> GetUserGoodBrowsingAsync(GetGoodBrowsingInput input);

        /// <summary>
        /// 获取用户浏览记录数量
        /// </summary>
        /// <returns></returns>
        Task<long> GetUserGoodBrowsingCountAsync();

        /// <summary>
        /// 获取用户商品收藏
        /// </summary>
        /// <returns></returns>
        Task<long> GetUserGoodCollectionCountAsync();

        /// <summary>
        /// 查询文章信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<ArticleDto> GetArticleByIdAsync(Guid id, bool includeDetails = true);

        /// <summary>
        /// 查询通知信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<NoticeDto> GetNoticeByIdAsync(Guid id);

        /// <summary>
        /// 查询文章分类
        /// </summary>
        /// <param name="limit">查询数量</param>
        /// <returns></returns>
        Task<List<ArticleTypeDto>> GetArticleTypeAsync(int limit = 1000);

        /// <summary>
        /// 获取(分页)文章列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<ArticleDto>> GetArticlePageListAsync(GetArticleInput input);

        /// <summary>
        /// 获取(分页)商品评价列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<GoodCommentDto>> GetGoodCommentPageListAsync(GetGoodCommentInput input);

        /// <summary>
        /// 获取商品数量
        /// </summary>
        /// <param name="goodsStocksWarn">库存报警数</param>
        /// <returns></returns>
        Task<GoodCountDto> GetGoodCountAsync(int goodsStocksWarn);
    }
}