using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Notices
{
    /// <summary>
    /// 公告服务接口
    /// </summary>
    public interface INoticeAppService : ICrudAppService<NoticeDto, Guid, PagedAndSortedResultRequestDto, NoticeCreateDto, NoticeUpdateDto>
    {
        /// <summary>
        /// 获取(分页)公告列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<NoticeDto>> GetPageListAsync(GetNoticeInput input);
    }
}