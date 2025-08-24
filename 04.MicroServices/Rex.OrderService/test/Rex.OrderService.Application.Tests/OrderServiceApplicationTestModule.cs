using Volo.Abp.Modularity;

namespace Rex.OrderService;

[DependsOn(
    typeof(OrderServiceApplicationModule),
    typeof(OrderServiceDomainTestModule)
    )]
public class OrderServiceApplicationTestModule : AbpModule
{

}
