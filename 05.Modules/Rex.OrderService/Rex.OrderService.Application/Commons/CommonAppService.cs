using Microsoft.AspNetCore.Authorization;
using Rex.OrderService.Bills;
using Rex.OrderService.Carts;
using Rex.OrderService.Orders;
using Rex.OrderService.Ships;
using Rex.OrderService.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.SettingManagement;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.OrderService.Commons
{
    /// <summary>
    /// 公共服务
    /// </summary>
    [RemoteService]
    public class CommonAppService : ApplicationService, ICommonAppService
    {
        private readonly IShipRepository _shipRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IBillAftersalesRepository _billAftersalesRepository;

        public ISettingManager SettingManager { get; set; }

        public CommonAppService(IShipRepository repository, ICartRepository cartRepository, IOrderRepository orderRepository, IBillAftersalesRepository billAftersalesRepository)
        {
            _shipRepository = repository;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _billAftersalesRepository = billAftersalesRepository;
        }

        /// <summary>
        /// 设置当前租户值
        /// </summary>
        /// <param name="name">Key</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        [Authorize]
        public async Task UpdateSettingCurrentTenantAsync(string name, string? value)
        {
            await SettingManager.SetForCurrentTenantAsync(name, value);
        }

        /// <summary>
        /// 获取待付款数量
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<int> GetPendingPaymentCountAsync()
        {
            Expression<Func<Order, bool>> orderWhere = _orderRepository.GetGlobalOrderStatusExpression((int)GlobalOrderStatusType.PendingPayment);
            return (await _orderRepository.GetQueryableAsync()).Where(orderWhere).Count();
        }

        /// <summary>
        /// 获取待发货数量
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<int> GetPendingShipmentCountAsync()
        {
            Expression<Func<Order, bool>> orderWhere = _orderRepository.GetGlobalOrderStatusExpression((int)GlobalOrderStatusType.PendingShipment);
            return (await _orderRepository.GetQueryableAsync()).Where(orderWhere).Count();
        }

        /// <summary>
        /// 获取待售后数量
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<int> GetPendingAftersalesCountAsync()
        {
            return (await _billAftersalesRepository.GetQueryableAsync()).Where(x => x.Status == (int)BillAftersalesStatus.WaitAudit).Count();
        }

        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <param name="orderIds">订单ID</param>
        /// <param name="status">订单状态</param>
        /// <param name="payStatus">支付状态</param>
        /// <returns></returns>
        [Authorize]
        public async Task<List<OrderDto>> GetOrderStatusAsync(Guid[] orderIds, int? status = null, int? payStatus = null)
        {
            List<Order> orders = (await (_orderRepository.GetQueryableAsync()))
               .WhereIf(orderIds != null, x => orderIds.Contains(x.Id))
               .WhereIf(status.HasValue, x => x.Status == status)
               .WhereIf(payStatus.HasValue, x => x.PayStatus == payStatus)
               .ToList();
            return ObjectMapper.Map<List<Order>, List<OrderDto>>(orders);
        }

        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="status">订单状态</param>
        /// <param name="payStatus">支付状态</param>
        /// <returns></returns>
        public async Task<OrderDto> GetOrderStatusAsync(Guid orderId, int? status = null, int? payStatus = null)
        {
            Order order = (await (_orderRepository.GetQueryableAsync()))
               .Where(x => x.Id == orderId)
               .WhereIf(status.HasValue, x => x.Status == status)
               .WhereIf(payStatus.HasValue, x => x.PayStatus == payStatus)
               .FirstOrDefault();
            return ObjectMapper.Map<Order, OrderDto>(order);
        }

        /// <summary>
        /// 根据区域ID获取配送方式
        /// </summary>
        /// <param name="areaId">区域ID</param>
        /// <returns></returns>
        [Authorize]
        public async Task<ShipDto> GetShipByAreaIdAsync(long areaId)
        {
            string area = areaId.ToString();
            Ship ship = await _shipRepository.FindAsync(x =>
                x.Status == (int)ShipStatus.Yes &&
                x.AreaType == (int)ShipAreaType.Part &&
                x.AreaFee != null &&
                x.AreaFee.Contains(area)
            );
            if (ship == null) ship = await _shipRepository.FindAsync(x => x.IsDefault == true && x.Status == (int)ShipStatus.Yes);
            if (ship == null) ship = await _shipRepository.FindAsync(x => x.Status == (int)ShipStatus.Yes);
            return ObjectMapper.Map<Ship, ShipDto>(ship);
        }

        /// <summary>
        /// 获取用户购物车列表
        /// </summary>
        /// <param name="type">购物车类型</param>
        /// <remarks>
        /// 默认：1[购物车类型]
        /// </remarks>
        /// <returns></returns>
        [Authorize]
        public async Task<List<CartDto>> GetUserCartsAsync(int type = 1)
        {
            List<Cart> carts = await _cartRepository.GetListAsync(x => x.UserId == CurrentUser.Id && x.Type == type);
            return ObjectMapper.Map<List<Cart>, List<CartDto>>(carts);
        }

        /// <summary>
        /// 根据订单ID获取订单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public async Task<OrderDto> GetOrderByIdAsync(Guid id, bool includeDetails = true)
        {
            Order order = await _orderRepository.GetOrderByIdAsync(id, includeDetails: includeDetails);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }

        /// <summary>
        /// 获取用户售后单数量
        /// </summary>
        /// <returns></returns>
        public async Task<long> GetUserBillAftersaleCountAsync()
        {
            return (await _billAftersalesRepository.GetQueryableAsync()).Where(x => x.UserId == CurrentUser.Id).Count();
        }

        /// <summary>
        /// 获取用户待付款数量
        /// </summary>
        /// <returns></returns>
        public async Task<long> GetUserPendingPaymentCountAsync()
        {
            long totalCount = (await _orderRepository.GetQueryableAsync()).Where(x => x.UserId == CurrentUser.Id && x.Status == (int)OrderStatus.Normal && x.PayStatus == (int)OrderPayStatus.No).Count();
            return totalCount;
        }

        /// <summary>
        /// 获取用户待发货数量
        /// </summary>
        /// <returns></returns>
        public async Task<long> GetUserPendingShipmentCountAsync()
        {
            long totalCount = (await _orderRepository.GetQueryableAsync()).Where(x => x.UserId == CurrentUser.Id && x.Status == (int)OrderStatus.Normal && x.PayStatus != (int)OrderPayStatus.No && (x.ShipStatus == (int)OrderShipStatus.No || x.ShipStatus == (int)OrderShipStatus.PartialYes)).Count();
            return totalCount;
        }

        /// <summary>
        /// 获取用户待收货数量
        /// </summary>
        /// <returns></returns>
        public async Task<long> GetUserPendingDeliveryCountAsync()
        {
            long totalCount = (await _orderRepository.GetQueryableAsync()).Where(x => x.UserId == CurrentUser.Id && x.Status == (int)OrderStatus.Normal && x.PayStatus != (int)OrderPayStatus.No && (x.ShipStatus == (int)OrderShipStatus.Yes || x.ShipStatus == (int)OrderShipStatus.PartialYes) && x.ConfirmStatus == (int)OrderConfirmStatus.ReceiptNotConfirmed).Count();
            return totalCount;
        }

        /// <summary>
        /// 获取用户待评价数量
        /// </summary>
        /// <returns></returns>
        public async Task<long> GetUserPendingEvaluateCountAsync()
        {
            long totalCount = (await _orderRepository.GetQueryableAsync()).Where(x => x.UserId == CurrentUser.Id && x.Status == (int)OrderStatus.Normal && x.PayStatus != (int)OrderPayStatus.No && x.ShipStatus != (int)OrderShipStatus.No && x.ConfirmStatus == (int)OrderConfirmStatus.ConfirmReceipt && x.IsComment == false).Count();
            return totalCount;
        }

        /// <summary>
        /// 获取本周订单销售总额
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, List<OrderAmountStatisticsDto>>> GetWeekOrderAmountStatisticsAsync()
        {
            return await Task.FromResult(_orderRepository.GetWeekOrderAmountStatistics());
        }
    }
}