using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品分类服务接口
    /// </summary>
    public interface IGoodCategoryAppService : ICrudAppService<GoodCategoryDto, Guid, PagedAndSortedResultRequestDto, GoodCategoryCreateDto, GoodCategoryUpdateDto>
    {
        /// <summary>
        /// 修改分类是否显示
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isShow">是否显示</param>
        /// <returns></returns>
        Task UpdateIsShowAsync(Guid id, bool isShow);

        /// <summary>
        /// 获取商品分类列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<List<GoodCategoryDto>> GetManyAsync(GetGoodCategoryInput input);

        /// <summary>
        /// 获取树形商品分类
        /// </summary>
        /// <returns></returns>
        //Task<List<GoodCategoryTreeDto>> GetTreeAsync();

        /// <summary>
        /// 获取树形商品分类
        /// </summary>
        /// <param name="parentId">上级分类ID</param>
        /// <returns></returns>
        Task<List<GoodCategoryTreeDto>> GetTreeAsync(Guid? parentId = null);

        /// <summary>
        /// 查询(显示)商品分类
        /// </summary>
        /// <param name="isShow">是否显示</param>
        /// <returns></returns>
        Task<List<GoodCategoryDto>> GetShowAsync(bool? isShow = null);
    }
}