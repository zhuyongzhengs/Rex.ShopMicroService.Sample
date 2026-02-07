using Rex.OrderService.Bills;
using Rex.OrderService.Carts;
using Rex.OrderService.Logisticss;
using Rex.OrderService.Orders;
using Rex.OrderService.Ships;
using Rex.Service.Core.Events.Orders;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Volo.Abp.Mapperly;

namespace Rex.OrderService;

#region Carts

[Mapper]
public partial class CartCreateDtoToCartMapper : MapperBase<CartCreateDto, Cart>
{
    public override partial Cart Map(CartCreateDto source);

    public override partial void Map(CartCreateDto source, Cart destination);
}

[Mapper]
public partial class CartUpdateDtoToCartMapper : MapperBase<CartUpdateDto, Cart>
{
    public override partial Cart Map(CartUpdateDto source);

    public override partial void Map(CartUpdateDto source, Cart destination);
}

[Mapper]
public partial class CartDtoToCartMapper : MapperBase<CartDto, Cart>
{
    public override partial Cart Map(CartDto source);

    public override partial void Map(CartDto source, Cart destination);
}

[Mapper]
public partial class CartToCartDtoMapper : MapperBase<Cart, CartDto>
{
    public override partial CartDto Map(Cart source);

    public override partial void Map(Cart source, CartDto destination);
}

#endregion Carts

