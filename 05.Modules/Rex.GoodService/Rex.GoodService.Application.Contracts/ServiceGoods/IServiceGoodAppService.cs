using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.ServiceGoods
{
    /// <summary>
    /// 服务商品服务接口
    /// </summary>
    public interface IServiceGoodAppService : ICrudAppService<ServiceGoodDto, Guid, PagedAndSortedResultRequestDto, ServiceGoodCreateDto, ServiceGoodUpdateDto>
    {
        /// <summary>
        /// 获取(分页)服务商品列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<ServiceGoodDto>> GetPageListAsync(GetServiceGoodInput input);

        /// <summary>
        /// 获取项目状态
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetStatusAsync();

        /// <summary>
        /// 核销有效期类型
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetValidityTypesAsync();
    }
}