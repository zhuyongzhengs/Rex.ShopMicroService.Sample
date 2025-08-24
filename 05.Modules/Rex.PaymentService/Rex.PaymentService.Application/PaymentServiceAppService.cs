using System;
using System.Collections.Generic;
using System.Text;
using Rex.PaymentService.Localization;
using Volo.Abp.Application.Services;

namespace Rex.PaymentService;

/* Inherit your application services from this class.
 */
public abstract class PaymentServiceAppService : ApplicationService
{
    protected PaymentServiceAppService()
    {
        LocalizationResource = typeof(PaymentServiceResource);
    }
}
