using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rex.BaseService.Data;
using Volo.Abp.DependencyInjection;

namespace Rex.BaseService.EntityFrameworkCore;

public class EntityFrameworkCoreBaseServiceDbSchemaMigrator
    : IBaseServiceDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreBaseServiceDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the BaseServiceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<BaseServiceDbContext>()
            .Database
            .MigrateAsync();
    }
}
