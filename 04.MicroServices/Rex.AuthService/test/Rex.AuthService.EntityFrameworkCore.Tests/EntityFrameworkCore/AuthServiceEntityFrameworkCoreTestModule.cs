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

namespace Rex.AuthService.EntityFrameworkCore;

[DependsOn(
    typeof(AuthServiceEntityFrameworkCoreModule),
    typeof(AuthServiceTestBaseModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule)
    )]
public class AuthServiceEntityFrameworkCoreTestModule : AbpModule
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

        var options = new DbContextOptionsBuilder<AuthServiceDbContext>()
            .UseNpgsql(connection)
            .Options;

        using (var context = new AuthServiceDbContext(options))
        {
            context.GetService<IRelationalDatabaseCreator>().CreateTables();
        }

        return connection;
    }
}