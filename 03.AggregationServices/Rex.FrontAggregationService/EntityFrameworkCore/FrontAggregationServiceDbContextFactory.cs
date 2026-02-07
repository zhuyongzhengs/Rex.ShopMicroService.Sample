using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Rex.FrontAggregationService.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */

public class FrontAggregationServiceDbContextFactory : IDesignTimeDbContextFactory<FrontAggregationServiceDbContext>
{
    public FrontAggregationServiceDbContext CreateDbContext(string[] args)
    {
        FrontAggregationServiceEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<FrontAggregationServiceDbContext>()
            .UseNpgsql(configuration.GetConnectionString(FrontAggregationServiceConsts.ConnectionStringName));

        return new FrontAggregationServiceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}