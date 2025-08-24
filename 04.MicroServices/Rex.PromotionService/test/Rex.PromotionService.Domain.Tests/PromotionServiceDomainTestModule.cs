using Rex.PromotionService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Rex.PromotionService;

[DependsOn(
    typeof(PromotionServiceEntityFrameworkCoreTestModule)
    )]
public class PromotionServiceDomainTestModule : AbpModule
{

}
