using Rex.AuthService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Rex.AuthService;

[DependsOn(
    typeof(AuthServiceEntityFrameworkCoreTestModule)
    )]
public class AuthServiceDomainTestModule : AbpModule
{

}
