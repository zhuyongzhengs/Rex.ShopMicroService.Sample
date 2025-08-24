using System;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Rex.Service.Permission.GoodServices
{
    /// <summary>
    /// 商品权限定义
    /// </summary>
    public static class GoodServicePermissionDefinitionExtensions
    {
        /// <summary>
        /// 添加品牌权限
        /// </summary>
        /// <param name="goodGroup">组名称</param>
        public static void AddGoodBrandPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition goodGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = goodGroup.AddPermission(GoodServicePermissions.GoodBrands.Default, localizableStr != null ? localizableStr.Invoke("Brand") : null);
            permissionDefinition.AddChild(GoodServicePermissions.GoodBrands.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(GoodServicePermissions.GoodBrands.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(GoodServicePermissions.GoodBrands.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加商品权限
        /// </summary>
        /// <param name="goodGroup">组名称</param>
        public static void AddGoodPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition goodGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = goodGroup.AddPermission(GoodServicePermissions.Goods.Default, localizableStr != null ? localizableStr.Invoke("Goods") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Goods.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Goods.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Goods.Look, localizableStr != null ? localizableStr.Invoke("Look") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Goods.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加商品分类权限
        /// </summary>
        /// <param name="goodGroup">组名称</param>
        public static void AddGoodCategoryPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition goodGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = goodGroup.AddPermission(GoodServicePermissions.GoodCategorys.Default, localizableStr != null ? localizableStr.Invoke("Good:Category") : null);
            permissionDefinition.AddChild(GoodServicePermissions.GoodCategorys.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(GoodServicePermissions.GoodCategorys.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(GoodServicePermissions.GoodCategorys.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加商品参数[模型]权限
        /// </summary>
        /// <param name="goodGroup">组名称</param>
        public static void AddGoodParamPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition goodGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = goodGroup.AddPermission(GoodServicePermissions.GoodParams.Default, localizableStr != null ? localizableStr.Invoke("Good:Param") : null);
            permissionDefinition.AddChild(GoodServicePermissions.GoodParams.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(GoodServicePermissions.GoodParams.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(GoodServicePermissions.GoodParams.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加商品类型规格权限
        /// </summary>
        /// <param name="goodGroup">组名称</param>
        public static void AddGoodTypeSpecPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition goodGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = goodGroup.AddPermission(GoodServicePermissions.GoodTypeSpecs.Default, localizableStr != null ? localizableStr.Invoke("Good:TypeSpec") : null);
            permissionDefinition.AddChild(GoodServicePermissions.GoodTypeSpecs.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(GoodServicePermissions.GoodTypeSpecs.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(GoodServicePermissions.GoodTypeSpecs.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加服务商品权限
        /// </summary>
        /// <param name="goodGroup">组名称</param>
        public static void AddServiceGoodPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition goodGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = goodGroup.AddPermission(GoodServicePermissions.ServiceGoods.Default, localizableStr != null ? localizableStr.Invoke("ServiceGood") : null);
            permissionDefinition.AddChild(GoodServicePermissions.ServiceGoods.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(GoodServicePermissions.ServiceGoods.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(GoodServicePermissions.ServiceGoods.Look, localizableStr != null ? localizableStr.Invoke("Look") : null);
            permissionDefinition.AddChild(GoodServicePermissions.ServiceGoods.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加门店权限
        /// </summary>
        /// <param name="goodGroup">组名称</param>
        public static void AddStorePermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition goodGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = goodGroup.AddPermission(GoodServicePermissions.Stores.Default, localizableStr != null ? localizableStr.Invoke("Store") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Stores.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Stores.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Stores.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加店铺店员关联权限
        /// </summary>
        /// <param name="goodGroup">组名称</param>
        public static void AddStoreClerkPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition goodGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = goodGroup.AddPermission(GoodServicePermissions.StoreClerks.Default, localizableStr != null ? localizableStr.Invoke("StoreClerk") : null);
            permissionDefinition.AddChild(GoodServicePermissions.StoreClerks.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(GoodServicePermissions.StoreClerks.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(GoodServicePermissions.StoreClerks.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加区域权限
        /// </summary>
        /// <param name="goodGroup">组名称</param>
        public static void AddAreaPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition goodGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = goodGroup.AddPermission(GoodServicePermissions.Areas.Default, localizableStr != null ? localizableStr.Invoke("Area") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Areas.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Areas.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Areas.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加文章权限
        /// </summary>
        /// <param name="goodGroup">组名称</param>
        public static void AddArticlePermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition goodGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = goodGroup.AddPermission(GoodServicePermissions.Articles.Default, localizableStr != null ? localizableStr.Invoke("Article") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Articles.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Articles.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Articles.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加文章分类权限
        /// </summary>
        /// <param name="goodGroup">组名称</param>
        public static void AddArticleTypePermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition goodGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = goodGroup.AddPermission(GoodServicePermissions.ArticleTypes.Default, localizableStr != null ? localizableStr.Invoke("ArticleType") : null);
            permissionDefinition.AddChild(GoodServicePermissions.ArticleTypes.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(GoodServicePermissions.ArticleTypes.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(GoodServicePermissions.ArticleTypes.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加页面设计权限
        /// </summary>
        /// <param name="goodGroup">组名称</param>
        public static void AddPageDesignPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition goodGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = goodGroup.AddPermission(GoodServicePermissions.PageDesigns.Default, localizableStr != null ? localizableStr.Invoke("PageDesign") : null);
            permissionDefinition.AddChild(GoodServicePermissions.PageDesigns.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(GoodServicePermissions.PageDesigns.Copy, localizableStr != null ? localizableStr.Invoke("Copy") : null);
            permissionDefinition.AddChild(GoodServicePermissions.PageDesigns.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(GoodServicePermissions.PageDesigns.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
            permissionDefinition.AddChild(GoodServicePermissions.PageDesigns.Preview, localizableStr != null ? localizableStr.Invoke("Preview") : null);
            permissionDefinition.AddChild(GoodServicePermissions.PageDesigns.Layout, localizableStr != null ? localizableStr.Invoke("Layout") : null);
        }

        /// <summary>
        /// 添加公告权限
        /// </summary>
        /// <param name="goodGroup">组名称</param>
        public static void AddNoticePermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition goodGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = goodGroup.AddPermission(GoodServicePermissions.Notices.Default, localizableStr != null ? localizableStr.Invoke("Notice") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Notices.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Notices.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Notices.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加自定义表单权限
        /// </summary>
        /// <param name="goodGroup">组名称</param>
        public static void AddFormPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition goodGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = goodGroup.AddPermission(GoodServicePermissions.Forms.Default, localizableStr != null ? localizableStr.Invoke("Form") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Forms.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Forms.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(GoodServicePermissions.Forms.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加商城服务描述权限
        /// </summary>
        /// <param name="goodGroup">组名称</param>
        public static void AddServiceDescriptionPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition goodGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = goodGroup.AddPermission(GoodServicePermissions.ServiceDescriptions.Default, localizableStr != null ? localizableStr.Invoke("ServiceDescription") : null);
            permissionDefinition.AddChild(GoodServicePermissions.ServiceDescriptions.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(GoodServicePermissions.ServiceDescriptions.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(GoodServicePermissions.ServiceDescriptions.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加商品评价权限
        /// </summary>
        /// <param name="goodGroup">组名称</param>
        public static void AddGoodCommentPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition goodGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = goodGroup.AddPermission(GoodServicePermissions.GoodComments.Default, localizableStr != null ? localizableStr.Invoke("GoodComment") : null);
            permissionDefinition.AddChild(GoodServicePermissions.GoodComments.SellerReply, localizableStr != null ? localizableStr.Invoke("SellerReply") : null);
            permissionDefinition.AddChild(GoodServicePermissions.GoodComments.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }
    }
}