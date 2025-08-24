using Rex.Service.Permission.PromotionServices;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Rex.PromotionService;

[Dependency(ReplaceServices = true)]
public class PromotionServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => PromotionServicePermissions.GroupName;
}