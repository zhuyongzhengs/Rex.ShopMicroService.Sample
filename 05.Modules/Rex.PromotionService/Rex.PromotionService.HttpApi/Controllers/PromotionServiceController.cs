using Rex.PromotionService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Rex.PromotionService.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class PromotionServiceController : AbpControllerBase
{
    protected PromotionServiceController()
    {
        LocalizationResource = typeof(PromotionServiceResource);
    }
}
