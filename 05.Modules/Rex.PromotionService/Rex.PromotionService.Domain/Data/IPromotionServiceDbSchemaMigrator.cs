using System.Threading.Tasks;

namespace Rex.PromotionService.Data;

public interface IPromotionServiceDbSchemaMigrator
{
    Task MigrateAsync();
}
