using Rex.Service.Permission.AuthServices;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Rex.AuthService;

[Dependency(ReplaceServices = true)]
public class AuthServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => AuthServicePermissions.GroupName;
}