using Microsoft.AspNetCore.Authorization;
using Rex.Service.Permission.GoodServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Notices
{
    /// <summary>
    /// 公告服务
    /// </summary>
    [RemoteService]
    [Authorize(GoodServicePermissions.Notices.Default)]
    public class NoticeAppService : CrudAppService<Notice, NoticeDto, Guid, PagedAndSortedResultRequestDto, NoticeCreateDto, NoticeUpdateDto>, INoticeAppService
    {
        private readonly INoticeRepository _noticeRepository;

        public NoticeAppService(INoticeRepository repository) : base(repository)
        {
            _noticeRepository = repository;
        }

        /// <summary>
        /// 获取(分页)公告列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<NoticeDto>> GetPageListAsync(GetNoticeInput input)
        {
            // 查询数量
            long totalCount = (await _noticeRepository.GetQueryableAsync())
                .WhereIf(!input.Title.IsNullOrWhiteSpace(), p => p.Title.Contains(input.Title))
                .LongCount();
            if (totalCount < 1)
            {
                return new PagedResultDto<NoticeDto>();
            }

            // 查询数据列表
            List<Notice> noticeList = (await _noticeRepository.GetQueryableAsync())
                    .WhereIf(!input.Title.IsNullOrWhiteSpace(), p => p.Title.Contains(input.Title))
                    .OrderByIf<Notice, IQueryable<Notice>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                    .PageBy(input.SkipCount, input.MaxResultCount)
                    .ToList();

            return new PagedResultDto<NoticeDto>(
                totalCount,
                ObjectMapper.Map<List<Notice>, List<NoticeDto>>(noticeList)
            );
        }
    }
}