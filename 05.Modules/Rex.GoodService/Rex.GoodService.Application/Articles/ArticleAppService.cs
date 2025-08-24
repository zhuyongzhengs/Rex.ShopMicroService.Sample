using Microsoft.AspNetCore.Authorization;
using Rex.Service.Permission.GoodServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Articles
{
    /// <summary>
    /// 文章服务
    /// </summary>
    [RemoteService]
    [Authorize(GoodServicePermissions.Articles.Default)]
    public class ArticleAppService : CrudAppService<Article, ArticleDto, Guid, PagedAndSortedResultRequestDto, ArticleCreateDto, ArticleUpdateDto>, IArticleAppService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleAppService(IArticleRepository repository) : base(repository)
        {
            _articleRepository = repository;
        }

        /// <summary>
        /// 获取(分页)文章列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<ArticleDto>> GetPageListAsync(GetArticleInput input)
        {
            // 查询数量
            long totalCount = await _articleRepository.GetPageCountAsync(input.Title, input.TypeId, input.IsPub);
            if (totalCount < 1)
            {
                return new PagedResultDto<ArticleDto>();
            }
            // 查询数据列表
            List<Article> articleList = await _articleRepository.GetPageListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Title, input.TypeId, input.IsPub);
            return new PagedResultDto<ArticleDto>(
                totalCount,
                ObjectMapper.Map<List<Article>, List<ArticleDto>>(articleList)
            );
        }

        /// <summary>
        /// 修改文章是否显示
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isPub">是否发布</param>
        /// <returns></returns>
        public async Task UpdateIsPubAsync(Guid id, bool isPub)
        {
            Article article = await _articleRepository.GetAsync(id);
            if (article != null)
            {
                article.IsPub = isPub;
            }
        }
    }
}