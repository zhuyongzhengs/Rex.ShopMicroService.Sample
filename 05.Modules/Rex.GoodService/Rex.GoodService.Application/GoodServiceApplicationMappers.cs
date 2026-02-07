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
using Riok.Mapperly.Abstractions;
using System;
using System.Linq;
using Volo.Abp.Mapperly;

namespace Rex.GoodService;

/* 在此处添加自己的映射关系 */

#region Brands

[Mapper]
public partial class BrandCreateDtoToBrandMapper : MapperBase<BrandCreateDto, Brand>
{
    public override partial Brand Map(BrandCreateDto source);

    public override partial void Map(BrandCreateDto source, Brand destination);
}

[Mapper]
public partial class BrandUpdateDtoToBrandMapper : MapperBase<BrandUpdateDto, Brand>
{
    public override partial Brand Map(BrandUpdateDto source);

    public override partial void Map(BrandUpdateDto source, Brand destination);
}

[Mapper]
public partial class BrandDtoToBrandMapper : MapperBase<BrandDto, Brand>
{
    public override partial Brand Map(BrandDto source);

    public override partial void Map(BrandDto source, Brand destination);
}

[Mapper]
public partial class BrandToBrandDtoMapper : MapperBase<Brand, BrandDto>
{
    public override partial BrandDto Map(Brand source);

    public override partial void Map(Brand source, BrandDto destination);
}

#endregion Brands

#region Goods

[Mapper]
public partial class GoodCreateDtoToGoodMapper : MapperBase<GoodCreateDto, Good>
{
    public override partial Good Map(GoodCreateDto source);

    public override partial void Map(GoodCreateDto source, Good destination);
}

[Mapper]
public partial class GoodUpdateDtoToGoodMapper : MapperBase<GoodUpdateDto, Good>
{
    public override partial Good Map(GoodUpdateDto source);

