using Rex.Service.Permission.BaseServices;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Rex.BaseService;

[Dependency(ReplaceServices = true)]
public class BaseServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => BaseServicePermissions.GroupName;
}