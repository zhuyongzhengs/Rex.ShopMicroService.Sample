using Rex.OrderService.Statistics;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单仓储接口
    /// </summary>
    public interface IOrderRepository : IRepository<Order, Guid>
    {
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
        Task<long> GetPageCountAsync(
            string no,
            int? payStatus,
            int? shipStatus,
            int? status,
            int? orderType,
            int? receiptType,
            int? confirmStatus,
            bool? isComment,
            Guid? userId = null,
            CancellationToken cancellationToken = default
            );

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
        Task<List<Order>> GetPageListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string no,
            int? payStatus,
            int? shipStatus,
            int? status,
            int? orderType,
            int? receiptType,
            int? confirmStatus,
            bool? isComment,
            Guid? userId = null,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 获取全局订单状态表达式
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        Expression<Func<Order, bool>> GetGlobalOrderStatusExpression(int status, Guid? userId = null);

        /// <summary>
        /// 根据订单号获取订单
        /// </summary>
        /// <param name="no">订单号</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        Task<Order> GetOrderByNoAsync(string no, Guid? userId = null, bool includeDetails = true);

        /// <summary>
        /// 根据订单ID获取订单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        Task<Order> GetOrderByIdAsync(Guid id, Guid? userId = null, bool includeDetails = true);

        /// <summary>
        /// 批量获取订单信息
        /// </summary>
        /// <param name="ids">订单ID</param>
        /// <returns></returns>
        Task<List<Order>> GetManyAsync(Guid[] ids, bool includeDetails = false);

        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <param name="orderIds">订单ID</param>
        /// <param name="status">订单状态</param>
        /// <param name="payStatus">支付状态</param>
        /// <returns></returns>
        Task<List<Order>> GetOrderStatusAsync(Guid[] orderIds, int? status = null, int? payStatus = null);

        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="status">订单状态</param>
        /// <param name="payStatus">支付状态</param>
        /// <returns></returns>
        Task<Order> GetOrderStatusAsync(Guid orderId, int? status = null, int? payStatus = null);

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="ids">订单ID</param>
        /// <returns></returns>
        Task<bool> CancelOrderAsync(Guid userId, Guid[] ids, string cancelMsg = "订单取消操作");

        /// <summary>
        /// 更新卖家备注
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="mark">备注</param>
        /// <returns></returns>
        Task UpdateOrderMarkAsync(Guid orderId, string mark);

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
        Task UpdateOrderShipAsync(Guid orderId, long shipAreaId, string? displayArea, string shipAddress, string shipName, string shipMobile);

        /// <summary>
        /// 更新发货状态
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="shipStatus">1：未发货、2：部分发货、3：已发货、4：部分退货、5：已退货</param>
        /// <returns></returns>
        Task UpdateShipStatusAsync(Guid orderId, int shipStatus);

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        Task ConfirmOrderAsync(Guid orderId, Guid? userId = null, string remark = "");

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        Task CompleteOrderAsync(Guid orderId, string remark = "后台订单完成操作！");

        /// <summary>
        /// 订单是否评价
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="isComment">是否评价</param>
        /// <returns></returns>
        Task OrderCommentAsync(Guid orderId, bool isComment, int type = (int)OrderLogType.LogTypeEvaluation);

        /// <summary>
        /// 自动取消订单任务
        /// </summary>
        /// <returns></returns>
        Task AutoCancelOrderJobAsync();

        /// <summary>
        /// 订单自动完成任务
        /// </summary>
        /// <returns></returns>
        Task AutoCompleteOrderJobAsync();

        /// <summary>
        /// 订单自动签收任务
        /// </summary>
        /// <returns></returns>
        Task AutoSignOrderJobAsync();

        /// <summary>
        /// 订单自动评价任务
        /// </summary>
        /// <returns></returns>
        Task AutoOrderCommentJobAsync();

        /// <summary>
        /// 获取本周订单销售总额
        /// </summary>
        /// <returns></returns>
        Dictionary<string, List<OrderAmountStatisticsDto>> GetWeekOrderAmountStatistics();

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="orderIds">订单ID</param>
        /// <param name="paymentCode">支付编码</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        Task UpdateOrderStatusAsync(Guid[] orderIds, string paymentCode, string description);

        /// <summary>
        /// 创建用户订单
        /// </summary>
        /// <param name="order">订单</param>
        /// <param name="isUsePoint">是否使用积分</param>
        /// <param name="cartIds">购物车ID</param>
        /// <returns></returns>
        Task CreateUserOrderAsync(Order order, bool isUsePoint, Guid[] cartIds);
    }
}