    public override partial void Map(GoodUpdateDto source, Good destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class GoodDtoToGoodMapper : MapperBase<GoodDto, Good>
{
    public override partial Good Map(GoodDto source);

    public override partial void Map(GoodDto source, Good destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class GoodToGoodDtoMapper : MapperBase<Good, GoodDto>
{
    public override partial GoodDto Map(Good source);

    public override partial void Map(Good source, GoodDto destination);
}

#endregion Goods

#region GoodCategorys

[Mapper]
public partial class GoodCategoryCreateDtoToGoodCategoryMapper : MapperBase<GoodCategoryCreateDto, GoodCategory>
{
    public override partial GoodCategory Map(GoodCategoryCreateDto source);

    public override partial void Map(GoodCategoryCreateDto source, GoodCategory destination);
}

[Mapper]
public partial class GoodCategoryUpdateDtoToGoodCategoryMapper : MapperBase<GoodCategoryUpdateDto, GoodCategory>
{
    public override partial GoodCategory Map(GoodCategoryUpdateDto source);

    public override partial void Map(GoodCategoryUpdateDto source, GoodCategory destination);
}

[Mapper]
public partial class GoodCategoryDtoToGoodCategoryMapper : MapperBase<GoodCategoryDto, GoodCategory>
{
    public override partial GoodCategory Map(GoodCategoryDto source);

    public override partial void Map(GoodCategoryDto source, GoodCategory destination);
}

[Mapper]
public partial class GoodCategoryToGoodCategoryDtoMapper : MapperBase<GoodCategory, GoodCategoryDto>
{
    public override partial GoodCategoryDto Map(GoodCategory source);

    public override partial void Map(GoodCategory source, GoodCategoryDto destination);
}

[Mapper]
public partial class GoodCategoryToGoodCategoryTreeDtoMapper : MapperBase<GoodCategory, GoodCategoryTreeDto>
{
    public override partial GoodCategoryTreeDto Map(GoodCategory source);

    public override partial void Map(GoodCategory source, GoodCategoryTreeDto destination);
}

#endregion GoodCategorys

#region GoodParams

[Mapper]
public partial class GoodParamCreateDtoToGoodParamMapper : MapperBase<GoodParamCreateDto, GoodParam>
{
    public override partial GoodParam Map(GoodParamCreateDto source);

    public override partial void Map(GoodParamCreateDto source, GoodParam destination);

    public override void AfterMap(GoodParamCreateDto source, GoodParam destination)
    {
        destination.Value = MapValue(source.Values);
    }

    private string? MapValue(string[]? values) =>
        values != null && values.Length > 0 ? string.Join(',', values) : null;
}

[Mapper]
public partial class GoodParamUpdateDtoToGoodParamMapper : MapperBase<GoodParamUpdateDto, GoodParam>
{
    public override partial GoodParam Map(GoodParamUpdateDto source);

    public override partial void Map(GoodParamUpdateDto source, GoodParam destination);

    public override void AfterMap(GoodParamUpdateDto source, GoodParam destination)
    {
        destination.Value = MapValue(source.Values);
    }

    private string? MapValue(string[]? values) =>
        values != null && values.Length > 0 ? string.Join(',', values) : null;
}

[Mapper]
public partial class GoodParamDtoToGoodParamMapper : MapperBase<GoodParamDto, GoodParam>
{
    public override partial GoodParam Map(GoodParamDto source);

    public override partial void Map(GoodParamDto source, GoodParam destination);

    public override void AfterMap(GoodParamDto source, GoodParam destination)
    {
        destination.Value = MapValue(source.Values);
    }

    private string? MapValue(string[]? values) =>
        values != null && values.Length > 0 ? string.Join(',', values) : null;
}

[Mapper]
public partial class GoodParamToGoodParamDtoMapper : MapperBase<GoodParam, GoodParamDto>
{
    public override partial GoodParamDto Map(GoodParam source);

    public override partial void Map(GoodParam source, GoodParamDto destination);

    public override void AfterMap(GoodParam source, GoodParamDto destination)
    {
        destination.Values = MapValues(source.Value);
    }

    private string[]? MapValues(string? value) =>
        value.IsNullOrWhiteSpace() ? null : value.Split(',');
}

#endregion GoodParams

#region GoodTypeSpecs

[Mapper]
public partial class GoodTypeSpecCreateDtoToGoodTypeSpecMapper : MapperBase<GoodTypeSpecCreateDto, GoodTypeSpec>
{
    public override partial GoodTypeSpec Map(GoodTypeSpecCreateDto source);

    public override partial void Map(GoodTypeSpecCreateDto source, GoodTypeSpec destination);
}

[Mapper]
public partial class GoodTypeSpecUpdateDtoToGoodTypeSpecMapper : MapperBase<GoodTypeSpecUpdateDto, GoodTypeSpec>
{
    public override partial GoodTypeSpec Map(GoodTypeSpecUpdateDto source);

    public override partial void Map(GoodTypeSpecUpdateDto source, GoodTypeSpec destination);
}

[Mapper]
public partial class GoodTypeSpecDtoToGoodTypeSpecMapper : MapperBase<GoodTypeSpecDto, GoodTypeSpec>
{
    public override partial GoodTypeSpec Map(GoodTypeSpecDto source);

    public override partial void Map(GoodTypeSpecDto source, GoodTypeSpec destination);
}

[Mapper]
public partial class GoodTypeSpecToGoodTypeSpecDtoMapper : MapperBase<GoodTypeSpec, GoodTypeSpecDto>
{
    public override partial GoodTypeSpecDto Map(GoodTypeSpec source);

    public override partial void Map(GoodTypeSpec source, GoodTypeSpecDto destination);
}

#endregion GoodTypeSpecs

#region GoodTypeSpecValues

[Mapper]
public partial class GoodTypeSpecValueCreateDtoToGoodTypeSpecValueMapper : MapperBase<GoodTypeSpecValueCreateDto, GoodTypeSpecValue>
{
    public override partial GoodTypeSpecValue Map(GoodTypeSpecValueCreateDto source);

    public override partial void Map(GoodTypeSpecValueCreateDto source, GoodTypeSpecValue destination);
}

[Mapper]
public partial class GoodTypeSpecValueUpdateDtoToGoodTypeSpecValueMapper : MapperBase<GoodTypeSpecValueUpdateDto, GoodTypeSpecValue>
{
    public override partial GoodTypeSpecValue Map(GoodTypeSpecValueUpdateDto source);

    public override partial void Map(GoodTypeSpecValueUpdateDto source, GoodTypeSpecValue destination);
}

[Mapper]
public partial class GoodTypeSpecValueDtoToGoodTypeSpecValueMapper : MapperBase<GoodTypeSpecValueDto, GoodTypeSpecValue>
{
    public override partial GoodTypeSpecValue Map(GoodTypeSpecValueDto source);

    public override partial void Map(GoodTypeSpecValueDto source, GoodTypeSpecValue destination);
}

[Mapper]
public partial class GoodTypeSpecValueToGoodTypeSpecValueDtoMapper : MapperBase<GoodTypeSpecValue, GoodTypeSpecValueDto>
{
    public override partial GoodTypeSpecValueDto Map(GoodTypeSpecValue source);

    public override partial void Map(GoodTypeSpecValue source, GoodTypeSpecValueDto destination);
}

#endregion GoodTypeSpecValues

#region GoodGrades

[Mapper]
public partial class GoodGradeCreateDtoToGoodGradeMapper : MapperBase<GoodGradeCreateDto, GoodGrade>
{
    public override partial GoodGrade Map(GoodGradeCreateDto source);

    public override partial void Map(GoodGradeCreateDto source, GoodGrade destination);
}

[Mapper]
public partial class GoodGradeUpdateDtoToGoodGradeMapper : MapperBase<GoodGradeUpdateDto, GoodGrade>
{
    public override partial GoodGrade Map(GoodGradeUpdateDto source);

    public override partial void Map(GoodGradeUpdateDto source, GoodGrade destination);
}

[Mapper]
public partial class GoodGradeDtoToGoodGradeMapper : MapperBase<GoodGradeDto, GoodGrade>
{
    public override partial GoodGrade Map(GoodGradeDto source);

    public override partial void Map(GoodGradeDto source, GoodGrade destination);
}

[Mapper]
public partial class GoodGradeToGoodGradeDtoMapper : MapperBase<GoodGrade, GoodGradeDto>
{
    public override partial GoodGradeDto Map(GoodGrade source);

    public override partial void Map(GoodGrade source, GoodGradeDto destination);
}

#endregion GoodGrades

#region GoodComments

[Mapper]
public partial class GoodCommentDtoToGoodCommentMapper : MapperBase<GoodCommentDto, GoodComment>
{
    public override partial GoodComment Map(GoodCommentDto source);

    public override partial void Map(GoodCommentDto source, GoodComment destination);
}

[Mapper]
public partial class GoodCommentToGoodCommentDtoMapper : MapperBase<GoodComment, GoodCommentDto>
{
    public override partial GoodCommentDto Map(GoodComment source);

    public override partial void Map(GoodComment source, GoodCommentDto destination);
}

#endregion GoodComments

#region Products

[Mapper]
public partial class ProductCreateDtoToProductMapper : MapperBase<ProductCreateDto, Product>
{
    public override partial Product Map(ProductCreateDto source);

    public override partial void Map(ProductCreateDto source, Product destination);
}

[Mapper]
public partial class ProductUpdateDtoToProductMapper : MapperBase<ProductUpdateDto, Product>
{
    public override partial Product Map(ProductUpdateDto source);

    public override partial void Map(ProductUpdateDto source, Product destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class ProductDtoToProductMapper : MapperBase<ProductDto, Product>
{
    public override partial Product Map(ProductDto source);

    public override partial void Map(ProductDto source, Product destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class ProductToProductDtoMapper : MapperBase<Product, ProductDto>
{
    public override partial ProductDto Map(Product source);

    public override partial void Map(Product source, ProductDto destination);
}

#endregion Products

#region ProductDistribution

[Mapper]
public partial class ProductDistributionCreateDtoToProductDistributionMapper : MapperBase<ProductDistributionCreateDto, ProductDistribution>
{
    public override partial ProductDistribution Map(ProductDistributionCreateDto source);

    public override partial void Map(ProductDistributionCreateDto source, ProductDistribution destination);
}

[Mapper]
public partial class ProductDistributionUpdateDtoToProductDistributionMapper : MapperBase<ProductDistributionUpdateDto, ProductDistribution>
{
    public override partial ProductDistribution Map(ProductDistributionUpdateDto source);

    public override partial void Map(ProductDistributionUpdateDto source, ProductDistribution destination);
}

[Mapper]
public partial class ProductDistributionDtoToProductDistributionMapper : MapperBase<ProductDistributionDto, ProductDistribution>
{
    public override partial ProductDistribution Map(ProductDistributionDto source);

    public override partial void Map(ProductDistributionDto source, ProductDistribution destination);
}

[Mapper]
public partial class ProductDistributionToProductDistributionDtoMapper : MapperBase<ProductDistribution, ProductDistributionDto>
{
    public override partial ProductDistributionDto Map(ProductDistribution source);

    public override partial void Map(ProductDistribution source, ProductDistributionDto destination);
}

#endregion ProductDistribution

#region Labels

[Mapper]
public partial class LabelCreateDtoToLabelMapper : MapperBase<LabelCreateDto, Label>
{
    public override partial Label Map(LabelCreateDto source);

    public override partial void Map(LabelCreateDto source, Label destination);
}

[Mapper]
public partial class LabelUpdateDtoToLabelMapper : MapperBase<LabelUpdateDto, Label>
{
    public override partial Label Map(LabelUpdateDto source);

    public override partial void Map(LabelUpdateDto source, Label destination);
}

[Mapper]
public partial class LabelDtoToLabelMapper : MapperBase<LabelDto, Label>
{
    public override partial Label Map(LabelDto source);

    public override partial void Map(LabelDto source, Label destination);
}

[Mapper]
public partial class LabelToLabelDtoMapper : MapperBase<Label, LabelDto>
{
    public override partial LabelDto Map(Label source);

    public override partial void Map(Label source, LabelDto destination);
}

#endregion Labels

#region ServiceGoods

[Mapper]
public partial class ServiceGoodCreateDtoToServiceGoodMapper : MapperBase<ServiceGoodCreateDto, ServiceGood>
{
    public override partial ServiceGood Map(ServiceGoodCreateDto source);

    public override partial void Map(ServiceGoodCreateDto source, ServiceGood destination);

    public override void AfterMap(ServiceGoodCreateDto source, ServiceGood destination)
    {
        destination.AllowedMembership = MapAllowedMembership(source.AllowedMemberships);
        destination.ConsumableStore = MapConsumableStore(source.ConsumableStores);
    }

    private string? MapAllowedMembership(string[]? memberships) =>
        memberships != null && memberships.Any() ? string.Join(',', memberships) : null;

    private string? MapConsumableStore(string[]? stores) =>
        stores != null && stores.Any() ? string.Join(',', stores) : null;
}

[Mapper]
public partial class ServiceGoodUpdateDtoToServiceGoodMapper : MapperBase<ServiceGoodUpdateDto, ServiceGood>
{
    public override partial ServiceGood Map(ServiceGoodUpdateDto source);

    public override partial void Map(ServiceGoodUpdateDto source, ServiceGood destination);

    public override void AfterMap(ServiceGoodUpdateDto source, ServiceGood destination)
    {
        destination.AllowedMembership = MapAllowedMembership(source.AllowedMemberships);
        destination.ConsumableStore = MapConsumableStore(source.ConsumableStores);
    }

    private string? MapAllowedMembership(string[]? memberships) =>
        memberships != null && memberships.Any() ? string.Join(',', memberships) : null;

    private string? MapConsumableStore(string[]? stores) =>
        stores != null && stores.Any() ? string.Join(',', stores) : null;
}

[Mapper]
public partial class ServiceGoodDtoToServiceGoodMapper : MapperBase<ServiceGoodDto, ServiceGood>
{
    public override partial ServiceGood Map(ServiceGoodDto source);

    public override partial void Map(ServiceGoodDto source, ServiceGood destination);

    public override void AfterMap(ServiceGoodDto source, ServiceGood destination)
    {
        destination.AllowedMembership = MapAllowedMembership(source.AllowedMemberships);
        destination.ConsumableStore = MapConsumableStore(source.ConsumableStores);
    }

    private string? MapAllowedMembership(string[]? memberships) =>
        memberships != null && memberships.Any() ? string.Join(',', memberships) : null;

    private string? MapConsumableStore(string[]? stores) =>
        stores != null && stores.Any() ? string.Join(',', stores) : null;
}

[Mapper]
public partial class ServiceGoodToServiceGoodDtoMapper : MapperBase<ServiceGood, ServiceGoodDto>
{
    public override partial ServiceGoodDto Map(ServiceGood source);

    public override partial void Map(ServiceGood source, ServiceGoodDto destination);

    public override void AfterMap(ServiceGood source, ServiceGoodDto destination)
    {
        destination.AllowedMemberships = MapAllowedMemberships(source.AllowedMembership);
        destination.ConsumableStores = MapConsumableStores(source.ConsumableStore);
    }

    private string[]? MapAllowedMemberships(string? membership) =>
        membership.IsNullOrEmpty() ? null : membership.Split(',');

    private string[]? MapConsumableStores(string? store) =>
        store.IsNullOrEmpty() ? null : store.Split(',');
}

#endregion ServiceGoods

#region Stores

[Mapper]
public partial class StoreCreateDtoToStoreMapper : MapperBase<StoreCreateDto, Store>
{
    public override partial Store Map(StoreCreateDto source);

    public override partial void Map(StoreCreateDto source, Store destination);
}

[Mapper]
public partial class StoreUpdateDtoToStoreMapper : MapperBase<StoreUpdateDto, Store>
{
    public override partial Store Map(StoreUpdateDto source);

    public override partial void Map(StoreUpdateDto source, Store destination);
}

[Mapper]
public partial class StoreDtoToStoreMapper : MapperBase<StoreDto, Store>
{
    public override partial Store Map(StoreDto source);

    public override partial void Map(StoreDto source, Store destination);
}

[Mapper]
public partial class StoreToStoreDtoMapper : MapperBase<Store, StoreDto>
{
    public override partial StoreDto Map(Store source);

    public override partial void Map(Store source, StoreDto destination);
}

#endregion Stores

#region StoreClerks

[Mapper]
public partial class StoreClerkCreateDtoToStoreClerkMapper : MapperBase<StoreClerkCreateDto, StoreClerk>
{
    public override partial StoreClerk Map(StoreClerkCreateDto source);

    public override partial void Map(StoreClerkCreateDto source, StoreClerk destination);
}

[Mapper]
public partial class StoreClerkUpdateDtoToStoreClerkMapper : MapperBase<StoreClerkUpdateDto, StoreClerk>
{
    public override partial StoreClerk Map(StoreClerkUpdateDto source);

    public override partial void Map(StoreClerkUpdateDto source, StoreClerk destination);
}

[Mapper]
public partial class StoreClerkDtoToStoreClerkMapper : MapperBase<StoreClerkDto, StoreClerk>
{
    public override partial StoreClerk Map(StoreClerkDto source);

    public override partial void Map(StoreClerkDto source, StoreClerk destination);
}

[Mapper]
public partial class StoreClerkToStoreClerkDtoMapper : MapperBase<StoreClerk, StoreClerkDto>
{
    public override partial StoreClerkDto Map(StoreClerk source);

    public override partial void Map(StoreClerk source, StoreClerkDto destination);
}

#endregion StoreClerks

#region Areas

[Mapper]
public partial class AreaCreateDtoToAreaMapper : MapperBase<AreaCreateDto, Area>
{
    public override partial Area Map(AreaCreateDto source);

    public override partial void Map(AreaCreateDto source, Area destination);
}

[Mapper]
public partial class AreaUpdateDtoToAreaMapper : MapperBase<AreaUpdateDto, Area>
{
    public override partial Area Map(AreaUpdateDto source);

    public override partial void Map(AreaUpdateDto source, Area destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class AreaDtoToAreaMapper : MapperBase<AreaDto, Area>
{
    public override partial Area Map(AreaDto source);

    public override partial void Map(AreaDto source, Area destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class AreaToAreaDtoMapper : MapperBase<Area, AreaDto>
{
    public override partial AreaDto Map(Area source);

    public override partial void Map(Area source, AreaDto destination);
}

[Mapper]
public partial class AreaToAreaTreeDtoMapper : MapperBase<Area, AreaTreeDto>
{
    public override partial AreaTreeDto Map(Area source);

    public override partial void Map(Area source, AreaTreeDto destination);
}

#endregion Areas

#region Articles

[Mapper]
public partial class ArticleCreateDtoToArticleMapper : MapperBase<ArticleCreateDto, Article>
{
    public override partial Article Map(ArticleCreateDto source);

    public override partial void Map(ArticleCreateDto source, Article destination);
}

[Mapper]
public partial class ArticleUpdateDtoToArticleMapper : MapperBase<ArticleUpdateDto, Article>
{
    public override partial Article Map(ArticleUpdateDto source);

    public override partial void Map(ArticleUpdateDto source, Article destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class ArticleDtoToArticleMapper : MapperBase<ArticleDto, Article>
{
    public override partial Article Map(ArticleDto source);

    public override partial void Map(ArticleDto source, Article destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class ArticleToArticleDtoMapper : MapperBase<Article, ArticleDto>
{
    public override partial ArticleDto Map(Article source);

    public override partial void Map(Article source, ArticleDto destination);
}

#endregion Articles

#region ArticleTypes

[Mapper]
public partial class ArticleTypeCreateDtoToArticleTypeMapper : MapperBase<ArticleTypeCreateDto, ArticleType>
{
    public override partial ArticleType Map(ArticleTypeCreateDto source);

    public override partial void Map(ArticleTypeCreateDto source, ArticleType destination);
}

[Mapper]
public partial class ArticleTypeUpdateDtoToArticleTypeMapper : MapperBase<ArticleTypeUpdateDto, ArticleType>
{
    public override partial ArticleType Map(ArticleTypeUpdateDto source);

    public override partial void Map(ArticleTypeUpdateDto source, ArticleType destination);
}

[Mapper]
public partial class ArticleTypeDtoToArticleTypeMapper : MapperBase<ArticleTypeDto, ArticleType>
{
    public override partial ArticleType Map(ArticleTypeDto source);

    public override partial void Map(ArticleTypeDto source, ArticleType destination);
}

[Mapper]
public partial class ArticleTypeToArticleTypeDtoMapper : MapperBase<ArticleType, ArticleTypeDto>
{
    public override partial ArticleTypeDto Map(ArticleType source);

    public override partial void Map(ArticleType source, ArticleTypeDto destination);
}

#endregion ArticleTypes

#region PageDesigns

[Mapper]
public partial class PageDesignCreateDtoToPageDesignMapper : MapperBase<PageDesignCreateDto, PageDesign>
{
    public override partial PageDesign Map(PageDesignCreateDto source);

    public override partial void Map(PageDesignCreateDto source, PageDesign destination);
}

[Mapper]
public partial class PageDesignUpdateDtoToPageDesignMapper : MapperBase<PageDesignUpdateDto, PageDesign>
{
    public override partial PageDesign Map(PageDesignUpdateDto source);

    public override partial void Map(PageDesignUpdateDto source, PageDesign destination);
}

[Mapper]
public partial class PageDesignDtoToPageDesignMapper : MapperBase<PageDesignDto, PageDesign>
{
    public override partial PageDesign Map(PageDesignDto source);

    public override partial void Map(PageDesignDto source, PageDesign destination);
}

[Mapper]
public partial class PageDesignToPageDesignDtoMapper : MapperBase<PageDesign, PageDesignDto>
{
    public override partial PageDesignDto Map(PageDesign source);

    public override partial void Map(PageDesign source, PageDesignDto destination);
}

#endregion PageDesigns

#region PageDesignItems

[Mapper]
public partial class PageDesignItemCreateDtoToPageDesignItemMapper : MapperBase<PageDesignItemCreateDto, PageDesignItem>
{
    public override partial PageDesignItem Map(PageDesignItemCreateDto source);

    public override partial void Map(PageDesignItemCreateDto source, PageDesignItem destination);
}

[Mapper]
public partial class PageDesignItemUpdateDtoToPageDesignItemMapper : MapperBase<PageDesignItemUpdateDto, PageDesignItem>
{
    public override partial PageDesignItem Map(PageDesignItemUpdateDto source);

    public override partial void Map(PageDesignItemUpdateDto source, PageDesignItem destination);
}

[Mapper]
public partial class PageDesignItemDtoToPageDesignItemMapper : MapperBase<PageDesignItemDto, PageDesignItem>
{
    public override partial PageDesignItem Map(PageDesignItemDto source);

    public override partial void Map(PageDesignItemDto source, PageDesignItem destination);
}

[Mapper]
public partial class PageDesignItemToPageDesignItemDtoMapper : MapperBase<PageDesignItem, PageDesignItemDto>
{
    public override partial PageDesignItemDto Map(PageDesignItem source);

    public override partial void Map(PageDesignItem source, PageDesignItemDto destination);
}

#endregion PageDesignItems

#region Notices

[Mapper]
public partial class NoticeCreateDtoToNoticeMapper : MapperBase<NoticeCreateDto, Notice>
{
    public override partial Notice Map(NoticeCreateDto source);

    public override partial void Map(NoticeCreateDto source, Notice destination);
}

[Mapper]
public partial class NoticeUpdateDtoToNoticeMapper : MapperBase<NoticeUpdateDto, Notice>
{
    public override partial Notice Map(NoticeUpdateDto source);

    public override partial void Map(NoticeUpdateDto source, Notice destination);
}

[Mapper]
public partial class NoticeDtoToNoticeMapper : MapperBase<NoticeDto, Notice>
{
    public override partial Notice Map(NoticeDto source);

    public override partial void Map(NoticeDto source, Notice destination);
}

[Mapper]
public partial class NoticeToNoticeDtoMapper : MapperBase<Notice, NoticeDto>
{
    public override partial NoticeDto Map(Notice source);

    public override partial void Map(Notice source, NoticeDto destination);
}

#endregion Notices

#region Forms

[Mapper]
public partial class FormCreateDtoToFormMapper : MapperBase<FormCreateDto, Form>
{
    public override partial Form Map(FormCreateDto source);

    public override partial void Map(FormCreateDto source, Form destination);
}

[Mapper]
public partial class FormUpdateDtoToFormMapper : MapperBase<FormUpdateDto, Form>
{
    public override partial Form Map(FormUpdateDto source);

    public override partial void Map(FormUpdateDto source, Form destination);
}

[Mapper]
public partial class FormDtoToFormMapper : MapperBase<FormDto, Form>
{
    public override partial Form Map(FormDto source);

    public override partial void Map(FormDto source, Form destination);
}

[Mapper]
public partial class FormToFormDtoMapper : MapperBase<Form, FormDto>
{
    public override partial FormDto Map(Form source);

    public override partial void Map(Form source, FormDto destination);
}

#endregion Forms

#region FormItems

[Mapper]
public partial class FormItemCreateDtoToFormItemMapper : MapperBase<FormItemCreateDto, FormItem>
{
    public override partial FormItem Map(FormItemCreateDto source);

    public override partial void Map(FormItemCreateDto source, FormItem destination);
}

[Mapper]
public partial class FormItemUpdateDtoToFormItemMapper : MapperBase<FormItemUpdateDto, FormItem>
{
    public override partial FormItem Map(FormItemUpdateDto source);

    public override partial void Map(FormItemUpdateDto source, FormItem destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class FormItemDtoToFormItemMapper : MapperBase<FormItemDto, FormItem>
{
    public override partial FormItem Map(FormItemDto source);

    public override partial void Map(FormItemDto source, FormItem destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class FormItemToFormItemDtoMapper : MapperBase<FormItem, FormItemDto>
{
    public override partial FormItemDto Map(FormItem source);

    public override partial void Map(FormItem source, FormItemDto destination);
}

#endregion FormItems

#region ServiceDescriptions

[Mapper]
public partial class ServiceDescriptionCreateDtoToServiceDescriptionMapper : MapperBase<ServiceDescriptionCreateDto, ServiceDescription>
{
    public override partial ServiceDescription Map(ServiceDescriptionCreateDto source);

    public override partial void Map(ServiceDescriptionCreateDto source, ServiceDescription destination);
}

[Mapper]
public partial class ServiceDescriptionUpdateDtoToServiceDescriptionMapper : MapperBase<ServiceDescriptionUpdateDto, ServiceDescription>
{
    public override partial ServiceDescription Map(ServiceDescriptionUpdateDto source);

    public override partial void Map(ServiceDescriptionUpdateDto source, ServiceDescription destination);
}

[Mapper]
public partial class ServiceDescriptionDtoToServiceDescriptionMapper : MapperBase<ServiceDescriptionDto, ServiceDescription>
{
    public override partial ServiceDescription Map(ServiceDescriptionDto source);

    public override partial void Map(ServiceDescriptionDto source, ServiceDescription destination);
}

[Mapper]
public partial class ServiceDescriptionToServiceDescriptionDtoMapper : MapperBase<ServiceDescription, ServiceDescriptionDto>
{
    public override partial ServiceDescriptionDto Map(ServiceDescription source);

    public override partial void Map(ServiceDescription source, ServiceDescriptionDto destination);
}

#endregion ServiceDescriptions

#region GoodCollections

[Mapper]
public partial class GoodCollectionMapper : TwoWayMapperBase<GoodCollection, GoodCollectionDto>
{
    public override partial GoodCollectionDto Map(GoodCollection source);

    public override partial void Map(GoodCollection source, GoodCollectionDto destination);

    public override partial GoodCollection ReverseMap(GoodCollectionDto source);

    public override partial void ReverseMap(GoodCollectionDto source, GoodCollection destination);
}

#endregion GoodCollections

#region GoodBrowsings

[Mapper]
public partial class GoodBrowsingMapper : TwoWayMapperBase<GoodBrowsing, GoodBrowsingDto>
{
    public override partial GoodBrowsingDto Map(GoodBrowsing source);

    public override partial void Map(GoodBrowsing source, GoodBrowsingDto destination);

    public override partial GoodBrowsing ReverseMap(GoodBrowsingDto source);

    public override partial void ReverseMap(GoodBrowsingDto source, GoodBrowsing destination);
}

#endregion GoodBrowsings