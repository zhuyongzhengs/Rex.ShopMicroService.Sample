using Rex.OrderService.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;
using Rex.Service.Setting.BaseServices;

namespace Rex.OrderService.Settings;

public class OrderServiceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        #region 平台设置

        context.AddBasePlatformSettingDefine(val => L(val));

        #endregion 平台设置
    }

    /// <summary>
    /// 本地化字符串
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<OrderServiceResource>(name);
    }
}