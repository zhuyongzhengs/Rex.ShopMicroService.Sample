using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Rex.BaseService.Systems.AdminUsers
{
    /// <summary>
    /// 管理员用户服务接口
    /// </summary>
    public interface IAdminUserAppService : ICrudAppService<IdentityUserDto, Guid, PagedAndSortedResultRequestDto, IdentityUserCreateDto, IdentityUserUpdateDto>
    {
        /// <summary>
        /// 获取(分页) 管理员用户列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<IdentityUserDto>> GetPageListAsync(GetAdminUserInput input);
    }
}