using System;
using System.Collections.Generic;
using System.Text;
using Rex.OrderService.Localization;
using Volo.Abp.Application.Services;

namespace Rex.OrderService;

/* Inherit your application services from this class.
 */
public abstract class OrderServiceAppService : ApplicationService
{
    protected OrderServiceAppService()
    {
        LocalizationResource = typeof(OrderServiceResource);
    }
}
