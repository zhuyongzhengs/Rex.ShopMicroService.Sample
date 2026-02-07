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

namespace Rex.PaymentService.EntityFrameworkCore;

[DependsOn(
    typeof(PaymentServiceEntityFrameworkCoreModule),
    typeof(PaymentServiceTestBaseModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule)
    )]
public class PaymentServiceEntityFrameworkCoreTestModule : AbpModule
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

        ConfigureInMemorySqlite(context.Services);
    }

    private void ConfigureInMemorySqlite(IServiceCollection services)
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

        var options = new DbContextOptionsBuilder<PaymentServiceDbContext>()
            .UseNpgsql(connection)
            .Options;

        using (var context = new PaymentServiceDbContext(options))
        {
            context.GetService<IRelationalDatabaseCreator>().CreateTables();
        }

        return connection;
    }
}