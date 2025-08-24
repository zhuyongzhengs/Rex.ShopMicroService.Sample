using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.OrderService.Ships
{
    /// <summary>
    /// 配送方式服务接口
    /// </summary>
    public interface IShipAppService : ICrudAppService<ShipDto, Guid, PagedAndSortedResultRequestDto, ShipCreateDto, ShipUpdateDto>
    {
        /// <summary>
        /// 获取(分页)配送方式列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<ShipDto>> GetPageListAsync(GetShipInput input);

        /// <summary>
        /// 获取配送方式重量
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetShipUnitsAsync();

        /// <summary>
        /// 更新默认配送方式
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isDefault">是否默认</param>
        /// <returns></returns>
        Task UpdateIsDefaultAsync(Guid id, bool isDefault);

        /// <summary>
        /// 更新是否包邮
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isFreePostage">是否包邮</param>
        /// <returns></returns>
        Task UpdateIsfreePostageAsync(Guid id, bool isFreePostage);
    }
}