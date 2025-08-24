using System.Threading.Tasks;

namespace Rex.AuthService.Data;

public interface IAuthServiceDbSchemaMigrator
{
    Task MigrateAsync();
}
