using Rex.BaseService.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Rex.BaseService.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BaseServiceEntityFrameworkCoreModule),
    typeof(BaseServiceApplicationContractsModule)
    )]
public class BaseServiceDbMigratorModule : AbpModule
{
}
