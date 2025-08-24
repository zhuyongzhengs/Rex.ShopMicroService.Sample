using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 优惠劵仓储接口
    /// </summary>
    public interface ICouponRepository : IRepository<Coupon, Guid>
    {
        /// <summary>
        /// 更新优惠券使用情况
        /// </summary>
        /// <param name="couponCode">优惠劵码</param>
        /// <param name="usedId">使用者</param>
        /// <returns></returns>
        Task UpdateUsedCouponAsync(string[] couponCode, Guid? usedId);

        /// <summary>
        /// 获取用户优惠券
        /// </summary>
        /// <param name="isUsed">是否使用</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        Task<List<Coupon>> GetUserCouponAsync(bool isUsed, Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取用户优惠券数量
        /// </summary>
        /// <param name="status">状态：1.未使用、2.已使用、3.已失效</param>
        /// <param name="userId">用户ID</param>
        /// <param name="couponCode">劵码</param>
        /// <returns></returns>
        Task<long> GetUserCouponCountAsync(int? status, Guid userId, string? couponCode, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取用户优惠券列表
        /// </summary>
        /// <param name="status">状态：1.未使用、2.已使用、3.已失效</param>
        /// <param name="userId">用户ID</param>
        /// <param name="couponCode">劵码</param>
        /// <returns></returns>
        Task<List<Coupon>> GetUserCouponListAsync(int? status, Guid userId, string? couponCode, CancellationToken cancellationToken = default);
    }
}