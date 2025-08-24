using Rex.PromotionService.PinTuans;
using Rex.PromotionService.Promotions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Rex.PromotionService.Commons
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
        /// 获取可领取的优惠劵
        /// </summary>
        /// <param name="limit">优惠劵数量，默认：3</param>
        /// <returns></returns>
        Task<List<PromotionDto>> GetReceiveCouponAsync(int limit = 3);

        /// <summary>
        /// 获取团购秒杀数据
        /// </summary>
        /// <param name="promotionId">促销ID</param>
        /// <param name="nowDate">当前日期</param>
        /// <param name="isEnable">是否启用</param>
        /// <returns></returns>
        Task<PromotionDto> GetGroupPurchaseAsync(Guid promotionId, DateTime nowDate, bool isEnable = true);

        /// <summary>
        /// 根据商品ID获取拼团信息
        /// </summary>
        /// <param name="goodsId">商品ID</param>
        /// <returns></returns>
        Task<PinTuanGoodDto> GetPinTuanGoodsAsync(Guid goodsId);

        /// <summary>
        /// 获取启用的全局促销
        /// </summary>
        /// <param name="isEnable">是否启用</param>
        /// <returns></returns>
        Task<List<PromotionDto>> GetPromotionGlobalIsEnableAsync(bool isEnable = true, bool includeDetails = true);

        /// <summary>
        /// 获取促销详情
        /// </summary>
        /// <param name="ids">ID</param>
        /// <returns></returns>
        Task<List<PromotionDto>> GetPromotionByIdsAsync(Guid[] ids, bool includeDetails = true);

        /// <summary>
        /// 获取促销详情
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<PromotionDto> GetPromotionByIdAsync(Guid id, bool includeDetails = true);

        /// <summary>
        /// 获取用户优惠劵数量
        /// </summary>
        /// <param name="isUsed">是否使用</param>
        /// <returns></returns>
        Task<long> GetUserCouponCountAsync(bool? isUsed);
    }
}