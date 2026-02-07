using Microsoft.AspNetCore.Authorization;
using Rex.GoodService.Labels;
using Rex.GoodService.Products;
using Rex.Service.Core.Configurations;
using Rex.Service.Permission.GoodServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.SettingManagement;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品服务
    /// </summary>
    [RemoteService]
    [Authorize(GoodServicePermissions.Goods.Default)]
    public class GoodsAppService : ApplicationService, IGoodsAppService
    {
        public IRepository<Product, Guid> ProductRepository { get; set; }
        public IRepository<ProductDistribution, Guid> ProductDistributionRepository { get; set; }
        public IRepository<GoodCategoryExtend, Guid> GoodCategoryExtendRepository { get; set; }
        public IRepository<GoodGrade, Guid> GoodGradeRepository { get; set; }
        private readonly IGoodRepository _goodRepository;
        private readonly ILabelRepository _labelRepository;

        public GoodsAppService(IGoodRepository goodRepository, ILabelRepository labelRepository, ISettingManager settingManager)
        {
            _goodRepository = goodRepository;
            _labelRepository = labelRepository;
        }

        /// <summary>
        /// 获取(分页)商品列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<GoodDto>> GetListAsync(GetGoodInput input)
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
        /// 添加商品
        /// </summary>
        /// <param name="input">商品信息</param>
        /// <returns></returns>
        public async Task CreateAsync(GoodDetailDto input)
        {
            #region 校验

            if (!input.SetSkuProduct.Products.Any())
                throw new UserFriendlyException($"至少包含一条货品信息！", SystemStatusCode.Fail.ToGoodServicePrefix(), "必须包含货品信息。"); // .WithData("SetSkuProduct.Products", input.SetSkuProduct.Products);

            if (input.SetSkuProduct.Products.Where(p => p.IsDefault).Count() < 1)
                throw new UserFriendlyException($"请设置某个货品为默认！", SystemStatusCode.Fail.ToGoodServicePrefix(), "必须包含默认的货品。");

            if (string.IsNullOrWhiteSpace(input.ImageOrVideo.Image))
                throw new UserFriendlyException($"请上传封面图片！", SystemStatusCode.Fail.ToGoodServicePrefix(), "商品封面图片不能为空。");

            if (string.IsNullOrWhiteSpace(input.ImageOrVideo.Images))
                throw new UserFriendlyException($"请上传图集！", SystemStatusCode.Fail.ToGoodServicePrefix(), "商品图集不能为空。");

            // 检验货品数据
            foreach (var product in input.SetSkuProduct.Products)
            {
                if (!product.Sn.StartsWith("SN", StringComparison.InvariantCultureIgnoreCase))
                    throw new UserFriendlyException($"请输入货品号，货号以“SN”英文开头！", SystemStatusCode.Fail.ToGoodServicePrefix(), "货品号不能为空，且必须以“SN”英文字母开头。");
                if (product.Stock <= 0)
                    throw new UserFriendlyException($"库存不能为0！", SystemStatusCode.Fail.ToGoodServicePrefix(), "货品库存不能小于0。");
                if (product.Price <= 0)
                    throw new UserFriendlyException($"销售价不能为0！", SystemStatusCode.Fail.ToGoodServicePrefix(), "货品销售价不能小于0。");
                if (product.MktPrice <= 0 || product.MktPrice < product.Price)
                    throw new UserFriendlyException($"市场价格不能为0，且不能小于销售价格！", SystemStatusCode.Fail.ToGoodServicePrefix(), "货品市场价格不能为0并且不能小于销售价格。");
                if (product.CostPrice <= 0 || product.CostPrice > product.Price)
                    throw new UserFriendlyException($"成本价格不能为0，且不能大于销售价格！", SystemStatusCode.Fail.ToGoodServicePrefix(), "成本价格不能为0并且不能大于销售价格。");
                if (product.LevelOne < 0 || product.LevelTwo < 0 || product.LevelThree < 0)
                    throw new UserFriendlyException($"返现金额不能小于0！", SystemStatusCode.Fail.ToGoodServicePrefix(), "返现金额(一/二/三级佣金)不能小于0。");
            }
            // 不允许存在重复的货号
            var productSnGroup = input.SetSkuProduct.Products.GroupBy(p => p.Sn).Where(p => p.Count() > 1).ToList();
            if (productSnGroup.Count > 0)
                throw new UserFriendlyException($"货品信息存在相同的货号，请检查！", SystemStatusCode.Fail.ToGoodServicePrefix(), "货品信息不允许存在相同的货号。");

            // 检验系统中是否存在重复的货号
            string[] productSns = input.SetSkuProduct.Products.Select(p => p.Sn).ToArray();
            long productSnCount = (await ProductRepository.GetQueryableAsync()).Where(p => productSns.Contains(p.Sn)).LongCount();
            if (productSnCount > 0)
                throw new UserFriendlyException($"系统中存在相同的货品号，请重新生成货品货号！", SystemStatusCode.Fail.ToGoodServicePrefix(), "货品信息不允许存在相同的货号。");

            #endregion 校验

            #region 商品信息

            Good goodCreate = new Good();
            goodCreate.BarCode = input.BasicInfo.BarCode;
            goodCreate.Name = input.BasicInfo.Name;
            goodCreate.Brief = input.BasicInfo.Brief;
            goodCreate.GoodCategoryId = input.BasicInfo.GoodCategoryId;
            goodCreate.BrandId = input.BasicInfo.BrandId;
            goodCreate.IsNomalVirtual = input.BasicInfo.IsNomalVirtual;
            goodCreate.IsMarketable = input.BasicInfo.IsMarketable;
            goodCreate.Unit = input.BasicInfo.Unit;
            goodCreate.Intro = input.Intro;
            goodCreate.Sort = input.BasicInfo.Sort;
            goodCreate.IsRecommend = input.BasicInfo.IsRecommend;
            goodCreate.IsHot = input.BasicInfo.IsHot;
            goodCreate.Image = input.ImageOrVideo.Image;
            goodCreate.Images = input.ImageOrVideo.Images;
            goodCreate.Video = input.ImageOrVideo.Video;
            goodCreate.ProductsDistributionType = input.SetSkuProduct.ProductsDistributionType;
            goodCreate.GoodSkuIds = input.SetSkuProduct.GoodSkuIds;
            goodCreate.SpesDesc = input.SetSkuProduct.SpesDesc;
            goodCreate.NewSpec = input.SetSkuProduct.NewSpec;
            goodCreate.OpenSpec = input.SetSkuProduct.OpenSpec;
            goodCreate.GoodParamsIds = input.SetParam.GoodParamsIds;
            goodCreate.Parameters = input.SetParam.Parameters;

            if (goodCreate.OpenSpec != 1)
            {
                goodCreate.NewSpec = null;
                goodCreate.SpesDesc = null;
                goodCreate.Parameters = null;
                goodCreate.GoodTypeId = Guid.Empty;
                goodCreate.GoodSkuIds = null;
                goodCreate.GoodParamsIds = null;
            }

            // 保存商品
            goodCreate = await _goodRepository.InsertAsync(goodCreate);

            #endregion 商品信息

            #region 商品分类扩展

            if (!input.BasicInfo.GoodCategoryIdExtends.IsNullOrWhiteSpace())
            {
                string[] goodCategoryIdExtends = input.BasicInfo.GoodCategoryIdExtends.Split(',');
                List<GoodCategoryExtend> goodCategoryExtends = new List<GoodCategoryExtend>();
                foreach (var goodCategoryIdExtend in goodCategoryIdExtends)
                {
                    goodCategoryExtends.Add(new GoodCategoryExtend()
                    {
                        GoodId = goodCreate.Id,
                        GoodCategroyId = Guid.Parse(goodCategoryIdExtend)
                    });
                }
                await GoodCategoryExtendRepository.InsertManyAsync(goodCategoryExtends);
            }

            #endregion 商品分类扩展

            #region 货品信息

            if (input.SetSkuProduct.Products.Any())
            {
                List<Product> products = ObjectMapper.Map<List<ProductDto>, List<Product>>(input.SetSkuProduct.Products);
                products.ForEach(product =>
                {
                    product.GoodId = goodCreate.Id;
                    product.BarCode = goodCreate.BarCode;
                });
                await ProductRepository.InsertManyAsync(products);

                #region 货品三级分佣

                List<ProductDistribution> productDistributions = new List<ProductDistribution>();
                foreach (var product in products)
                {
                    productDistributions.Add(new ProductDistribution()
                    {
                        ProductId = product.Id,
                        ProductSn = product.Sn,
                        LevelOne = product.LevelOne,
                        LevelTwo = product.LevelTwo,
                        LevelThree = product.LevelThree
                    });
                }
                await ProductDistributionRepository.InsertManyAsync(productDistributions);

                #endregion 货品三级分佣
            }

            #endregion 货品信息

            #region 会员折扣

            if (input.GoodGrades.Any())
            {
                List<GoodGrade> goodGrades = ObjectMapper.Map<List<GoodGradeDto>, List<GoodGrade>>(input.GoodGrades);
                goodGrades.ForEach(goodGrade =>
                {
                    goodGrade.GoodId = goodCreate.Id;
                });
                await GoodGradeRepository.InsertManyAsync(goodGrades);
            }

            #endregion 会员折扣
        }

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="input">商品信息</param>
        /// <returns></returns>
        public async Task UpdateAsync(GoodDetailDto input)
        {
            Good good = await _goodRepository.GetAsync(input.BasicInfo.Id);

            #region 校验

            if (good == null)
                throw new UserFriendlyException($"该商品不存在或已被删除，请检查！", SystemStatusCode.Fail.ToGoodServicePrefix(), "不存在该商品信息。"); // .WithData("SetSkuProduct.Products", input.SetSkuProduct.Products);

            if (!input.SetSkuProduct.Products.Any())
                throw new UserFriendlyException($"至少包含一条货品信息！", SystemStatusCode.Fail.ToGoodServicePrefix(), "必须包含货品信息。");

            if (input.SetSkuProduct.Products.Where(p => p.IsDefault).Count() < 1)
                throw new UserFriendlyException($"请设置某个货品为默认！", SystemStatusCode.Fail.ToGoodServicePrefix(), "必须包含默认的货品。");

            if (string.IsNullOrWhiteSpace(input.ImageOrVideo.Image))
                throw new UserFriendlyException($"请上传封面图片！", SystemStatusCode.Fail.ToGoodServicePrefix(), "商品封面图片不能为空。");

            if (string.IsNullOrWhiteSpace(input.ImageOrVideo.Images))
                throw new UserFriendlyException($"请上传图集！", SystemStatusCode.Fail.ToGoodServicePrefix(), "商品图集不能为空。");

            // 检验货品数据
            foreach (var product in input.SetSkuProduct.Products)
            {
                if (!product.Sn.StartsWith("SN", StringComparison.InvariantCultureIgnoreCase))
                    throw new UserFriendlyException($"请输入货品号，货号以“SN”英文开头！", SystemStatusCode.Fail.ToGoodServicePrefix(), "货品号不能为空。");
                if (product.Stock <= 0)
                    throw new UserFriendlyException($"库存不能为0！", SystemStatusCode.Fail.ToGoodServicePrefix(), "货品库存不能小于0。");
                if (product.Price <= 0)
                    throw new UserFriendlyException($"销售价不能为0！", SystemStatusCode.Fail.ToGoodServicePrefix(), "货品销售价不能小于0。");
                if (product.MktPrice <= 0 || product.MktPrice < product.Price)
                    throw new UserFriendlyException($"市场价格不能为0，且不能小于销售价格！", SystemStatusCode.Fail.ToGoodServicePrefix(), "货品市场价格不能为0并且不能小于销售价格。");
                if (product.CostPrice <= 0 || product.CostPrice > product.Price)
                    throw new UserFriendlyException($"成本价格不能为0，且不能大于销售价格！", SystemStatusCode.Fail.ToGoodServicePrefix(), "成本价格不能为0并且不能大于销售价格。");
                if (product.LevelOne < 0 || product.LevelTwo < 0 || product.LevelThree < 0)
                    throw new UserFriendlyException($"返现金额不能小于0！", SystemStatusCode.Fail.ToGoodServicePrefix(), "返现金额(一/二/三级佣金)不能小于0。");
            }
            // 不允许存在重复的货号
            var productSnGroup = input.SetSkuProduct.Products.GroupBy(p => p.Sn).Where(p => p.Count() > 1).ToList();
            if (productSnGroup.Count > 0)
                throw new UserFriendlyException($"货品信息存在相同的货号，请检查！", SystemStatusCode.Fail.ToGoodServicePrefix(), "货品信息不允许存在相同的货号。");

            // 检验系统中是否存在重复的货号
            string[] productSns = input.SetSkuProduct.Products.Select(p => p.Sn).ToArray();
            long productSnCount = (await ProductRepository.GetQueryableAsync()).Where(p => productSns.Contains(p.Sn) && p.GoodId != good.Id).LongCount();
            if (productSnCount > 0)
                throw new UserFriendlyException($"系统中存在相同的货品号，请重新生成货品货号！", SystemStatusCode.Fail.ToGoodServicePrefix(), "货品信息不允许存在相同的货号。");

            #endregion 校验

            #region 商品信息

            good.BarCode = input.BasicInfo.BarCode;
            good.Name = input.BasicInfo.Name;
            good.Brief = input.BasicInfo.Brief;
            good.GoodCategoryId = input.BasicInfo.GoodCategoryId;
            good.BrandId = input.BasicInfo.BrandId;
            good.IsNomalVirtual = input.BasicInfo.IsNomalVirtual;
            good.IsMarketable = input.BasicInfo.IsMarketable;
            good.Unit = input.BasicInfo.Unit;
            good.Intro = input.Intro;
            good.Sort = input.BasicInfo.Sort;
            good.IsRecommend = input.BasicInfo.IsRecommend;
            good.IsHot = input.BasicInfo.IsHot;
            good.Image = input.ImageOrVideo.Image;
            good.Images = input.ImageOrVideo.Images;
            good.Video = input.ImageOrVideo.Video;
            good.ProductsDistributionType = input.SetSkuProduct.ProductsDistributionType;
            good.GoodSkuIds = input.SetSkuProduct.GoodSkuIds;
            good.SpesDesc = input.SetSkuProduct.SpesDesc;
            good.NewSpec = input.SetSkuProduct.NewSpec;
            good.OpenSpec = input.SetSkuProduct.OpenSpec;
            good.GoodParamsIds = input.SetParam.GoodParamsIds;
            good.Parameters = input.SetParam.Parameters;

            if (good.OpenSpec != 1)
            {
                good.NewSpec = null;
                good.SpesDesc = null;
                good.Parameters = null;
                good.GoodTypeId = Guid.Empty;
                good.GoodSkuIds = null;
                good.GoodParamsIds = null;
            }

            #endregion 商品信息

            #region 商品分类扩展

            //await GoodCategoryExtendRepository.DeleteAsync(p => p.GoodId == good.Id);
            if (!input.BasicInfo.GoodCategoryIdExtends.IsNullOrWhiteSpace())
            {
                List<Guid?> goodCategoryIdExtends = new List<Guid?>();
                foreach (var goodCategoryIdExtend in input.BasicInfo.GoodCategoryIdExtends.Split(','))
                {
                    Guid goodCategoryId = Guid.Parse(goodCategoryIdExtend);
                    goodCategoryIdExtends.Add(goodCategoryId);
                }

                List<GoodCategoryExtend> goodCategoryExtends = await GoodCategoryExtendRepository.GetListAsync(p => p.GoodId == good.Id);
                if (goodCategoryExtends.Count > 0)
                {
                    // 删除
                    List<GoodCategoryExtend> removeCategoryExtend = goodCategoryExtends.Where(p => !goodCategoryIdExtends.Contains(p.GoodCategroyId)).ToList();
                    if (removeCategoryExtend.Any())
                    {
                        await GoodCategoryExtendRepository.DeleteAsync(p => removeCategoryExtend.Select(p => p.Id).ToArray().Contains(p.Id));
                        goodCategoryExtends.RemoveAll(removeCategoryExtend);
                    }

                    // 新增
                    goodCategoryIdExtends = goodCategoryIdExtends.Where(p => !goodCategoryExtends.Select(p => p.GoodCategroyId).ToList().Contains(p)).ToList();
                    if (goodCategoryIdExtends.Count > 0)
                    {
                        List<GoodCategoryExtend> addGoodCategoryExtends = new List<GoodCategoryExtend>();
                        foreach (var categoryIdExtend in goodCategoryIdExtends)
                        {
                            addGoodCategoryExtends.Add(new GoodCategoryExtend()
                            {
                                GoodId = good.Id,
                                GoodCategroyId = categoryIdExtend
                            });
                        }
                        await GoodCategoryExtendRepository.InsertManyAsync(addGoodCategoryExtends);
                    }
                }
            }

            #endregion 商品分类扩展

            #region 货品信息

            if (input.SetSkuProduct.Products.Any())
            {
                //List<Product> products = ObjectMapper.Map<List<ProductDto>, List<Product>>(input.SetSkuProduct.Products);
                List<Product> productDbs = await ProductRepository.GetListAsync(p => p.GoodId == good.Id);
                foreach (var productDb in productDbs)
                {
                    // 修改货品
                    ProductDto product = input.SetSkuProduct.Products.Where(p => p.Id == productDb.Id).FirstOrDefault();
                    if (product == null) continue;
                    productDb.GoodId = product.GoodId.Value;
                    productDb.BarCode = product.BarCode;
                    productDb.Sn = product.Sn;
                    productDb.Price = product.Price;
                    productDb.CostPrice = product.CostPrice;
                    productDb.MktPrice = product.MktPrice;
                    productDb.Marketable = product.Marketable;
                    productDb.Weight = product.Weight;
                    productDb.Stock = product.Stock;
                    //productDb.FreezeStock = product.FreezeStock;
                    productDb.SpesDesc = product.SpesDesc;
                    productDb.IsDefault = product.IsDefault;
                    productDb.Images = product.Images;
                    await _goodRepository.UpdateProductStockCacheAsync(productDb.Id, good.Id, product.Stock);

                    // 修改货品分佣
                    ProductDistribution productDistribution = await ProductDistributionRepository.GetAsync(p => p.ProductId == productDb.Id);
                    if (productDistribution == null) continue;
                    productDistribution.ProductSn = product.Sn;
                    productDistribution.LevelOne = product.LevelOne;
                    productDistribution.LevelTwo = product.LevelTwo;
                    productDistribution.LevelThree = product.LevelThree;
                }

                // 删除货品
                List<Guid> editProductIds = input.SetSkuProduct.Products.Where(p => p.Id != Guid.Empty).Select(p => p.Id).ToList();
                List<Product> deleteProductDbs = productDbs.Where(p => !editProductIds.Contains(p.Id)).ToList();
                if (deleteProductDbs.Any())
                {
                    Guid[] productIds = deleteProductDbs.Select(p => p.Id).ToArray();
                    productDbs.RemoveAll(deleteProductDbs);
                    await ProductRepository.DeleteAsync(p => productIds.Contains(p.Id));
                    await ProductDistributionRepository.DeleteAsync(p => productIds.Contains(p.ProductId));
                    foreach (var pId in productIds)
                    {
                        await _goodRepository.DeleteProductStockCacheAsync(pId, good.Id);
                    }
                }

                // 新增货品
                List<ProductDto> addProducts = input.SetSkuProduct.Products.Where(p => p.Id == Guid.Empty).ToList();
                if (addProducts.Any())
                {
                    addProducts.ForEach(product =>
                    {
                        product.GoodId = good.Id;
                        product.BarCode = good.BarCode;
                    });
                    List<Product> newProducts = ObjectMapper.Map<List<ProductDto>, List<Product>>(addProducts);
                    await ProductRepository.InsertManyAsync(newProducts);

                    #region 货品三级分佣

                    List<ProductDistribution> productDistributions = new List<ProductDistribution>();
                    foreach (var product in newProducts)
                    {
                        productDistributions.Add(new ProductDistribution()
                        {
                            ProductId = product.Id,
                            ProductSn = product.Sn,
                            LevelOne = product.LevelOne,
                            LevelTwo = product.LevelTwo,
                            LevelThree = product.LevelThree
                        });
                    }
                    await ProductDistributionRepository.InsertManyAsync(productDistributions);

                    #endregion 货品三级分佣
                }
            }

            #endregion 货品信息

            #region 会员折扣

            if (input.GoodGrades.Any())
            {
                //List<GoodGrade> goodGrades = ObjectMapper.Map<List<GoodGradeDto>, List<GoodGrade>>(input.GoodGrades);
                List<GoodGrade> goodGradeDbs = await GoodGradeRepository.GetListAsync(p => p.GoodId == good.Id);

                // 修改
                foreach (var goodGradeDb in goodGradeDbs)
                {
                    GoodGradeDto goodGrade = input.GoodGrades.Where(p => p.GoodId == goodGradeDb.GoodId && p.Id == goodGradeDb.Id).FirstOrDefault();
                    if (goodGrade == null) continue;
                    goodGradeDb.GradeId = goodGrade.GradeId;
                    goodGradeDb.GradePrice = goodGrade.GradePrice;
                }

                // 删除
                List<Guid> editGoodGradeIds = input.GoodGrades.Where(p => p.Id != Guid.Empty).Select(p => p.Id).ToList();
                List<GoodGrade> deleteProductDbs = goodGradeDbs.Where(p => !editGoodGradeIds.Contains(p.Id)).ToList();
                if (editGoodGradeIds.Any())
                {
                    goodGradeDbs.RemoveAll(deleteProductDbs);
                    await GoodGradeRepository.DeleteAsync(p => deleteProductDbs.Select(p => p.Id).ToArray().Contains(p.Id));
                }

                // 新增
                List<GoodGradeDto> addGoodGrades = input.GoodGrades.Where(p => p.Id == Guid.Empty).ToList();
                if (addGoodGrades.Any())
                {
                    addGoodGrades.ForEach(goodGrade =>
                    {
                        goodGrade.GoodId = good.Id;
                    });
                    List<GoodGrade> newGoodGrades = ObjectMapper.Map<List<GoodGradeDto>, List<GoodGrade>>(addGoodGrades);
                    await GoodGradeRepository.InsertManyAsync(newGoodGrades);
                }
            }

            #endregion 会员折扣
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            // 删除商品
            await _goodRepository.DeleteAsync(id);

            // 删除商品分类扩展
            await GoodCategoryExtendRepository.DeleteAsync(p => p.GoodId == id);

            // 货品信息
            List<Guid> productIds = (await ProductRepository.GetQueryableAsync()).Where(p => p.GoodId == id).Select(p => p.Id).ToList();
            if (productIds.Any())
            {
                await ProductRepository.DeleteManyAsync(productIds);
                // 货品三级分佣
                await ProductDistributionRepository.DeleteAsync(p => productIds.Contains(p.ProductId));
                // 删除库存缓存
                foreach (var pId in productIds)
                {
                    await _goodRepository.DeleteProductStockCacheAsync(pId, id);
                }
            }

            // 删除会员折扣
            await GoodGradeRepository.DeleteAsync(p => p.GoodId == id);
        }

        /// <summary>
        /// 根据ID查询商品信息
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <returns></returns>
        public async Task<GoodDetailDto> GetGoodDetailAsync(Guid id)
        {
            Good good = await _goodRepository.GetAsync(id);
            if (good == null)
            {
                throw new UserFriendlyException($"商品ID[{id}]不存在或已被删除，请检查！", SystemStatusCode.Fail.ToGoodServicePrefix());
            }
            GoodDetailDto goodDetail = new GoodDetailDto();

            #region 基础信息

            GoodBasicInfoDto goodBasicInfo = new GoodBasicInfoDto();
            goodBasicInfo.Id = good.Id;
            goodBasicInfo.Name = good.Name;
            goodBasicInfo.GoodCategoryId = good.GoodCategoryId;
            goodBasicInfo.BrandId = good.BrandId;
            goodBasicInfo.BarCode = good.BarCode;
            goodBasicInfo.Brief = good.Brief;
            goodBasicInfo.Unit = good.Unit;
            goodBasicInfo.Sort = good.Sort;
            goodBasicInfo.IsMarketable = good.IsMarketable;
            goodBasicInfo.IsNomalVirtual = good.IsNomalVirtual;
            goodBasicInfo.IsRecommend = good.IsRecommend;
            goodBasicInfo.IsHot = good.IsHot;

            // 商品分类扩展
            List<GoodCategoryExtend> goodCategoryExtends = await GoodCategoryExtendRepository.GetListAsync(p => p.GoodId == good.Id);
            if (goodCategoryExtends.Any())
            {
                List<Guid?> goodCategroyIds = goodCategoryExtends.Where(p => p.GoodCategroyId != null).Select(p => p.GoodCategroyId).ToList();
                goodBasicInfo.GoodCategoryIdExtends = string.Join(',', goodCategroyIds);
            }
            goodDetail.BasicInfo = goodBasicInfo;

            #endregion 基础信息

            #region 图集/视频

            GoodImageOrVideoDto goodImageOrVideo = new GoodImageOrVideoDto();
            goodImageOrVideo.Image = good.Image;
            goodImageOrVideo.Images = good.Images;
            goodImageOrVideo.Video = good.Video;
            goodDetail.ImageOrVideo = goodImageOrVideo;

            #endregion 图集/视频

            #region SKU/货品设置

            GoodSetSkuProductDto goodSetSkuProduct = new GoodSetSkuProductDto();
            goodSetSkuProduct.GoodSkuIds = good.GoodSkuIds;
            goodSetSkuProduct.SpesDesc = good.SpesDesc;
            goodSetSkuProduct.NewSpec = good.NewSpec;
            goodSetSkuProduct.ProductsDistributionType = good.ProductsDistributionType;
            goodSetSkuProduct.OpenSpec = good.OpenSpec;

            List<Product> products = await ProductRepository.GetListAsync(p => p.GoodId == good.Id);
            if (products.Count > 0)
            {
                goodSetSkuProduct.Products = ObjectMapper.Map<List<Product>, List<ProductDto>>(products);
                if (goodSetSkuProduct.Products.Any())
                {
                    goodSetSkuProduct.Products = goodSetSkuProduct.Products.OrderBy(p => p.CreationTime).ToList();
                }

                // 货品分佣
                List<Guid> productIds = goodSetSkuProduct.Products.Select(p => p.Id).ToList();
                List<ProductDistribution> productDistributions = await ProductDistributionRepository.GetListAsync(p => productIds.Contains(p.ProductId));
                foreach (var product in goodSetSkuProduct.Products)
                {
                    ProductDistribution productDistribution = productDistributions.Where(p => p.ProductId == product.Id).FirstOrDefault();
                    if (productDistribution == null) continue;
                    product.LevelOne = productDistribution.LevelOne;
                    product.LevelTwo = productDistribution.LevelTwo;
                    product.LevelThree = productDistribution.LevelThree;
                }
            }
            goodDetail.SetSkuProduct = goodSetSkuProduct;

            #endregion SKU/货品设置

            #region 参数设置

            GoodParameterDto goodParameter = new GoodParameterDto();
            goodParameter.GoodParamsIds = good.GoodParamsIds;
            goodParameter.Parameters = good.Parameters;
            goodDetail.SetParam = goodParameter;

            #endregion 参数设置

            #region 会员折扣

            List<GoodGrade> goodGrades = await GoodGradeRepository.GetListAsync(p => p.GoodId == good.Id);
            if (goodGrades.Count > 0)
            {
                goodDetail.GoodGrades = ObjectMapper.Map<List<GoodGrade>, List<GoodGradeDto>>(goodGrades);
            }

            #endregion 会员折扣

            #region 商品详情

            goodDetail.Intro = good.Intro;

            #endregion 商品详情

            return goodDetail;
        }

        /// <summary>
        /// 修改商品是否上架
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <param name="isMarketable">是否上架</param>
        /// <returns></returns>
        public async Task UpdateIsMarketableAsync(Guid id, bool isMarketable)
        {
            Good good = await _goodRepository.GetAsync(id);
            if (good != null)
            {
                good.IsMarketable = isMarketable;
            }
        }

        /// <summary>
        /// 修改商品是否推荐
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <param name="isRecommend">是否推荐</param>
        /// <returns></returns>
        public async Task UpdateIsRecommendAsync(Guid id, bool isRecommend)
        {
            Good good = await _goodRepository.GetAsync(id);
            if (good != null)
            {
                good.IsRecommend = isRecommend;
            }
        }

        /// <summary>
        /// 修改商品是否热门
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <param name="isHot">是否热门</param>
        /// <returns></returns>
        public async Task UpdateIsHotAsync(Guid id, bool isHot)
        {
            Good good = await _goodRepository.GetAsync(id);
            if (good != null)
            {
                good.IsHot = isHot;
            }
        }

        #region 修改商品价格

        /// <summary>
        /// 商品价格修改
        /// </summary>
        /// <param name="input">修改价格Dto</param>
        /// <returns></returns>
        public async Task UpdatePriceAsync(GoodPriceUpdateDto input)
        {
            // 查询商品
            List<Good> goods = await _goodRepository.GetListAsync(p => input.GoodIds.Contains(p.Id));
            if (!goods.Any())
            {
                throw new UserFriendlyException($"选择的商品不存在或已被删除，请检查！", SystemStatusCode.Fail.ToGoodServicePrefix());
            }
            Guid[] goodIds = goods.Select(p => p.Id).ToArray();

            /*
            // 获取货品信息
            List<Product> products = await ProductRepository.GetListAsync(p => goodIds.Contains(p.GoodId));
            Guid[] productIds = products.Select(p => p.Id).ToArray();
            */

            // 获取自定义价格信息
            List<GoodGrade> goodGrades = await GoodGradeRepository.GetListAsync(p => goodIds.Contains(p.GoodId));
            Guid[] goodGradeIds = goodGrades.Select(p => p.Id).ToArray();

            decimal priceValue = input.PriceValue;
            switch (input.ModifyType)
            {
                case "=":
                    await CalcEqualPrice(goodIds, goodGradeIds, input.PriceType, priceValue);
                    break;

                case "-":
                    await CalcSubtractPrice(goodIds, goodGradeIds, input.PriceType, priceValue);
                    break;

                case "+":
                    await CalcAddPrice(goodIds, goodGradeIds, input.PriceType, priceValue);
                    break;

                case "*":
                    await CalcMultiplyPrice(goodIds, goodGradeIds, input.PriceType, priceValue);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 计算“=”商品价格
        /// </summary>
        /// <param name="goodIds">商品ID</param>
        /// <param name="goodGradeIds">商品会员价格ID</param>
        /// <param name="priceType">价格类型</param>
        /// <param name="priceValue">商品价格</param>
        private async Task CalcEqualPrice(Guid[] goodIds, Guid[] goodGradeIds, string priceType, decimal priceValue)
        {
            if (priceType.StartsWith("grade_price_"))
            {
                Guid goodGradeId = Guid.Parse(priceType.Split('_')[2]);
                List<GoodGrade> goodGrades = await GoodGradeRepository.GetListAsync(p => goodGradeIds.Contains(p.Id) && p.GradeId == goodGradeId);
                goodGrades.ForEach(goodGrade =>
                {
                    goodGrade.GradePrice = priceValue;
                });
                return;
            }
            List<Product> products = await ProductRepository.GetListAsync(p => goodIds.Contains(p.GoodId));
            foreach (var product in products)
            {
                if (priceType == GlobalEnums.PriceType.Price.ToString())
                {
                    product.Price = priceValue;
                }
                else if (priceType == GlobalEnums.PriceType.CostPrice.ToString())
                {
                    product.CostPrice = priceValue;
                }
                else if (priceType == GlobalEnums.PriceType.MktPrice.ToString())
                {
                    product.MktPrice = priceValue;
                }
            }
            ;
        }

        /// <summary>
        /// 计算“-”商品价格
        /// </summary>
        /// <param name="goodIds">商品ID</param>
        /// <param name="goodGradeIds">商品会员价格ID</param>
        /// <param name="priceType">价格类型</param>
        /// <param name="priceValue">商品价格</param>
        private async Task CalcSubtractPrice(Guid[] goodIds, Guid[] goodGradeIds, string priceType, decimal priceValue)
        {
            if (priceType.StartsWith("grade_price_"))
            {
                Guid goodGradeId = Guid.Parse(priceType.Split('_')[2]);
                List<GoodGrade> goodGrades = await GoodGradeRepository.GetListAsync(p => goodGradeIds.Contains(p.Id) && p.GradeId == goodGradeId);
                goodGrades.ForEach(goodGrade =>
                {
                    goodGrade.GradePrice = goodGrade.GradePrice - priceValue;
                });
                return;
            }
            List<Product> products = await ProductRepository.GetListAsync(p => goodIds.Contains(p.GoodId));
            foreach (var product in products)
            {
                if (priceType == GlobalEnums.PriceType.Price.ToString())
                {
                    product.Price = product.Price - priceValue;
                }
                else if (priceType == GlobalEnums.PriceType.CostPrice.ToString())
                {
                    product.CostPrice = product.CostPrice - priceValue;
                }
                else if (priceType == GlobalEnums.PriceType.MktPrice.ToString())
                {
                    product.MktPrice = product.MktPrice - priceValue;
                }
            }
            ;
        }

        /// <summary>
        /// 计算“+”商品价格
        /// </summary>
        /// <param name="goodIds">商品ID</param>
        /// <param name="goodGradeIds">商品会员价格ID</param>
        /// <param name="priceType">价格类型</param>
        /// <param name="priceValue">商品价格</param>
        private async Task CalcAddPrice(Guid[] goodIds, Guid[] goodGradeIds, string priceType, decimal priceValue)
        {
            if (priceType.StartsWith("grade_price_"))
            {
                Guid goodGradeId = Guid.Parse(priceType.Split('_')[2]);
                List<GoodGrade> goodGrades = await GoodGradeRepository.GetListAsync(p => goodGradeIds.Contains(p.Id) && p.GradeId == goodGradeId);
                goodGrades.ForEach(goodGrade =>
                {
                    goodGrade.GradePrice = goodGrade.GradePrice + priceValue;
                });
                return;
            }
            List<Product> products = await ProductRepository.GetListAsync(p => goodIds.Contains(p.GoodId));
            foreach (var product in products)
            {
                if (priceType == GlobalEnums.PriceType.Price.ToString())
                {
                    product.Price = product.Price + priceValue;
                }
                else if (priceType == GlobalEnums.PriceType.CostPrice.ToString())
                {
                    product.CostPrice = product.CostPrice + priceValue;
                }
                else if (priceType == GlobalEnums.PriceType.MktPrice.ToString())
                {
                    product.MktPrice = product.MktPrice + priceValue;
                }
            }
            ;
        }

        /// <summary>
        /// 计算“×”商品价格
        /// </summary>
        /// <param name="goodIds">商品ID</param>
        /// <param name="goodGradeIds">商品会员价格ID</param>
        /// <param name="priceType">价格类型</param>
        /// <param name="priceValue">商品价格</param>
        private async Task CalcMultiplyPrice(Guid[] goodIds, Guid[] goodGradeIds, string priceType, decimal priceValue)
        {
            if (priceType.StartsWith("grade_price_"))
            {
                Guid goodGradeId = Guid.Parse(priceType.Split('_')[2]);
                List<GoodGrade> goodGrades = await GoodGradeRepository.GetListAsync(p => goodGradeIds.Contains(p.Id) && p.GradeId == goodGradeId);
                goodGrades.ForEach(goodGrade =>
                {
                    goodGrade.GradePrice = goodGrade.GradePrice * priceValue;
                });
                return;
            }
            List<Product> products = await ProductRepository.GetListAsync(p => goodIds.Contains(p.GoodId));
            foreach (var product in products)
            {
                if (priceType == GlobalEnums.PriceType.Price.ToString())
                {
                    product.Price = product.Price * priceValue;
                }
                else if (priceType == GlobalEnums.PriceType.CostPrice.ToString())
                {
                    product.CostPrice = product.CostPrice * priceValue;
                }
                else if (priceType == GlobalEnums.PriceType.MktPrice.ToString())
                {
                    product.MktPrice = product.MktPrice * priceValue;
                }
            }
            ;
        }

        #endregion 修改商品价格

        #region 修改商品库存

        /// <summary>
        /// 商品库存修改
        /// </summary>
        /// <param name="input">修改库存Dto</param>
        /// <returns></returns>
        public async Task UpdateStockAsync(GoodStockUpdateDto input)
        {
            // 查询商品
            List<Good> goods = await _goodRepository.GetListAsync(p => input.GoodIds.Contains(p.Id));
            if (!goods.Any())
            {
                throw new UserFriendlyException($"选择的商品不存在或已被删除，请检查！", SystemStatusCode.Fail.ToGoodServicePrefix());
            }
            Guid[] goodIds = goods.Select(p => p.Id).ToArray();

            // 获取货品信息
            List<Product> products = await ProductRepository.GetListAsync(p => goodIds.Contains(p.GoodId));
            int stockValue = input.StockValue;
            switch (input.ModifyType)
            {
                case "=":
                    foreach (var product in products) product.Stock = stockValue;
                    break;

                case "-":
                    foreach (var product in products) product.Stock = product.Stock - stockValue;
                    break;

                case "+":
                    foreach (var product in products) product.Stock = product.Stock + stockValue;
                    break;

                case "*":
                    foreach (var product in products) product.Stock = product.Stock * stockValue;
                    break;

                default:
                    break;
            }
            foreach (var product in products)
            {
                await _goodRepository.UpdateProductStockCacheAsync(product.Id, product.GoodId, product.Stock);
            }
        }

        #endregion 修改商品库存

        /// <summary>
        /// 批量修改商品是否上架
        /// </summary>
        /// <param name="input">修改上架Dto</param>
        /// <returns></returns>
        public async Task UpdateIsMarketableManyAsync(GoodMarketableUpdateDto input)
        {
            List<Good> goods = await _goodRepository.GetListAsync(p => input.GoodIds.Contains(p.Id));
            foreach (var good in goods)
            {
                good.IsMarketable = input.IsMarketable;
            }
        }

        /// <summary>
        /// 批量删除商品
        /// </summary>
        /// <param name="ids">商品ID</param>
        /// <returns></returns>
        public async Task DeleteManyAsync(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                await DeleteAsync(id);
            }
        }

        /// <summary>
        /// 批量打标签
        /// </summary>
        /// <param name="input">商品标签</param>
        /// <returns></returns>
        public async Task UpdateGoodLabelManyAsync(GoodLabelManyDto input)
        {
            #region 添加[标签]

            string[] labelNames = input.Labels.Select(p => p.Name).ToArray();
            List<Label> dbLabels = await _labelRepository.GetListAsync(p => labelNames.Contains(p.Name));
            foreach (var label in input.Labels)
            {
                if (dbLabels.Where(p => p.Name.Equals(label.Name)).Count() <= 0)
                {
                    Label addLabel = await _labelRepository.InsertAsync(new Label()
                    {
                        Name = label.Name,
                        Style = label.Style,
                    });
                    dbLabels.Add(addLabel);
                }
            }

            #endregion 添加[标签]

            #region 修改商品标签

            List<Good> goods = await _goodRepository.GetListAsync(p => input.GoodIds.Contains(p.Id));
            List<Guid> labelIds = dbLabels.Select(p => p.Id).ToList();
            foreach (var good in goods)
            {
                good.LabelIds = labelIds.Any() ? string.Join(",", labelIds) : null;
            }
            await _goodRepository.UpdateManyAsync(goods);

            #endregion 修改商品标签
        }

        /// <summary>
        /// 根据ID获取商品信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="includeDetails">是否包括明细</param>
        /// <returns></returns>
        public async Task<List<GoodDto>> GetGoodByIdsAsync(List<Guid> ids, bool includeDetails = true)
        {
            List<Good> goods = await _goodRepository.GetGoodByIdsAsync(ids, includeDetails);
            foreach (var good in goods)
            {
                Product product = good.Products?.Where(p => p.IsDefault)?.FirstOrDefault();
                if (product == null) continue;
                good.Price = product.Price;
                good.CostPrice = product.CostPrice;
                good.MktPrice = product.MktPrice;
            }
            return ObjectMapper.Map<List<Good>, List<GoodDto>>(goods);
        }

        /// <summary>
        /// 更新货品库存信息
        /// </summary>
        /// <param name="input">更新参数</param>
        /// <returns></returns>
        public async Task UpdateProductStocksAsync(List<ProductDetailUpdateDto> input)
        {
            if (input == null || input.Count == 0)
                throw new UserFriendlyException("没有需要更新的商品信息！", SystemStatusCode.Fail.ToGoodServicePrefix());
            if (input.Where(p => p.IsDefault).Count() > 1)
                throw new UserFriendlyException("默认货品只能有一个！", SystemStatusCode.Fail.ToGoodServicePrefix());
            if (input.Where(p => p.IsDefault).Count() < 1)
                input[0].IsDefault = true;

            Guid[] productIds = input.Select(p => p.Id).ToArray();
            List<Product> products = await ProductRepository.GetListAsync(p => productIds.Contains(p.Id));
            foreach (var product in products)
            {
                if (product.Stock <= 0)
                    throw new UserFriendlyException($"库存不能为0！", SystemStatusCode.Fail.ToGoodServicePrefix(), "货品库存不能小于0。");
                if (product.Price <= 0)
                    throw new UserFriendlyException($"销售价不能为0！", SystemStatusCode.Fail.ToGoodServicePrefix(), "货品销售价不能小于0。");
                if (product.MktPrice <= 0 || product.MktPrice < product.Price)
                    throw new UserFriendlyException($"市场价格不能为0，且不能小于销售价格！", SystemStatusCode.Fail.ToGoodServicePrefix(), "货品市场价格不能为0并且不能小于销售价格。");
                if (product.CostPrice <= 0 || product.CostPrice > product.Price)
                    throw new UserFriendlyException($"成本价格不能为0，且不能大于销售价格！", SystemStatusCode.Fail.ToGoodServicePrefix(), "成本价格不能为0并且不能大于销售价格。");

                ProductDetailUpdateDto productStock = input.FirstOrDefault(x => x.Id == product.Id);
                product.Stock = productStock.Stock;
                product.Price = productStock.Price;
                product.CostPrice = productStock.CostPrice;
                product.MktPrice = productStock.MktPrice;
                product.Weight = productStock.Weight;
                product.IsDefault = productStock.IsDefault;
                await _goodRepository.UpdateProductStockCacheAsync(product.Id, product.GoodId, product.Stock);
            }
        }
    }
}