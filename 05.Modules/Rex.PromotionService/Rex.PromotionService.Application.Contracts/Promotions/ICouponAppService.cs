using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 优惠劵服务接口
    /// </summary>
    public interface ICouponAppService : IApplicationService
    {
        /// <summary>
        /// 根据[使用者ID]获取优惠劵
        /// </summary>
        /// <param name="usedId">使用者ID</param>
        /// <returns></returns>
        Task<List<CouponDto>> GetCouponByUsedIdAsync(Guid usedId);

        /// <summary>
        /// 获取(分页)优惠劵列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<CouponDto>> GetPageListAsync(GetCouponInput input);

        /// <summary>
        /// 获取用户优惠券
        /// </summary>
        /// <param name="isUsed">是否使用</param>
        /// <returns></returns>
        Task<List<CouponDto>> GetUserCouponAsync(bool isUsed = false);

        /// <summary>
        /// 获取用户优惠券列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<CouponDto>> GetUserCouponListAsync(GetUserCouponInput input);
    }
}