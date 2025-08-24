using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Rex.PromotionService.Data;

/* This is used if database provider does't define
 * IPromotionServiceDbSchemaMigrator implementation.
 */
public class NullPromotionServiceDbSchemaMigrator : IPromotionServiceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
