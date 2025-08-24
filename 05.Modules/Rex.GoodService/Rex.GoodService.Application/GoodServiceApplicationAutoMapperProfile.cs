using AutoMapper;
using Rex.GoodService.Areas;
using Rex.GoodService.Articles;
using Rex.GoodService.Brands;
using Rex.GoodService.Forms;
using Rex.GoodService.Goods;
using Rex.GoodService.Labels;
using Rex.GoodService.Notices;
using Rex.GoodService.PageDesigns;
using Rex.GoodService.Products;
using Rex.GoodService.ServiceDescriptions;
using Rex.GoodService.ServiceGoods;
using Rex.GoodService.StoreClerks;
using Rex.GoodService.Stores;
using System;
using System.Linq;

namespace Rex.GoodService;

public class GoodServiceApplicationAutoMapperProfile : Profile
{
    public GoodServiceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        #region Brands

        CreateMap<BrandCreateDto, Brand>();
        CreateMap<BrandUpdateDto, Brand>();
        CreateMap<BrandDto, Brand>();
        CreateMap<Brand, BrandDto>();

        #endregion Brands

        #region Goods

        CreateMap<GoodCreateDto, Good>();
        CreateMap<GoodUpdateDto, Good>();
        CreateMap<GoodDto, Good>();
        CreateMap<Good, GoodDto>();

        #endregion Goods

        #region GoodCategorys

        CreateMap<GoodCategoryCreateDto, GoodCategory>();
        CreateMap<GoodCategoryUpdateDto, GoodCategory>();
        CreateMap<GoodCategoryDto, GoodCategory>();
        CreateMap<GoodCategory, GoodCategoryDto>();
        CreateMap<GoodCategory, GoodCategoryTreeDto>();

        #endregion GoodCategorys

        #region GoodParams

        CreateMap<GoodParamCreateDto, GoodParam>()
            .AfterMap((gpc, gp) =>
            {
                gp.Value = (gpc.Values != null && gpc.Values.Length > 0) ? string.Join(',', gpc.Values) : null;
            });
        CreateMap<GoodParamUpdateDto, GoodParam>()
            .AfterMap((gpc, gp) =>
            {
                gp.Value = (gpc.Values != null && gpc.Values.Length > 0) ? string.Join(',', gpc.Values) : null;
            });
        CreateMap<GoodParamDto, GoodParam>()
            .AfterMap((gpc, gp) =>
            {
                gp.Value = (gpc.Values != null && gpc.Values.Length > 0) ? string.Join(',', gpc.Values) : null;
            });
        CreateMap<GoodParam, GoodParamDto>()
            .AfterMap((gp, gpd) =>
            {
                gpd.Values = gp.Value.IsNullOrWhiteSpace() ? null : gp.Value.Split(',');
            });

        #endregion GoodParams

        #region GoodTypeSpecs

        CreateMap<GoodTypeSpecCreateDto, GoodTypeSpec>();
        CreateMap<GoodTypeSpecUpdateDto, GoodTypeSpec>();
        CreateMap<GoodTypeSpecDto, GoodTypeSpec>();
        CreateMap<GoodTypeSpec, GoodTypeSpecDto>();

        #endregion GoodTypeSpecs

        #region GoodTypeSpecValues

        CreateMap<GoodTypeSpecValueCreateDto, GoodTypeSpecValue>();
        CreateMap<GoodTypeSpecValueUpdateDto, GoodTypeSpecValue>();
        CreateMap<GoodTypeSpecValueDto, GoodTypeSpecValue>();
        CreateMap<GoodTypeSpecValue, GoodTypeSpecValueDto>();

        #endregion GoodTypeSpecValues

        #region GoodGrades

        CreateMap<GoodGradeCreateDto, GoodGrade>();
        CreateMap<GoodGradeUpdateDto, GoodGrade>();
        CreateMap<GoodGradeDto, GoodGrade>();
        CreateMap<GoodGrade, GoodGradeDto>();

        #endregion GoodGrades

        #region GoodComments

        CreateMap<GoodCommentDto, GoodComment>();
        CreateMap<GoodComment, GoodCommentDto>();

        #endregion GoodComments

        #region Products

        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductUpdateDto, Product>();
        CreateMap<ProductDto, Product>();
        CreateMap<Product, ProductDto>();

        #endregion Products

        #region ProductDistribution

        CreateMap<ProductDistributionCreateDto, ProductDistribution>();
        CreateMap<ProductDistributionUpdateDto, ProductDistribution>();
        CreateMap<ProductDistributionDto, ProductDistribution>();
        CreateMap<ProductDistribution, ProductDistributionDto>();

        #endregion ProductDistribution

        #region Labels

        CreateMap<LabelCreateDto, Label>();
        CreateMap<LabelUpdateDto, Label>();
        CreateMap<LabelDto, Label>();
        CreateMap<Label, LabelDto>();

        #endregion Labels

        #region ServiceGoods

