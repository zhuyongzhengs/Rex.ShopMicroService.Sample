using Xunit;

namespace Rex.OrderService.EntityFrameworkCore;

[CollectionDefinition(OrderServiceTestConsts.CollectionDefinitionName)]
public class OrderServiceEntityFrameworkCoreCollection : ICollectionFixture<OrderServiceEntityFrameworkCoreFixture>
{

}
