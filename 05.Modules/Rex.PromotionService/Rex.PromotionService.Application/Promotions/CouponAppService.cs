using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 优惠劵服务
    /// </summary>
    [RemoteService]
    public class CouponAppService : ApplicationService, ICouponAppService
    {
        //private const int PromotionType = (int)GlobalEnums.PromotionType.Coupon;

        private readonly ICouponRepository _couponRepository;

        public CouponAppService(ICouponRepository repository)
        {
            _couponRepository = repository;
        }

        /// <summary>
        /// 根据[使用者ID]获取优惠劵
        /// </summary>
        /// <param name="usedId">使用者ID</param>
        /// <returns></returns>
        public async Task<List<CouponDto>> GetCouponByUsedIdAsync(Guid usedId)
        {
            List<Coupon> couponList = await _couponRepository.GetListAsync(p => p.UsedId == usedId);
            return ObjectMapper.Map<List<Coupon>, List<CouponDto>>(couponList);
        }

        /// <summary>
        /// 获取(分页)优惠劵列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<CouponDto>> GetPageListAsync(GetCouponInput input)
        {
            DateTime? startTime = null;
            DateTime? endTime = null;
            if (input.StartAndEndTime.Any())
            {
                startTime = input.StartAndEndTime[0];
                endTime = input.StartAndEndTime[1];
            }

            // 查询数量
            long totalCount = (await _couponRepository.GetQueryableAsync())
                .WhereIf(input.PromotionId.HasValue, p => p.PromotionId == input.PromotionId.Value)
                .WhereIf(!input.CouponCode.IsNullOrWhiteSpace(), p => p.CouponCode == input.CouponCode)
                .WhereIf(input.IsUsed.HasValue, p => p.IsUsed == input.IsUsed)
                .WhereIf(startTime.HasValue && endTime.HasValue, p => p.StartTime >= startTime.Value && p.EndTime <= endTime.Value)
                .LongCount();
            if (totalCount < 1)
            {
                return new PagedResultDto<CouponDto>();
            }

            // 查询数据列表
            List<Coupon> couponList = (await _couponRepository.GetQueryableAsync())
                    .WhereIf(input.PromotionId.HasValue, p => p.PromotionId == input.PromotionId.Value)
                    .WhereIf(!input.CouponCode.IsNullOrWhiteSpace(), p => p.CouponCode == input.CouponCode)
                    .WhereIf(input.IsUsed.HasValue, p => p.IsUsed == input.IsUsed)
                    .WhereIf(startTime.HasValue && endTime.HasValue, p => p.StartTime >= startTime.Value && p.EndTime <= endTime.Value)
                    .OrderByIf<Coupon, IQueryable<Coupon>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                    .PageBy(input.SkipCount, input.MaxResultCount)
                    .ToList();
            return new PagedResultDto<CouponDto>(
                totalCount,
                ObjectMapper.Map<List<Coupon>, List<CouponDto>>(couponList)
            );
        }

        /// <summary>
        /// 获取用户优惠券
        /// </summary>
        /// <param name="isUsed">是否使用</param>
        /// <returns></returns>
        public async Task<List<CouponDto>> GetUserCouponAsync(bool isUsed = false)
        {
            List<Coupon> couponList = await _couponRepository.GetUserCouponAsync(isUsed, CurrentUser.Id.Value);
            return ObjectMapper.Map<List<Coupon>, List<CouponDto>>(couponList);
        }

        /// <summary>
        /// 获取用户优惠券列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<CouponDto>> GetUserCouponListAsync(GetUserCouponInput input)
        {
            // 查询数量
            long totalCount = await _couponRepository.GetUserCouponCountAsync(input.Status, CurrentUser.Id.Value, input.CouponCode);
            if (totalCount < 1)
            {
                return new PagedResultDto<CouponDto>();
            }

            // 查询数据列表
            List<Coupon> couponList = await _couponRepository.GetUserCouponListAsync(input.Status, CurrentUser.Id.Value, input.CouponCode);
            return new PagedResultDto<CouponDto>(
                totalCount,
                ObjectMapper.Map<List<Coupon>, List<CouponDto>>(couponList)
            );
        }
    }
}