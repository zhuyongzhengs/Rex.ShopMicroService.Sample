using Rex.GoodService.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Rex.GoodService.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(GoodServiceEntityFrameworkCoreModule),
    typeof(GoodServiceApplicationContractsModule)
    )]
public class GoodServiceDbMigratorModule : AbpModule
{
}
