using Volo.Abp.Modularity;

namespace Rex.PromotionService;

[DependsOn(
    typeof(PromotionServiceApplicationModule),
    typeof(PromotionServiceDomainTestModule)
    )]
public class PromotionServiceApplicationTestModule : AbpModule
{

}
