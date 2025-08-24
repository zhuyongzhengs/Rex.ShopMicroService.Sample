using System;
using System.Collections.Generic;
using System.Text;
using Rex.PromotionService.Localization;
using Volo.Abp.Application.Services;

namespace Rex.PromotionService;

/* Inherit your application services from this class.
 */
public abstract class PromotionServiceAppService : ApplicationService
{
    protected PromotionServiceAppService()
    {
        LocalizationResource = typeof(PromotionServiceResource);
    }
}
