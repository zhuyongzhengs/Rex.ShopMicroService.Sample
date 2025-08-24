using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.OrderService.Logisticss
{
    /// <summary>
    /// 物流服务接口
    /// </summary>
    public interface ILogisticsAppService : ICrudAppService<LogisticsDto, Guid, PagedAndSortedResultRequestDto, LogisticsCreateDto, LogisticsUpdateDto>
    {
        /// <summary>
        /// 获取(分页)物流列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<LogisticsDto>> GetPageListAsync(GetLogisticsInput input);

        /// <summary>
        /// 查询物流列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        Task<List<LogisticsDto>> GetFiltersAsync(string? keyword = "");
    }
}