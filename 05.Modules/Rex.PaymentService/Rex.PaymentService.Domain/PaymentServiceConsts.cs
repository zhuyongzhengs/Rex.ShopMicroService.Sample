namespace Rex.PaymentService;

public static class PaymentServiceConsts
{
    #region 系统(授权)前缀

    public const string AuthDbTablePrefix = "Auth_";
    public const string AuthDbSchema = null;

    #endregion 系统(授权)前缀

    #region 系统前缀

    public const string SysDbTablePrefix = "Sys_";
    public const string SysDbSchema = null;

    #endregion 系统前缀

    #region 默认(支付)前缀

    public const string DefaultDbTablePrefix = "Pm_";
    public const string DefaultDbSchema = null;

    #endregion 默认(支付)前缀

    #region 连接字符串

    public const string ConnectionStringName = "Payments";

    #endregion 连接字符串
}
