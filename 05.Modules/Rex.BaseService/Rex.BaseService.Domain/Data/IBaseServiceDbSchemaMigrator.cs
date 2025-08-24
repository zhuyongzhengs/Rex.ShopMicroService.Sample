using System.Threading.Tasks;

namespace Rex.BaseService.Data;

public interface IBaseServiceDbSchemaMigrator
{
    Task MigrateAsync();
}
