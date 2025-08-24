using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Rex.PromotionService.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */

public class PromotionServiceDbContextFactory : IDesignTimeDbContextFactory<PromotionServiceDbContext>
{
    public PromotionServiceDbContext CreateDbContext(string[] args)
    {
        PromotionServiceEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<PromotionServiceDbContext>()
            .UseMySql(configuration.GetConnectionString(PromotionServiceConsts.ConnectionStringName), MySqlServerVersion.LatestSupportedServerVersion);

        return new PromotionServiceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Rex.PromotionService.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}