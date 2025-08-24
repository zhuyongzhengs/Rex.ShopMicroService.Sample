using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Articles
{
    /// <summary>
    /// 文章分类服务接口
    /// </summary>
    public interface IArticleTypeAppService : ICrudAppService<ArticleTypeDto, Guid, PagedAndSortedResultRequestDto, ArticleTypeCreateDto, ArticleTypeUpdateDto>
    {
        /// <summary>
        /// 获取(分页)文章分类列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<ArticleTypeDto>> GetPageListAsync(GetArticleTypeInput input);
    }
}