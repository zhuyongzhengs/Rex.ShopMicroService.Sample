using Microsoft.AspNetCore.Authorization;
using Rex.OrderService.Orders;
using Rex.Service.AspNetCore.Extensions;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 发货单服务
    /// </summary>
    [RemoteService]
    [Authorize]
    public class BillDeliveryAppService : ApplicationService, IBillDeliveryAppService
    {
        private readonly IBillDeliveryRepository _billDeliveryRepository;
        private readonly IBillDeliveryItemRepository _billDeliveryItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderLogRepository _orderLogRepository;

        public BillDeliveryAppService(IBillDeliveryRepository billDeliveryRepository, IBillDeliveryItemRepository billDeliveryItemRepository, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IOrderLogRepository orderLogRepository)
        {
            _billDeliveryRepository = billDeliveryRepository;
            _billDeliveryItemRepository = billDeliveryItemRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _orderLogRepository = orderLogRepository;
        }

        /// <summary>
        /// 获取(分页)发货单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<BillDeliveryDto>> GetListAsync(GetBillDeliveryInput input)
        {
            // 查询数量
            long totalCount = (await _billDeliveryRepository.GetQueryableAsync())
                .WhereIf(!input.No.IsNullOrWhiteSpace(), p => p.No == input.No)
                .WhereIf(!input.OrderNo.IsNullOrWhiteSpace(), p => p.Order.No.Contains(input.OrderNo))
                .WhereIf(!input.LogiNo.IsNullOrWhiteSpace(), p => p.LogiNo == input.LogiNo)
                .WhereIf(!input.ShipName.IsNullOrWhiteSpace(), p => p.ShipName == input.ShipName)
                .WhereIf(input.BeginTime.HasValue, p => p.CreationTime >= input.BeginTime)
                .WhereIf(input.EndTime.HasValue, p => p.CreationTime <= input.EndTime)
                .LongCount();
            if (totalCount < 1) return new PagedResultDto<BillDeliveryDto>();

            // 查询数据列表
            List<BillDelivery> billDeliveryList = (await _billDeliveryRepository.GetQueryableAsync())
                .WhereIf(!input.No.IsNullOrWhiteSpace(), p => p.No == input.No)
                .WhereIf(!input.OrderNo.IsNullOrWhiteSpace(), p => p.Order.No.Contains(input.OrderNo))
                .WhereIf(!input.LogiNo.IsNullOrWhiteSpace(), p => p.LogiNo == input.LogiNo)
                .WhereIf(!input.ShipName.IsNullOrWhiteSpace(), p => p.ShipName == input.ShipName)
                .WhereIf(input.BeginTime.HasValue, p => p.CreationTime >= input.BeginTime)
                .WhereIf(input.EndTime.HasValue, p => p.CreationTime <= input.EndTime)
                .OrderByIf<BillDelivery, IQueryable<BillDelivery>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                .PageBy(input.SkipCount, input.MaxResultCount)
                .ToList();

            List<Guid> orderIds = billDeliveryList.Select(x => x.OrderId).ToList();
            List<Order> orderList = await _orderRepository.GetListAsync(x => orderIds.Contains(x.Id));
            List<BillDeliveryDto> billDeliverys = ObjectMapper.Map<List<BillDelivery>, List<BillDeliveryDto>>(billDeliveryList);
            foreach (var billDelivery in billDeliverys)
            {
                if (billDelivery.Order != null) continue;
                Order order = orderList.FirstOrDefault(x => x.Id == billDelivery.OrderId);
                if (order == null) continue;
                billDelivery.Order = ObjectMapper.Map<Order, OrderDto>(order);
            }
            return new PagedResultDto<BillDeliveryDto>(
                totalCount,
                billDeliverys
            );
        }

        /// <summary>
        /// 根据[订单ID]获取发货单
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public async Task<List<BillDeliveryDto>> GetOrderIdAsync(Guid orderId)
        {
            List<BillDelivery> billDeliveries = await _billDeliveryRepository.GetListAsync(x => x.OrderId == orderId);
            return ObjectMapper.Map<List<BillDelivery>, List<BillDeliveryDto>>(billDeliveries);
        }

        /// <summary>
        /// 订单发货
        /// </summary>
        /// <param name="input">发货信息</param>
        /// <returns></returns>
        public async Task CreateOrderDeliveryAsync(OrderDeliveryDto input)
        {
            // 0.数据校验
            Order order = await _orderRepository.GetOrderByIdAsync(input.Id, includeDetails: true);
            if (order == null)
                throw new UserFriendlyException("该订单不存在或已失效，请检查！", SystemStatusCode.Fail.ToOrderServicePrefix());
            if (order.Status != (int)OrderStatus.Normal)
                throw new UserFriendlyException($"订单号：{order.No}非正常状态不能发货！", SystemStatusCode.Fail.ToOrderServicePrefix());
            if (order.PayStatus == (int)OrderPayStatus.No)
                throw new UserFriendlyException($"订单号：{order.No}未支付不能发货！", SystemStatusCode.Fail.ToOrderServicePrefix());
            if (order.ShipStatus != (int)OrderShipStatus.No && order.ShipStatus != (int)OrderShipStatus.PartialYes)
                throw new UserFriendlyException($"订单号：{order.No}不是待发货和部分发货状态不能发货！", SystemStatusCode.Fail.ToOrderServicePrefix());

            // 1.创建发货单
            BillDelivery delivery = new BillDelivery();
            delivery.OrderId = input.Id;
            delivery.No = CommonHelper.GetSerialNumberType((int)SerialNumberType.DeliveryCode);
            delivery.LogiCode = input.LogiCode;
            delivery.LogiName = input.LogiName;
            delivery.LogiNo = input.LogiNo;
            delivery.ShipAreaId = input.ShipAreaId;
            delivery.DisplayArea = input.DisplayArea;
            delivery.ShipAddress = input.ShipAddress;
            delivery.ShipName = input.ShipName;
            delivery.ShipMobile = input.ShipMobile;
            delivery.Status = (int)BillDeliveryStatus.Already;
            delivery.Memo = input.Memo;

            // 1.1创建发货单明细
            delivery.BillDeliveryItems = new List<BillDeliveryItem>();
            foreach (var orderItem in input.OrderItems)
            {
                BillDeliveryItem billDeliveryItem = new BillDeliveryItem();
                billDeliveryItem.DeliveryId = delivery.Id;
                billDeliveryItem.ProductId = orderItem.ProductId;
                billDeliveryItem.GoodId = orderItem.GoodId;
                billDeliveryItem.Bn = orderItem.Bn;
                billDeliveryItem.Sn = orderItem.Sn;
                billDeliveryItem.Weight = orderItem.Weight;
                billDeliveryItem.Name = orderItem.Name;
                billDeliveryItem.Addon = orderItem.Addon;
                billDeliveryItem.Nums = orderItem.SendNums;
                delivery.BillDeliveryItems.Add(billDeliveryItem);
            }

            // 2.保存订单发货单
            await _billDeliveryRepository.CreateOrderDeliveryAsync(delivery);

            // 3.更新订单明细发货数量
            foreach (var orderItem in input.OrderItems)
            {
                await _orderItemRepository.UpdateSendNumAsync(orderItem.Id, orderItem.SendNums);
            }

            // 4.更新订单发货状态
            await _orderRepository.UpdateShipStatusAsync(delivery.OrderId, (int)OrderShipStatus.Yes);

            // 5.保存订单记录
            OrderLog orderLog = new OrderLog();
            orderLog.OrderId = order.Id;
            orderLog.UserId = order.UserId;
            orderLog.Type = (int)OrderLogType.LogTypeShip;
            orderLog.Msg = $"订单发货操作，发货单号：{delivery.No} ！";
            orderLog.Data = JsonSerializer.Serialize(delivery, AspNetCoreExtension.GetJsonSerializerOptions());
            await _orderLogRepository.InsertAsync(orderLog);

            // 6.推送通知消息
            // TODO
        }

        /// <summary>
        /// 查询发货单信息
        /// </summary>
        /// <param name="id">发货单ID</param>
        /// <returns></returns>
        public async Task<BillDeliveryDto> GetAsync(Guid id, bool includeDetails = false)
        {
            BillDelivery billDelivery = await _billDeliveryRepository.GetBillDeliveryByIdAsync(id, includeDetails);
            if (billDelivery == null) return null;
            if (billDelivery.Order == null)
            {
                Order order = await _orderRepository.GetAsync(billDelivery.OrderId);
                if (order != null) billDelivery.Order = order;
            }
            return ObjectMapper.Map<BillDelivery, BillDeliveryDto>(billDelivery);
        }

        /// <summary>
        /// 更新发货单信息
        /// </summary>
        /// <param name="id">发货单ID</param>
        /// <param name="input">发货信息</param>
        /// <returns></returns>
        public async Task UpdateOrderDeliveryAsync(Guid id, BillDeliveryUpdateDto input)
        {
            BillDelivery billDelivery = await _billDeliveryRepository.GetAsync(id);
            if (billDelivery == null)
                throw new UserFriendlyException($"更新失败，该发货单不存在！", SystemStatusCode.Fail.ToOrderServicePrefix());
            billDelivery.LogiCode = input.LogiCode;
            billDelivery.LogiName = input.LogiName;
            billDelivery.LogiNo = input.LogiNo;
            billDelivery.ShipAreaId = input.ShipAreaId;
            billDelivery.DisplayArea = input.DisplayArea;
            billDelivery.ShipAddress = input.ShipAddress;
            billDelivery.ShipName = input.ShipName;
            billDelivery.ShipMobile = input.ShipMobile;
            billDelivery.Memo = input.Memo;
        }
    }
}