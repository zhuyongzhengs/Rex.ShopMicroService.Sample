using Microsoft.AspNetCore.Authorization;
using Rex.Service.Permission.PromotionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 拼团记录服务
    /// </summary>
    [RemoteService]
    [Authorize(PromotionServicePermissions.PinTuanRecords.Default)]
    public class PinTuanRecordAppService : ApplicationService, IPinTuanRecordAppService
    {
        private readonly IPinTuanRecordRepository _pinTuanRecordRepository;

        public PinTuanRecordAppService(IPinTuanRecordRepository repository)
        {
            _pinTuanRecordRepository = repository;
        }

        /// <summary>
        /// 获取(分页)拼团列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<PinTuanRecordDto>> GetListAsync(GetPinTuanRecordInput input)
        {
            // 查询数量
            long totalCount = (await _pinTuanRecordRepository.GetQueryableAsync())
                .WhereIf(input.TeamId.HasValue, p => p.TeamId == input.TeamId)
                .WhereIf(input.Status.HasValue, p => p.Status == input.Status)
                .WhereIf(!input.OrderId.IsNullOrWhiteSpace(), p => p.OrderId == input.OrderId)
                .LongCount();

            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<PinTuanRecordDto>();
            }
            List<PinTuanRecord> pinTuanRecordList = (await _pinTuanRecordRepository.GetQueryableAsync())
                    .WhereIf(input.TeamId.HasValue, p => p.TeamId == input.TeamId)
                    .WhereIf(input.Status.HasValue, p => p.Status == input.Status)
                    .WhereIf(!input.OrderId.IsNullOrWhiteSpace(), p => p.OrderId == input.OrderId)
                    .OrderByIf<PinTuanRecord, IQueryable<PinTuanRecord>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                    .PageBy(input.SkipCount, input.MaxResultCount)
                    .ToList();

            return new PagedResultDto<PinTuanRecordDto>(
                totalCount,
                ObjectMapper.Map<List<PinTuanRecord>, List<PinTuanRecordDto>>(pinTuanRecordList)
            );
        }
    }
}