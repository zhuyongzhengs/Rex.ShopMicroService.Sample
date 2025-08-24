using Volo.Abp.Settings;

namespace Rex.PromotionService.Settings;

public class PromotionServiceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(PromotionServiceSettings.MySetting1));
    }
}
