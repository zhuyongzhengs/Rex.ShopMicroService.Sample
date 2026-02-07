using Microsoft.AspNetCore.Authorization;
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
using Rex.Service.AspNetCore.Extensions;
using Rex.Service.Core.Configurations;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.SettingManagement;

namespace Rex.GoodService.Commons
{
    /// <summary>
    /// 公共服务
    /// </summary>
    [RemoteService]
    public class CommonAppService : ApplicationService, ICommonAppService
    {
        private readonly IPageDesignRepository _pageDesignRepository;
        private readonly IPageDesignItemRepository _pageDesignItemRepository;
        private readonly INoticeRepository _noticeRepository;
        private readonly IGoodRepository _goodRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGoodCategoryRepository _goodCategoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IServiceGoodRepository _serviceGoodRepository;
        private readonly IServiceDescriptionRepository _serviceDescriptionRepository;
        private readonly IGoodParamRepository _goodParamRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IArticleTypeRepository _articleTypeRepository;
        private readonly IGoodCommentRepository _goodCommentRepository;
        private readonly IRepository<GoodGrade, Guid> _goodGradeRepository;
        private readonly IRepository<GoodCollection, Guid> _goodCollectionRepository;
        private readonly IRepository<GoodBrowsing, Guid> _goodBrowsingRepository;
        private readonly IConnectionMultiplexer _multiplexer;

        public ISettingManager SettingManager { get; set; }
        public IGoodCategoryAppService GoodCategoryAppService { get; set; }
        public IGoodsAppService GoodAppService { get; set; }
        public IAreaAppService AreaAppService { get; set; }
        public IDistributedCache<List<ServiceDescriptionDto>> ServiceDescriptionDistributedCache { get; set; }
        //public IDistributedCache<ProductStockRc> ProductStockDistributedCache { get; set; }

        public CommonAppService(IGoodRepository goodRepository, IPageDesignRepository pageDesignRepository, IPageDesignItemRepository pageDesignItemRepository, INoticeRepository noticeRepository, IGoodCategoryRepository goodCategoryRepository, IBrandRepository brandRepository, IArticleRepository articleRepository, IServiceGoodRepository serviceGoodRepository, IServiceDescriptionRepository serviceDescriptionRepository, IGoodParamRepository goodParamRepository, IProductRepository productRepository, IStoreRepository storeRepository, IArticleTypeRepository articleTypeRepository, IGoodCommentRepository goodCommentRepository, IRepository<GoodGrade, Guid> goodGradeRepository, IRepository<GoodCollection, Guid> goodCollectionRepository, IRepository<GoodBrowsing, Guid> goodBrowsingRepository, IConnectionMultiplexer multiplexer)
        {
            _goodRepository = goodRepository;
            _pageDesignRepository = pageDesignRepository;
            _pageDesignItemRepository = pageDesignItemRepository;
            _noticeRepository = noticeRepository;
            _goodCategoryRepository = goodCategoryRepository;
            _brandRepository = brandRepository;
            _articleRepository = articleRepository;
            _serviceGoodRepository = serviceGoodRepository;
            _serviceDescriptionRepository = serviceDescriptionRepository;
            _goodParamRepository = goodParamRepository;
            _productRepository = productRepository;
            _storeRepository = storeRepository;
            _articleTypeRepository = articleTypeRepository;
            _goodCommentRepository = goodCommentRepository;
            _goodGradeRepository = goodGradeRepository;
            _goodCollectionRepository = goodCollectionRepository;
            _goodBrowsingRepository = goodBrowsingRepository;
            _multiplexer = multiplexer;
        }

        /// <summary>
        /// 设置当前租户值
        /// </summary>
        /// <param name="name">Key</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        [Authorize]
        public async Task UpdateSettingCurrentTenantAsync(string name, string? value)
        {
            await SettingManager.SetForCurrentTenantAsync(name, value);
        }

        /// <summary>
        /// 获取库存报警数量
        /// </summary>
        /// <param name="warnNums">库存剩余数</param>
        /// <returns></returns>
        [Authorize]
        public async Task<int> GetStockWarnCountAsync(int warnNums)
        {
            return await _goodRepository.GetStockWarnCountAsync(warnNums);
        }

