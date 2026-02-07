using Microsoft.Extensions.DependencyInjection;
using Rex.BaseService.EntityFrameworkCore;
using System;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;

namespace Rex.BaseService.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BaseServiceEntityFrameworkCoreModule),
    typeof(BaseServiceApplicationContractsModule)
    )]
public class BaseServiceDbMigratorModule : AbpModule
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