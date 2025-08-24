using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品分类服务
    /// </summary>
    [RemoteService]
    [Authorize]
    public class GoodCategoryAppService : CrudAppService<GoodCategory, GoodCategoryDto, Guid, PagedAndSortedResultRequestDto, GoodCategoryCreateDto, GoodCategoryUpdateDto>, IGoodCategoryAppService
    {
        private readonly IGoodCategoryRepository _goodCategoryRepository;

        public GoodCategoryAppService(IGoodCategoryRepository repository) : base(repository)
        {
            _goodCategoryRepository = repository;
        }

        /// <summary>
        /// 获取商品分类列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<List<GoodCategoryDto>> GetManyAsync(GetGoodCategoryInput input)
        {
            List<GoodCategory> goodCategoryList = (await _goodCategoryRepository.GetQueryableAsync())
                    .WhereIf(!input.Name.IsNullOrWhiteSpace(), p => p.Name.Contains(input.Name))
                    .WhereIf(input.IsShow.HasValue, p => p.IsShow == input.IsShow.Value)
                    .OrderByIf<GoodCategory, IQueryable<GoodCategory>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                    .PageBy(input.SkipCount, input.MaxResultCount)
                    .ToList();

            return ObjectMapper.Map<List<GoodCategory>, List<GoodCategoryDto>>(goodCategoryList);
        }

        /// <summary>
        /// 查询(显示)商品分类
        /// </summary>
        /// <param name="isShow">是否显示</param>
        /// <returns></returns>
        public async Task<List<GoodCategoryDto>> GetShowAsync(bool? isShow = null)
        {
            List<GoodCategory> goodCategoryList = (await _goodCategoryRepository.GetQueryableAsync())
                    .WhereIf(isShow.HasValue, p => p.IsShow == isShow.Value).OrderBy(p => p.Sort).ToList();
            return ObjectMapper.Map<List<GoodCategory>, List<GoodCategoryDto>>(goodCategoryList);
        }

        /// <summary>
        /// 修改品牌是否显示
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isShow">是否显示</param>
        /// <returns></returns>
        public async Task UpdateIsShowAsync(Guid id, bool isShow)
        {
            GoodCategory goodCategory = await _goodCategoryRepository.GetAsync(id);
            if (goodCategory != null)
            {
                goodCategory.IsShow = isShow;
            }
        }

        /// <summary>
        /// 获取树形商品分类
        /// </summary>
        /// <returns></returns>
        public async Task<List<GoodCategoryTreeDto>> GetTreeAsync()
        {
            return await GetTreeAsync(null);
        }

        /// <summary>
        /// 获取树形商品分类
        /// </summary>
        /// <param name="parentId">上级分类ID</param>
        /// <returns></returns>
        public async Task<List<GoodCategoryTreeDto>> GetTreeAsync(Guid? parentId = null)
        {
            Expression<Func<GoodCategory, bool>> categoryExp = p => p.IsShow;
            if (parentId.HasValue)
                categoryExp = categoryExp.And(p => p.ParentId == parentId);
            List<GoodCategory> goodCategoryList = (await _goodCategoryRepository.GetListAsync(categoryExp)).OrderBy(p => p.Sort).ToList();
            List<GoodCategory> goodCategoryRoot = goodCategoryList.Where(p => p.ParentId == null).OrderBy(p => p.Sort).ToList();
            return LoadRootGoodCategorysTree(goodCategoryRoot, goodCategoryList);
        }

        /// <summary>
        /// 加载树形商品分类
        /// </summary>
        /// <param name="roots">(根)商品分类</param>
        /// <param name="goodCategorys">商品分类</param>
        /// <returns></returns>
        private List<GoodCategoryTreeDto> LoadRootGoodCategorysTree(List<GoodCategory> goodCategoryRoot, List<GoodCategory> goodCategoryList)
        {
            List<GoodCategoryTreeDto> resultGoodCategory = new List<GoodCategoryTreeDto>();
            foreach (var gcRoot in goodCategoryRoot)
            {
                GoodCategoryTreeDto goodCategoryTree = ObjectMapper.Map<GoodCategory, GoodCategoryTreeDto>(gcRoot);
                if (goodCategoryList.Where(p => p.ParentId == gcRoot.Id).Any())
                {
                    goodCategoryTree.Children = LoadRootGoodCategorysTree(goodCategoryList.Where(m => m.ParentId == gcRoot.Id).OrderBy(o => o.Sort).ToList(), goodCategoryList);
                }
                resultGoodCategory.Add(goodCategoryTree);
            }
            return resultGoodCategory;
        }
    }
}