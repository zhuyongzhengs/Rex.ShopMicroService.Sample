using System.Threading.Tasks;

namespace Rex.OrderService.Data;

public interface IOrderServiceDbSchemaMigrator
{
    Task MigrateAsync();
}
