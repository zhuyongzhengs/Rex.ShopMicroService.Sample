using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Rex.OrderService.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */

public class OrderServiceDbContextFactory : IDesignTimeDbContextFactory<OrderServiceDbContext>
{
    public OrderServiceDbContext CreateDbContext(string[] args)
    {
        OrderServiceEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<OrderServiceDbContext>()
            .UseMySql(configuration.GetConnectionString(OrderServiceConsts.ConnectionStringName), MySqlServerVersion.LatestSupportedServerVersion);

        return new OrderServiceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Rex.OrderService.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}