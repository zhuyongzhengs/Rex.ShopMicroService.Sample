using System;
using System.Collections.Generic;
using System.Text;
using Rex.GoodService.Localization;
using Volo.Abp.Application.Services;

namespace Rex.GoodService;

/* Inherit your application services from this class.
 */
public abstract class GoodServiceAppService : ApplicationService
{
    protected GoodServiceAppService()
    {
        LocalizationResource = typeof(GoodServiceResource);
    }
}
