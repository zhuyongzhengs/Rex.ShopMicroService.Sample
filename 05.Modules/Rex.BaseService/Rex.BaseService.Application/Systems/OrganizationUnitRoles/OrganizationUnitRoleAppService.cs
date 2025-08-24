using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Rex.BaseService.Systems.OrganizationUnitRoles
{
    /// <summary>
    /// 组织单元【角色】服务
    /// </summary>
    [RemoteService]
    public class OrganizationUnitRoleAppService : ApplicationService, IOrganizationUnitRoleAppService
    {
        public IRepository<OrganizationUnitRole> OrganizationUnitRoleRepository { get; set; }
        public IIdentityRoleRepository IdentityRoleRepository { get; set; }
        private readonly IOrganizationUnitRoleRepository _roleOrganizationUnitRepository;

        public OrganizationUnitRoleAppService(IOrganizationUnitRoleRepository roleOrganizationUnitRepository)
        {
            _roleOrganizationUnitRepository = roleOrganizationUnitRepository;
        }

        /// <summary>
        /// 获取组织单元【角色】信息
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        public async Task<PagedResultDto<OrganizationUnitRoleDto>> GetListAsync(GetOrganizationUnitRoleInput input)
        {
            long totalCount = await _roleOrganizationUnitRepository.GetListCountAsync(input.Filter, input.OrganizationUnitId);
            if (totalCount < 1) return new PagedResultDto<OrganizationUnitRoleDto>();

            List<OrganizationUnitRole> roleOrganizationUnitList = await _roleOrganizationUnitRepository.GetListAsync(input.Filter, input.MaxResultCount, input.SkipCount, input.OrganizationUnitId, input.Sorting);
            List<Guid> roleIds = roleOrganizationUnitList.Select(p => p.RoleId).ToList();
            List<IdentityRole> identityRoles = await IdentityRoleRepository.GetListAsync(roleIds);

            List<OrganizationUnitRoleDto> roleOuList = ObjectMapper.Map<List<OrganizationUnitRole>, List<OrganizationUnitRoleDto>>(roleOrganizationUnitList);
            foreach (var roleOu in roleOuList)
            {
                roleOu.RoleName = identityRoles.Where(u => u.Id == roleOu.RoleId).FirstOrDefault().Name;
            }
            return new PagedResultDto<OrganizationUnitRoleDto>(totalCount, roleOuList); ;
        }

        /// <summary>
        /// 创建组织单元【角色】信息
        /// </summary>
        /// <param name="input">角色组织单元</param>
        /// <returns></returns>
        public async Task CreateAsync(OrganizationUnitRoleCreateDto input)
        {
            OrganizationUnitRole roleOrganizationUnit = new OrganizationUnitRole(input.RoleId, input.OrganizationUnitId, CurrentTenant.Id);
            if (roleOrganizationUnit != null) await OrganizationUnitRoleRepository.InsertAsync(roleOrganizationUnit);
        }

        /// <summary>
        /// 批量创建组织单元【角色】信息
        /// </summary>
        /// <param name="input">角色组织单元</param>
        /// <returns></returns>
        public async Task CreateManyAsync(List<OrganizationUnitRoleCreateDto> input)
        {
            List<OrganizationUnitRole> roleOrganizationUnitList = new List<OrganizationUnitRole>();
            foreach (var ourItem in input)
            {
                roleOrganizationUnitList.Add(new OrganizationUnitRole(ourItem.RoleId, ourItem.OrganizationUnitId, CurrentTenant.Id));
            }
            if (roleOrganizationUnitList.Count > 0) await OrganizationUnitRoleRepository.InsertManyAsync(roleOrganizationUnitList);
        }

        /// <summary>
        /// 删除组织单元【角色】信息
        /// </summary>
        /// <param name="organizationUnitId">组织单位ID</param>
        /// <param name="roleIds">角色ID</param>
        /// <returns></returns>
        public async Task DeleteRoleIdAsync(Guid organizationUnitId, List<Guid> roleIds)
        {
            await OrganizationUnitRoleRepository.DeleteAsync(p => p.OrganizationUnitId == organizationUnitId && roleIds.Contains(p.RoleId));
        }

        /// <summary>
        /// 选择组织单元角色
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResultDto<IdentityRoleDto>> GetListSelectRoleAsync(Guid? organizationUnitId, GetSelectRoleInput input)
        {
            List<Guid> noRoleIds = new List<Guid>();
            if (organizationUnitId.HasValue)
            {
                List<OrganizationUnitRole> organizationUnitRoleList = await _roleOrganizationUnitRepository.GetListAsync(p => p.OrganizationUnitId == organizationUnitId.Value);
                if (organizationUnitRoleList.Count > 0)
                {
                    noRoleIds = organizationUnitRoleList.Select(p => p.RoleId).ToList();
                }
            }
            long totalCount = await _roleOrganizationUnitRepository.GetSelectRoleCountAsync(noRoleIds, input.Filter);
            if (totalCount < 1) return new PagedResultDto<IdentityRoleDto>();

            List<IdentityRole> identityRoleList = await _roleOrganizationUnitRepository.GetSelectRoleListAsync(noRoleIds, input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter, input.IncludeDetails);
            List<IdentityRoleDto> roleList = ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(identityRoleList);
            return new PagedResultDto<IdentityRoleDto>(totalCount, roleList);
        }
    }
}