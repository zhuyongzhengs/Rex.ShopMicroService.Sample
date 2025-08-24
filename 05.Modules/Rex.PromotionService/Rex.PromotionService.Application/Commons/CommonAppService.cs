using Microsoft.AspNetCore.Authorization;
using Rex.PromotionService.PinTuans;
using Rex.PromotionService.Promotions;
using Rex.Service.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.SettingManagement;

namespace Rex.PromotionService.Commons
{
    /// <summary>
    /// 公共服务
    /// </summary>
    [RemoteService]
    public class CommonAppService : ApplicationService, ICommonAppService
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly ICommonRepository _commonRepository;
        private readonly IPromotionCommonRepository _promotionCommonRepository;
        private readonly ICouponRepository _couponRepository;

        public ISettingManager SettingManager { get; set; }

        public CommonAppService(IPromotionRepository promotionRepository, ICommonRepository commonRepository, IPromotionCommonRepository promotionCommonRepository, ICouponRepository couponRepository)
        {
            _promotionRepository = promotionRepository;
            _commonRepository = commonRepository;
            _promotionCommonRepository = promotionCommonRepository;
            _couponRepository = couponRepository;
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
        /// 获取可领取的优惠劵
        /// </summary>
        /// <param name="limit">优惠劵数量，默认：3</param>
        /// <returns></returns>
        public async Task<List<PromotionDto>> GetReceiveCouponAsync(int limit = 3)
        {
            List<Promotion> promotions = await _commonRepository.GetReceiveCouponAsync(limit, (int)GlobalEnums.PromotionType.Coupon);
            return ObjectMapper.Map<List<Promotion>, List<PromotionDto>>(promotions);
        }

        /// <summary>
        /// 获取团购秒杀数据
        /// </summary>
        /// <param name="promotionId">促销ID</param>
        /// <param name="nowDate">当前日期</param>
        /// <param name="isEnable">是否启用</param>
        /// <returns></returns>
        public async Task<PromotionDto> GetGroupPurchaseAsync(Guid promotionId, DateTime nowDate, bool isEnable = true)
        {
            Promotion promotions = await _commonRepository.GetGroupPurchaseAsync(promotionId, nowDate, isEnable);
            return ObjectMapper.Map<Promotion, PromotionDto>(promotions);
        }

        /// <summary>
        /// 根据商品ID获取拼团信息
        /// </summary>
        /// <param name="goodsId">商品ID</param>
        /// <returns></returns>
        public async Task<PinTuanGoodDto> GetPinTuanGoodsAsync(Guid goodsId)
        {
            PinTuanGood pinTuanGoods = await _commonRepository.GetPinTuanGoodsAsync(goodsId);
            return ObjectMapper.Map<PinTuanGood, PinTuanGoodDto>(pinTuanGoods);
        }

        /// <summary>
        /// 获取启用的全局促销
        /// </summary>
        /// <param name="isEnable">是否启用</param>
        /// <returns></returns>
        public async Task<List<PromotionDto>> GetPromotionGlobalIsEnableAsync(bool isEnable = true, bool includeDetails = true)
        {
            List<Promotion> promotions = await _promotionCommonRepository.GetPromotionGlobalIsEnableAsync(isEnable, includeDetails);
            return ObjectMapper.Map<List<Promotion>, List<PromotionDto>>(promotions);
        }

        /// <summary>
        /// 获取促销详情
        /// </summary>
        /// <param name="ids">ID</param>
        /// <returns></returns>
        public async Task<List<PromotionDto>> GetPromotionByIdsAsync(Guid[] ids, bool includeDetails = true)
        {
            List<Promotion> promotions = await _promotionRepository.GetPromotionByIdsAsync(ids, includeDetails);
            return ObjectMapper.Map<List<Promotion>, List<PromotionDto>>(promotions);
        }

        /// <summary>
        /// 获取促销详情
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public async Task<PromotionDto> GetPromotionByIdAsync(Guid id, bool includeDetails = true)
        {
            Promotion promotion = await _promotionRepository.GetPromotionByIdAsync(id, includeDetails);
            return ObjectMapper.Map<Promotion, PromotionDto>(promotion);
        }

        /// <summary>
        /// 获取用户优惠劵数量
        /// </summary>
        /// <param name="isUsed">是否使用</param>
        /// <returns></returns>
        public async Task<long> GetUserCouponCountAsync(bool? isUsed)
        {
            return (await _couponRepository.GetQueryableAsync())
                .Where(x => x.UserId == CurrentUser.Id)
                .WhereIf(isUsed.HasValue, x => x.IsUsed == isUsed)
                .Count();
        }
    }
}