using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rex.PromotionService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 优惠劵表仓储
    /// </summary>
    public class CouponRepository : EfCoreRepository<PromotionServiceDbContext, Coupon, Guid>, ICouponRepository
    {
        public PromotionServiceDbContext sServiceDbContext { get; set; }

        public CouponRepository(IDbContextProvider<PromotionServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取用户优惠券
        /// </summary>
        /// <param name="isUsed">是否使用</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public async Task<List<Coupon>> GetUserCouponAsync(bool isUsed, Guid userId, CancellationToken cancellationToken = default)
        {
            DateTime now = DateTime.Now;
            return await (await GetDbSetAsync())
                .Where(x => x.IsUsed == isUsed && x.UserId == userId && x.EndTime > now)
                .WhereIf(isUsed == false, x => x.EndTime > now)
                .Include(x => x.Promotion).ThenInclude(x => x.PromotionConditions)
                .Include(x => x.Promotion).ThenInclude(x => x.PromotionResults)
                .OrderBy(x => x.Promotion.Weight)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 更新优惠券使用情况
        /// </summary>
        /// <param name="couponCode">优惠劵码</param>
        /// <param name="usedId">使用者</param>
        /// <returns></returns>
        public async Task UpdateUsedCouponAsync(string[] couponCode, Guid? usedId)
        {
            List<Coupon> coupons = await (await GetDbSetAsync())
                .Where(x => couponCode.Contains(x.CouponCode))
                .ToListAsync();
            foreach (Coupon coupon in coupons)
            {
                coupon.IsUsed = usedId.HasValue;
                coupon.UsedId = usedId;
            }
        }

        /// <summary>
        /// 获取用户优惠券数量
        /// </summary>
        /// <param name="status">状态：1.未使用、2.已使用、3.已失效</param>
        /// <param name="userId">用户ID</param>
        /// <param name="couponCode">劵码</param>
        /// <returns></returns>
        public async Task<long> GetUserCouponCountAsync(int? status, Guid userId, string? couponCode, CancellationToken cancellationToken = default)
        {
            DateTime now = DateTime.Now;
            return await (await GetDbSetAsync())
                .Where(x => x.UserId == userId)
                .WhereIf(!couponCode.IsNullOrWhiteSpace(), x => x.CouponCode == couponCode)
                .WhereIf(status.HasValue && status.Value == 1, x => x.IsUsed == false && x.EndTime > now)
                .WhereIf(status.HasValue && status.Value == 2, x => x.IsUsed == true)
                .WhereIf(status.HasValue && status.Value == 3, x => x.IsUsed == false && x.EndTime < now)
                .CountAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取用户优惠券列表
        /// </summary>
        /// <param name="status">状态：1.未使用、2.已使用、3.已失效</param>
        /// <param name="userId">用户ID</param>
        /// <param name="couponCode">劵码</param>
        /// <returns></returns>
        public async Task<List<Coupon>> GetUserCouponListAsync(int? status, Guid userId, string? couponCode, CancellationToken cancellationToken = default)
        {
            DateTime now = DateTime.Now;
            return await (await GetDbSetAsync())
                .Where(x => x.UserId == userId)
                .WhereIf(!couponCode.IsNullOrWhiteSpace(), x => x.CouponCode == couponCode)
                .WhereIf(status.HasValue && status.Value == 1, x => x.IsUsed == false && x.EndTime > now)
                .WhereIf(status.HasValue && status.Value == 2, x => x.IsUsed == true)
                .WhereIf(status.HasValue && status.Value == 3, x => x.IsUsed == false && x.EndTime < now)
                .Include(x => x.Promotion).ThenInclude(x => x.PromotionConditions)
                .Include(x => x.Promotion).ThenInclude(x => x.PromotionResults)
                .OrderBy(x => x.CreationTime)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}