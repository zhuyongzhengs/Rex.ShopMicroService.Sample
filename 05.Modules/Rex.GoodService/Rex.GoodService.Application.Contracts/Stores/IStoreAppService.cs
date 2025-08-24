using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Stores
{
    /// <summary>
    /// 门店服务接口
    /// </summary>
    public interface IStoreAppService : ICrudAppService<StoreDto, Guid, PagedAndSortedResultRequestDto, StoreCreateDto, StoreUpdateDto>
    {
        /// <summary>
        /// 获取(分页)门店列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<StoreDto>> GetPageListAsync(GetStoreInput input);

        /// <summary>
        /// 获取(所有)门店信息
        /// </summary>
        /// <param name="storeName">门店名称</param>
        /// <returns></returns>
        Task<List<StoreDto>> GetManyAsync(string storeName = "");
    }
}