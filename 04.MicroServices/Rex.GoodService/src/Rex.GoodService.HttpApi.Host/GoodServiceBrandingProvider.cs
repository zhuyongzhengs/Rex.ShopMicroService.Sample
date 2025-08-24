using Rex.Service.Permission.GoodServices;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Rex.GoodService;

[Dependency(ReplaceServices = true)]
public class GoodServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => GoodServicePermissions.GroupName;
}