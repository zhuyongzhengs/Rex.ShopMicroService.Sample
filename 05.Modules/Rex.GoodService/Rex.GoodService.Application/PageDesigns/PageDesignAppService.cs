using Microsoft.AspNetCore.Authorization;
using Rex.GoodService.Articles;
using Rex.GoodService.Brands;
using Rex.GoodService.Goods;
using Rex.Service.Core.Configurations;
using Rex.Service.Permission.GoodServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.PageDesigns
{
    /// <summary>
    /// 页面设计服务
    /// </summary>
    [RemoteService]
    [Authorize(GoodServicePermissions.PageDesigns.Default)]
    public class PageDesignAppService : CrudAppService<PageDesign, PageDesignDto, Guid, PagedAndSortedResultRequestDto, PageDesignCreateDto, PageDesignUpdateDto>, IPageDesignAppService
    {
        private readonly IPageDesignRepository _pageDesignRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IArticleTypeRepository _articleTypeRepository;
        public IGoodCategoryAppService GoodCategoryAppService { get; set; }

        public PageDesignAppService(IPageDesignRepository repository, IBrandRepository brandRepository, IArticleTypeRepository articleTypeRepository) : base(repository)
        {
            _pageDesignRepository = repository;
            _brandRepository = brandRepository;
            _articleTypeRepository = articleTypeRepository;
        }

        /// <summary>
        /// 获取(分页)页面设计列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<PageDesignDto>> GetPageListAsync(GetPageDesignInput input)
        {
            // 查询数量
            long totalCount = (await _pageDesignRepository.GetQueryableAsync())
                .WhereIf(!input.Code.IsNullOrWhiteSpace(), p => p.Code == input.Code)
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), p => p.Name.Contains(input.Name))
                .LongCount();

            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<PageDesignDto>();
            }
            List<PageDesign> pageDesignList = (await _pageDesignRepository.GetQueryableAsync())
                    .WhereIf(!input.Code.IsNullOrWhiteSpace(), p => p.Code == input.Code)
                    .WhereIf(!input.Name.IsNullOrWhiteSpace(), p => p.Name.Contains(input.Name))
                    .OrderByIf<PageDesign, IQueryable<PageDesign>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                    .PageBy(input.SkipCount, input.MaxResultCount)
                    .ToList();

            return new PagedResultDto<PageDesignDto>(
                totalCount,
                ObjectMapper.Map<List<PageDesign>, List<PageDesignDto>>(pageDesignList)
            );
        }

        /// <summary>
        /// 创建页面设计
        /// </summary>
        /// <param name="input">创建Dto</param>
        /// <returns></returns>
        [Authorize(GoodServicePermissions.PageDesigns.Create)]
        public override async Task<PageDesignDto> CreateAsync(PageDesignCreateDto input)
        {
            PageDesign pageDesign = await _pageDesignRepository.FindAsync(p => p.Code.Equals(input.Code));
            if (pageDesign != null)
                throw new UserFriendlyException($"设计编码[{input.Code}]已存在，请重新输入！", SystemStatusCode.Fail.ToGoodServicePrefix(), "设计编码唯一，不允许重复。").WithData("Code", input.Code);

            if (input.Type == 1)
            {
                List<PageDesign> pageDesigns = await _pageDesignRepository.GetListAsync(p => p.Type == input.Type);
                foreach (var pDesign in pageDesigns)
                {
                    pDesign.Type = 2;
                }
            }
            return await base.CreateAsync(input);
        }

        /// <summary>
        /// 修改页面设计
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="input">修改Dto</param>
        /// <returns></returns>
        [Authorize(GoodServicePermissions.PageDesigns.Update)]
        public override async Task<PageDesignDto> UpdateAsync(Guid id, PageDesignUpdateDto input)
        {
            PageDesign pageDesign = await _pageDesignRepository.FindAsync(p => p.Code.Equals(input.Code) && p.Id != id);
            if (pageDesign != null)
                throw new UserFriendlyException($"设计编码[{input.Code}]已存在，请重新输入！", SystemStatusCode.Fail.ToGoodServicePrefix(), "设计编码唯一，不允许重复。").WithData("Code", input.Code);
            if (input.Type == 1)
            {
                List<PageDesign> pageDesigns = await _pageDesignRepository.GetListAsync(p => p.Type == input.Type);
                foreach (var pDesign in pageDesigns)
                {
                    pDesign.Type = 2;
                }
            }
            return await base.UpdateAsync(id, input);
        }

        /// <summary>
        /// 修改是否默认
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="type">是否默认：1、是  2、否</param>
        /// <returns></returns>
        public async Task UpdateIsTypeAsync(Guid id, int type)
        {
            PageDesign pageDesign = await _pageDesignRepository.GetAsync(id);
            if (pageDesign != null)
            {
                pageDesign.Type = type;
                if (type == 1)
                {
                    List<PageDesign> pageDesigns = await _pageDesignRepository.GetListAsync(p => p.Type == type);
                    foreach (var pDesign in pageDesigns)
                    {
                        pDesign.Type = 2;
                    }
                }
            }
        }

        /// <summary>
        /// 获取版面设计数据
        /// </summary>
        /// <param name="pageDesignId">页面设计ID</param>
        /// <returns></returns>
        public async Task<LayoutDesignDto> GetLayoutDesignAsync(Guid pageDesignId)
        {
            PageDesign pageDesign = await _pageDesignRepository.GetAsync(pageDesignId);
            if (pageDesign == null)
                throw new UserFriendlyException($"不存在该页面设计(或已被删除)，请检查！", SystemStatusCode.Fail.ToGoodServicePrefix());
            // 品牌
            List<Brand> brands = (await _brandRepository.GetQueryableAsync()).OrderBy(p => p.Sort).Where(p => p.IsShow).ToList();
            // 商品分类
            List<GoodCategoryTreeDto> goodCategoryTrees = await GoodCategoryAppService.GetTreeAsync();
            // 文章分类
            List<ArticleType> articleTypes = (await _articleTypeRepository.GetQueryableAsync()).OrderBy(p => p.Sort).ToList();
            return new LayoutDesignDto()
            {
                PageDesign = ObjectMapper.Map<PageDesign, PageDesignDto>(pageDesign),
                Brands = ObjectMapper.Map<List<Brand>, List<BrandDto>>(brands),
                GoodCategoryTrees = goodCategoryTrees,
                ArticleTypes = ObjectMapper.Map<List<ArticleType>, List<ArticleTypeDto>>(articleTypes)
            };
        }
    }
}