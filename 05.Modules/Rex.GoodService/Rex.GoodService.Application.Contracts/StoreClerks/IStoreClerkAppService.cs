using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.StoreClerks
{
    /// <summary>
    /// 店铺店员关联服务接口
    /// </summary>
    public interface IStoreClerkAppService : ICrudAppService<StoreClerkDto, Guid, PagedAndSortedResultRequestDto, StoreClerkCreateDto, StoreClerkUpdateDto>
    {
        /// <summary>
        /// 获取(分页)店铺店员关联列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<StoreClerkDto>> GetPageListAsync(GetStoreClerkInput input);

        /// <summary>
        /// 删除店员信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <returns></returns>
        Task DeleteManyAsync(List<Guid> ids);
    }
}