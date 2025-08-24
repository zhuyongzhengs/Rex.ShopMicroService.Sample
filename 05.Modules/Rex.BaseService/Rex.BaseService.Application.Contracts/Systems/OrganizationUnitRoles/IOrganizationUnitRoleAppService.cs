using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Rex.BaseService.Systems.OrganizationUnitRoles
{
    /// <summary>
    /// 组织单元【角色】服务接口
    /// </summary>
    public interface IOrganizationUnitRoleAppService : IApplicationService
    {
        /// <summary>
        /// 获取组织单元【角色】信息
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        Task<PagedResultDto<OrganizationUnitRoleDto>> GetListAsync(
            GetOrganizationUnitRoleInput input
        );

        /// <summary>
        /// 添加组织单元【角色】信息
        /// </summary>
        /// <param name="input">角色组织单元</param>
        /// <returns></returns>
        public Task CreateAsync(OrganizationUnitRoleCreateDto input);

        /// <summary>
        /// 批量添加组织单元【角色】信息
        /// </summary>
        /// <param name="input">角色组织单元</param>
        /// <returns></returns>
        public Task CreateManyAsync(List<OrganizationUnitRoleCreateDto> input);

        /// <summary>
        /// 删除组织单元【角色】信息
        /// </summary>
        /// <param name="organizationUnitId">组织单位ID</param>
        /// <param name="roleIds">角色ID</param>
        /// <returns></returns>
        public Task DeleteRoleIdAsync(Guid organizationUnitId, List<Guid> roleIds);

        /// <summary>
        /// 选择组织单元角色
        /// </summary>
        /// <returns></returns>
        Task<PagedResultDto<IdentityRoleDto>> GetListSelectRoleAsync(
            Guid? organizationUnitId,
            GetSelectRoleInput input
        );
    }
}