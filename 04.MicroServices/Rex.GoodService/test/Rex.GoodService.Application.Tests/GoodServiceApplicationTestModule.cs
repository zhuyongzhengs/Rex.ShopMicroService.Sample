using Volo.Abp.Modularity;

namespace Rex.GoodService;

[DependsOn(
    typeof(GoodServiceApplicationModule),
    typeof(GoodServiceDomainTestModule)
    )]
public class GoodServiceApplicationTestModule : AbpModule
{

}
