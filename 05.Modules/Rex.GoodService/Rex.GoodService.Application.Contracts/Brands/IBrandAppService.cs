using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Brands
{
    /// <summary>
    /// 品牌服务接口
    /// </summary>
    public interface IBrandAppService : ICrudAppService<BrandDto, Guid, PagedAndSortedResultRequestDto, BrandCreateDto, BrandUpdateDto>
    {
        /// <summary>
        /// 查询品牌
        /// </summary>
        /// <param name="isShow">是否显示</param>
        /// <returns></returns>
        Task<List<BrandDto>> GetShowAsync(bool? isShow = null);

        /// <summary>
        /// 修改品牌是否显示
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isShow">是否显示</param>
        /// <returns></returns>
        Task UpdateIsShowAsync(Guid id, bool isShow);

        /// <summary>
        /// 获取(分页)品牌列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<BrandDto>> GetPageListAsync(GetBrandInput input);
    }
}