using Microsoft.AspNetCore.Authorization;
using Rex.Service.Permission.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Rex.BaseService.Systems.UserOrganizationUnits
{
    /// <summary>
    /// 组织单元【用户】服务
    /// </summary>
    [RemoteService]
    [Authorize(BaseServicePermissions.OrganizationUnits.ManagingUser)]
    public class UserOrganizationUnitAppService : ApplicationService, IUserOrganizationUnitAppService
    {
        public IRepository<IdentityUserOrganizationUnit> IdentityUserOrganizationUnitRepository { get; set; }
        public IIdentityUserRepository IdentityUserRepository { get; set; }
        private readonly IUserOrganizationUnitRepository _userOrganizationUnitRepository;

        public UserOrganizationUnitAppService(IUserOrganizationUnitRepository userOrganizationUnitRepository)
        {
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
        }

        /// <summary>
        /// 获取组织单元【用户】信息
        /// </summary>
        /// <param name="filter">过滤筛选</param>
        /// <param name="maxResultCount">查询数量</param>
        /// <param name="skipCount">跳过数</param>
        /// <param name="organizationUnitId">组织单元ID</param>
        /// <param name="sorting">排序</param>
        /// <returns></returns>
        public async Task<PagedResultDto<UserOrganizationUnitDto>> GetListAsync(string? filter, int maxResultCount, int skipCount, Guid? organizationUnitId = null, string sorting = null)
        {
            long totalCount = await _userOrganizationUnitRepository.GetListCountAsync(filter, organizationUnitId);
            if (totalCount < 1) return new PagedResultDto<UserOrganizationUnitDto>();

            List<IdentityUserOrganizationUnit> userOrganizationUnitList = await _userOrganizationUnitRepository.GetListAsync(filter, maxResultCount, skipCount, organizationUnitId, sorting);
            List<Guid> userIds = userOrganizationUnitList.Select(p => p.UserId).ToList();
            List<IdentityUser> identityUsers = await IdentityUserRepository.GetListByIdsAsync(userIds);

            List<UserOrganizationUnitDto> userOuList = ObjectMapper.Map<List<IdentityUserOrganizationUnit>, List<UserOrganizationUnitDto>>(userOrganizationUnitList);
            foreach (var userOu in userOuList)
            {
                userOu.UserName = identityUsers.Where(u => u.Id == userOu.UserId).FirstOrDefault()?.UserName;
            }
            return new PagedResultDto<UserOrganizationUnitDto>(totalCount, userOuList);
        }

        /// <summary>
        /// 创建组织单元【用户】信息
        /// </summary>
        /// <param name="input">用户组织单元</param>
        /// <returns></returns>
        public async Task CreateAsync(UserOrganizationUnitCreateDto input)
        {
            IdentityUserOrganizationUnit userOrganizationUnit = new IdentityUserOrganizationUnit(input.UserId, input.OrganizationUnitId, CurrentTenant.Id);
            if (userOrganizationUnit != null) await IdentityUserOrganizationUnitRepository.InsertAsync(userOrganizationUnit);
        }

        /// <summary>
        /// 批量创建组织单元【用户】信息
        /// </summary>
        /// <param name="input">用户组织单元</param>
        /// <returns></returns>
        public async Task CreateManyAsync(List<UserOrganizationUnitCreateDto> input)
        {
            List<IdentityUserOrganizationUnit> userOrganizationUnitList = new List<IdentityUserOrganizationUnit>();
            foreach (var uouItem in input)
            {
                userOrganizationUnitList.Add(new IdentityUserOrganizationUnit(uouItem.UserId, uouItem.OrganizationUnitId, CurrentTenant.Id));
            }
            if (userOrganizationUnitList.Count > 0) await IdentityUserOrganizationUnitRepository.InsertManyAsync(userOrganizationUnitList);
        }

        /// <summary>
        /// 删除组织单元【用户】信息
        /// </summary>
        /// <param name="organizationUnitId">组织单位ID</param>
        /// <param name="userIds">用户ID</param>
        /// <returns></returns>
        public async Task DeleteByOuIdAsync(Guid organizationUnitId, List<Guid> userIds)
        {
            await IdentityUserOrganizationUnitRepository.DeleteAsync(p => p.OrganizationUnitId == organizationUnitId && userIds.Contains(p.UserId));
        }

        /// <summary>
        /// 选择组织单元用户
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResultDto<IdentityUserDto>> GetSelectUserAsync(string sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, string filter = null, bool includeDetails = false, Guid? roleId = null, Guid? organizationUnitId = null, string userName = null, string phoneNumber = null, string emailAddress = null, string name = null, string surname = null)
        {
            List<Guid> noUserIds = new List<Guid>();
            if (organizationUnitId.HasValue)
            {
                List<IdentityUserOrganizationUnit> userOrganizationUnitList = await _userOrganizationUnitRepository.GetListAsync(p => p.OrganizationUnitId == organizationUnitId.Value);
                if (userOrganizationUnitList.Count > 0)
                {
                    noUserIds = userOrganizationUnitList.Select(p => p.UserId).ToList();
                }
            }

            long totalCount = await _userOrganizationUnitRepository.GetSelectUserCountAsync(noUserIds, filter);
            if (totalCount < 1) return new PagedResultDto<IdentityUserDto>();

            List<IdentityUser> identityUserList = await _userOrganizationUnitRepository.GetSelectUserListAsync(noUserIds, sorting, maxResultCount, skipCount, filter);
            List<IdentityUserDto> userList = ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(identityUserList);
            return new PagedResultDto<IdentityUserDto>(totalCount, userList);
        }
    }
}