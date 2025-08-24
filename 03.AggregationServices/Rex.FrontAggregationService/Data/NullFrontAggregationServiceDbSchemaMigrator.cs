using Volo.Abp.DependencyInjection;

namespace Rex.FrontAggregationService.Data;

/* This is used if database provider does't define
 * IFrontAggregationServiceDbSchemaMigrator implementation.
 */

public class NullFrontAggregationServiceDbSchemaMigrator : IFrontAggregationServiceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}