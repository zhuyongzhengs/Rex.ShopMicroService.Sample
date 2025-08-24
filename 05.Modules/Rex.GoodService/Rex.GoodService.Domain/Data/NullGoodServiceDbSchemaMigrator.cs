using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Rex.GoodService.Data;

/* This is used if database provider does't define
 * IGoodServiceDbSchemaMigrator implementation.
 */
public class NullGoodServiceDbSchemaMigrator : IGoodServiceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
