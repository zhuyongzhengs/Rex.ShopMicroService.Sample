using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rex.OrderService.Data;
using Volo.Abp.DependencyInjection;

namespace Rex.OrderService.EntityFrameworkCore;

public class EntityFrameworkCoreOrderServiceDbSchemaMigrator
    : IOrderServiceDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreOrderServiceDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the OrderServiceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<OrderServiceDbContext>()
            .Database
            .MigrateAsync();
    }
}
