using System;
using System.Collections.Generic;
using System.Text;
using Rex.BaseService.Localization;
using Volo.Abp.Application.Services;

namespace Rex.BaseService;

/* Inherit your application services from this class.
 */
public abstract class BaseServiceAppService : ApplicationService
{
    protected BaseServiceAppService()
    {
        LocalizationResource = typeof(BaseServiceResource);
    }
}
