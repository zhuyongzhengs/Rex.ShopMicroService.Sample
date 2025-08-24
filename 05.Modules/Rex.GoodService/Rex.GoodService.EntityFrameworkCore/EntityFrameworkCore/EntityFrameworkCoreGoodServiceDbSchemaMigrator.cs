using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rex.GoodService.Data;
using Volo.Abp.DependencyInjection;

namespace Rex.GoodService.EntityFrameworkCore;

public class EntityFrameworkCoreGoodServiceDbSchemaMigrator
    : IGoodServiceDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreGoodServiceDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the GoodServiceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<GoodServiceDbContext>()
            .Database
            .MigrateAsync();
    }
}
