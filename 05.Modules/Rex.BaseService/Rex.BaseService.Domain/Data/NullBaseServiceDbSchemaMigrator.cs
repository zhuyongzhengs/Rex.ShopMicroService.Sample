using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Rex.BaseService.Data;

/* This is used if database provider does't define
 * IBaseServiceDbSchemaMigrator implementation.
 */
public class NullBaseServiceDbSchemaMigrator : IBaseServiceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
