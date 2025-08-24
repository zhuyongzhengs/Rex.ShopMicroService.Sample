using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Rex.PaymentService.Data;

/* This is used if database provider does't define
 * IPaymentServiceDbSchemaMigrator implementation.
 */
public class NullPaymentServiceDbSchemaMigrator : IPaymentServiceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
