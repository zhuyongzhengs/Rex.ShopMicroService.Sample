using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rex.FrontAggregationService.Data;
using Volo.Abp.DependencyInjection;

namespace Rex.FrontAggregationService.EntityFrameworkCore;

public class EntityFrameworkCoreFrontAggregationServiceDbSchemaMigrator
    : IFrontAggregationServiceDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreFrontAggregationServiceDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the FrontAggregationServiceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<FrontAggregationServiceDbContext>()
            .Database
            .MigrateAsync();
    }
}