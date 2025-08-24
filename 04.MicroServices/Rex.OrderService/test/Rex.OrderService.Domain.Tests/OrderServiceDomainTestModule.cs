using Rex.OrderService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Rex.OrderService;

[DependsOn(
    typeof(OrderServiceEntityFrameworkCoreTestModule)
    )]
public class OrderServiceDomainTestModule : AbpModule
{

}
