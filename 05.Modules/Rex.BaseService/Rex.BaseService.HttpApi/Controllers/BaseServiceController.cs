using Rex.BaseService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Rex.BaseService.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BaseServiceController : AbpControllerBase
{
    protected BaseServiceController()
    {
        LocalizationResource = typeof(BaseServiceResource);
    }
}
