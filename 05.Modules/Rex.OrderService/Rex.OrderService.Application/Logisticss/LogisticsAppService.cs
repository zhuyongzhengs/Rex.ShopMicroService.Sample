using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.OrderService.Logisticss
{
    /// <summary>
    /// 物流服务
    /// </summary>
    [RemoteService]
    public class LogisticsAppService : CrudAppService<Logistics, LogisticsDto, Guid, PagedAndSortedResultRequestDto, LogisticsCreateDto, LogisticsUpdateDto>, ILogisticsAppService
    {
        private readonly ILogisticsRepository _logisticsRepository;

        public LogisticsAppService(ILogisticsRepository repository) : base(repository)
        {
            _logisticsRepository = repository;
        }

        /// <summary>
        /// 查询物流列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public async Task<List<LogisticsDto>> GetFiltersAsync(string? keyword = "")
        {
            List<Logistics> logisticsList = (await _logisticsRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(keyword), x => x.LogiCode.Contains(keyword) || x.LogiName.Contains(keyword))
                .OrderBy(x => x.Sort)
                .ToList();
            return ObjectMapper.Map<List<Logistics>, List<LogisticsDto>>(logisticsList);
        }

        /// <summary>
        /// 获取(分页)物流列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<LogisticsDto>> GetPageListAsync(GetLogisticsInput input)
        {
            // 查询数量
            long totalCount = await _logisticsRepository.GetPageCountAsync(input.LogiCode, input.LogiName, input.Phone);
            if (totalCount < 1)
            {
                return new PagedResultDto<LogisticsDto>();
            }
            // 查询数据列表
            List<Logistics> logisticsList = await _logisticsRepository.GetPageListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.LogiCode, input.LogiName, input.Phone);
            return new PagedResultDto<LogisticsDto>(
                totalCount,
                ObjectMapper.Map<List<Logistics>, List<LogisticsDto>>(logisticsList)
            );
        }
    }
}