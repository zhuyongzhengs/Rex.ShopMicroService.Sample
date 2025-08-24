using Rex.PaymentService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Rex.PaymentService;

[DependsOn(
    typeof(PaymentServiceEntityFrameworkCoreTestModule)
    )]
public class PaymentServiceDomainTestModule : AbpModule
{

}
