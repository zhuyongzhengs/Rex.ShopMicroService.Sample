using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Rex.OrderService.Carts
{
    /// <summary>
    /// 购物车服务
    /// </summary>
    [RemoteService]
    [Authorize]
    public class CartAppService : ApplicationService, ICartAppService
    {
        private readonly ICartRepository _cartRepository;

        public CartAppService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        /// <summary>
        /// 创建购物车
        /// </summary>
        /// <param name="input">购物车信息</param>
        /// <returns></returns>
        public async Task<CartDto> CreateAsync(CartCreateDto input)
        {
            Cart inertCart = ObjectMapper.Map<CartCreateDto, Cart>(input);
            // 先验证是否为同一个货品
            Cart cart = await _cartRepository.FindAsync(x => x.UserId == input.UserId && x.ProductId == input.ProductId);
            if (cart == null)
            {
                cart = await _cartRepository.InsertAsync(inertCart);
            }
            else
            {
                cart.Nums += input.Nums;
            }
            return ObjectMapper.Map<Cart, CartDto>(cart);
        }

        /// <summary>
        /// 删除购物车
        /// </summary>
        /// <param name="ids">ID</param>
        /// <returns></returns>
        public async Task DeleteManyAsync(List<Guid> ids)
        {
            await _cartRepository.DeleteManyAsync(ids);
        }

        /// <summary>
        /// 获取购物车数量
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetNumberAsync(int type = 1)
        {
            int cartNumber = (await _cartRepository.GetQueryableAsync()).Where(x => x.UserId == CurrentUser.Id && x.Type == type).Count();
            return cartNumber;
        }

        /// <summary>
        /// 根据购物(车)ID获取购物车信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <returns></returns>
        public async Task<List<CartDto>> GetUserCartByIdsAsync(List<Guid> ids)
        {
            List<Cart> carts = await _cartRepository.GetListAsync(x => ids.Contains(x.Id));
            return ObjectMapper.Map<List<Cart>, List<CartDto>>(carts);
        }

        /// <summary>
        /// 获取用户的购物车信息
        /// </summary>
        /// <param name="type">类型：1：普通购物车，2：秒杀购物车</param>
        /// <returns></returns>
        public async Task<List<CartDto>> GetUserCartByTypeAsync(int type = 1)
        {
            List<Cart> carts = await _cartRepository.GetListAsync(x => x.UserId == CurrentUser.Id && x.Type == type);
            return ObjectMapper.Map<List<Cart>, List<CartDto>>(carts);
        }

        /// <summary>
        /// 更新用户的购物车货品数量
        /// </summary>
        /// <param name="id">购物车ID</param>
        /// <param name="nums">货品数量</param>
        /// <returns></returns>
        public async Task UpdateUserCartNumsAsnyc(Guid id, int nums)
        {
            if (nums < 1) return;
            Cart cart = await _cartRepository.FindAsync(x => x.Id == id && x.UserId == CurrentUser.Id);
            if (cart == null) return;
            cart.Nums = nums;
            await _cartRepository.UpdateAsync(cart);
        }
    }
}