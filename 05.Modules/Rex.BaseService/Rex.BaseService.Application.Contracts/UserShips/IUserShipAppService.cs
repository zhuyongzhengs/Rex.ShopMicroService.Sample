using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.BaseService.UserShips
{
    /// <summary>
    /// 用户(收货)地址服务接口
    /// </summary>
    public interface IUserShipAppService : ICrudAppService<UserShipDto, Guid, PagedAndSortedResultRequestDto, UserShipCreateDto, UserShipUpdateDto>
    {
        /// <summary>
        /// 获取默认的用户(收货)地址
        /// </summary>
        /// <returns></returns>
        Task<UserShipDto> GetDefaultAsync();
    }
}