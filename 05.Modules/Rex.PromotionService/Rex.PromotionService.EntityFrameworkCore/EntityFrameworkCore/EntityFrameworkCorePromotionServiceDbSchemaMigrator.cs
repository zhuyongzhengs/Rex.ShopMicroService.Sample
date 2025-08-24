using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rex.PromotionService.Data;
using Volo.Abp.DependencyInjection;

namespace Rex.PromotionService.EntityFrameworkCore;

public class EntityFrameworkCorePromotionServiceDbSchemaMigrator
    : IPromotionServiceDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCorePromotionServiceDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the PromotionServiceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<PromotionServiceDbContext>()
            .Database
            .MigrateAsync();
    }
}
