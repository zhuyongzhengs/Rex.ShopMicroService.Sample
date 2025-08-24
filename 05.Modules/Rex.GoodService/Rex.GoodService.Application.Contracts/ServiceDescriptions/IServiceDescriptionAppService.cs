using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.ServiceDescriptions
{
    /// <summary>
    /// 商城服务描述服务接口
    /// </summary>
    public interface IServiceDescriptionAppService : ICrudAppService<ServiceDescriptionDto, Guid, PagedAndSortedResultRequestDto, ServiceDescriptionCreateDto, ServiceDescriptionUpdateDto>
    {
        /// <summary>
        /// 获取(分页)商城服务描述服务列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<ServiceDescriptionDto>> GetPageListAsync(GetServiceDescriptionInput input);

        /// <summary>
        /// 查询商城服务描述类型
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetNoteTypesAsync();

        /// <summary>
        /// 修改商城服务描述是否显示
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isShow">是否显示</param>
        /// <returns></returns>
        Task UpdateIsShowAsync(Guid id, bool isShow);
    }
}