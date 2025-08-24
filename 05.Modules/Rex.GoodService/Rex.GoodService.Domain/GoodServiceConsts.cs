using Volo.Abp.Caching;

namespace Rex.GoodService;

public static class GoodServiceConsts
{
    #region 系统(授权)前缀

    public const string AuthDbTablePrefix = "Auth_";
    public const string AuthDbSchema = null;

    #endregion 系统(授权)前缀

    #region 系统前缀

    public const string SysDbTablePrefix = "Sys_";
    public const string SysDbSchema = null;

    #endregion 系统前缀

    #region 默认(商品)前缀

    public const string DefaultDbTablePrefix = "Gd_";
    public const string DefaultDbSchema = null;

    #endregion 默认(商品)前缀

    #region 连接字符串

    public const string ConnectionStringName = "Goods";

    #endregion 连接字符串
}