using System;
using System.Collections.Generic;
using System.Text;
using Rex.AuthService.Localization;
using Volo.Abp.Application.Services;

namespace Rex.AuthService;

/* Inherit your application services from this class.
 */
public abstract class AuthServiceAppService : ApplicationService
{
    protected AuthServiceAppService()
    {
        LocalizationResource = typeof(AuthServiceResource);
    }
}