        /// <summary>
        /// 根据ID获取商品信息
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <param name="includeDetails">是否包括明细</param>
        /// <returns></returns>
        public async Task<GoodDto> GetGoodByIdAsync(Guid id, bool includeDetails = true)
        {
            Good good = await _goodRepository.GetGoodByIdAsync(id, includeDetails);
            if (good == null || !good.IsMarketable) return null;
            if (good.Products != null && good.Products.Any())
            {
                Product product = good.Products.Where(p => p.IsDefault)?.FirstOrDefault();
                if (product != null)
                {
                    good.Price = product.Price;
                    good.CostPrice = product.CostPrice;
                    good.MktPrice = product.MktPrice;
                }

                #region 将[库存]写入缓存(Redis)中

                foreach (var item in good.Products)
                {
                    ProductStockRc productStockRc = await GetProductStockAsync(item.Id, id);
                    if (productStockRc == null) continue;
                    item.Stock = productStockRc.Stock;
                    item.FreezeStock = productStockRc.FreezeStock;
                }

                #endregion 将[库存]写入缓存(Redis)中
            }

            await UpdateGoodViewCountAsync(id);

            GoodDto goodItem = ObjectMapper.Map<Good, GoodDto>(good);
            if (CurrentUser.Id.HasValue)
            {
                goodItem.IsFav = await GetIsUserGoodCollectionAsync(CurrentUser.Id.Value, goodItem.Id);
                await UserGoodBrowsingByGoodIdAsync(good.Id);
            }
            return goodItem;
        }

        /// <summary>
        /// 根据ID获取商品信息
        /// </summary>
        /// <param name="ids">商品ID</param>
        /// <param name="includeDetails">是否包括明细</param>
        /// <returns></returns>
        public async Task<List<GoodDto>> GetGoodByIdsAsync(List<Guid> ids, bool includeDetails = true)
        {
            List<Good> goods = await _goodRepository.GetGoodByIdsAsync(ids, includeDetails);
            foreach (var good in goods)
            {
                Product product = good.Products.Where(p => p.IsDefault).FirstOrDefault();
                if (product == null) continue;
                good.Price = product.Price;
                good.CostPrice = product.CostPrice;
                good.MktPrice = product.MktPrice;
            }
            return ObjectMapper.Map<List<Good>, List<GoodDto>>(goods);
        }

        /// <summary>
        /// 根据设计编码查询页面设计
        /// </summary>
        /// <param name="code">设计编码</param>
        /// <returns></returns>
        public async Task<PageDesignDto> GetPageDesignByCodeAsync(string code)
        {
            PageDesign pageDesign = (await _pageDesignRepository.GetQueryableAsync()).Where(p => p.Code == code).FirstOrDefault();
            if (pageDesign == null)
                throw new UserFriendlyException($"页面编码[{code}]不存在或已被删除，请检查！", SystemStatusCode.Fail.ToGoodServicePrefix());
            return ObjectMapper.Map<PageDesign, PageDesignDto>(pageDesign);
        }

        /// <summary>
        /// 根据设计编码查询页面设计
        /// </summary>
        /// <param name="layout">布局样式：1、移动端  2、PC端</param>
        /// <param name="code">设计编码</param>
        /// <remarks>
        /// [设计编码]为空则获取默认的设计编码
        /// </remarks>
        /// <returns></returns>
        public async Task<PageDesignDto> GetPageDesignByLayoutCodeAsync(int layout, string code = "")
        {
            Expression<Func<PageDesign, bool>> pageDesignExp = p => p.Layout == layout;
            if (code.IsNullOrWhiteSpace())
                pageDesignExp = pageDesignExp.And(p => p.Type == 1);
            else
                pageDesignExp = pageDesignExp.And(p => p.Code == code);
            PageDesign pageDesign = (await _pageDesignRepository.GetQueryableAsync()).Where(pageDesignExp).FirstOrDefault();
            if (pageDesign == null)
                throw new UserFriendlyException($"页面编码[{code}]不存在或已被删除，请检查！", SystemStatusCode.Fail.ToGoodServicePrefix());
            return ObjectMapper.Map<PageDesign, PageDesignDto>(pageDesign);
        }

