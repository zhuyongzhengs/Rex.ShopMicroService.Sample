using Microsoft.Extensions.DependencyInjection;
using Rex.OrderService.EntityFrameworkCore;
using System;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;

namespace Rex.OrderService.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(OrderServiceEntityFrameworkCoreModule),
    typeof(OrderServiceApplicationContractsModule)
    )]
public class OrderServiceDbMigratorModule : AbpModule
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