using System;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Rex.Service.Permission.BaseServices
{
    /// <summary>
    /// Base权限定义
    /// </summary>
    public static class BaseServicePermissionDefinitionExtensions
    {
        /// <summary>
        /// 添加首页权限
        /// </summary>
        /// <param name="baseGroup">组名称</param>
        public static void AddBaseHomePermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition baseGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = baseGroup.AddPermission(BaseServicePermissions.Homes.Default, localizableStr != null ? localizableStr.Invoke("Home") : null);
        }

        /// <summary>
        /// 添加菜单权限
        /// </summary>
        /// <param name="baseGroup">组名称</param>
        public static void AddBaseMenuPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition baseGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = baseGroup.AddPermission(BaseServicePermissions.Menus.Default, localizableStr != null ? localizableStr.Invoke("Menu") : null);
            permissionDefinition.AddChild(BaseServicePermissions.Menus.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(BaseServicePermissions.Menus.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(BaseServicePermissions.Menus.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加组织单元权限
        /// </summary>
        /// <param name="baseGroup">组名称</param>
        public static void AddBaseOrganizationUnitDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition baseGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = baseGroup.AddPermission(BaseServicePermissions.OrganizationUnits.Default, localizableStr != null ? localizableStr.Invoke("OrganizationUnit") : null);
            permissionDefinition.AddChild(BaseServicePermissions.OrganizationUnits.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(BaseServicePermissions.OrganizationUnits.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(BaseServicePermissions.OrganizationUnits.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
            permissionDefinition.AddChild(BaseServicePermissions.OrganizationUnits.ManagingUser, localizableStr != null ? localizableStr.Invoke("OrganizationUnit:ManagingUser") : null);
            permissionDefinition.AddChild(BaseServicePermissions.OrganizationUnits.ManagingRole, localizableStr != null ? localizableStr.Invoke("OrganizationUnit:ManagingRole") : null);
        }

        /// <summary>
        /// 添加审计日志权限
        /// </summary>
        /// <param name="baseGroup">组名称</param>
        public static void AddBaseAuditLoggingPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition baseGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = baseGroup.AddPermission(BaseServicePermissions.AuditLoggings.Default, localizableStr != null ? localizableStr.Invoke("AuditLogging") : null);
        }

        /// <summary>
        /// 添加安全日志权限
        /// </summary>
        /// <param name="baseGroup">组名称</param>
        public static void AddBaseSecurityLogPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition baseGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = baseGroup.AddPermission(BaseServicePermissions.SecurityLogs.Default, localizableStr != null ? localizableStr.Invoke("SecurityLog") : null);
        }

        /// <summary>
        /// 添加注册用户权限
        /// </summary>
        /// <param name="baseGroup">组名称</param>
        public static void AddBaseSysUserPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition baseGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = baseGroup.AddPermission(BaseServicePermissions.SysUsers.Default, localizableStr != null ? localizableStr.Invoke("SysUser") : null);
            permissionDefinition.AddChild(BaseServicePermissions.SysUsers.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(BaseServicePermissions.SysUsers.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(BaseServicePermissions.SysUsers.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加微信用户权限
        /// </summary>
        /// <param name="baseGroup">组名称</param>
        public static void AddBaseUserWeChatPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition baseGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = baseGroup.AddPermission(BaseServicePermissions.UserWeChats.Default, localizableStr != null ? localizableStr.Invoke("UserWeChat") : null);
            permissionDefinition.AddChild(BaseServicePermissions.UserWeChats.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(BaseServicePermissions.UserWeChats.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(BaseServicePermissions.UserWeChats.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加用户等级权限
        /// </summary>
        /// <param name="baseGroup">组名称</param>
        public static void AddBaseUserGradePermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition baseGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = baseGroup.AddPermission(BaseServicePermissions.UserGrades.Default, localizableStr != null ? localizableStr.Invoke("UserGrade") : null);
            permissionDefinition.AddChild(BaseServicePermissions.UserGrades.Create, localizableStr != null ? localizableStr.Invoke("Create") : null);
            permissionDefinition.AddChild(BaseServicePermissions.UserGrades.Update, localizableStr != null ? localizableStr.Invoke("Update") : null);
            permissionDefinition.AddChild(BaseServicePermissions.UserGrades.Delete, localizableStr != null ? localizableStr.Invoke("Delete") : null);
        }

        /// <summary>
        /// 添加平台设置权限
        /// </summary>
        /// <param name="baseGroup">组名称</param>
        public static void AddBasePlatformSettingPermissionDefine(this IPermissionDefinitionContext context, PermissionGroupDefinition baseGroup, Func<string, LocalizableString> localizableStr = null)
        {
            var permissionDefinition = baseGroup.AddPermission(BaseServicePermissions.PlatformSettings.Default, localizableStr != null ? localizableStr.Invoke("PlatformSetting") : null);
        }
    }
}