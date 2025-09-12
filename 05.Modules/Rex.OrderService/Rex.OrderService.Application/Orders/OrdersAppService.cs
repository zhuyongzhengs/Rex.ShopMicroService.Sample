using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rex.OrderService.Bills;
using Rex.Service.AspNetCore.Extensions;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Events;
using Rex.Service.Core.Helper;
using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单服务
    /// </summary>
    [RemoteService]
    [Authorize]
    public class OrdersAppService : ApplicationService, IOrdersAppService, ICapSubscribe
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderLogRepository _orderLogRepository;
        private readonly IBillAftersalesRepository _billAftersalesRepository;
        private readonly IBillAftersalesItemAppService _billAftersalesItemAppService;
        private readonly ICapPublisher _capEventBus;

        public OrdersAppService(IOrderRepository orderRepository, IOrderLogRepository orderLogRepository, IBillAftersalesRepository billAftersalesRepository, IBillAftersalesItemAppService billAftersalesItemAppService, ICapPublisher capEventBus)
        {
            _orderRepository = orderRepository;
            _orderLogRepository = orderLogRepository;
            _billAftersalesRepository = billAftersalesRepository;
            _billAftersalesItemAppService = billAftersalesItemAppService;
            _capEventBus = capEventBus;
        }

        /// <summary>
        /// 获取(分页)订单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<OrderDto>> GetListAsync(GetOrderInput input)
        {
            Expression<Func<Order, bool>> orderWhere = x => true;
            if (input.GlobalOrderStatus.HasValue) orderWhere = _orderRepository.GetGlobalOrderStatusExpression(input.GlobalOrderStatus.Value);

            // 查询数量
            long totalCount = (await _orderRepository.GetQueryableAsync())
                .Where(orderWhere)
                .WhereIf(!input.No.IsNullOrWhiteSpace(), p => p.No == input.No)
                .WhereIf(!input.PaymentCode.IsNullOrWhiteSpace(), p => p.PaymentCode == input.PaymentCode)
                .WhereIf(!input.ShipName.IsNullOrWhiteSpace(), p => p.ShipName == input.ShipName)
                .WhereIf(!input.ShipMobile.IsNullOrWhiteSpace(), p => p.ShipMobile == input.ShipMobile)
                .WhereIf(input.PayStatus.HasValue, p => p.PayStatus == input.PayStatus)
                .WhereIf(input.ShipStatus.HasValue, p => p.ShipStatus == input.ShipStatus)
                .WhereIf(input.Status.HasValue, p => p.Status == input.Status)
                .WhereIf(input.OrderType.HasValue, p => p.OrderType == input.OrderType)
                .WhereIf(input.ReceiptType.HasValue, p => p.ReceiptType == input.ReceiptType)
                .WhereIf(input.ConfirmStatus.HasValue, p => p.ConfirmStatus == input.ConfirmStatus)
                .WhereIf(input.Source.HasValue, p => p.Source == input.Source)
                .WhereIf(input.IsComment.HasValue, p => p.IsComment == input.IsComment)
                .WhereIf(input.BeginTime.HasValue, p => p.CreationTime >= input.BeginTime)
                .WhereIf(input.EndTime.HasValue, p => p.CreationTime <= input.EndTime)
                .LongCount();
            if (totalCount < 1) return new PagedResultDto<OrderDto>();

            // 查询数据列表
            List<Order> orderList = (await _orderRepository.GetQueryableAsync())
                .Where(orderWhere)
                .WhereIf(!input.No.IsNullOrWhiteSpace(), p => p.No == input.No)
                .WhereIf(!input.PaymentCode.IsNullOrWhiteSpace(), p => p.PaymentCode == input.PaymentCode)
                .WhereIf(!input.ShipName.IsNullOrWhiteSpace(), p => p.ShipName == input.ShipName)
                .WhereIf(!input.ShipMobile.IsNullOrWhiteSpace(), p => p.ShipMobile == input.ShipMobile)
                .WhereIf(input.PayStatus.HasValue, p => p.PayStatus == input.PayStatus)
                .WhereIf(input.ShipStatus.HasValue, p => p.ShipStatus == input.ShipStatus)
                .WhereIf(input.Status.HasValue, p => p.Status == input.Status)
                .WhereIf(input.OrderType.HasValue, p => p.OrderType == input.OrderType)
                .WhereIf(input.ReceiptType.HasValue, p => p.ReceiptType == input.ReceiptType)
                .WhereIf(input.ConfirmStatus.HasValue, p => p.ConfirmStatus == input.ConfirmStatus)
                .WhereIf(input.Source.HasValue, p => p.Source == input.Source)
                .WhereIf(input.IsComment.HasValue, p => p.IsComment == input.IsComment)
                .WhereIf(input.BeginTime.HasValue, p => p.CreationTime >= input.BeginTime)
                .WhereIf(input.EndTime.HasValue, p => p.CreationTime <= input.EndTime)
                .OrderByIf<Order, IQueryable<Order>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                .PageBy(input.SkipCount, input.MaxResultCount)
                .ToList();
            return new PagedResultDto<OrderDto>(
                totalCount,
                ObjectMapper.Map<List<Order>, List<OrderDto>>(orderList)
            );
        }

        /// <summary>
        /// 查询订单数量
        /// </summary>
        /// <returns></returns>
        public async Task<OrderStatusQuantityDto> GetStatusQuantityAsync()
        {
            var allTask = _orderRepository.GetQueryableAsync();

            // 并发查询各状态数量
            var pendingPaymentTask = GetStatusCountAsync((int)GlobalOrderStatusType.PendingPayment, null);
            var pendingShipmentTask = GetStatusCountAsync((int)GlobalOrderStatusType.PendingShipment, null);
            var pendingDeliveryTask = GetStatusCountAsync((int)GlobalOrderStatusType.PendingDelivery, null);
            var pendingEvaluateTask = GetStatusCountAsync((int)GlobalOrderStatusType.PendingEvaluate, null);
            var cancelledTask = GetStatusCountAsync((int)GlobalOrderStatusType.Cancel, null);
            var completedTask = GetStatusCountAsync((int)GlobalOrderStatusType.Completed, null);

            // 并发等待所有任务完成
            await Task.WhenAll(
                allTask,
                pendingPaymentTask,
                pendingShipmentTask,
                pendingDeliveryTask,
                pendingEvaluateTask,
                cancelledTask,
                completedTask
            );

            var orderStatusQuantity = new OrderStatusQuantityDto
            {
                All = allTask.Result.Count(),
                PendingPayment = pendingPaymentTask.Result,
                PendingShipment = pendingShipmentTask.Result,
                PendingDelivery = pendingDeliveryTask.Result,
                PendingEvaluate = pendingEvaluateTask.Result,
                Cancelled = cancelledTask.Result,
                Completed = completedTask.Result
            };

            return orderStatusQuantity;
        }

        /// <summary>
        /// 查询订单[状态]数量
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public async Task<int> GetStatusCountAsync(int status, Guid? userId = null)
        {
            Expression<Func<Order, bool>> orderWhere = _orderRepository.GetGlobalOrderStatusExpression(status, userId);
            return (await _orderRepository.GetQueryableAsync()).Where(orderWhere).Count();
        }

        /// <summary>
        /// 获取用户订单(分页)订单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<UserOrderDto>> GetUserOrdersAsync(GetUserOrderInput input)
        {
            // 查询数量
            long totalCount = await _orderRepository.GetPageCountAsync(input.No, input.PayStatus, input.ShipStatus, input.Status, input.OrderType, input.ReceiptType, input.ConfirmStatus, input.IsComment, CurrentUser.Id.Value);
            if (totalCount < 1)
            {
                return new PagedResultDto<UserOrderDto>();
            }
            // 查询数据列表
            List<Order> orderList = await _orderRepository.GetPageListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.No, input.PayStatus, input.ShipStatus, input.Status, input.OrderType, input.ReceiptType, input.ConfirmStatus, input.IsComment, CurrentUser.Id.Value);
            List<UserOrderDto> userOrderList = ObjectMapper.Map<List<Order>, List<UserOrderDto>>(orderList);
            foreach (var userOrder in userOrderList)
            {
                userOrder.ProductNums = userOrder.OrderItems.Sum(x => x.Nums);
            }
            return new PagedResultDto<UserOrderDto>(
                totalCount,
                userOrderList
            );
        }

        /// <summary>
        /// 查询用户订单
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <returns></returns>
        public async Task<UserOrderDto> GetUserOrderAsync(Guid id)
        {
            #region 用户订单

            Order order = await _orderRepository.GetOrderByIdAsync(id, CurrentUser.Id.Value);
            if (order == null) throw new UserFriendlyException("订单不存在，请检查！", SystemStatusCode.Fail.ToOrderServicePrefix());
            UserOrderDto userOrder = ObjectMapper.Map<Order, UserOrderDto>(order);

            #endregion 用户订单

            #region 用户退货单

            List<BillAftersalesItemDto> bAftersalesItems = await _billAftersalesItemAppService.GetOrderItemIdsAsync(new GetBillAftersalesItemToOrderItemIdInput()
            {
                OrderItemIds = userOrder.OrderItems.Select(x => x.Id).ToList()
            }, true);
            userOrder.IsAftersalesStatus = bAftersalesItems == null || !bAftersalesItems.Any();
            if (!userOrder.IsAftersalesStatus)
            {
                int bAftersalesItemCount = bAftersalesItems.Where(x => x.Aftersales?.Status != null && x.Aftersales?.Status != (int)GlobalEnums.BillAftersalesStatus.Refuse).Count();
                userOrder.IsAftersalesStatus = bAftersalesItemCount < userOrder.OrderItems.Count;

                userOrder.Aftersales = userOrder.Aftersales ?? new List<BillAftersalesDto>();
                foreach (var bAftersalesItem in bAftersalesItems)
                {
                    if (bAftersalesItem.Aftersales == null) continue;
                    if (!userOrder.Aftersales.Any(x => x.Id == bAftersalesItem.Aftersales.Id))
                    {
                        userOrder.Aftersales.Add(bAftersalesItem.Aftersales);
                    }
                }
                foreach (var oItem in userOrder.OrderItems)
                {
                    oItem.Disabled = bAftersalesItems.Any(x => x.OrderItemId == oItem.Id && x.Aftersales.Status != (int)GlobalEnums.BillAftersalesStatus.Refuse);
                }
            }

            // 支付状态“未支付”或订单状态不为“正常”则不允许发起售后申请
            if (userOrder.PayStatus == (int)OrderPayStatus.No || userOrder.Status != (int)OrderStatus.Normal)
            {
                userOrder.IsAftersalesStatus = false;
            }

            #endregion 用户退货单

            return userOrder;
        }

        /// <summary>
        /// 根据订单号获取用户订单
        /// </summary>
        /// <param name="no">订单号</param>
        /// <returns></returns>
        public async Task<OrderDto> GetUserOrderNoAsync(string no)
        {
            Order order = await _orderRepository.GetOrderByNoAsync(no, CurrentUser.Id.Value);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }

        /// <summary>
        /// 查询批量订单信息
        /// </summary>
        /// <param name="ids">订单ID</param>
        /// <returns></returns>
        public async Task<List<OrderDto>> GetManyAsync(Guid[] ids, bool includeDetails = false)
        {
            List<Order> orders = await _orderRepository.GetManyAsync(ids, includeDetails: includeDetails);
            return ObjectMapper.Map<List<Order>, List<OrderDto>>(orders);
        }

        /// <summary>
        /// 根据订单ID获取订单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public async Task<OrderDto> GetAsync(Guid id, bool includeDetails = true)
        {
            Order order = await _orderRepository.GetOrderByIdAsync(id, includeDetails: includeDetails);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="status">状态：1[订单正常]、2[订单完成]、3[订单取消]</param>
        /// <returns></returns>
        public async Task UpdateStatusAsync(Guid orderId, int status)
        {
            Order order = await _orderRepository.GetAsync(orderId);
            if (order == null)
                throw new UserFriendlyException("订单不存在，请检查订单！", SystemStatusCode.Fail.ToOrderServicePrefix());
            order.Status = status;
        }

        /// <summary>
        /// 创建订单记录
        /// </summary>
        /// <param name="input">订单记录</param>
        /// <returns></returns>
        public async Task<OrderLogDto> CreateOrderLogAsync(OrderLogCreateDto input)
        {
            OrderLog orderLog = ObjectMapper.Map<OrderLogCreateDto, OrderLog>(input);
            orderLog = await _orderLogRepository.InsertAsync(orderLog);
            return ObjectMapper.Map<OrderLog, OrderLogDto>(orderLog);
        }

        /// <summary>
        /// 批量更新订单
        /// </summary>
        /// <param name="input">订单</param>
        /// <returns></returns>
        public async Task<List<OrderDto>> UpdateOrderManyAsync(List<OrderUpdateDto> input)
        {
            //List<Order> orders = ObjectMapper.Map<List<OrderUpdateDto>, List<Order>>(input);
            List<Guid> orderIds = input.Select(x => x.Id).ToList();
            List<Order> orders = await _orderRepository.GetListAsync(x => orderIds.Contains(x.Id));
            foreach (var order in orders)
            {
                OrderUpdateDto orderUpdate = input.FirstOrDefault(x => x.Id == order.Id);
                if (orderUpdate == null) continue;
                order.GoodAmount = orderUpdate.GoodAmount;
                order.PayedAmount = orderUpdate.PayedAmount;
                order.OrderAmount = orderUpdate.OrderAmount;
                order.PayStatus = orderUpdate.PayStatus;
                order.ShipStatus = orderUpdate.ShipStatus;
                order.Status = orderUpdate.Status;
                order.OrderType = orderUpdate.OrderType;
                order.ReceiptType = orderUpdate.ReceiptType;
                order.PaymentCode = orderUpdate.PaymentCode;
                order.PaymentTime = orderUpdate.PaymentTime;
                if (orderUpdate.LogisticsId.HasValue) order.LogisticsId = orderUpdate.LogisticsId.Value;
                order.LogisticsName = orderUpdate.LogisticsName;
                order.CostFreight = orderUpdate.CostFreight;
                order.UserId = orderUpdate.UserId;
                order.SellerId = orderUpdate.SellerId;
                order.ConfirmStatus = orderUpdate.ConfirmStatus;
                order.ConfirmTime = orderUpdate.ConfirmTime;
                order.StoreId = orderUpdate.StoreId;
                order.ShipAreaId = orderUpdate.ShipAreaId;
                order.DisplayArea = orderUpdate.DisplayArea;
                order.ShipAddress = orderUpdate.ShipAddress;
                order.ShipName = orderUpdate.ShipName;
                order.ShipMobile = orderUpdate.ShipMobile;
                order.Weight = orderUpdate.Weight;
                order.Point = orderUpdate.Point;
                order.PointMoney = orderUpdate.PointMoney;
                order.OrderDiscountAmount = orderUpdate.OrderDiscountAmount;
                order.GoodsDiscountAmount = orderUpdate.GoodsDiscountAmount;
                order.CouponDiscountAmount = orderUpdate.CouponDiscountAmount;
                order.Coupon = orderUpdate.Coupon;
                order.PromotionList = orderUpdate.PromotionList;
                order.Memo = orderUpdate.Memo;
                order.Ip = orderUpdate.Ip;
                order.Mark = orderUpdate.Mark;
                order.Source = orderUpdate.Source;
                order.ObjectId = orderUpdate.ObjectId;
                order.ConcurrencyStamp = orderUpdate.ConcurrencyStamp;
            }
            await _orderRepository.UpdateManyAsync(orders);
            return ObjectMapper.Map<List<Order>, List<OrderDto>>(orders);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="ids">订单ID</param>
        /// <returns></returns>
        public async Task<bool> UpdateCancelAsync(Guid[] ids)
        {
            List<Order> orders = await _orderRepository.GetListAsync(x => ids.Contains(x.Id));
            if (orders == null || orders.Count == 0) return false;
            bool cancelResult = true;
            foreach (var order in orders)
            {
                cancelResult = await _orderRepository.CancelOrderAsync(order.UserId, [order.Id]);
                if (!cancelResult) break;
            }
            return cancelResult;
        }

        /// <summary>
        /// 用户取消订单
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="ids">订单ID</param>
        /// <returns></returns>
        public async Task<bool> UpdateUserCancelAsync(Guid userId, Guid[] ids)
        {
            return await _orderRepository.CancelOrderAsync(userId, ids, "用户取消订单操作");
        }

        /// <summary>
        /// 查询订单类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetOrderTypeAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.OrderType>());
        }

        /// <summary>
        /// 查询发货状态
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetShipStatusAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.OrderShipStatus>());
        }

        /// <summary>
        /// 查询售后状态
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetConfirmStatusAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.OrderConfirmStatus>());
        }

        /// <summary>
        /// 查询订单状态
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetStatusAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.OrderStatus>());
        }

        /// <summary>
        /// 查询订单来源
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetSourceAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.Source>());
        }

        /// <summary>
        /// 查询订单支付状态
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetPayStatusAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.OrderPayStatus>());
        }

        /// <summary>
        /// 查询订单收货方式
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetReceiptTypeAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.OrderReceiptType>());
        }

        /// <summary>
        /// 更新卖家备注
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="mark">备注</param>
        /// <returns></returns>
        public async Task UpdateMarkAsync(Guid orderId, string mark)
        {
            await _orderRepository.UpdateOrderMarkAsync(orderId, mark);
        }

        /// <summary>
        /// 更新订单收货信息
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="input">收货信息</param>
        /// <returns></returns>
        public async Task UpdateShipAsync(Guid orderId, OrderShipUpdateDto input)
        {
            await _orderRepository.UpdateOrderShipAsync(orderId, input.ShipAreaId, input.DisplayArea, input.ShipAddress, input.ShipName, input.ShipMobile);
        }

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="input">收货订单</param>
        /// <returns></returns>
        public async Task UpdateConfirmAsync(ConfirmOrderDto input)
        {
            await _orderRepository.ConfirmOrderAsync(input.OrderId, input.UserId);
        }

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public async Task UpdateCompleteAsync(Guid orderId)
        {
            BillAftersales? billAftersales = await _billAftersalesRepository.FindAsync(x => x.OrderId == orderId && x.Status == (int)BillAftersalesStatus.WaitAudit);
            if (billAftersales != null)
            {
                Order order = await _orderRepository.GetOrderByIdAsync(orderId, includeDetails: false);
                throw new UserFriendlyException($"该订单[{order.No}]存在未处理的售后，请先处理！", SystemStatusCode.Fail.ToOrderServicePrefix());
            }
            await _orderRepository.CompleteOrderAsync(orderId);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="ids">订单ID</param>
        /// <returns></returns>
        public async Task DeleteManyAsync(Guid[] ids)
        {
            await _orderRepository.DeleteManyAsync(ids);
        }

        /// <summary>
        /// 测试事件总线
        /// </summary>
        /// <param name="isException">是否模拟异常</param>
        /// <returns></returns>
        public async Task<string> TestOrderToGoodMsgAsync(string orderMsg, bool isException = false)
        {
            OrderToGoodEto otdEto = new OrderToGoodEto();
            otdEto.Id = Guid.NewGuid();
            otdEto.IsException = isException;
            otdEto.OrderMsg = $"我是订单消息：{orderMsg}";
            await _capEventBus.PublishAsync(OrderToGoodEto.EventName, otdEto, OrderToGoodEto.EventCallbackName);
            return JsonSerializer.Serialize(otdEto, AspNetCoreExtension.GetJsonSerializerOptions());
        }

        /// <summary>
        /// [回调]测试事件总线
        /// </summary>
        /// <param name="param">数据</param>
        /// <returns></returns>
        [CapSubscribe(OrderToGoodEto.EventCallbackName)]
        [NonAction]
        public async Task CallbackTestOrderToGoodMsgAsync(JsonElement param)
        {
            string jsonData = await Task.FromResult(JsonSerializer.Serialize(param, AspNetCoreExtension.GetJsonSerializerOptions()));
            //OrderToGoodEto? orderToGood = JsonSerializer.Deserialize<OrderToGoodEto>(jsonResult);
            Console.WriteLine($"商品服务已收到：{jsonData}");
        }
    }
}