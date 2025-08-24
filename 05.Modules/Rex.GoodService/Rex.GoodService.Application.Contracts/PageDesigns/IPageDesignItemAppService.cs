using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.PageDesigns
{
    /// <summary>
    /// 页面设计项服务接口
    /// </summary>
    public interface IPageDesignItemAppService : ICrudAppService<PageDesignItemDto, Guid, PagedAndSortedResultRequestDto, PageDesignItemCreateDto, PageDesignItemUpdateDto>
    {
        /// <summary>
        /// 获取布局设计数据
        /// </summary>
        /// <param name="pageDesignId">设计ID</param>
        /// <returns></returns>
        Task<dynamic> GetLayoutAsync(Guid pageDesignId);

        /// <summary>
        /// 提交页面设计
        /// </summary>
        /// <param name="pageDesignId">页面设计ID</param>
        /// <param name="pageDesignItems">页面设计项</param>
        /// <returns></returns>
        Task SubmitAsync(Guid pageDesignId, List<PageDesignItemDto> pageDesignItems);
    }
}