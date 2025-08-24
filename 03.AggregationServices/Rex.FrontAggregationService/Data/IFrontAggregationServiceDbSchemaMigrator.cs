namespace Rex.FrontAggregationService.Data;

public interface IFrontAggregationServiceDbSchemaMigrator
{
    Task MigrateAsync();
}