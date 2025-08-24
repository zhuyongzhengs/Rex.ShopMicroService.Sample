using AutoMapper;
using Rex.OrderService.Bills;
using Rex.OrderService.Carts;
using Rex.OrderService.Logisticss;
using Rex.OrderService.Orders;
using Rex.OrderService.Ships;
using Rex.Service.Core.Events.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Rex.OrderService;

public class OrderServiceApplicationAutoMapperProfile : Profile
{
    public OrderServiceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        #region Carts

        CreateMap<CartCreateDto, Cart>();
        CreateMap<CartUpdateDto, Cart>();
        CreateMap<CartDto, Cart>();
        CreateMap<Cart, CartDto>();

        #endregion Carts

        #region Orders

        CreateMap<OrderUpdateDto, Order>();
        CreateMap<OrderDto, Order>();
        CreateMap<Order, OrderDto>();
        CreateMap<Order, UserOrderDto>();
        CreateMap<OrderDto, OrderDeliveryDto>();
        CreateMap<OrderCreateEto, Order>();
        CreateMap<OrderItemCreateEto, OrderItem>();

        #endregion Orders

        #region OrderItems

        CreateMap<OrderItemUpdateDto, OrderItem>();
        CreateMap<OrderItemDto, OrderItem>();
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<OrderItem, UserOrderItemDto>();

        #endregion OrderItems

        #region OrderLogs

        CreateMap<OrderLogCreateDto, OrderLog>();
        CreateMap<OrderLogUpdateDto, OrderLog>();
        CreateMap<OrderLogDto, OrderLog>();
        CreateMap<OrderLog, OrderLogDto>();

        #endregion OrderLogs

        #region Logisticss

        CreateMap<LogisticsCreateDto, Logistics>();
        CreateMap<LogisticsUpdateDto, Logistics>();
        CreateMap<LogisticsDto, Logistics>();
        CreateMap<Logistics, LogisticsDto>();

        #endregion Logisticss

        #region Ships

        CreateMap<ShipCreateDto, Ship>()
            .AfterMap((scd, sp) =>
            {
                sp.AreaFee = scd.AreaFees.Any() ? JsonSerializer.Serialize(scd.AreaFees) : null;
            });

        CreateMap<ShipUpdateDto, Ship>()
            .AfterMap((sud, sp) =>
            {
                sp.AreaFee = sud.AreaFees.Any() ? JsonSerializer.Serialize(sud.AreaFees) : null;
            });
        CreateMap<ShipDto, Ship>()
            .AfterMap((sd, sp) =>
             {
                 sp.AreaFee = sd.AreaFees.Any() ? JsonSerializer.Serialize(sd.AreaFees) : null;
             });
        CreateMap<Ship, ShipDto>()
            .AfterMap((sp, sd) =>
            {
                sd.AreaFees = sp.AreaFee.IsNullOrWhiteSpace() ? new List<AreaFeeDto>() : JsonSerializer.Deserialize<List<AreaFeeDto>>(sp.AreaFee);
            });

        #endregion Ships

        #region BillAftersales

        CreateMap<BillAftersalesCreateDto, BillAftersales>();
        CreateMap<BillAftersalesUpdateDto, BillAftersales>();
        CreateMap<BillAftersalesDto, BillAftersales>();
        CreateMap<BillAftersales, BillAftersalesDto>();

        #endregion BillAftersales

        #region BillAftersalesImages

        CreateMap<BillAftersalesImagesCreateDto, BillAftersalesImages>();
        CreateMap<BillAftersalesImagesUpdateDto, BillAftersalesImages>();
        CreateMap<BillAftersalesImagesDto, BillAftersalesImages>();
        CreateMap<BillAftersalesImages, BillAftersalesImagesDto>();

        #endregion BillAftersalesImages

        #region BillAftersalesItem

        CreateMap<BillAftersalesItemCreateDto, BillAftersalesItem>();
        CreateMap<BillAftersalesItemUpdateDto, BillAftersalesItem>();
        CreateMap<BillAftersalesItemDto, BillAftersalesItem>();
        CreateMap<BillAftersalesItem, BillAftersalesItemDto>();

        #endregion BillAftersalesItem

        #region BillDeliverys

        CreateMap<BillDeliveryCreateDto, BillDelivery>();
        CreateMap<BillDeliveryUpdateDto, BillDelivery>();
        CreateMap<BillDeliveryDto, BillDelivery>();
        CreateMap<BillDelivery, BillDeliveryDto>();
        CreateMap<BillDeliveryItem, BillDeliveryItemDto>();

        #endregion BillDeliverys

        #region BillReships

        CreateMap<BillReshipDto, BillReship>();
        CreateMap<BillReship, BillReshipDto>();
        CreateMap<BillReshipItem, BillReshipItemDto>();

        #endregion BillReships
    }
}