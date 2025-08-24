using Rex.GoodService.Products;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品类型规格服务接口
    /// </summary>
    public interface IGoodTypeSpecAppService : ICrudAppService<GoodTypeSpecDto, Guid, PagedAndSortedResultRequestDto, GoodTypeSpecCreateDto, GoodTypeSpecUpdateDto>
    {
        /// <summary>
        /// 获取(分页)商品类型规格列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<GoodTypeSpecDto>> GetPageListAsync(GetGoodTypeSpecInput input);

        /// <summary>
        /// 根据ID获取商品类型规格信息
        /// </summary>
        /// <param name="ids">商品类型规格ID</param>
        /// <returns></returns>
        Task<List<GoodTypeSpecDto>> GetManyByIdsAsync(List<Guid> ids);

        /// <summary>
        /// 生成商品类型规格(SKU)
        /// </summary>
        /// <param name="input">生成商品类型规格Dto</param>
        /// <returns></returns>
        Task<List<ProductDto>> CreateGenerateSpecAsync(List<GoodTypeSpecGenerateDto> input);
    }
}