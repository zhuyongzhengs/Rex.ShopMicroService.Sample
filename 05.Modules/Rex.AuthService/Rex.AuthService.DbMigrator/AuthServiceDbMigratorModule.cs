using Rex.AuthService.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Rex.AuthService.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AuthServiceEntityFrameworkCoreModule),
    typeof(AuthServiceApplicationContractsModule)
    )]
public class AuthServiceDbMigratorModule : AbpModule
{
}
