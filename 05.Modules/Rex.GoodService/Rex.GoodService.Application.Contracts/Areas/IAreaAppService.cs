using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Areas
{
    /// <summary>
    /// 区域服务接口
    /// </summary>
    public interface IAreaAppService : ICrudAppService<AreaDto, long, PagedAndSortedResultRequestDto, AreaCreateDto, AreaUpdateDto>
    {
        /// <summary>
        /// 获取(分页)区域列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<AreaDto>> GetPageListAsync(GetAreaInput input);

        /// <summary>
        /// 获取全部的区域
        /// </summary>
        /// <returns></returns>
        Task<List<AreaDto>> GetManyAsync();

        /// <summary>
        /// 获取(树形)区域
        /// </summary>
        /// <returns></returns>
        [Obsolete("此方法已过时，请改用GetTreeAsync(long parentId, int? depth = null)。")]
        Task<List<AreaTreeDto>> GetTreeManyAsync();

        /// <summary>
        /// 获取(树形)区域
        /// </summary>
        /// <param name="parentId">区域父ID</param>
        /// <param name="depth">深度</param>
        /// <param name="activeId">选中的ID</param>
        /// <returns></returns>
        Task<List<AreaTreeDto>> GetTreeAsync(long parentId, int? depth = null, int? activeId = null);

        /// <summary>
        /// 获取选中的区域
        /// </summary>
        /// <param name="id">区域ID</param>
        /// <returns></returns>
        public Task<AreaTreeDto> GetTreeActiveAsync(long id);

        /// <summary>
        /// 获取选中的区域ID
        /// </summary>
        /// <param name="id">区域ID</param>
        /// <returns></returns>
        public Task<long[]> GetTreeActiveIdsAsync(long id);
    }
}