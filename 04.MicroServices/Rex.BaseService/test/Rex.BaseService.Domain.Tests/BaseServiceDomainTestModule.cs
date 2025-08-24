using Rex.BaseService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Rex.BaseService;

[DependsOn(
    typeof(BaseServiceEntityFrameworkCoreTestModule)
    )]
public class BaseServiceDomainTestModule : AbpModule
{

}
