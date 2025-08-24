using Volo.Abp.Settings;

namespace Rex.GoodService.Settings;

public class GoodServiceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(GoodServiceSettings.MySetting1));
    }
}