        /// <summary>
        /// 根据设计编码查询页面设计项
        /// </summary>
        /// <param name="pageCode">设计编码</param>
        /// <returns></returns>
        public async Task<List<PageDesignItemDto>> GetPageDesignItemByPageCodeAsync(string pageCode)
        {
            List<PageDesignItem> pageDesignItems = (await _pageDesignItemRepository.GetQueryableAsync()).OrderBy(p => p.Sort).Where(p => p.PageCode == pageCode).ToList();
            return ObjectMapper.Map<List<PageDesignItem>, List<PageDesignItemDto>>(pageDesignItems);
        }

        /// <summary>
        /// 分页获取通知列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <returns></returns>
        public async Task<List<NoticeDto>> GetNoticePagedListAsync(int skipCount, int maxResultCount, string sorting)
        {
            List<Notice> notices = await _noticeRepository.GetPagedListAsync(skipCount, maxResultCount, "Sort ASC");
            return ObjectMapper.Map<List<Notice>, List<NoticeDto>>(notices);
        }

        /// <summary>
        /// 根据ID获取通知信息
        /// </summary>
        /// <param name="ids">通知ID</param>
        /// <returns></returns>
        public async Task<List<NoticeDto>> GetNoticeByIdsAsync(List<Guid> ids)
        {
            List<Notice> notices = (await _noticeRepository.GetQueryableAsync()).Where(p => ids.Contains(p.Id)).OrderBy(p => p.Sort).ToList();
            return ObjectMapper.Map<List<Notice>, List<NoticeDto>>(notices);
        }

        /// <summary>
        /// 获取商品分类中的子分类
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <returns></returns>
        public async Task<List<GoodCategoryDto>> GetGoodCategoryByParentIdAsync(Guid parentId)
        {
            List<GoodCategory> goodCategorys = (await _goodCategoryRepository.GetQueryableAsync()).Where(p => p.ParentId == parentId && p.IsShow).OrderBy(p => p.Sort).ToList();
            return ObjectMapper.Map<List<GoodCategory>, List<GoodCategoryDto>>(goodCategorys);
        }

        /// <summary>
        /// 根据ID获取商品品牌
        /// </summary>
        /// <param name="ids">品牌ID</param>
        /// <returns></returns>
        public async Task<List<BrandDto>> GetBrandByIdsAsync(List<Guid> ids)
        {
            List<Brand> brands = (await _brandRepository.GetQueryableAsync()).Where(p => ids.Contains(p.Id)).OrderBy(p => p.Sort).ToList();
            return ObjectMapper.Map<List<Brand>, List<BrandDto>>(brands);
        }

        /// <summary>
        /// 获取商品选项卡数据
        /// </summary>
        /// <param name="categoryIds">分类ID</param>
        /// <param name="brandId">品牌ID</param>
        /// <param name="limit">数量</param>
        /// <returns></returns>
        public async Task<List<GoodDto>> GetGoodTabBarListAsync(List<Guid> categoryIds, Guid brandId, int limit = 10)
        {
            List<Good> goods = await _goodRepository.GetGoodTabBarListAsync(categoryIds, brandId, limit);
            foreach (var good in goods)
            {
                Product product = good.Products.Where(p => p.IsDefault).FirstOrDefault();
                if (product == null) continue;
                good.Price = product.Price;
                good.CostPrice = product.CostPrice;
                good.MktPrice = product.MktPrice;
            }
            return ObjectMapper.Map<List<Good>, List<GoodDto>>(goods);
        }

        /// <summary>
        /// 根据文章分类查询文章信息
        /// </summary>
        /// <param name="typeId">分类ID</param>
        /// <param name="isPub">是否发布</param>
        /// <param name="limit">数量</param>
        /// <returns></returns>
        public async Task<List<ArticleDto>> GetArticleByClassifyIdAsync(Guid typeId, bool? isPub = null, int limit = 10)
        {
            List<Article> articles = (await _articleRepository.GetQueryableAsync())
                .Where(p => p.TypeId == typeId)
                .WhereIf(isPub.HasValue, p => p.IsPub == isPub.Value)
                .OrderByDescending(p => p.CreationTime)
                .PageBy(0, limit).ToList();
            return ObjectMapper.Map<List<Article>, List<ArticleDto>>(articles);
        }

        /// <summary>
        /// 根据ID获取服务商品
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public async Task<ServiceGoodDto> GetServiceGoodByIdAsync(Guid id)
        {
            ServiceGood serviceGood = await _serviceGoodRepository.GetAsync(id);
            return ObjectMapper.Map<ServiceGood, ServiceGoodDto>(serviceGood);
        }

