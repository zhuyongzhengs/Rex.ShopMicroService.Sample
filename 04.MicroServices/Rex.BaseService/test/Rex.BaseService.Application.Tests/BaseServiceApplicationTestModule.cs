using Volo.Abp.Modularity;

namespace Rex.BaseService;

[DependsOn(
    typeof(BaseServiceApplicationModule),
    typeof(BaseServiceDomainTestModule)
    )]
public class BaseServiceApplicationTestModule : AbpModule
{

}
