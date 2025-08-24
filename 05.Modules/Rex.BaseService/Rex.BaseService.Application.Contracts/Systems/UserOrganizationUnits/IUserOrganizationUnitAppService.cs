using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Rex.BaseService.Systems.UserOrganizationUnits
{
    /// <summary>
    /// 组织单元【用户】服务接口
    /// </summary>
    public interface IUserOrganizationUnitAppService : IApplicationService
    {
        /// <summary>
        /// 获取组织单元【用户】信息
        /// </summary>
        /// <param name="filter">过滤筛选</param>
        /// <param name="maxResultCount">查询数量</param>
        /// <param name="skipCount">跳过数</param>
        /// <param name="organizationUnitId">组织单元ID</param>
        /// <param name="sorting">排序</param>
        /// <returns></returns>
        Task<PagedResultDto<UserOrganizationUnitDto>> GetListAsync(
            string? filter,
            int maxResultCount,
            int skipCount,
            Guid? organizationUnitId = null,
            string sorting = null
        );

        /// <summary>
        /// 添加组织单元【用户】信息
        /// </summary>
        /// <param name="input">用户组织单元</param>
        /// <returns></returns>
        public Task CreateAsync(UserOrganizationUnitCreateDto input);

        /// <summary>
        /// 批量添加组织单元【用户】信息
        /// </summary>
        /// <param name="input">用户组织单元</param>
        /// <returns></returns>
        public Task CreateManyAsync(List<UserOrganizationUnitCreateDto> input);

        /// <summary>
        /// 删除组织单元【用户】信息
        /// </summary>
        /// <param name="organizationUnitId">组织单位ID</param>
        /// <param name="userIds">用户ID</param>
        /// <returns></returns>
        public Task DeleteByOuIdAsync(Guid organizationUnitId, List<Guid> userIds);

        /// <summary>
        /// 选择组织单元用户
        /// </summary>
        /// <returns></returns>
        Task<PagedResultDto<IdentityUserDto>> GetSelectUserAsync(
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null,
            bool includeDetails = false,
            Guid? roleId = null,
            Guid? organizationUnitId = null,
            string userName = null,
            string phoneNumber = null,
            string emailAddress = null,
            string name = null,
            string surname = null
        );
    }
}