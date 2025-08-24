using Rex.OrderService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Rex.OrderService.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class OrderServiceController : AbpControllerBase
{
    protected OrderServiceController()
    {
        LocalizationResource = typeof(OrderServiceResource);
    }
}
