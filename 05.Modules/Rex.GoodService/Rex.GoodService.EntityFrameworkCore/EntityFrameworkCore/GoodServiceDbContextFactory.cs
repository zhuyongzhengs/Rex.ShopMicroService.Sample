using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Rex.GoodService.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */

public class GoodServiceDbContextFactory : IDesignTimeDbContextFactory<GoodServiceDbContext>
{
    public GoodServiceDbContext CreateDbContext(string[] args)
    {
        GoodServiceEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<GoodServiceDbContext>()
            .UseMySql(configuration.GetConnectionString(GoodServiceConsts.ConnectionStringName), MySqlServerVersion.LatestSupportedServerVersion);

        return new GoodServiceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Rex.GoodService.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}