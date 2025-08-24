using Rex.OrderService.EntityFrameworkCore;
using Xunit;

namespace Rex.OrderService;

[CollectionDefinition(OrderServiceTestConsts.CollectionDefinitionName)]
public class OrderServiceDomainCollection : OrderServiceEntityFrameworkCoreCollectionFixtureBase
{

}
