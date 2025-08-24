using System;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Rex.Service.Permission.PromotionServices
{
    /// <summary>
    /// 促销权限定义
    /// </summary>
    public static class PromotionServicePermissionDefinitionExtensions
    {
        /// <summary>
        /// 添加促销权限
        /// </summary>
        /// <param name="pinTuanGroup">组名称</param>
        public static void AddPromotionPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition pinTuanGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = pinTuanGroup.AddPermission(PromotionServicePermissions.Promotions.Default, localizableStr != null ? localizableStr.Invoke("Promotion") : null);
            permissionDefinition.AddChild(PromotionServicePermissions.Promotions.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(PromotionServicePermissions.Promotions.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(PromotionServicePermissions.Promotions.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加优惠劵权限
        /// </summary>
        /// <param name="pinTuanGroup">组名称</param>
        public static void AddCouponPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition pinTuanGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = pinTuanGroup.AddPermission(PromotionServicePermissions.Coupons.Default, localizableStr != null ? localizableStr.Invoke("Coupon") : null);
            permissionDefinition.AddChild(PromotionServicePermissions.Coupons.Couponlist, localizableStr != null ? localizableStr.Invoke("Couponlist") : null);
            permissionDefinition.AddChild(PromotionServicePermissions.Coupons.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(PromotionServicePermissions.Coupons.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(PromotionServicePermissions.Coupons.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加团购秒杀权限
        /// </summary>
        /// <param name="pinTuanGroup">组名称</param>
        public static void AddGroupSeckillPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition pinTuanGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = pinTuanGroup.AddPermission(PromotionServicePermissions.GroupSeckills.Default, localizableStr != null ? localizableStr.Invoke("GroupSeckill") : null);
            permissionDefinition.AddChild(PromotionServicePermissions.GroupSeckills.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(PromotionServicePermissions.GroupSeckills.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(PromotionServicePermissions.GroupSeckills.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加拼团规则权限
        /// </summary>
        /// <param name="pinTuanGroup">组名称</param>
        public static void AddPinTuanRulePermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition pinTuanGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = pinTuanGroup.AddPermission(PromotionServicePermissions.PinTuanRules.Default, localizableStr != null ? localizableStr.Invoke("PinTuanRule") : null);
            permissionDefinition.AddChild(PromotionServicePermissions.PinTuanRules.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(PromotionServicePermissions.PinTuanRules.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(PromotionServicePermissions.PinTuanRules.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加拼团记录权限
        /// </summary>
        /// <param name="pinTuanGroup">组名称</param>
        public static void AddPinTuanRecordPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition pinTuanGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = pinTuanGroup.AddPermission(PromotionServicePermissions.PinTuanRecords.Default, localizableStr != null ? localizableStr.Invoke("PinTuanRecord") : null);
        }
    }
}