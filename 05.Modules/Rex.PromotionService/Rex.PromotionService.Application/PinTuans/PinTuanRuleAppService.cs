using Microsoft.AspNetCore.Authorization;
using Rex.PinTuanGoodService.PinTuans;
using Rex.Service.Permission.PromotionServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 拼团规则服务
    /// </summary>
    [RemoteService]
    [Authorize(PromotionServicePermissions.PinTuanRules.Default)]
    public class PinTuanRuleAppService : CrudAppService<PinTuanRule, PinTuanRuleDto, Guid, PagedAndSortedResultRequestDto, PinTuanRuleCreateDto, PinTuanRuleUpdateDto>, IPinTuanRuleAppService
    {
        public IRepository<PinTuanGood> PinTuanGoodRepositoty { get; set; }
        private readonly IPinTuanRuleRepository _pinTuanRuleRepository;

        public PinTuanRuleAppService(IPinTuanRuleRepository repository) : base(repository)
        {
            _pinTuanRuleRepository = repository;
        }

        /// <summary>
        /// 获取(分页)拼团列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<PinTuanRuleDto>> GetPageListAsync(GetPinTuanRuleInput input)
        {
            // 查询数量
            long totalCount = await _pinTuanRuleRepository.GetPageCountAsync(input.Name, input.StartTime, input.EndTime, input.IsStatusOpen);
            if (totalCount < 1)
            {
                return new PagedResultDto<PinTuanRuleDto>();
            }

            // 查询数据列表
            List<PinTuanRule> pinTuanRuleList = await _pinTuanRuleRepository.GetPageListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Name, input.StartTime, input.EndTime, input.IsStatusOpen);
            return new PagedResultDto<PinTuanRuleDto>(
                totalCount,
                ObjectMapper.Map<List<PinTuanRule>, List<PinTuanRuleDto>>(pinTuanRuleList)
            );
        }

        /// <summary>
        /// 添加拼团规则
        /// </summary>
        /// <param name="input">拼团规则Dto</param>
        /// <returns></returns>
        public override async Task<PinTuanRuleDto> CreateAsync(PinTuanRuleCreateDto input)
        {
            PinTuanRule pinTuanRule = ObjectMapper.Map<PinTuanRuleCreateDto, PinTuanRule>(input);
            pinTuanRule = await _pinTuanRuleRepository.InsertAsync(pinTuanRule);
            List<PinTuanGood> pinTuanGoods = input.GoodIds.Select(goodId => new PinTuanGood() { GoodId = goodId, RuleId = pinTuanRule.Id }).ToList();
            if (pinTuanGoods.Any())
            {
                await PinTuanGoodRepositoty.InsertManyAsync(pinTuanGoods);
            }
            return ObjectMapper.Map<PinTuanRule, PinTuanRuleDto>(pinTuanRule);
        }

        /// <summary>
        /// 修改拼团规则
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="input">修改拼团Dto</param>
        /// <returns></returns>
        public override async Task<PinTuanRuleDto> UpdateAsync(Guid id, PinTuanRuleUpdateDto input)
        {
            /* 删除拼团商品关联 */
            if (input.GoodIds.Any())
            {
                await PinTuanGoodRepositoty.DeleteAsync(p => p.RuleId == id);
                List<PinTuanGood> pinTuanGoods = input.GoodIds.Select(goodId => new PinTuanGood() { GoodId = goodId, RuleId = id }).ToList();
                await PinTuanGoodRepositoty.InsertManyAsync(pinTuanGoods);
            }
            return await base.UpdateAsync(id, input);
        }

        /// <summary>
        /// 修改是否开启
        /// </summary>
        /// <param name="pinTuanRuleId">拼团规则ID</param>
        /// <param name="isStatusOpen">是否开启</param>
        /// <returns></returns>
        public async Task UpdateIsStatusOpenAsync(Guid pinTuanRuleId, bool isStatusOpen)
        {
            PinTuanRule pinTuanRule = await _pinTuanRuleRepository.GetAsync(pinTuanRuleId);
            if (pinTuanRule != null)
            {
                pinTuanRule.IsStatusOpen = isStatusOpen;
            }
        }

        /// <summary>
        /// 根据拼团规则ID获取拼团商品
        /// </summary>
        /// <param name="ruleIds">拼团规则ID</param>
        /// <returns></returns>
        public async Task<List<PinTuanGoodDto>> GetPinTuanGoodByRuleIdsAsync(Guid[] ruleIds)
        {
            List<PinTuanGood> pinTuanGoods = await _pinTuanRuleRepository.GetPinTuanGoodByRuleIdsAsync(ruleIds);
            return ObjectMapper.Map<List<PinTuanGood>, List<PinTuanGoodDto>>(pinTuanGoods);
        }
    }
}