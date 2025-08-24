using Rex.PaymentService.EntityFrameworkCore;
using Xunit;

namespace Rex.PaymentService;

[CollectionDefinition(PaymentServiceTestConsts.CollectionDefinitionName)]
public class PaymentServiceApplicationCollection : PaymentServiceEntityFrameworkCoreCollectionFixtureBase
{

}
