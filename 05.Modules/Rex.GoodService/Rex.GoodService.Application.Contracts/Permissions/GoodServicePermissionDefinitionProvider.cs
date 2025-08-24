using Rex.GoodService.Localization;
using Rex.Service.Permission.GoodServices;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Rex.GoodService.Permissions;

public class GoodServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var goodGroup = context.AddGroup(GoodServicePermissions.GroupName);

        #region 品牌定义

        context.AddGoodBrandPermissionDefine(goodGroup, val => L(val));

        #endregion 品牌定义

        #region 商品定义

        context.AddGoodPermissionDefine(goodGroup, val => L(val));

        #endregion 商品定义

        #region 商品分类定义

        context.AddGoodCategoryPermissionDefine(goodGroup, val => L(val));

        #endregion 商品分类定义

        #region 商品参数[模型]定义

        context.AddGoodParamPermissionDefine(goodGroup, val => L(val));

        #endregion 商品参数[模型]定义

        #region 商品类型规格定义

        context.AddGoodTypeSpecPermissionDefine(goodGroup, val => L(val));

        #endregion 商品类型规格定义

        #region 商品服务定义

        context.AddServiceGoodPermissionDefine(goodGroup, val => L(val));

        #endregion 商品服务定义

        #region 门店定义

        context.AddStorePermissionDefine(goodGroup, val => L(val));

        #endregion 门店定义

        #region 店铺店员关联定义

        context.AddStoreClerkPermissionDefine(goodGroup, val => L(val));

        #endregion 店铺店员关联定义

        #region 区域定义

        context.AddAreaPermissionDefine(goodGroup, val => L(val));

        #endregion 区域定义

        #region 文章定义

        context.AddArticlePermissionDefine(goodGroup, val => L(val));

        #endregion 文章定义

        #region 文章分类定义

        context.AddArticleTypePermissionDefine(goodGroup, val => L(val));

        #endregion 文章分类定义

        #region 页面设计定义

        context.AddPageDesignPermissionDefine(goodGroup, val => L(val));

        #endregion 页面设计定义

        #region 公告定义

        context.AddNoticePermissionDefine(goodGroup, val => L(val));

        #endregion 公告定义

        #region 自定义表单定义

        context.AddFormPermissionDefine(goodGroup, val => L(val));

        #endregion 自定义表单定义

        #region 商城服务描述定义

        context.AddServiceDescriptionPermissionDefine(goodGroup, val => L(val));

        #endregion 商城服务描述定义

        #region 商品评价定义

        context.AddGoodCommentPermissionDefine(goodGroup, val => L(val));

        #endregion 商品评价定义
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<GoodServiceResource>(name);
    }
}