using System.Threading.Tasks;

namespace Rex.GoodService.Data;

public interface IGoodServiceDbSchemaMigrator
{
    Task MigrateAsync();
}
