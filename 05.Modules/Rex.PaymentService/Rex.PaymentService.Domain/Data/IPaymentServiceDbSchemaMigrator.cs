using System.Threading.Tasks;

namespace Rex.PaymentService.Data;

public interface IPaymentServiceDbSchemaMigrator
{
    Task MigrateAsync();
}
