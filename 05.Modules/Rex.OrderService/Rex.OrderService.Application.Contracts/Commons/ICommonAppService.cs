using Rex.OrderService.Carts;
using Rex.OrderService.Orders;
using Rex.OrderService.Ships;
using Rex.OrderService.Statistics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Rex.OrderService.Commons
{
    /// <summary>
    /// 公共服务接口
    /// </summary>
    public interface ICommonAppService : IApplicationService
    {
        /// <summary>
        /// 设置当前租户值
        /// </summary>
        /// <param name="name">Key</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        Task UpdateSettingCurrentTenantAsync(string name, string? value);

        /// <summary>
        /// 获取待付款数量
        /// </summary>
        /// <returns></returns>
        Task<int> GetPendingPaymentCountAsync();

        /// <summary>
        /// 获取待发货数量
        /// </summary>
        /// <returns></returns>
        Task<int> GetPendingShipmentCountAsync();

        /// <summary>
        /// 获取待售后数量
        /// </summary>
        /// <returns></returns>
        Task<int> GetPendingAftersalesCountAsync();

        /// <summary>
        /// 根据区域ID获取配送方式
        /// </summary>
        /// <param name="areaId">区域ID</param>
        /// <returns></returns>
        Task<ShipDto> GetShipByAreaIdAsync(long areaId);

        /// <summary>
        /// 获取用户购物车列表
        /// </summary>
        /// <param name="type">购物车类型</param>
        /// <remarks>
        /// 默认：1[购物车类型]
        /// </remarks>
        /// <returns></returns>
        Task<List<CartDto>> GetUserCartsAsync(int type = 1);

        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <param name="orderIds">订单ID</param>
        /// <param name="status">订单状态</param>
        /// <param name="payStatus">支付状态</param>
        /// <returns></returns>
        Task<List<OrderDto>> GetOrderStatusAsync(Guid[] orderIds, int? status = null, int? payStatus = null);

        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="status">订单状态</param>
        /// <param name="payStatus">支付状态</param>
        /// <returns></returns>
        Task<OrderDto> GetOrderStatusAsync(Guid orderId, int? status = null, int? payStatus = null);

        /// <summary>
        /// 根据订单ID获取订单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<OrderDto> GetOrderByIdAsync(Guid id, bool includeDetails = true);

        /// <summary>
        /// 获取用户售后单数量
        /// </summary>
        /// <returns></returns>
        Task<long> GetUserBillAftersaleCountAsync();

        /// <summary>
        /// 获取用户待付款数量
        /// </summary>
        /// <returns></returns>
        Task<long> GetUserPendingPaymentCountAsync();

        /// <summary>
        /// 获取用户待发货数量
        /// </summary>
        /// <returns></returns>
        Task<long> GetUserPendingShipmentCountAsync();

        /// <summary>
        /// 获取用户待收货数量
        /// </summary>
        /// <returns></returns>
        Task<long> GetUserPendingDeliveryCountAsync();

        /// <summary>
        /// 获取用户待评价数量
        /// </summary>
        /// <returns></returns>
        Task<long> GetUserPendingEvaluateCountAsync();

        /// <summary>
        /// 获取本周订单销售总额
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, List<OrderAmountStatisticsDto>>> GetWeekOrderAmountStatisticsAsync();
    }
}