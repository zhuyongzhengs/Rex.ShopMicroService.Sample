using Rex.GoodService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Rex.GoodService.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class GoodServiceController : AbpControllerBase
{
    protected GoodServiceController()
    {
        LocalizationResource = typeof(GoodServiceResource);
    }
}
