using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.VirtualFileSystem;

namespace Rex.AuthService;

[DependsOn(
    typeof(AuthServiceApplicationContractsModule),
    typeof(AbpAccountHttpApiClientModule),
typeof(AbpIdentityHttpApiClientModule),
typeof(AbpPermissionManagementHttpApiClientModule),
typeof(AbpTenantManagementHttpApiClientModule),
typeof(AbpFeatureManagementHttpApiClientModule),
typeof(AbpSettingManagementHttpApiClientModule)
)]
public class AuthServiceHttpApiClientModule : AbpModule
{
    public const string RemoteServiceName = "AuthService";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(AuthServiceApplicationContractsModule).Assembly,
            RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AuthServiceHttpApiClientModule>();
        });
    }
}