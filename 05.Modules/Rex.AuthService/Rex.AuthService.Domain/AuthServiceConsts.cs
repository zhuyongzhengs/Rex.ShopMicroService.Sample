using Volo.Abp.Identity;

namespace Rex.AuthService;

public static class AuthServiceConsts
{
    #region 默认(授权)前缀

    public const string DefaultDbTablePrefix = "Auth_";
    public const string DefaultDbSchema = null;

    #endregion 默认(授权)前缀

    #region 系统前缀

    public const string SysDbTablePrefix = "Sys_";
    public const string SysDbSchema = null;

    #endregion 系统前缀

    #region 连接字符串

    public const string ConnectionStringName = "Default";

    #endregion 连接字符串

    /// <summary>
    /// 授权类型
    /// </summary>
    public static class GrantTypes
    {
        public const string WechatCode = "wechat_code";
    }

    public const string AdminEmailDefaultValue = IdentityDataSeedContributor.AdminEmailDefaultValue;
    public const string AdminPasswordDefaultValue = IdentityDataSeedContributor.AdminPasswordDefaultValue;
}