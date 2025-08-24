using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Rex.PaymentService;

[Dependency(ReplaceServices = true)]
public class PaymentServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "PaymentService";
}
