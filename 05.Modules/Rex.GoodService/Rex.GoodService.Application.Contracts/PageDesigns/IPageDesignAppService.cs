using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.PageDesigns
{
    /// <summary>
    /// 页面设计服务接口
    /// </summary>
    public interface IPageDesignAppService : ICrudAppService<PageDesignDto, Guid, PagedAndSortedResultRequestDto, PageDesignCreateDto, PageDesignUpdateDto>
    {
        /// <summary>
        /// 获取(分页)页面设计列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<PageDesignDto>> GetPageListAsync(GetPageDesignInput input);

        /// <summary>
        /// 获取版面设计数据
        /// </summary>
        /// <param name="pageDesignId">页面设计ID</param>
        /// <returns></returns>
        Task<LayoutDesignDto> GetLayoutDesignAsync(Guid pageDesignId);

        /// <summary>
        /// 修改是否默认
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="type">是否默认：1、是  2、否</param>
        /// <returns></returns>
        Task UpdateIsTypeAsync(Guid id, int type);
    }
}