using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.OrderService.Orders
{
    /// <summary>
    /// 订单服务接口
    /// </summary>
    public interface IOrdersAppService : IApplicationService
    {
        /// <summary>
        /// 获取(分页)订单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<OrderDto>> GetListAsync(GetOrderInput input);

        /// <summary>
        /// 查询订单[状态]数量
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        Task<int> GetStatusCountAsync(int status, Guid? userId = null);

        /// <summary>
        /// 查询订单数量
        /// </summary>
        /// <returns></returns>
        Task<OrderStatusQuantityDto> GetStatusQuantityAsync();

        /// <summary>
        /// 获取用户订单(分页)订单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<UserOrderDto>> GetUserOrdersAsync(GetUserOrderInput input);

        /// <summary>
        /// 查询用户订单
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <returns></returns>
        Task<UserOrderDto> GetUserOrderAsync(Guid id);

        /// <summary>
        /// 根据订单ID获取订单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<OrderDto> GetAsync(Guid id, bool includeDetails = true);

        /// <summary>
        /// 查询批量订单信息
        /// </summary>
        /// <param name="ids">订单ID</param>
        /// <returns></returns>
        Task<List<OrderDto>> GetManyAsync(Guid[] ids, bool includeDetails = false);

        /// <summary>
        /// 根据订单号获取用户订单
        /// </summary>
        /// <param name="no">订单号</param>
        /// <returns></returns>
        Task<OrderDto> GetUserOrderNoAsync(string no);

        /// <summary>
        /// 批量更新订单
        /// </summary>
        /// <param name="input">订单</param>
        /// <returns></returns>
        Task<List<OrderDto>> UpdateOrderManyAsync(List<OrderUpdateDto> input);

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="status">状态：1[订单正常]、2[订单完成]、3[订单取消]</param>
        /// <returns></returns>
        Task UpdateStatusAsync(Guid orderId, int status);

        /// <summary>
        /// 创建订单记录
        /// </summary>
        /// <param name="input">订单记录</param>
        /// <returns></returns>
        Task<OrderLogDto> CreateOrderLogAsync(OrderLogCreateDto input);

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="ids">订单ID</param>
        /// <returns></returns>
        Task<bool> UpdateCancelAsync(Guid[] ids);

        /// <summary>
        /// 用户取消订单
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="ids">订单ID</param>
        /// <returns></returns>
        Task<bool> UpdateUserCancelAsync(Guid userId, Guid[] ids);

        /// <summary>
        /// 查询订单类型
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetOrderTypeAsync();

        /// <summary>
        /// 查询发货状态
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetShipStatusAsync();

        /// <summary>
        /// 查询售后状态
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetConfirmStatusAsync();

        /// <summary>
        /// 查询订单状态
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetStatusAsync();

        /// <summary>
        /// 查询订单来源
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetSourceAsync();

        /// <summary>
        /// 查询订单支付状态
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetPayStatusAsync();

        /// <summary>
        /// 查询订单收货方式
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetReceiptTypeAsync();

        /// <summary>
        /// 更新卖家备注
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="mark">备注</param>
        /// <returns></returns>
        Task UpdateMarkAsync(Guid orderId, string mark);

        /// <summary>
        /// 更新订单收货信息
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="input">收货信息</param>
        /// <returns></returns>
        Task UpdateShipAsync(Guid orderId, OrderShipUpdateDto input);

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="input">收货订单</param>
        /// <returns></returns>
        Task UpdateConfirmAsync(ConfirmOrderDto input);

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        Task UpdateCompleteAsync(Guid orderId);

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="ids">订单ID</param>
        /// <returns></returns>
        Task DeleteManyAsync(Guid[] ids);

        /// <summary>
        /// 测试事件总线
        /// </summary>
        /// <param name="orderMsg">订单消息</param>
        /// <param name="isException">是否模拟异常</param>
        /// <returns></returns>
        public Task<string> TestOrderToGoodMsgAsync(string orderMsg, bool isException = false);
    }
}