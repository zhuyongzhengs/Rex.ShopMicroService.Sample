using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using Rex.OrderService.Bills;
using Rex.OrderService.Carts;
using Rex.OrderService.EntityFrameworkCore;
using Rex.OrderService.Statistics;
using Rex.Service.AspNetCore.Extensions;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Events.Bases;
using Rex.Service.Core.Events.Goods;
using Rex.Service.Core.Events.Orders;
using Rex.Service.Core.Events.Promotions;
using Rex.Service.Setting.BaseServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Settings;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单仓储
    /// </summary>
    public class OrderRepository : EfCoreRepository<OrderServiceDbContext, Order, Guid>, IOrderRepository
    {
        public OrderServiceDbContext oServiceDbContext { get; set; }
        public ISettingProvider SettingProvider { get; set; }
        private readonly IOrderLogRepository _orderLogRepository;
        private readonly IBillAftersalesRepository _billAftersalesRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICapPublisher _capEventBus;

        public OrderRepository(IDbContextProvider<OrderServiceDbContext> dbContextProvider, IOrderLogRepository orderLogRepository, IBillAftersalesRepository billAftersalesRepository, ICartRepository cartRepository, ICapPublisher capEventBus) : base(dbContextProvider)
        {
            _orderLogRepository = orderLogRepository;
            _billAftersalesRepository = billAftersalesRepository;
            _cartRepository = cartRepository;
            _capEventBus = capEventBus;
        }

        /// <summary>
        /// 获取(分页) 订单列表总数
        /// </summary>
        /// <param name="no">订单号</param>
        /// <param name="payStatus">支付状态</param>
        /// <param name="shipStatus">发货状态</param>
        /// <param name="status">订单状态</param>
        /// <param name="orderType">订单类型</param>
        /// <param name="receiptType">收货方式</param>
        /// <param name="confirmStatus">售后状态</param>
        /// <param name="isComment">是否评论</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public async Task<long> GetPageCountAsync(string no, int? payStatus, int? shipStatus, int? status, int? orderType, int? receiptType, int? confirmStatus, bool? isComment, Guid? userId = null, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!no.IsNullOrWhiteSpace(), p => p.No == no)
                .WhereIf(payStatus.HasValue, p => p.PayStatus == payStatus)
                .WhereIf(shipStatus.HasValue, p => p.ShipStatus == shipStatus)
                .WhereIf(status.HasValue, p => p.Status == status)
                .WhereIf(orderType.HasValue, p => p.OrderType == orderType)
                .WhereIf(receiptType.HasValue, p => p.ReceiptType == receiptType)
                .WhereIf(confirmStatus.HasValue, p => p.ConfirmStatus == confirmStatus)
                .WhereIf(isComment.HasValue, p => p.IsComment == isComment)
                .WhereIf(userId.HasValue, p => p.UserId == userId)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取(分页) 订单列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="no">订单号</param>
        /// <param name="payStatus">支付状态</param>
        /// <param name="shipStatus">发货状态</param>
        /// <param name="status">订单状态</param>
        /// <param name="orderType">订单类型</param>
        /// <param name="receiptType">收货方式</param>
        /// <param name="confirmStatus">售后状态</param>
        /// <param name="isComment">是否评论</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public async Task<List<Order>> GetPageListAsync(int skipCount, int maxResultCount, string sorting, string no, int? payStatus, int? shipStatus, int? status, int? orderType, int? receiptType, int? confirmStatus, bool? isComment, Guid? userId = null, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!no.IsNullOrWhiteSpace(), p => p.No == no)
                .WhereIf(payStatus.HasValue, p => p.PayStatus == payStatus)
                .WhereIf(shipStatus.HasValue, p => p.ShipStatus == shipStatus)
                .WhereIf(status.HasValue, p => p.Status == status)
                .WhereIf(orderType.HasValue, p => p.OrderType == orderType)
                .WhereIf(receiptType.HasValue, p => p.ReceiptType == receiptType)
                .WhereIf(confirmStatus.HasValue, p => p.ConfirmStatus == confirmStatus)
                .WhereIf(isComment.HasValue, p => p.IsComment == isComment)
                .WhereIf(userId.HasValue, p => p.UserId == userId)
                .Include(p => p.OrderItems)
                .OrderByIf<Order, IQueryable<Order>>(!sorting.IsNullOrWhiteSpace(), sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 根据订单号获取订单
        /// </summary>
        /// <param name="no">订单号</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public async Task<Order> GetOrderByNoAsync(string no, Guid? userId = null, bool includeDetails = true)
        {
            return await (await GetDbSetAsync())
                .Where(p => p.No == no)
                .WhereIf(userId.HasValue, p => p.UserId == userId)
                .IncludeIf(includeDetails, p => p.OrderItems).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 根据订单ID获取订单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public async Task<Order> GetOrderByIdAsync(Guid id, Guid? userId = null, bool includeDetails = true)
        {
            return await (await GetDbSetAsync())
                .Where(p => p.Id == id)
                .WhereIf(userId.HasValue, p => p.UserId == userId)
                .IncludeIf(includeDetails, p => p.OrderItems).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <param name="orderIds">订单ID</param>
        /// <param name="status">订单状态</param>
        /// <param name="payStatus">支付状态</param>
        /// <returns></returns>
        public async Task<List<Order>> GetOrderStatusAsync(Guid[] orderIds, int? status = null, int? payStatus = null)
        {
            List<Order> orders = await (await GetDbSetAsync())
               .WhereIf(orderIds != null, x => orderIds.Contains(x.Id))
               .WhereIf(status.HasValue, x => x.Status == status)
               .WhereIf(payStatus.HasValue, x => x.PayStatus == payStatus)
               .ToListAsync();
            return orders;
        }

        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="status">订单状态</param>
        /// <param name="payStatus">支付状态</param>
        /// <returns></returns>
        public async Task<Order> GetOrderStatusAsync(Guid orderId, int? status = null, int? payStatus = null)
        {
            Order order = await (await GetDbSetAsync())
               .Where(x => x.Id == orderId)
               .WhereIf(status.HasValue, x => x.Status == status)
               .WhereIf(payStatus.HasValue, x => x.PayStatus == payStatus)
               .FirstOrDefaultAsync();
            return order;
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="ids">订单ID</param>
        /// <returns></returns>
        public async Task<bool> CancelOrderAsync(Guid userId, Guid[] ids, string cancelMsg = "订单取消操作")
        {
            List<Order> orders = await (await GetDbSetAsync())
               .Where(x => ids.Contains(x.Id) && x.UserId == userId && x.PayStatus == (int)OrderPayStatus.No && x.Status == (int)OrderStatus.Normal && x.ShipStatus == (int)OrderShipStatus.No)
               .Include(x => x.OrderItems)
               //.AsNoTracking()
               .ToListAsync();
            if (orders == null || !orders.Any()) return false;

            /*
            bool cancelOrderResult = false;
            List<Guid> orderIds = orders.Select(x => x.Id).ToList();

            #region 订单状态修改

            int rowsUpdated = await (await GetDbSetAsync()).Where(x => orderIds.Contains(x.Id)).ExecuteUpdateAsync(x => x.SetProperty(p => p.Status, (int)OrderStatus.Cancel));
            cancelOrderResult = rowsUpdated > 0;

            #endregion 订单状态修改

            if (!cancelOrderResult) return false;
            */

            foreach (var order in orders)
            {
                order.Status = (int)OrderStatus.Cancel;

                #region 订单记录

                OrderLog orderLog = new OrderLog();
                orderLog.OrderId = order.Id;
                orderLog.UserId = userId;
                orderLog.Type = (int)OrderLogType.LogTypeCancel;
                orderLog.Msg = cancelMsg;
                orderLog.Data = JsonSerializer.Serialize(order, AspNetCoreExtension.GetJsonSerializerOptions());
                await _orderLogRepository.InsertAsync(orderLog);

                #endregion 订单记录

                // 积分返还
                if (order.Point > 0)
                {
                    await _capEventBus.PublishAsync(UserChangePointEto.EventName, new UserChangePointEto()
                    {
                        UserId = order.UserId,
                        PointType = (int)UserPointSourceTypes.PointCanCelOrder,
                        Point = order.Point,
                        Remark = $"取消订单【{order.No}】积分返还：+{order.Point}。"
                    });
                }

                // 优惠券返还
                if (!order.Coupon.IsNullOrWhiteSpace())
                {
                    string[] couponCodes = JsonSerializer.Deserialize<string[]>(order.Coupon);
                    await _capEventBus.PublishAsync(UsedCouponEto.EventName, new UsedCouponEto()
                    {
                        CouponCode = couponCodes,
                        UsedId = null
                    });
                }

                #region 更改库存

                foreach (var orderItem in order.OrderItems)
                {
                    await _capEventBus.PublishAsync(ChangeStockEto.EventName, new ChangeStockEto()
                    {
                        ProductId = orderItem.ProductId,
                        ChangeStockType = (int)OrderChangeStockType.Cancel,
                        Nums = orderItem.Nums
                    });
                }

                // Redis库存回滚
                await _capEventBus.PublishAsync(PlaceOrderCallbackEto.EventName, new PlaceOrderCallbackEto()
                {
                    Id = order.Id,
                    Explain = "订单取消，(Redis)库存回滚。"
                });

                #endregion 更改库存
            }
            return true;
        }

        /// <summary>
        /// 更新卖家备注
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="mark">备注</param>
        /// <returns></returns>
        public async Task UpdateOrderMarkAsync(Guid orderId, string mark)
        {
            await (await GetDbSetAsync()).Where(x => x.Id == orderId).ExecuteUpdateAsync(x => x.SetProperty(p => p.Mark, mark));
        }

        /// <summary>
        /// 更新订单收货信息
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="shipAreaId">收货地区ID</param>
        /// <param name="displayArea">显示区域信息</param>
        /// <param name="shipAddress">收货详细地址</param>
        /// <param name="shipName">收货人姓名</param>
        /// <param name="shipMobile">收货电话</param>
        /// <returns></returns>
        public async Task UpdateOrderShipAsync(Guid orderId, long shipAreaId, string? displayArea, string shipAddress, string shipName, string shipMobile)
        {
            await (await GetDbSetAsync())
                .Where(x => x.Id == orderId)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.ShipAreaId, shipAreaId)
                    .SetProperty(p => p.DisplayArea, displayArea)
                    .SetProperty(p => p.ShipAddress, shipAddress)
                    .SetProperty(p => p.ShipName, shipName)
                    .SetProperty(p => p.ShipMobile, shipMobile)
                );
        }

        /// <summary>
        /// 更新发货状态
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="shipStatus">1：未发货、2：部分发货、3：已发货、4：部分退货、5：已退货</param>
        /// <returns></returns>
        public async Task UpdateShipStatusAsync(Guid orderId, int shipStatus)
        {
            await (await GetDbSetAsync())
                .Where(x => x.Id == orderId)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.ShipStatus, shipStatus)
                );
        }

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public async Task ConfirmOrderAsync(Guid orderId, Guid? userId = null, string remark = "")
        {
            Order? order = await (await GetDbSetAsync()).Where(x => x.Id == orderId)
                .WhereIf(userId.HasValue, x => x.UserId == userId)
                .FirstOrDefaultAsync();

            #region 校验

            if (order == null)
                throw new UserFriendlyException($"订单不存在或已被删除，请检查！", SystemStatusCode.Fail.ToOrderServicePrefix());

            if (order.PayStatus != (int)OrderPayStatus.Yes)
                throw new UserFriendlyException($"该订单[{order.No}]未付款，不能确认收货！", SystemStatusCode.Fail.ToOrderServicePrefix());

            if (order.ShipStatus != (int)OrderShipStatus.Yes)
                throw new UserFriendlyException($"该订单[{order.No}]未发货，不能确认收货！", SystemStatusCode.Fail.ToOrderServicePrefix());

            if (order.Status != (int)OrderStatus.Normal)
                throw new UserFriendlyException($"该订单[{order.No}]为：{GlobalEnums.GetDescription<OrderStatus>(order.Status)}，不能确认收货！", SystemStatusCode.Fail.ToOrderServicePrefix());

            if (order.ConfirmStatus != (int)OrderConfirmStatus.ReceiptNotConfirmed)
                throw new UserFriendlyException($"该订单[{order.No}]{GlobalEnums.GetDescription<OrderConfirmStatus>(order.ConfirmStatus)}，不能再次确认收货！", SystemStatusCode.Fail.ToOrderServicePrefix());

            #endregion 校验

            // 修改订单状态
            /*
            await (await GetDbSetAsync())
                .Where(x => x.Id == orderId)
                .ExecuteUpdateAsync(x => x.SetProperty(s => s.ConfirmStatus, (int)OrderConfirmStatus.ConfirmReceipt));
            */
            order.ConfirmStatus = (int)OrderConfirmStatus.ConfirmReceipt;

            // 保存订单记录
            OrderLog orderLog = new OrderLog();
            orderLog.OrderId = orderId;
            orderLog.UserId = order.UserId;
            orderLog.Type = (int)OrderLogType.LogTypeSign;
            orderLog.Msg = remark.IsNullOrWhiteSpace() ? $"订单 {order.No} 确认收货成功！" : remark;
            orderLog.Data = JsonSerializer.Serialize(new { OrderId = orderId, UserId = userId }, AspNetCoreExtension.GetJsonSerializerOptions());
            await _orderLogRepository.InsertAsync(orderLog);
        }

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public async Task CompleteOrderAsync(Guid orderId, string remark = "后台订单完成操作！")
        {
            Order? order = await (await GetDbSetAsync()).Where(x => x.Id == orderId).FirstOrDefaultAsync();

            #region 校验

            if (order == null)
                throw new UserFriendlyException($"订单不存在或已被删除，请检查！", SystemStatusCode.Fail.ToOrderServicePrefix());

            if (order.PayStatus != (int)OrderPayStatus.Yes)
                throw new UserFriendlyException($"该订单[{order.No}]未付款，不能完成订单！", SystemStatusCode.Fail.ToOrderServicePrefix());

            if (order.ShipStatus != (int)OrderShipStatus.Yes)
                throw new UserFriendlyException($"该订单[{order.No}]未发货，不能完成订单！", SystemStatusCode.Fail.ToOrderServicePrefix());

            if (order.Status != (int)OrderStatus.Normal)
                throw new UserFriendlyException($"该订单[{order.No}]为：{GlobalEnums.GetDescription<OrderStatus>(order.Status)}，不能完成订单！", SystemStatusCode.Fail.ToOrderServicePrefix());

            #endregion 校验

            // 修改订单状态
            /*
            await (await GetDbSetAsync())
                .Where(x => x.Id == orderId)
                .ExecuteUpdateAsync(x => x.SetProperty(s => s.Status, (int)OrderStatus.Complete));
            */
            order.Status = (int)OrderStatus.Complete;

            // 奖励积分
            string? ordersRewardProportion = await SettingProvider.GetOrNullAsync(BaseServiceSettings.PointsSetting.OrdersRewardProportion);
            int.TryParse(ordersRewardProportion, out int oRewardProportion);
            if (oRewardProportion > 0)
            {
                // 计算订单实际支付金额（要减去售后退款的金额）
                decimal money = order.PayedAmount;

                // 查询售后单
                List<BillAftersales> billAftersaleList = await (await _billAftersalesRepository.GetQueryableAsync()).Where(x => x.OrderId == order.Id && x.Status == (int)BillAftersalesStatus.Success).ToListAsync();
                if (billAftersaleList != null && billAftersaleList.Count > 0)
                {
                    decimal refundMoney = 0;
                    foreach (var item in billAftersaleList)
                    {
                        refundMoney = Math.Round(refundMoney + item.RefundAmount, 2);
                    }
                    money = Math.Round(money - refundMoney, 2);
                }

                // 用户积分调整
                int point = Convert.ToInt32(money / oRewardProportion);
                await _capEventBus.PublishAsync(UserChangePointEto.EventName, new UserChangePointEto()
                {
                    UserId = order.UserId,
                    PointType = (int)UserPointSourceTypes.PointTypeRebate,
                    Point = point,
                    Remark = $"订单【{order.No}】积分奖励：{order.Point}。"
                });
            }

            // 保存订单记录
            OrderLog orderLog = new OrderLog();
            orderLog.OrderId = orderId;
            orderLog.UserId = order.UserId;
            orderLog.Type = (int)OrderLogType.LogTypeComplete;
            orderLog.Msg = remark;
            order.Status = (int)OrderStatus.Complete;
            orderLog.Data = JsonSerializer.Serialize(order, AspNetCoreExtension.GetJsonSerializerOptions());
            await _orderLogRepository.InsertAsync(orderLog);
        }

        /// <summary>
        /// 批量获取订单信息
        /// </summary>
        /// <param name="ids">订单ID</param>
        /// <returns></returns>
        public async Task<List<Order>> GetManyAsync(Guid[] ids, bool includeDetails = false)
        {
            return await (await GetDbSetAsync())
                .Where(p => ids.Contains(p.Id))
                .IncludeIf(includeDetails, p => p.OrderItems).ToListAsync();
        }

        /// <summary>
        /// 订单是否评价
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="isComment">是否评价</param>
        /// <returns></returns>
        public async Task OrderCommentAsync(Guid orderId, bool isComment, int type = (int)OrderLogType.LogTypeEvaluation)
        {
            Order? order = await (await GetDbSetAsync()).Where(x => x.Id == orderId).FirstOrDefaultAsync();
            if (order == null) return;
            order.IsComment = isComment;
            order.LastModifierId = order.UserId;

            #region 订单记录

            OrderLog orderLog = new OrderLog();
            orderLog.OrderId = order.Id;
            orderLog.UserId = order.UserId;
            orderLog.Type = type;
            orderLog.Msg = "用户订单评价。";
            if (orderLog.Type == (int)OrderLogType.LogTypeAutoEvaluation)
            {
                orderLog.Msg = "订单(后台)自动评价。";
            }

            orderLog.Data = JsonSerializer.Serialize(order, AspNetCoreExtension.GetJsonSerializerOptions());
            await _orderLogRepository.InsertAsync(orderLog);

            #endregion 订单记录
        }

        /// <summary>
        /// 自动取消订单任务
        /// </summary>
        /// <returns></returns>
        public async Task AutoCancelOrderJobAsync()
        {
            string? orderCancelTime = await SettingProvider.GetOrNullAsync(BaseServiceSettings.OrderSetting.OrderCancelTime);
            int.TryParse(orderCancelTime, out int minutes);
            if (minutes < 1) return;
            DateTime endTime = DateTime.Now.AddMinutes(-minutes);

            List<Order> orders = await (await GetDbSetAsync()).Where(x => x.PayStatus == (int)OrderPayStatus.No)
                .Where(x => x.Status == (int)OrderStatus.Normal)
                .Where(x => x.CreationTime <= endTime).ToListAsync();
            if (orders == null || !orders.Any()) return;
            var orderGroups = orders.GroupBy(x => x.UserId);
            foreach (var oGroup in orderGroups)
            {
                Guid[] ids = oGroup.Select(x => x.Id).Distinct().ToArray();
                bool cancelResult = await CancelOrderAsync(oGroup.Key, ids, "[超时]自动取消订单。");
                List<string> orderNos = oGroup.Select(x => x.No).Distinct().ToList();
                if (!cancelResult) Logger.LogError($"订单【{string.Join('、', orderNos)}】自动取消失败！");
            }
        }

        /// <summary>
        /// 订单自动完成任务
        /// </summary>
        /// <returns></returns>
        public async Task AutoCompleteOrderJobAsync()
        {
            string? orderCompleteTime = await SettingProvider.GetOrNullAsync(BaseServiceSettings.OrderSetting.OrderCompleteTime);
            int.TryParse(orderCompleteTime, out int day);
            if (day < 1) return;
            DateTime endTime = DateTime.Now.AddDays(-day);
            List<Order> orders = await (await GetDbSetAsync())
                .Where(x => x.PayStatus == (int)OrderPayStatus.Yes)
                .Where(x => x.Status == (int)OrderStatus.Normal)
                .Where(x => x.ShipStatus == (int)OrderShipStatus.Yes)
                .Where(x => x.ConfirmStatus == (int)OrderConfirmStatus.ConfirmReceipt)
                .Where(x => x.PaymentTime <= endTime)
                .Include(x => x.BillAftersales)
                .AsNoTracking()
                .ToListAsync();
            if (orders == null || !orders.Any()) return;
            foreach (var order in orders)
            {
                if (
                    order.BillAftersales != null &&
                    order.BillAftersales.Any(x => x.Status == (int)BillAftersalesStatus.WaitAudit) == true
                ) continue;
                await CompleteOrderAsync(order.Id, "订单自动(任务)完成操作！");
            }
        }

        /// <summary>
        /// 订单自动签收任务
        /// </summary>
        /// <returns></returns>
        public async Task AutoSignOrderJobAsync()
        {
            string? orderAutoSignTime = await SettingProvider.GetOrNullAsync(BaseServiceSettings.OrderSetting.OrderAutoSignTime);
            int.TryParse(orderAutoSignTime, out int day);
            if (day < 1) return;
            DateTime endTime = DateTime.Now.AddDays(-day);
            List<Order> orders = await (await GetDbSetAsync())
                .Where(x => x.PayStatus == (int)OrderPayStatus.Yes)
                .Where(x => x.Status == (int)OrderStatus.Normal)
                .Where(x => x.ShipStatus == (int)OrderShipStatus.Yes)
                .Where(x => x.LastModificationTime <= endTime)
                .AsNoTracking()
                .ToListAsync();
            if (orders == null || !orders.Any()) return;
            foreach (var order in orders)
            {
                await ConfirmOrderAsync(order.Id, order.UserId, $"订单【{order.No}】自动签收成功！");
            }
        }

        /// <summary>
        /// 订单自动评价任务
        /// </summary>
        /// <returns></returns>
        public async Task AutoOrderCommentJobAsync()
        {
            string? orderAutoEvalTime = await SettingProvider.GetOrNullAsync(BaseServiceSettings.OrderSetting.OrderAutoEvalTime);
            int.TryParse(orderAutoEvalTime, out int day);
            if (day < 1) return;
            DateTime endTime = DateTime.Now.AddDays(-day);
            List<Order> orders = await (await GetDbSetAsync())
                .Where(x => x.PayStatus == (int)OrderPayStatus.Yes)
                .Where(x => x.Status == (int)OrderStatus.Normal)
                .Where(x => x.ShipStatus == (int)OrderShipStatus.Yes)
                .Where(x => x.ConfirmStatus == (int)OrderConfirmStatus.ConfirmReceipt)
                .Where(x => x.ConfirmTime <= endTime)
                .Include(x => x.OrderItems)
                .AsNoTracking()
                .ToListAsync();
            if (orders == null || !orders.Any()) return;

            UserCommentEto userComment = new UserCommentEto();
            userComment.UserCommentDetails = new List<UserCommentDetailEto>();
            foreach (var order in orders)
            {
                if (order.OrderItems == null || order.OrderItems.Count < 1) continue;
                foreach (var oItem in order.OrderItems)
                {
                    UserCommentDetailEto uComment = new UserCommentDetailEto();
                    uComment.OrderId = order.Id;
                    uComment.UserId = order.UserId;
                    uComment.ImageUrl = oItem.ImageUrl;
                    uComment.Name = oItem.Name;
                    uComment.Nums = oItem.Nums;
                    uComment.Amount = oItem.Amount;
                    uComment.GoodId = oItem.GoodId;
                    uComment.Addon = oItem.Addon;
                    uComment.Score = 5;
                    uComment.ContentBody = $"用户 {day} 天内未对商品做出评价，已由系统自动评价。";
                    userComment.UserCommentDetails.Add(uComment);
                }
            }
            await _capEventBus.PublishAsync(UserCommentEto.EventName, userComment);
        }

        /// <summary>
        /// 获取全局订单状态表达式
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public Expression<Func<Order, bool>> GetGlobalOrderStatusExpression(int status, Guid? userId = null)
        {
            Expression<Func<Order, bool>> orderWhere = x => true;
            switch (status)
            {
                case (int)GlobalOrderStatusType.PendingPayment: // 待付款
                    orderWhere = orderWhere.And(x => x.Status == (int)OrderStatus.Normal);
                    orderWhere = orderWhere.And(x => x.PayStatus == (int)OrderPayStatus.No);
                    break;

                case (int)GlobalOrderStatusType.PendingShipment: // 待发货
                    orderWhere = orderWhere.And(p => p.Status == (int)OrderStatus.Normal);
                    orderWhere = orderWhere.And(p => p.PayStatus != (int)OrderPayStatus.No);
                    orderWhere = orderWhere.And(p => p.ShipStatus == (int)OrderShipStatus.No || p.ShipStatus == (int)OrderShipStatus.PartialYes);
                    break;

                case (int)GlobalOrderStatusType.PendingDelivery: // 待收货
                    orderWhere = orderWhere.And(p => p.Status == (int)OrderStatus.Normal);
                    orderWhere = orderWhere.And(p => p.PayStatus != (int)OrderPayStatus.No);
                    orderWhere = orderWhere.And(p => p.ShipStatus == (int)OrderShipStatus.Yes || p.ShipStatus == (int)OrderShipStatus.PartialYes);
                    orderWhere = orderWhere.And(p => p.ConfirmStatus == (int)OrderConfirmStatus.ReceiptNotConfirmed);
                    break;

                case (int)GlobalOrderStatusType.PendingEvaluate: // 待评价
                    orderWhere = orderWhere.And(p => p.Status == (int)OrderStatus.Normal);
                    orderWhere = orderWhere.And(p => p.PayStatus != (int)OrderPayStatus.No);
                    orderWhere = orderWhere.And(p => p.ShipStatus != (int)OrderShipStatus.No);
                    orderWhere = orderWhere.And(p => p.ConfirmStatus == (int)OrderConfirmStatus.ConfirmReceipt);
                    orderWhere = orderWhere.And(p => p.IsComment == false);
                    break;

                case (int)GlobalOrderStatusType.CompletedEvaluate: // 已评价
                    orderWhere = orderWhere.And(p => p.Status == (int)OrderStatus.Normal);
                    orderWhere = orderWhere.And(p => p.PayStatus != (int)OrderPayStatus.No);
                    orderWhere = orderWhere.And(p => p.ShipStatus != (int)OrderShipStatus.No);
                    orderWhere = orderWhere.And(p => p.ConfirmStatus == (int)OrderConfirmStatus.ConfirmReceipt);
                    orderWhere = orderWhere.And(p => p.IsComment == true);
                    break;

                case (int)GlobalOrderStatusType.Completed: // 已完成
                    orderWhere = orderWhere.And(p => p.Status == (int)OrderStatus.Complete);
                    break;

                case (int)GlobalOrderStatusType.Cancel: // 已取消
                    orderWhere = orderWhere.And(p => p.Status == (int)OrderStatus.Cancel);
                    break;

                default:
                    break;
            }
            if (userId.HasValue) orderWhere = orderWhere.And(p => p.UserId == userId.Value);
            return orderWhere;
        }

        /// <summary>
        /// 获取本周订单销售总额
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<OrderAmountStatisticsDto>> GetWeekOrderAmountStatistics()
        {
            Dictionary<string, List<OrderAmountStatisticsDto>> orderAmountStatisticsDic = new Dictionary<string, List<OrderAmountStatisticsDto>>();
            string sql = @"
                WITH RECURSIVE DateSeries AS (
                    SELECT
                        CURDATE() - INTERVAL WEEKDAY(CURDATE()) DAY AS dt
                    UNION ALL
                    SELECT dt + INTERVAL 1 DAY
                    FROM DateSeries
                    WHERE dt < CURDATE() - INTERVAL WEEKDAY(CURDATE()) DAY + INTERVAL 6 DAY
                )
                SELECT
                    ds.dt AS OrderDate,
                    COALESCE(SUM(o.OrderAmount), 0) AS TotalAmount
                FROM
                    DateSeries ds
                LEFT JOIN
                    od_orders o
                    ON (DATE(o.CreationTime) = ds.dt AND o.IsDeleted = 0 {0})
                GROUP BY
                    ds.dt
                ORDER BY
                    ds.dt
            ";

            #region 全部

            string sqlAll = string.Format(sql, "");
            DataTable dataTableAll = oServiceDbContext.ExecuteQuery(sqlAll, CommandType.Text);
            List<DataRow> dataRowAllList = dataTableAll.AsEnumerable().ToList();
            List<OrderAmountStatisticsDto> orderAmountStatisticsAllList = new List<OrderAmountStatisticsDto>();
            foreach (DataRow dataRow in dataRowAllList)
            {
                OrderAmountStatisticsDto orderAmountStatistics = new OrderAmountStatisticsDto();
                orderAmountStatistics.OrderDate = dataRow.Field<DateTime>("OrderDate");
                orderAmountStatistics.TotalAmount = dataRow.Field<decimal>("TotalAmount");
                orderAmountStatisticsAllList.Add(orderAmountStatistics);
            }
            orderAmountStatisticsDic.Add("全部", orderAmountStatisticsAllList);

            #endregion 全部

            #region 待付款

            string sqlPendingPay = string.Format(sql, " AND o.Status = 1 AND o.PayStatus = 1 ");
            DataTable dataTablePendingPay = oServiceDbContext.ExecuteQuery(sqlPendingPay, CommandType.Text);
            List<DataRow> dataRowPendingPayList = dataTablePendingPay.AsEnumerable().ToList();
            List<OrderAmountStatisticsDto> orderAmountStatisticsPendingPayList = new List<OrderAmountStatisticsDto>();
            foreach (DataRow dataRow in dataRowPendingPayList)
            {
                OrderAmountStatisticsDto orderAmountStatistics = new OrderAmountStatisticsDto();
                orderAmountStatistics.OrderDate = dataRow.Field<DateTime>("OrderDate");
                orderAmountStatistics.TotalAmount = dataRow.Field<decimal>("TotalAmount");
                orderAmountStatisticsPendingPayList.Add(orderAmountStatistics);
            }
            orderAmountStatisticsDic.Add("待付款", orderAmountStatisticsPendingPayList);

            #endregion 待付款

            #region 已付款

            string sqlPaidPay = string.Format(sql, " AND o.Status = 1 AND o.PayStatus = 2 ");
            DataTable dataTablePaidPay = oServiceDbContext.ExecuteQuery(sqlPaidPay, CommandType.Text);
            List<DataRow> dataRowPaidPayList = dataTablePaidPay.AsEnumerable().ToList();
            List<OrderAmountStatisticsDto> orderAmountStatisticsPaidPayList = new List<OrderAmountStatisticsDto>();
            foreach (DataRow dataRow in dataRowPaidPayList)
            {
                OrderAmountStatisticsDto orderAmountStatistics = new OrderAmountStatisticsDto();
                orderAmountStatistics.OrderDate = dataRow.Field<DateTime>("OrderDate");
                orderAmountStatistics.TotalAmount = dataRow.Field<decimal>("TotalAmount");
                orderAmountStatisticsPaidPayList.Add(orderAmountStatistics);
            }
            orderAmountStatisticsDic.Add("已付款", orderAmountStatisticsPaidPayList);

            #endregion 已付款

            return orderAmountStatisticsDic;
        }

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="orderIds">订单ID</param>
        /// <param name="paymentCode">支付编码</param>
        /// <param name="description">描述</param>
        public async Task UpdateOrderStatusAsync(Guid[] orderIds, string paymentCode, string description)
        {
            List<Order> orders = await (await GetDbSetAsync())
               .Where(x => orderIds.Contains(x.Id))
               .Where(x => x.Status == (int)OrderStatus.Normal)
               .AsNoTracking()
               .ToListAsync();
            if (orders == null || !orders.Any()) throw new UserFriendlyException("更新订单ID[" + string.Join('、', orderIds) + "]状态失败或该订单已失效！", SystemStatusCode.Fail.ToOrderServicePrefix());

            // 定义要修改的订单信息
            Dictionary<Guid, Expression<Func<SetPropertyCalls<Order>, SetPropertyCalls<Order>>>> setOrderPropertyDic = new Dictionary<Guid, Expression<Func<SetPropertyCalls<Order>, SetPropertyCalls<Order>>>>();
            foreach (Order order in orders)
            {
                if (order.PayStatus == (int)OrderPayStatus.Yes || order.PayStatus == (int)OrderPayStatus.PartialNo || order.PayStatus == (int)OrderPayStatus.Refunded)
                {
                    Debug.WriteLine("订单[" + order.No + "]已支付，无需更新状态！");
                    continue;
                }
                order.PayedAmount = order.OrderAmount;
                order.PaymentTime = DateTime.Now;
                order.PaymentCode = paymentCode;
                order.PayStatus = (int)OrderPayStatus.Yes;
                Expression<Func<SetPropertyCalls<Order>, SetPropertyCalls<Order>>> setOrderProperty = x => x
                    .SetProperty(e => e.PayedAmount, order.PayedAmount)
                    .SetProperty(e => e.PaymentTime, order.PaymentTime)
                    .SetProperty(e => e.PaymentCode, order.PaymentCode)
                    .SetProperty(e => e.PayStatus, order.PayStatus);
                setOrderPropertyDic.Add(order.Id, setOrderProperty);
            }

            #region 订单状态更新

            if (setOrderPropertyDic.Any())
            {
                #region 更新订单

                foreach (var orderProperty in setOrderPropertyDic)
                {
                    int rowsUpdated = await (await GetDbSetAsync()).Where(x => x.Id == orderProperty.Key).ExecuteUpdateAsync(orderProperty.Value);
                    Debug.WriteLine("更新了[" + rowsUpdated + "]条数据！");
                }

                #endregion 更新订单

                #region 增加订单记录

                foreach (var order in orders)
                {
                    OrderLog orderLogCreate = new OrderLog();
                    orderLogCreate.OrderId = order.Id;
                    orderLogCreate.UserId = order.UserId;
                    orderLogCreate.Type = (int)OrderLogType.LogTypePay;
                    orderLogCreate.Msg = "订单支付成功！";
                    orderLogCreate.Data = JsonSerializer.Serialize(order, AspNetCoreExtension.GetJsonSerializerOptions());
                    await _orderLogRepository.InsertAsync(orderLogCreate);
                }

                #endregion 增加订单记录
            }

            #endregion 订单状态更新

            // TODO: 结佣处理
            // TODO: 易联云打印机打印
            // TODO: 发送支付成功信息，增加发送内容
            // TODO: 用户升级处理

            Debug.WriteLine("订单状态更新成功！");
        }

        /// <summary>
        /// 创建用户订单
        /// </summary>
        /// <param name="order">订单</param>
        /// <param name="isUsePoint">是否使用积分</param>
        /// <param name="cartIds">购物车ID</param>
        /// <returns></returns>
        public async Task CreateUserOrderAsync(Order order, bool isUsePoint, Guid[] cartIds)
        {
            #region 1.保存订单

            await (await GetDbSetAsync()).AddAsync(order);

            // 优惠劵核销
            if (!order.Coupon.IsNullOrWhiteSpace())
            {
                string[] couponCodes = JsonSerializer.Deserialize<string[]>(order.Coupon);
                await _capEventBus.PublishAsync(UsedCouponEto.EventName, new UsedCouponEto()
                {
                    CouponCode = couponCodes,
                    UsedId = order.Id
                });
            }

            // 积分核销
            if (isUsePoint)
            {
                await _capEventBus.PublishAsync(UserChangePointEto.EventName, new UserChangePointEto()
                {
                    UserId = order.UserId,
                    PointType = (int)UserPointSourceTypes.PointTypeDiscount,
                    Point = 0 - order.Point,
                    Remark = $"订单【{order.No}】使用积分：-{order.Point}。"
                });
            }

            // 清空购物车
            await _cartRepository.DeleteManyAsync(cartIds);

            // 订单记录
            OrderLog orderLogCreate = new OrderLog();
            orderLogCreate.OrderId = order.Id;
            orderLogCreate.UserId = order.UserId;
            orderLogCreate.Type = (int)OrderLogType.LogTypeCreate;
            orderLogCreate.Msg = "订单创建";
            orderLogCreate.Data = JsonSerializer.Serialize(order, AspNetCoreExtension.GetJsonSerializerOptions());
            await _orderLogRepository.InsertAsync(orderLogCreate);

            // 0元订单，直接支付成功
            if (order.OrderAmount <= 0)
            {
                orderLogCreate.Type = (int)OrderLogType.LogTypePay;
                orderLogCreate.Msg = "0元订单直接支付成功";
                await _orderLogRepository.InsertAsync(orderLogCreate);
            }

            #endregion 1.保存订单

            #region 2.库存变更(扣减库存)

            OrderDeductionProductStockEto oDeductionProductStock = new OrderDeductionProductStockEto();
            oDeductionProductStock.Id = order.Id;
            oDeductionProductStock.StockDatas = order.OrderItems.Select(x => new ProductDeductionStockEto()
            {
                ProductId = x.ProductId,
                Nums = x.Nums
            }).ToList();
            await _capEventBus.PublishAsync(OrderDeductionProductStockEto.EventName, oDeductionProductStock);

            #endregion 2.库存变更(扣减库存)
        }
    }
}