        CreateMap<ServiceGoodCreateDto, ServiceGood>()
            .AfterMap((sgcd, sg) =>
            {
                sg.AllowedMembership = sgcd.AllowedMemberships.Any() ? string.Join(',', sgcd.AllowedMemberships) : null;
                sg.ConsumableStore = sgcd.ConsumableStores.Any() ? string.Join(',', sgcd.ConsumableStores) : null;
            });

        CreateMap<ServiceGoodUpdateDto, ServiceGood>()
            .AfterMap((sgud, sg) =>
             {
                 sg.AllowedMembership = sgud.AllowedMemberships.Any() ? string.Join(',', sgud.AllowedMemberships) : null;
                 sg.ConsumableStore = sgud.ConsumableStores.Any() ? string.Join(',', sgud.ConsumableStores) : null;
             });

        CreateMap<ServiceGoodDto, ServiceGood>()
            .AfterMap((sgd, sg) =>
            {
                sg.AllowedMembership = sgd.AllowedMemberships.Any() ? string.Join(',', sgd.AllowedMemberships) : null;
                sg.ConsumableStore = sgd.ConsumableStores.Any() ? string.Join(',', sgd.ConsumableStores) : null;
            });
        CreateMap<ServiceGood, ServiceGoodDto>()
            .AfterMap((sg, sgd) =>
            {
                sgd.AllowedMemberships = sg.AllowedMembership.IsNullOrEmpty() ? null : sg.AllowedMembership.Split(',');
                sgd.ConsumableStores = sg.ConsumableStore.IsNullOrEmpty() ? null : sg.ConsumableStore.Split(',');
            });

        #endregion ServiceGoods

        #region Stores

        CreateMap<StoreCreateDto, Store>();
        CreateMap<StoreUpdateDto, Store>();
        CreateMap<StoreDto, Store>();
        CreateMap<Store, StoreDto>();

        #endregion Stores

        #region StoreClerks

        CreateMap<StoreClerkCreateDto, StoreClerk>();
        CreateMap<StoreClerkUpdateDto, StoreClerk>();
        CreateMap<StoreClerkDto, StoreClerk>();
        CreateMap<StoreClerk, StoreClerkDto>();

        #endregion StoreClerks

        #region Areas

        CreateMap<AreaCreateDto, Area>();
        CreateMap<AreaUpdateDto, Area>();
        CreateMap<AreaDto, Area>();
        CreateMap<Area, AreaDto>();
        CreateMap<Area, AreaTreeDto>();

        #endregion Areas

        #region Articles

        CreateMap<ArticleCreateDto, Article>();
        CreateMap<ArticleUpdateDto, Article>();
        CreateMap<ArticleDto, Article>();
        CreateMap<Article, ArticleDto>();

        #endregion Articles

        #region ArticleTypes

        CreateMap<ArticleTypeCreateDto, ArticleType>();
        CreateMap<ArticleTypeUpdateDto, ArticleType>();
        CreateMap<ArticleTypeDto, ArticleType>();
        CreateMap<ArticleType, ArticleTypeDto>();

        #endregion ArticleTypes

        #region PageDesigns

        CreateMap<PageDesignCreateDto, PageDesign>();
        CreateMap<PageDesignUpdateDto, PageDesign>();
        CreateMap<PageDesignDto, PageDesign>();
        CreateMap<PageDesign, PageDesignDto>();

        #endregion PageDesigns

        #region PageDesignItems

        CreateMap<PageDesignItemCreateDto, PageDesignItem>();
        CreateMap<PageDesignItemUpdateDto, PageDesignItem>();
        CreateMap<PageDesignItemDto, PageDesignItem>();
        CreateMap<PageDesignItem, PageDesignItemDto>();

        #endregion PageDesignItems

        #region Notices

        CreateMap<NoticeCreateDto, Notice>();
        CreateMap<NoticeUpdateDto, Notice>();
        CreateMap<NoticeDto, Notice>();
        CreateMap<Notice, NoticeDto>();

        #endregion Notices

        #region Forms

        CreateMap<FormCreateDto, Form>();
        CreateMap<FormUpdateDto, Form>();
        CreateMap<FormDto, Form>();
        CreateMap<Form, FormDto>();

        #endregion Forms

        #region FormItems

        CreateMap<FormItemCreateDto, FormItem>();
        CreateMap<FormItemUpdateDto, FormItem>();
        CreateMap<FormItemDto, FormItem>();
        CreateMap<FormItem, FormItemDto>();

        #endregion FormItems

        #region ServiceDescriptions

        CreateMap<ServiceDescriptionCreateDto, ServiceDescription>();
        CreateMap<ServiceDescriptionUpdateDto, ServiceDescription>();
        CreateMap<ServiceDescriptionDto, ServiceDescription>();
        CreateMap<ServiceDescription, ServiceDescriptionDto>();

        #endregion ServiceDescriptions

        #region GoodCollections

        CreateMap<GoodCollection, GoodCollectionDto>();
        CreateMap<GoodCollectionDto, GoodCollection>();

        #endregion GoodCollections

        #region GoodBrowsings

        CreateMap<GoodBrowsing, GoodBrowsingDto>();
        CreateMap<GoodBrowsingDto, GoodBrowsing>();

        #endregion GoodBrowsings
    }
}