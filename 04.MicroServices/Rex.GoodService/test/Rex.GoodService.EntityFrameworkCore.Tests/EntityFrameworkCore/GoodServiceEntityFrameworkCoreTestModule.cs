using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace Rex.GoodService.EntityFrameworkCore;

[DependsOn(
    typeof(GoodServiceEntityFrameworkCoreModule),
    typeof(GoodServiceTestBaseModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule)
    )]
public class GoodServiceEntityFrameworkCoreTestModule : AbpModule
{
    private NpgsqlConnection? _npgsqlConnection;

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<FeatureManagementOptions>(options =>
        {
            options.SaveStaticFeaturesToDatabase = false;
            options.IsDynamicFeatureStoreEnabled = false;
        });
        Configure<PermissionManagementOptions>(options =>
        {
            options.SaveStaticPermissionsToDatabase = false;
            options.IsDynamicPermissionStoreEnabled = false;
        });
        context.Services.AddAlwaysDisableUnitOfWorkTransaction();

        ConfigureInMemoryPostgreSql(context.Services);
    }

    private void ConfigureInMemoryPostgreSql(IServiceCollection services)
    {
        _npgsqlConnection = CreateDatabaseAndGetConnection();

        services.Configure<AbpDbContextOptions>(options =>
        {
            options.Configure(context =>
            {
                context.DbContextOptions.UseNpgsql(_npgsqlConnection);
            });
        });
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        _npgsqlConnection?.Dispose();
    }

    private static NpgsqlConnection CreateDatabaseAndGetConnection()
    {
        var connection = new NpgsqlConnection("Data Source=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<GoodServiceDbContext>()
            .UseNpgsql(connection)
            .Options;

        using (var context = new GoodServiceDbContext(options))
        {
            context.GetService<IRelationalDatabaseCreator>().CreateTables();
        }

        return connection;
    }
}