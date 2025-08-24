using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Rex.OrderService;

[Dependency(ReplaceServices = true)]
public class OrderServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "OrderService";
}