        /// <summary>
        /// 获取树形商品分类
        /// </summary>
        /// <param name="parentId">上级分类ID</param>
        /// <returns></returns>
        public async Task<List<GoodCategoryTreeDto>> GetGoodCategorysTreeAsync(Guid? parentId = null)
        {
            return await GoodCategoryAppService.GetTreeAsync(parentId);
        }

        /// <summary>
        /// 获取(分页)商品列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<GoodDto>> GetGoodPageListAsync(GetGoodInput input)
        {
            // 查询数量
            long totalCount = await _goodRepository.GetPageCountAsync(input.Name, input.GoodCategoryId, input.BrandId, input.IsMarketable, input.IsStockWarn, input.GoodStockWarn);
            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<GoodDto>();
            }
            List<Good> goodList = await _goodRepository.GetPageListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Name, input.GoodCategoryId, input.BrandId, input.IsMarketable, input.IsStockWarn, input.GoodStockWarn);
            foreach (var good in goodList)
            {
                Product product = good.Products.Where(p => p.IsDefault).FirstOrDefault();
                if (product == null) continue;
                good.Price = product.Price;
                good.CostPrice = product.CostPrice;
                good.MktPrice = product.MktPrice;
            }
            return new PagedResultDto<GoodDto>(
                totalCount,
                ObjectMapper.Map<List<Good>, List<GoodDto>>(goodList)
            );
        }

        /// <summary>
        /// 更新商品浏览次数
        /// </summary>
        /// <param name="goodId">商品ID</param>
        /// <returns></returns>
        public async Task UpdateGoodViewCountAsync(Guid goodId)
        {
            Good good = await _goodRepository.GetAsync(goodId);
            if (good != null) good.ViewCount++;
        }

        /// <summary>
        /// 查询商城服务描述
        /// </summary>
        /// <remarks>
        /// 在缓存中查询不到则会从原始数据库中获取(得到数据则会存入缓存中)
        /// </remarks>
        /// <returns></returns>
        public async Task<List<ServiceDescriptionDto>> GetServiceDescriptionAllAsync()
        {
            return await ServiceDescriptionDistributedCache.GetOrAddAsync(GlobalConsts.CacheServiceDescriptionKey,
                async () =>
                {
                    List<ServiceDescription> serviceDescriptions = (await _serviceDescriptionRepository.GetQueryableAsync()).Where(p => p.IsShow).OrderBy(p => p.Sort).PageBy(0, 100000).ToList();
                    return ObjectMapper.Map<List<ServiceDescription>, List<ServiceDescriptionDto>>(serviceDescriptions);
                });
        }

        /// <summary>
        /// 更新商城服务描述缓存
        /// </summary>
        /// <returns></returns>
        public async Task UpdateServiceDescriptionCacheAsync()
        {
            List<ServiceDescription> serviceDescriptions = (await _serviceDescriptionRepository.GetQueryableAsync()).Where(p => p.IsShow).OrderBy(p => p.Sort).PageBy(0, 100000).ToList();
            if (!serviceDescriptions.Any())
            {
                await ServiceDescriptionDistributedCache.RemoveAsync(GlobalConsts.CacheServiceDescriptionKey);
                return;
            }
            List<ServiceDescriptionDto> serviceDescriptionList = ObjectMapper.Map<List<ServiceDescription>, List<ServiceDescriptionDto>>(serviceDescriptions);
            await ServiceDescriptionDistributedCache.SetAsync(GlobalConsts.CacheServiceDescriptionKey, serviceDescriptionList);
        }

        /// <summary>
        /// 获取商品推荐数据
        /// </summary>
        /// <param name="limit">获取数量</param>
        /// <param name="isRecommend">是否推荐[默认：true]</param>
        /// <returns></returns>
        public async Task<List<GoodDto>> GetGoodRecommendListAsync(int limit = 10, bool isRecommend = true)
        {
            List<Good> goods = await _goodRepository.GetGoodRecommendListAsync(limit, isRecommend);
            foreach (var good in goods)
            {
                Product product = good.Products.Where(p => p.IsDefault).FirstOrDefault();
                if (product == null) continue;
                good.Price = product.Price;
                good.CostPrice = product.CostPrice;
                good.MktPrice = product.MktPrice;
            }
            return ObjectMapper.Map<List<Good>, List<GoodDto>>(goods);
        }

        /// <summary>
        /// 根据参数ID查询商品参数
        /// </summary>
        /// <param name="paramIds">参数ID</param>
        /// <returns></returns>
        public async Task<List<GoodParamDto>> GetGoodParamByIdsAsync(List<Guid> paramIds)
        {
            List<GoodParam> goodParams = await _goodParamRepository.GetListAsync(p => paramIds.Contains(p.Id));
            return ObjectMapper.Map<List<GoodParam>, List<GoodParamDto>>(goodParams);
        }

        /// <summary>
        /// 获取货品信息
        /// </summary>
        /// <param name="ids">货品ID</param>
        /// <returns></returns>
        public async Task<List<ProductDto>> GetProductByIdsAsync(List<Guid> ids)
        {
            if (!ids.Any()) return new List<ProductDto>();
            List<Product> products = await _productRepository.GetListAsync(x => ids.Contains(x.Id));
            return ObjectMapper.Map<List<Product>, List<ProductDto>>(products);
        }

        /// <summary>
        /// 获取(树形)区域
        /// </summary>
        /// <returns></returns>
        public async Task<List<AreaTreeDto>> GetAreaTreeAsync()
        {
            return await AreaAppService.GetTreeManyAsync();
        }

        /// <summary>
        /// 根据ID获取门店信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [Authorize]
        public async Task<StoreDto> GetStoreByIdAsync(Guid id)
        {
            Store store = await _storeRepository.GetAsync(id);
            return ObjectMapper.Map<Store, StoreDto>(store);
        }

        /// <summary>
        /// 根据ID获取货品信息
        /// </summary>
        /// <param name="id">货品ID</param>
        /// <returns></returns>
        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            Product product = await _productRepository.GetAsync(id);
            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        /// <summary>
        /// 获取商品会员折扣价格
        /// </summary>
        /// <param name="goodId">商品ID</param>
        /// <returns></returns>
        public async Task<List<GoodGradeDto>> GetGoodGradeByGoodIdAsync(Guid goodId)
        {
            List<GoodGrade> goodGrades = await _goodGradeRepository.GetListAsync(p => p.GoodId == goodId);
            return ObjectMapper.Map<List<GoodGrade>, List<GoodGradeDto>>(goodGrades);
        }

        /// <summary>
        /// 验证用户是否收藏商品
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="goodId">商品ID</param>
        /// <returns></returns>
        public async Task<bool> GetIsUserGoodCollectionAsync(Guid userId, Guid goodId)
        {
            GoodCollection goodCollection = await _goodCollectionRepository.FindAsync(p => p.GoodId == goodId && p.UserId == userId);
            return goodCollection != null;
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
            return await _productRepository.ChangeStockAsync(productId, type, num);
        }

        /// <summary>
        /// 获取货品库存信息
        /// </summary>
        /// <param name="id">货品ID</param>
        /// <param name="goodId">商品ID</param>
        /// <returns></returns>
        public async Task<ProductStockRc> GetProductStockAsync(Guid id, Guid goodId)
        {
            string productStockKey = string.Format(ProductStockRc.CacheKey, $"{{{goodId.ToString()}}}", $"{{{id.ToString()}}}");

            var db = _multiplexer.GetDatabase();
            var getStockScript = @"
                local stockVal = tostring(redis.call('HGET',KEYS[1], 'Stock') or '');
                local freezeStockVal = tostring(redis.call('HGET',KEYS[1], 'FreezeStock') or '');
                if string.len(stockVal) == 0 or string.len(freezeStockVal) == 0 then
                    return nil;
                end
                return { stockVal, freezeStockVal };
            ";
            var stockResult = await db.ScriptEvaluateAsync(
                getStockScript,
                new RedisKey[] { productStockKey }
            );
            if (!stockResult.IsNull)
            {
                RedisValue[] redisValues = (RedisValue[])stockResult;
                int stockVal = (int)redisValues[0];
                int freezeStockVal = (int)redisValues[1];
                return new ProductStockRc()
                {
                    Stock = stockVal,
                    FreezeStock = freezeStockVal
                };
            }
            Product product = await _productRepository.GetAsync(id);
            if (product == null) return null;
            ProductStockRc productStock = new ProductStockRc { Stock = product.Stock, FreezeStock = product.FreezeStock };
            var setStockScript = @"
                redis.call('HSET',KEYS[1], 'Stock', ARGV[1]);
                redis.call('HSET',KEYS[1], 'FreezeStock', ARGV[2]);
                return 1;
            ";
            await db.ScriptEvaluateAsync(
                setStockScript,
                new RedisKey[] { productStockKey },
                new RedisValue[] { product.Stock, product.FreezeStock }
            );
            return productStock;
        }

        /// <summary>
        /// 获取货品库存信息
        /// </summary>
        /// <param name="goodId">商品ID</param>
        /// <returns></returns>
        public async Task<List<ProductStockRc>> GetProductStocksAsync(Guid goodId)
        {
            List<ProductDto> productList = await GetProductByGoodIdsAsync(goodId);
            if (productList == null || productList.Count == 0) return new List<ProductStockRc>();
            List<ProductStockRc> productStockRcs = new List<ProductStockRc>();
            foreach (var product in productList)
            {
                ProductStockRc productStockRc = await GetProductStockAsync(product.Id, goodId);
                productStockRcs.Add(productStockRc);
            }
            return productStockRcs;
        }

        /// <summary>
        /// 根据商品ID获取货品信息
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <returns></returns>
        public async Task<List<ProductDto>> GetProductByGoodIdsAsync(Guid id)
        {
            List<Product> products = await _productRepository.GetListAsync(p => p.GoodId == id);
            return ObjectMapper.Map<List<Product>, List<ProductDto>>(products);
        }

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
        public async Task<PagedResultDto<StoreDto>> GetStoreCoordinateListAsync(string? storeName, int skipCount, int maxResultCount, string sorting, decimal latitude = 0, decimal longitude = 0)
        {
            // 查询数量
            long totalCount = await _storeRepository.GetStoreByCoordinateCountAsync(storeName, latitude, longitude);

            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<StoreDto>();
            }
            List<Store> storeList = await _storeRepository.GetStoreByCoordinateListAsync(storeName, skipCount, maxResultCount, sorting, latitude, longitude);

            List<StoreDto> stores = ObjectMapper.Map<List<Store>, List<StoreDto>>(storeList);
            foreach (var store in stores)
            {
                if (store.Distance <= 0)
                {
                    store.DistanceDisplay = "未知";
                    continue;
                }
                if (store.Distance > 1000)
                {
                    store.DistanceDisplay = Math.Round(store.Distance / 1000, 2) + "km";
                }
                else
                {
                    store.DistanceDisplay = Math.Round(store.Distance, 2) + "m";
                }
            }

            return new PagedResultDto<StoreDto>(
                totalCount,
                stores
            );
        }

        /// <summary>
        /// 商品收藏
        /// </summary>
        /// <param name="goodId">商品ID</param>
        /// <remarks>
        /// 存在：删除、不存在：新增
        /// </remarks>
        /// <returns></returns>
        public async Task CreateGoodCollectionAsync(Guid goodId)
        {
            if (!CurrentUser.Id.HasValue) return;
            GoodCollection goodCollection = await _goodCollectionRepository.FindAsync(p => p.GoodId == goodId && p.UserId == CurrentUser.Id.Value);
            if (goodCollection == null)
            {
                // 收藏
                Good good = await _goodRepository.GetAsync(goodId);
                goodCollection = new GoodCollection();
                goodCollection.GoodId = goodId;
                goodCollection.Image = good.Image;
                goodCollection.UserId = CurrentUser.Id.Value;
                goodCollection.GoodName = good.Name;
                await _goodCollectionRepository.InsertAsync(goodCollection);
                return;
            }

            // 取消收藏
            await _goodCollectionRepository.DeleteDirectAsync(p => p.GoodId == goodId && p.UserId == CurrentUser.Id.Value);
        }

        /// <summary>
        /// 查询(用户)商品收藏
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        public async Task<PagedResultDto<GoodCollectionDto>> GetUserGoodCollectionAsync(GetGoodCollectionInput input)
        {
            // 查询数量
            long totalCount = (await _goodCollectionRepository.GetQueryableAsync())
                .Where(p => p.UserId == CurrentUser.Id)
                .WhereIf(!input.GoodName.IsNullOrWhiteSpace(), p => p.GoodName.Contains(input.GoodName))
                .LongCount();

            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<GoodCollectionDto>();
            }
            List<GoodCollection> goodCollectionList = (await _goodCollectionRepository.GetQueryableAsync())
                .Where(p => p.UserId == CurrentUser.Id)
                .WhereIf(!input.GoodName.IsNullOrWhiteSpace(), p => p.GoodName.Contains(input.GoodName))
                .OrderByIf<GoodCollection, IQueryable<GoodCollection>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                .PageBy(input.SkipCount, input.MaxResultCount)
                .ToList();

            return new PagedResultDto<GoodCollectionDto>(
                totalCount,
                ObjectMapper.Map<List<GoodCollection>, List<GoodCollectionDto>>(goodCollectionList)
            );
        }

        /// <summary>
        /// 商品浏览记录
        /// </summary>
        /// <param name="goodId">商品ID</param>
        /// <remarks>
        /// 存在：删除、不存在：新增
        /// </remarks>
        /// <returns></returns>
        public async Task UserGoodBrowsingByGoodIdAsync(Guid goodId)
        {
            if (!CurrentUser.Id.HasValue) return;
            GoodBrowsing goodBrowsing = await _goodBrowsingRepository.FindAsync(p => p.GoodId == goodId && p.UserId == CurrentUser.Id.Value);
            if (goodBrowsing != null)
                await _goodBrowsingRepository.DeleteDirectAsync(p => p.GoodId == goodId && p.UserId == CurrentUser.Id.Value);

            Good good = await _goodRepository.GetAsync(goodId);
            goodBrowsing = new GoodBrowsing();
            goodBrowsing.GoodId = goodId;
            goodBrowsing.Image = good.Image;
            goodBrowsing.UserId = CurrentUser.Id.Value;
            goodBrowsing.GoodName = good.Name;
            await _goodBrowsingRepository.InsertAsync(goodBrowsing);
        }

        /// <summary>
        /// 查询(用户)浏览记录
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        public async Task<PagedResultDto<GoodBrowsingDto>> GetUserGoodBrowsingAsync(GetGoodBrowsingInput input)
        {
            // 查询数量
            long totalCount = (await _goodBrowsingRepository.GetQueryableAsync())
                .Where(p => p.UserId == CurrentUser.Id)
                .WhereIf(!input.GoodName.IsNullOrWhiteSpace(), p => p.GoodName.Contains(input.GoodName))
                .LongCount();

            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<GoodBrowsingDto>();
            }
            List<GoodBrowsing> goodBrowsingList = (await _goodBrowsingRepository.GetQueryableAsync())
                .Where(p => p.UserId == CurrentUser.Id)
                .WhereIf(!input.GoodName.IsNullOrWhiteSpace(), p => p.GoodName.Contains(input.GoodName))
                .OrderByIf<GoodBrowsing, IQueryable<GoodBrowsing>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                .PageBy(input.SkipCount, input.MaxResultCount)
                .ToList();

            return new PagedResultDto<GoodBrowsingDto>(
                totalCount,
                ObjectMapper.Map<List<GoodBrowsing>, List<GoodBrowsingDto>>(goodBrowsingList)
            );
        }

        /// <summary>
        /// 获取用户浏览记录数量
        /// </summary>
        /// <returns></returns>
        public async Task<long> GetUserGoodBrowsingCountAsync()
        {
            long totalCount = (await _goodBrowsingRepository.GetQueryableAsync()).Where(x => x.UserId == CurrentUser.Id).LongCount();
            return totalCount;
        }

        /// <summary>
        /// 获取用户商品收藏
        /// </summary>
        /// <returns></returns>
        public async Task<long> GetUserGoodCollectionCountAsync()
        {
            long totalCount = (await _goodCollectionRepository.GetQueryableAsync()).Where(x => x.UserId == CurrentUser.Id).LongCount();
            return totalCount;
        }

        /// <summary>
        /// 查询文章信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public async Task<ArticleDto> GetArticleByIdAsync(Guid id, bool includeDetails = true)
        {
            Article article = await _articleRepository.GetArticleByIdAsync(id, includeDetails);
            if (article == null) return null;
            return ObjectMapper.Map<Article, ArticleDto>(article);
        }

        /// <summary>
        /// 查询通知信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public async Task<NoticeDto> GetNoticeByIdAsync(Guid id)
        {
            Notice notice = await _noticeRepository.GetAsync(id);
            if (notice == null) return null;
            return ObjectMapper.Map<Notice, NoticeDto>(notice);
        }

        /// <summary>
        /// 查询文章分类
        /// </summary>
        /// <param name="limit">查询数量</param>
        /// <returns></returns>
        public async Task<List<ArticleTypeDto>> GetArticleTypeAsync(int limit = 1000)
        {
            List<ArticleType> articleTypeList = (await _articleTypeRepository.GetQueryableAsync())
                .OrderBy(x => x.Sort)
                .PageBy(0, limit)
                .ToList();
            return ObjectMapper.Map<List<ArticleType>, List<ArticleTypeDto>>(articleTypeList);
        }

        /// <summary>
        /// 获取(分页)文章列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<ArticleDto>> GetArticlePageListAsync(GetArticleInput input)
        {
            // 查询数量
            long totalCount = await _articleRepository.GetPageCountAsync(input.Title, input.TypeId, input.IsPub);
            if (totalCount < 1)
            {
                return new PagedResultDto<ArticleDto>();
            }
            // 查询数据列表
            List<Article> articleList = await _articleRepository.GetPageListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Title, input.TypeId, input.IsPub);
            return new PagedResultDto<ArticleDto>(
                totalCount,
                ObjectMapper.Map<List<Article>, List<ArticleDto>>(articleList)
            );
        }

        /// <summary>
        /// 获取(分页)商品评价列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<GoodCommentDto>> GetGoodCommentPageListAsync(GetGoodCommentInput input)
        {
            // 查询数量
            long totalCount = (await _goodCommentRepository.GetQueryableAsync())
                .WhereIf(input.Score.HasValue && input.Score > 0, p => p.Score == input.Score)
                .WhereIf(input.UserId.HasValue, p => p.UserId == input.UserId)
                .WhereIf(input.GoodId.HasValue, p => p.GoodId == input.GoodId)
                .WhereIf(input.OrderId.HasValue, p => p.OrderId == input.OrderId)
                .WhereIf(input.IsDisplay.HasValue, p => p.IsDisplay == input.IsDisplay)
                .LongCount();

            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<GoodCommentDto>();
            }
            List<GoodComment> goodCommentList = (await _goodCommentRepository.GetQueryableAsync())
                .WhereIf(input.Score.HasValue && input.Score > 0, p => p.Score == input.Score)
                .WhereIf(input.UserId.HasValue, p => p.UserId == input.UserId)
                .WhereIf(input.GoodId.HasValue, p => p.GoodId == input.GoodId)
                .WhereIf(input.OrderId.HasValue, p => p.OrderId == input.OrderId)
                .WhereIf(input.IsDisplay.HasValue, p => p.IsDisplay == input.IsDisplay)
                .OrderByIf<GoodComment, IQueryable<GoodComment>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                .PageBy(input.SkipCount, input.MaxResultCount)
                .ToList();

            return new PagedResultDto<GoodCommentDto>(
                totalCount,
                ObjectMapper.Map<List<GoodComment>, List<GoodCommentDto>>(goodCommentList)
            );
        }

        /// <summary>
        /// 获取商品数量
        /// </summary>
        /// <param name="goodsStocksWarn">库存报警数</param>
        /// <returns></returns>
        public async Task<GoodCountDto> GetGoodCountAsync(int goodsStocksWarn)
        {
            GoodCountDto goodCount = new GoodCountDto();
            goodCount.AllCount = (await _goodRepository.GetQueryableAsync()).LongCount();
            goodCount.UpMarketableCount = (await _goodRepository.GetQueryableAsync()).Where(p => p.IsMarketable).LongCount();
            goodCount.DownMarketableCount = (await _goodRepository.GetQueryableAsync()).Where(p => !p.IsMarketable).LongCount();
            if (goodsStocksWarn > 0)
            {
                goodCount.StockWarningCount = (await _productRepository.GetQueryableAsync()).Where(x => x.Stock <= goodsStocksWarn).Select(x => x.GoodId).Distinct().Count();
            }
            return goodCount;
        }
    }
}