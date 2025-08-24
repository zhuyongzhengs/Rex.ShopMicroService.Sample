using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Rex.BaseService.Systems.AdminUsers
{
    /// <summary>
    /// 管理员用户服务
    /// </summary>
    [RemoteService]
    [Authorize(IdentityPermissions.Users.Default)]
    public class AdminUserAppService : CrudAppService<IdentityUser, IdentityUserDto, Guid, PagedAndSortedResultRequestDto, IdentityUserCreateDto, IdentityUserUpdateDto>, IAdminUserAppService
    {
        private readonly IAdminUserRepository _adminUserRepository;

        public AdminUserAppService(IAdminUserRepository repository) : base(repository)
        {
            _adminUserRepository = repository;
        }

        /// <summary>
        /// 获取(分页) 管理员用户列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<IdentityUserDto>> GetPageListAsync(GetAdminUserInput input)
        {
            // 查询数量
            long totalCount = await _adminUserRepository.GetPageCountAsync(input.UserName);
            if (totalCount < 1)
            {
                return new PagedResultDto<IdentityUserDto>();
            }
            // 查询数据列表
            List<IdentityUser> adminUserList = await _adminUserRepository.GetPageListAsync(input.UserName, input.Sorting, input.MaxResultCount, input.SkipCount);
            return new PagedResultDto<IdentityUserDto>(
                totalCount,
                ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(adminUserList)
            );
        }
    }
}