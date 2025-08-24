using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rex.Service.Permission.GoodServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Articles
{
    /// <summary>
    /// 文章分类服务
    /// </summary>
    [RemoteService]
    [Authorize(GoodServicePermissions.ArticleTypes.Default)]
    public class ArticleTypeAppService : CrudAppService<ArticleType, ArticleTypeDto, Guid, PagedAndSortedResultRequestDto, ArticleTypeCreateDto, ArticleTypeUpdateDto>, IArticleTypeAppService
    {
        private readonly IArticleTypeRepository _articleTypeRepository;

        public ArticleTypeAppService(IArticleTypeRepository repository) : base(repository)
        {
            _articleTypeRepository = repository;
        }

        /// <summary>
        /// 获取(分页)文章列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<ArticleTypeDto>> GetPageListAsync(GetArticleTypeInput input)
        {
            // 查询数量
            long totalCount = (await _articleTypeRepository.GetQueryableAsync())
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), p => p.Name.Contains(input.Name))
                .LongCount();

            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<ArticleTypeDto>();
            }
            List<ArticleType> articleTypeList = (await _articleTypeRepository.GetQueryableAsync())
                    .WhereIf(!input.Name.IsNullOrWhiteSpace(), p => p.Name.Contains(input.Name))
                    .OrderByIf<ArticleType, IQueryable<ArticleType>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                    .PageBy(input.SkipCount, input.MaxResultCount)
                    .ToList();

            return new PagedResultDto<ArticleTypeDto>(
                totalCount,
                ObjectMapper.Map<List<ArticleType>, List<ArticleTypeDto>>(articleTypeList)
            );
        }

        /// <summary>
        /// 新增文章分类
        /// </summary>
        /// <param name="input">新增文章分类Dto</param>
        /// <returns></returns>
        [Authorize(GoodServicePermissions.ArticleTypes.Create)]
        public override Task<ArticleTypeDto> CreateAsync(ArticleTypeCreateDto input)
        {
            return base.CreateAsync(input);
        }

        /// <summary>
        /// 修改文章分类
        /// </summary>
        /// <returns></returns>
        [Authorize(GoodServicePermissions.ArticleTypes.Update)]
        public override async Task<ArticleTypeDto> UpdateAsync([FromRoute] Guid id, [FromBody] ArticleTypeUpdateDto input)
        {
            return await base.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除文章分类
        /// </summary>
        /// <returns></returns>
        [Authorize(GoodServicePermissions.ArticleTypes.Delete)]
        public override async Task DeleteAsync([FromRoute] Guid id)
        {
            await base.DeleteAsync(id);
        }

        /// <summary>
        /// 根据ID查询文章分类信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public override async Task<ArticleTypeDto> GetAsync([FromRoute] Guid id)
        {
            return await base.GetAsync(id);
        }
    }
}