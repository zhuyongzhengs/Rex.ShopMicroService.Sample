using Microsoft.Extensions.DependencyInjection;
using Rex.PromotionService.EntityFrameworkCore;
using System;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;

namespace Rex.PromotionService.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(PromotionServiceEntityFrameworkCoreModule),
    typeof(PromotionServiceApplicationContractsModule)
    )]
public class PromotionServiceDbMigratorModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        configuration["Redis:Configuration"] = configuration["ConnectionStrings:Redis"];
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpClockOptions>(options =>
        {
            options.Kind = DateTimeKind.Utc;
        });
    }
}