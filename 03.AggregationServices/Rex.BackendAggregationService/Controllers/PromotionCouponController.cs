using Microsoft.AspNetCore.Mvc;
using Rex.BackendAggregationService.Core.Mappers;
using Rex.BackendAggregationService.Promotions;
using Rex.BaseService.Systems.SysUsers;
using Rex.PromotionService.Promotions;
using Volo.Abp.Application.Dtos;

namespace Rex.BackendAggregationService.Controllers
{
    /// <summary>
    /// 控制器
    /// </summary>
    [Route("api/backend/aggregation/promotion/coupon")]
    [ApiController]
    public class PromotionCouponController : ControllerBase
    {
        private const int PromotionType = 2;
        private readonly ICouponAppService _couponAppService;
        private readonly ISysUserAppService _sysUserAppService;

        public PromotionCouponController(ICouponAppService couponAppService, ISysUserAppService sysUserAppService)
        {
            _couponAppService = couponAppService;
            _sysUserAppService = sysUserAppService;
        }

        // <summary>
        /// 查询优惠劵列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetListAsync([FromQuery] GetCouponInput input)
        {
            PagedResultDto<CouponDto> pagedResult = await _couponAppService.GetPageListAsync(input);
            if (pagedResult.Items.Count == 0)
                return Ok(pagedResult);

            Guid[] userIds = pagedResult.Items.Select(x => x.UserId).Distinct().ToArray();
            List<SysUserDto> sysUsers = await _sysUserAppService.GetManyAsync(userIds);
            List<CouponManyDto> couponList = new List<CouponManyDto>();
            foreach (var coupon in pagedResult.Items)
            {
                CouponManyDto promotionMany = ObjectMapper.Map<CouponDto, CouponManyDto>(coupon);
                SysUserDto sysUser = sysUsers.FirstOrDefault(x => x.Id == coupon.UserId);
                if (sysUser != null)
                {
                    promotionMany.UserName = sysUser.Name;
                }
                couponList.Add(promotionMany);
            }

            return Ok(new PagedResultDto<CouponManyDto>()
            {
                TotalCount = pagedResult.TotalCount,
                Items = couponList
            });
        }
    }
}