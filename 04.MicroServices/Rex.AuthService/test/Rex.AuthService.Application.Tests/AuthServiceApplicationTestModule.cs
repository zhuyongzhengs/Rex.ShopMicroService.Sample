using Volo.Abp.Modularity;

namespace Rex.AuthService;

[DependsOn(
    typeof(AuthServiceApplicationModule),
    typeof(AuthServiceDomainTestModule)
    )]
public class AuthServiceApplicationTestModule : AbpModule
{

}
