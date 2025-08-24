using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 拼团规则服务接口
    /// </summary>
    public interface IPinTuanRuleAppService : ICrudAppService<PinTuanRuleDto, Guid, PagedAndSortedResultRequestDto, PinTuanRuleCreateDto, PinTuanRuleUpdateDto>
    {
        /// <summary>
        /// 获取(分页)拼团规则列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<PinTuanRuleDto>> GetPageListAsync(GetPinTuanRuleInput input);

        /// <summary>
        /// 修改是否开启
        /// </summary>
        /// <param name="pinTuanRuleId">拼团规则ID</param>
        /// <param name="isStatusOpen">是否开启</param>
        /// <returns></returns>
        Task UpdateIsStatusOpenAsync(Guid pinTuanRuleId, bool isStatusOpen);

        /// <summary>
        /// 根据拼团规则ID获取拼团商品
        /// </summary>
        /// <param name="ruleIds">拼团规则ID</param>
        /// <returns></returns>
        Task<List<PinTuanGoodDto>> GetPinTuanGoodByRuleIdsAsync(Guid[] ruleIds);
    }
}