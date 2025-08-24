using Rex.PromotionService.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Rex.PromotionService.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(PromotionServiceEntityFrameworkCoreModule),
    typeof(PromotionServiceApplicationContractsModule)
    )]
public class PromotionServiceDbMigratorModule : AbpModule
{
}
