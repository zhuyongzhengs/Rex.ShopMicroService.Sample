using Microsoft.AspNetCore.Authorization;
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
    /// 常用促销方法服务
    /// </summary>
    [RemoteService]
    [Authorize]
    public class PromotionCommonTagAppService : ApplicationService, IPromotionCommonTagAppService
    {
        private readonly IPromotionCommonRepository _promotionCommonRepository;

        public PromotionCommonTagAppService(IPromotionCommonRepository repository)
        {
            _promotionCommonRepository = repository;
        }

        /// <summary>
        /// 获取(分页) 常用促销列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<PromotionDto>> GetListAsync(GetPromotionCommonInput input)
        {
            DateTime nowDate = DateTime.Now;
            DateTime? startTime = null;
            DateTime? endTime = null;
            if (input.StartAndEndTime.Any())
            {
                startTime = input.StartAndEndTime[0];
                endTime = input.StartAndEndTime[1];
            }

            // 查询数量
            long totalCount = await _promotionCommonRepository.GetTagPromotionCountAsync(nowDate, input.Name, input.IsExclusive, startTime, endTime);
            if (totalCount < 1)
            {
                return new PagedResultDto<PromotionDto>();
            }
            // 查询数据列表
            List<Promotion> promotionList = await _promotionCommonRepository.GetTagPromotionAsync(nowDate, input.SkipCount, input.MaxResultCount, input.Sorting, input.Name, input.IsExclusive, startTime, endTime);
            return new PagedResultDto<PromotionDto>(
                totalCount,
                ObjectMapper.Map<List<Promotion>, List<PromotionDto>>(promotionList)
            );
        }
    }
}