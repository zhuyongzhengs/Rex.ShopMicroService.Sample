using Rex.PaymentService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Rex.PaymentService.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class PaymentServiceController : AbpControllerBase
{
    protected PaymentServiceController()
    {
        LocalizationResource = typeof(PaymentServiceResource);
    }
}
