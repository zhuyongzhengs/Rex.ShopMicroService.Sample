using Rex.AuthService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Rex.AuthService.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class AuthServiceController : AbpControllerBase
{
    protected AuthServiceController()
    {
        LocalizationResource = typeof(AuthServiceResource);
    }
}
