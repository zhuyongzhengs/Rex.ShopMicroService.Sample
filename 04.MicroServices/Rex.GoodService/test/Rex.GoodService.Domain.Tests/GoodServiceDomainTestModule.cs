using Rex.GoodService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Rex.GoodService;

[DependsOn(
    typeof(GoodServiceEntityFrameworkCoreTestModule)
    )]
public class GoodServiceDomainTestModule : AbpModule
{

}
