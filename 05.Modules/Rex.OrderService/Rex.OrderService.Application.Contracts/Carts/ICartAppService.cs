using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Rex.OrderService.Carts
{
    /// <summary>
    /// 购物车服务接口
    /// </summary>
    public interface ICartAppService : IApplicationService
    {
        /// <summary>
        /// 创建购物车
        /// </summary>
        /// <param name="input">购物车信息</param>
        /// <returns></returns>
        Task<CartDto> CreateAsync(CartCreateDto input);

        /// <summary>
        /// 获取购物车数量
        /// </summary>
        /// <returns></returns>
        Task<int> GetNumberAsync(int type = 1);

        /// <summary>
        /// 获取用户的购物车信息
        /// </summary>
        /// <param name="type">类型：1：普通购物车，2：秒杀购物车</param>
        /// <returns></returns>
        Task<List<CartDto>> GetUserCartByTypeAsync(int type = 1);

        /// <summary>
        /// 根据购物(车)ID获取购物车信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <returns></returns>
        Task<List<CartDto>> GetUserCartByIdsAsync(List<Guid> ids);

        /// <summary>
        /// 删除购物车
        /// </summary>
        /// <param name="ids">ID</param>
        /// <returns></returns>
        Task DeleteManyAsync(List<Guid> ids);

        /// <summary>
        /// 更新用户的购物车货品数量
        /// </summary>
        /// <param name="id">购物车ID</param>
        /// <param name="nums">货品数量</param>
        /// <returns></returns>
        Task UpdateUserCartNumsAsnyc(Guid id, int nums);
    }
}