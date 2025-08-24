using Rex.BaseService.Localization;
using Rex.Service.Permission.BaseServices;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Rex.BaseService.Permissions;

public class BaseServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var baseGroup = context.AddGroup(BaseServicePermissions.GroupName);

        #region 首页定义

        context.AddBaseHomePermissionDefine(baseGroup, val => L(val));

        #endregion 首页定义

        #region 菜单定义

        context.AddBaseMenuPermissionDefine(baseGroup, val => L(val));

        #endregion 菜单定义

        #region 组织单元定义

        context.AddBaseOrganizationUnitDefine(baseGroup, val => L(val));

        #endregion 组织单元定义

        #region 审计日志定义

        context.AddBaseAuditLoggingPermissionDefine(baseGroup, val => L(val));

        #endregion 审计日志定义

        #region 安全日志定义

        context.AddBaseSecurityLogPermissionDefine(baseGroup, val => L(val));

        #endregion 安全日志定义

        #region 注册用户定义

        context.AddBaseSysUserPermissionDefine(baseGroup, val => L(val));

        #endregion 注册用户定义

        #region 微信用户定义

        context.AddBaseUserWeChatPermissionDefine(baseGroup, val => L(val));

        #endregion 微信用户定义

        #region 用户等级定义

        context.AddBaseUserGradePermissionDefine(baseGroup, val => L(val));

        #endregion 用户等级定义

        #region 平台设置定义

        context.AddBasePlatformSettingPermissionDefine(baseGroup, val => L(val));

        #endregion 平台设置定义
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BaseServiceResource>(name);
    }
}