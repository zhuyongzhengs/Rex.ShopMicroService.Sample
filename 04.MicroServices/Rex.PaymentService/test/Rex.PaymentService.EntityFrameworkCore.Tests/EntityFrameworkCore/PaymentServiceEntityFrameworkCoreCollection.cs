using Xunit;

namespace Rex.PaymentService.EntityFrameworkCore;

[CollectionDefinition(PaymentServiceTestConsts.CollectionDefinitionName)]
public class PaymentServiceEntityFrameworkCoreCollection : ICollectionFixture<PaymentServiceEntityFrameworkCoreFixture>
{

}