#region Orders

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class OrderUpdateDtoToOrderMapper : MapperBase<OrderUpdateDto, Order>
{
    public override partial Order Map(OrderUpdateDto source);

    public override partial void Map(OrderUpdateDto source, Order destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class OrderDtoToOrderMapper : MapperBase<OrderDto, Order>
{
    public override partial Order Map(OrderDto source);

    public override partial void Map(OrderDto source, Order destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class OrderToOrderDtoMapper : MapperBase<Order, OrderDto>
{
    public override partial OrderDto Map(Order source);

    public override partial void Map(Order source, OrderDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class OrderToUserOrderDtoMapper : MapperBase<Order, UserOrderDto>
{
    public override partial UserOrderDto Map(Order source);

    public override partial void Map(Order source, UserOrderDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class OrderDtoToOrderDeliveryDtoMapper : MapperBase<OrderDto, OrderDeliveryDto>
{
    public override partial OrderDeliveryDto Map(OrderDto source);

    public override partial void Map(OrderDto source, OrderDeliveryDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class OrderCreateEtoToOrderMapper : MapperBase<OrderCreateEto, Order>
{
    public override partial Order Map(OrderCreateEto source);

    public override partial void Map(OrderCreateEto source, Order destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class OrderItemCreateEtoToOrderItemMapper : MapperBase<OrderItemCreateEto, OrderItem>
{
    public override partial OrderItem Map(OrderItemCreateEto source);

    public override partial void Map(OrderItemCreateEto source, OrderItem destination);
}

#endregion Orders

#region OrderItems

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class OrderItemUpdateDtoToOrderItemMapper : MapperBase<OrderItemUpdateDto, OrderItem>
{
    public override partial OrderItem Map(OrderItemUpdateDto source);

    public override partial void Map(OrderItemUpdateDto source, OrderItem destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class OrderItemDtoToOrderItemMapper : MapperBase<OrderItemDto, OrderItem>
{
    public override partial OrderItem Map(OrderItemDto source);

    public override partial void Map(OrderItemDto source, OrderItem destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class OrderItemToOrderItemDtoMapper : MapperBase<OrderItem, OrderItemDto>
{
    public override partial OrderItemDto Map(OrderItem source);

    public override partial void Map(OrderItem source, OrderItemDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class OrderItemToUserOrderItemDtoMapper : MapperBase<OrderItem, UserOrderItemDto>
{
    public override partial UserOrderItemDto Map(OrderItem source);

    public override partial void Map(OrderItem source, UserOrderItemDto destination);
}

#endregion OrderItems

#region OrderLogs

[Mapper]
public partial class OrderLogCreateDtoToOrderLogMapper : MapperBase<OrderLogCreateDto, OrderLog>
{
    public override partial OrderLog Map(OrderLogCreateDto source);

    public override partial void Map(OrderLogCreateDto source, OrderLog destination);
}

[Mapper]
public partial class OrderLogUpdateDtoToOrderLogMapper : MapperBase<OrderLogUpdateDto, OrderLog>
{
    public override partial OrderLog Map(OrderLogUpdateDto source);

    public override partial void Map(OrderLogUpdateDto source, OrderLog destination);
}

[Mapper]
public partial class OrderLogDtoToOrderLogMapper : MapperBase<OrderLogDto, OrderLog>
{
    public override partial OrderLog Map(OrderLogDto source);

    public override partial void Map(OrderLogDto source, OrderLog destination);
}

[Mapper]
public partial class OrderLogToOrderLogDtoMapper : MapperBase<OrderLog, OrderLogDto>
{
    public override partial OrderLogDto Map(OrderLog source);

    public override partial void Map(OrderLog source, OrderLogDto destination);
}

#endregion OrderLogs

#region Logisticss

[Mapper]
public partial class LogisticsCreateDtoToLogisticsMapper : MapperBase<LogisticsCreateDto, Logistics>
{
    public override partial Logistics Map(LogisticsCreateDto source);

    public override partial void Map(LogisticsCreateDto source, Logistics destination);
}

[Mapper]
public partial class LogisticsUpdateDtoToLogisticsMapper : MapperBase<LogisticsUpdateDto, Logistics>
{
    public override partial Logistics Map(LogisticsUpdateDto source);

    public override partial void Map(LogisticsUpdateDto source, Logistics destination);
}

[Mapper]
public partial class LogisticsDtoToLogisticsMapper : MapperBase<LogisticsDto, Logistics>
{
    public override partial Logistics Map(LogisticsDto source);

    public override partial void Map(LogisticsDto source, Logistics destination);
}

[Mapper]
public partial class LogisticsToLogisticsDtoMapper : MapperBase<Logistics, LogisticsDto>
{
    public override partial LogisticsDto Map(Logistics source);

    public override partial void Map(Logistics source, LogisticsDto destination);
}

#endregion Logisticss

#region Ships

[Mapper]
public partial class ShipCreateDtoToShipMapper : MapperBase<ShipCreateDto, Ship>
{
    public override partial Ship Map(ShipCreateDto source);

    public override partial void Map(ShipCreateDto source, Ship destination);

    public override void AfterMap(ShipCreateDto source, Ship destination)
    {
        destination.AreaFee = MapAreaFee(source.AreaFees);
    }

    private string? MapAreaFee(List<AreaFeeDto> areaFees) =>
        areaFees != null && areaFees?.Any() == true ? JsonSerializer.Serialize(areaFees) : null;
}

[Mapper]
public partial class ShipUpdateDtoToShipMapper : MapperBase<ShipUpdateDto, Ship>
{
    public override partial Ship Map(ShipUpdateDto source);

    public override partial void Map(ShipUpdateDto source, Ship destination);

    public override void AfterMap(ShipUpdateDto source, Ship destination)
    {
        destination.AreaFee = MapAreaFee(source.AreaFees);
    }

    private string? MapAreaFee(List<AreaFeeDto> areaFees) =>
        areaFees != null && areaFees?.Any() == true ? JsonSerializer.Serialize(areaFees) : null;
}

[Mapper]
public partial class ShipDtoToShipMapper : MapperBase<ShipDto, Ship>
{
    public override partial Ship Map(ShipDto source);

    public override partial void Map(ShipDto source, Ship destination);

    public override void AfterMap(ShipDto source, Ship destination)
    {
        destination.AreaFee = MapAreaFee(source.AreaFees);
    }

    private string? MapAreaFee(List<AreaFeeDto> areaFees) =>
        areaFees != null && areaFees?.Any() == true ? JsonSerializer.Serialize(areaFees) : null;
}

[Mapper]
public partial class ShipToShipDtoMapper : MapperBase<Ship, ShipDto>
{
    public override partial ShipDto Map(Ship source);

    public override partial void Map(Ship source, ShipDto destination);

    public override void AfterMap(Ship source, ShipDto destination)
    {
        destination.AreaFees = MapAreaFees(source.AreaFee);
    }

    private List<AreaFeeDto> MapAreaFees(string? areaFeeJson) =>
        areaFeeJson.IsNullOrWhiteSpace()
            ? new List<AreaFeeDto>()
            : JsonSerializer.Deserialize<List<AreaFeeDto>>(areaFeeJson) ?? new List<AreaFeeDto>();
}

#endregion Ships

#region BillAftersales

[Mapper]
public partial class BillAftersalesCreateDtoToBillAftersalesMapper : MapperBase<BillAftersalesCreateDto, BillAftersales>
{
    public override partial BillAftersales Map(BillAftersalesCreateDto source);

    public override partial void Map(BillAftersalesCreateDto source, BillAftersales destination);
}

[Mapper]
public partial class BillAftersalesUpdateDtoToBillAftersalesMapper : MapperBase<BillAftersalesUpdateDto, BillAftersales>
{
    public override partial BillAftersales Map(BillAftersalesUpdateDto source);

    public override partial void Map(BillAftersalesUpdateDto source, BillAftersales destination);
}

[Mapper]
public partial class BillAftersalesDtoToBillAftersalesMapper : MapperBase<BillAftersalesDto, BillAftersales>
{
    public override partial BillAftersales Map(BillAftersalesDto source);

    public override partial void Map(BillAftersalesDto source, BillAftersales destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class BillAftersalesToBillAftersalesDtoMapper : MapperBase<BillAftersales, BillAftersalesDto>
{
    public override partial BillAftersalesDto Map(BillAftersales source);

    public override partial void Map(BillAftersales source, BillAftersalesDto destination);
}

#endregion BillAftersales

#region BillAftersalesImages

[Mapper]
public partial class BillAftersalesImagesCreateDtoToBillAftersalesImagesMapper : MapperBase<BillAftersalesImagesCreateDto, BillAftersalesImages>
{
    public override partial BillAftersalesImages Map(BillAftersalesImagesCreateDto source);

    public override partial void Map(BillAftersalesImagesCreateDto source, BillAftersalesImages destination);
}

[Mapper]
public partial class BillAftersalesImagesUpdateDtoToBillAftersalesImagesMapper : MapperBase<BillAftersalesImagesUpdateDto, BillAftersalesImages>
{
    public override partial BillAftersalesImages Map(BillAftersalesImagesUpdateDto source);

    public override partial void Map(BillAftersalesImagesUpdateDto source, BillAftersalesImages destination);
}

[Mapper]
public partial class BillAftersalesImagesDtoToBillAftersalesImagesMapper : MapperBase<BillAftersalesImagesDto, BillAftersalesImages>
{
    public override partial BillAftersalesImages Map(BillAftersalesImagesDto source);

    public override partial void Map(BillAftersalesImagesDto source, BillAftersalesImages destination);
}

[Mapper]
public partial class BillAftersalesImagesToBillAftersalesImagesDtoMapper : MapperBase<BillAftersalesImages, BillAftersalesImagesDto>
{
    public override partial BillAftersalesImagesDto Map(BillAftersalesImages source);

    public override partial void Map(BillAftersalesImages source, BillAftersalesImagesDto destination);
}

#endregion BillAftersalesImages

#region BillAftersalesItem

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class BillAftersalesItemCreateDtoToBillAftersalesItemMapper : MapperBase<BillAftersalesItemCreateDto, BillAftersalesItem>
{
    public override partial BillAftersalesItem Map(BillAftersalesItemCreateDto source);

    public override partial void Map(BillAftersalesItemCreateDto source, BillAftersalesItem destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class BillAftersalesItemUpdateDtoToBillAftersalesItemMapper : MapperBase<BillAftersalesItemUpdateDto, BillAftersalesItem>
{
    public override partial BillAftersalesItem Map(BillAftersalesItemUpdateDto source);

    public override partial void Map(BillAftersalesItemUpdateDto source, BillAftersalesItem destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class BillAftersalesItemDtoToBillAftersalesItemMapper : MapperBase<BillAftersalesItemDto, BillAftersalesItem>
{
    public override partial BillAftersalesItem Map(BillAftersalesItemDto source);

    public override partial void Map(BillAftersalesItemDto source, BillAftersalesItem destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class BillAftersalesItemToBillAftersalesItemDtoMapper : MapperBase<BillAftersalesItem, BillAftersalesItemDto>
{
    public override partial BillAftersalesItemDto Map(BillAftersalesItem source);

    public override partial void Map(BillAftersalesItem source, BillAftersalesItemDto destination);
}

#endregion BillAftersalesItem

#region BillDeliverys

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class BillDeliveryCreateDtoToBillDeliveryMapper : MapperBase<BillDeliveryCreateDto, BillDelivery>
{
    public override partial BillDelivery Map(BillDeliveryCreateDto source);

    public override partial void Map(BillDeliveryCreateDto source, BillDelivery destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class BillDeliveryUpdateDtoToBillDeliveryMapper : MapperBase<BillDeliveryUpdateDto, BillDelivery>
{
    public override partial BillDelivery Map(BillDeliveryUpdateDto source);

    public override partial void Map(BillDeliveryUpdateDto source, BillDelivery destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class BillDeliveryDtoToBillDeliveryMapper : MapperBase<BillDeliveryDto, BillDelivery>
{
    public override partial BillDelivery Map(BillDeliveryDto source);

    public override partial void Map(BillDeliveryDto source, BillDelivery destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class BillDeliveryToBillDeliveryDtoMapper : MapperBase<BillDelivery, BillDeliveryDto>
{
    public override partial BillDeliveryDto Map(BillDelivery source);

    public override partial void Map(BillDelivery source, BillDeliveryDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class BillDeliveryItemToBillDeliveryItemDtoMapper : MapperBase<BillDeliveryItem, BillDeliveryItemDto>
{
    public override partial BillDeliveryItemDto Map(BillDeliveryItem source);

    public override partial void Map(BillDeliveryItem source, BillDeliveryItemDto destination);
}

#endregion BillDeliverys

#region BillReships

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class BillReshipDtoToBillReshipMapper : MapperBase<BillReshipDto, BillReship>
{
    public override partial BillReship Map(BillReshipDto source);

    public override partial void Map(BillReshipDto source, BillReship destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class BillReshipToBillReshipDtoMapper : MapperBase<BillReship, BillReshipDto>
{
    public override partial BillReshipDto Map(BillReship source);

    public override partial void Map(BillReship source, BillReshipDto destination);
}

// 单向
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class BillReshipItemToBillReshipItemDtoMapper : MapperBase<BillReshipItem, BillReshipItemDto>
{
    public override partial BillReshipItemDto Map(BillReshipItem source);

    public override partial void Map(BillReshipItem source, BillReshipItemDto destination);
}

#endregion BillReships