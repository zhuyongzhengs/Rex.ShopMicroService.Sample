using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品参数服务接口
    /// </summary>
    public interface IGoodParamAppService : ICrudAppService<GoodParamDto, Guid, PagedAndSortedResultRequestDto, GoodParamCreateDto, GoodParamUpdateDto>
    {
        /// <summary>
        /// 获取(分页)商品参数[模型]列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<GoodParamDto>> GetPageListAsync(GetGoodParamInput input);

        /// <summary>
        /// 根据ID获取商品参数[模型]信息
        /// </summary>
        /// <param name="ids">商品参数[模型]ID</param>
        /// <returns></returns>
        Task<List<GoodParamDto>> GetManyByIdsAsync(List<Guid> ids);

        /// <summary>
        /// 查询商品参数类型
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetTypesAsync();
    }
}