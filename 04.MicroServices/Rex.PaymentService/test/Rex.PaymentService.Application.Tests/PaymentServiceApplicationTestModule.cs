using Volo.Abp.Modularity;

namespace Rex.PaymentService;

[DependsOn(
    typeof(PaymentServiceApplicationModule),
    typeof(PaymentServiceDomainTestModule)
    )]
public class PaymentServiceApplicationTestModule : AbpModule
{

}
