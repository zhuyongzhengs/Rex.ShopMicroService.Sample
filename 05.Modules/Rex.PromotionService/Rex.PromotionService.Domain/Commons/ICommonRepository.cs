using Rex.PromotionService.PinTuans;
using Rex.PromotionService.Promotions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.PromotionService.Commons
{
    /// <summary>
    /// 公共仓储接口
    /// </summary>
    public interface ICommonRepository : IRepository<Promotion, Guid>
    {
        /// <summary>
        /// 获取可领取的优惠劵
        /// </summary>
        /// <param name="limit">优惠劵数量，默认：3</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        Task<List<Promotion>> GetReceiveCouponAsync(int limit = 3, int type = 2, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取团购秒杀数据
        /// </summary>
        /// <param name="promotionId">促销ID</param>
        /// <param name="nowDate">当前日期</param>
        /// <param name="isEnable">是否启用</param>
        /// <returns></returns>
        Task<Promotion> GetGroupPurchaseAsync(Guid promotionId, DateTime nowDate, bool isEnable = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据商品ID获取拼团信息
        /// </summary>
        /// <param name="goodsId">商品ID</param>
        /// <returns></returns>
        Task<PinTuanGood> GetPinTuanGoodsAsync(Guid goodsId, CancellationToken cancellationToken = default);
    }
}