using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Articles
{
    /// <summary>
    /// 文章服务接口
    /// </summary>
    public interface IArticleAppService : ICrudAppService<ArticleDto, Guid, PagedAndSortedResultRequestDto, ArticleCreateDto, ArticleUpdateDto>
    {
        /// <summary>
        /// 获取(分页)文章列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<ArticleDto>> GetPageListAsync(GetArticleInput input);

        /// <summary>
        /// 修改文章是否显示
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isPub">是否发布</param>
        /// <returns></returns>
        Task UpdateIsPubAsync(Guid id, bool isPub);
    }
}