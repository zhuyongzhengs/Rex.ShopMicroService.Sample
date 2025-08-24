using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Rex.BaseService.Systems.SysUsers;
using Rex.BaseService.Systems.UserPointLogs;
using Rex.BaseService.Systems.UserWeChats;
using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Identity;

namespace Rex.BaseService.Systems.Commons
{
    /// <summary>
    /// 公共服务
    /// </summary>
    [RemoteService]
    public class CommonAppService : ApplicationService, ICommonAppService
    {
        private readonly ISysUserRepository _sysUserRepository;
        private readonly IUserWeChatRepository _userWeChatRepository;
        private readonly IUserPointLogRepository _userPointLogRepository;
        private readonly IdentityUserManager _userManager;

        public IDistributedCache<CurrentSysUserEto> CurrentSysUserDistributedCache { get; set; }

        public CommonAppService(ISysUserRepository repository, IUserWeChatRepository userWeChatRepository, IUserPointLogRepository userPointLogRepository, IdentityUserManager userManager)
        {
            _sysUserRepository = repository;
            _userWeChatRepository = userWeChatRepository;
            _userPointLogRepository = userPointLogRepository;
            _userManager = userManager;
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<CurrentSysUserEto> GetCurrentUserAsync()
        {
            return await _sysUserRepository.GetCurrentUserAsync(CurrentUser.Id.Value);
        }

        /// <summary>
        /// 更新当前用户信息
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<CurrentSysUserEto> UpdateCurrentUserAsync()
        {
            return await _sysUserRepository.UpdateCurrentUserAsync(CurrentUser.Id.Value);
        }

        /// <summary>
        /// 获取(分页)用户积分记录列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        [Authorize]
        public async Task<PagedResultDto<UserPointLogDto>> GetUserPointLogPageListAsync(GetUserPointLogInput input)
        {
            // 查询数量
            long totalCount = (await _userPointLogRepository.GetQueryableAsync())
                .Where(p => p.UserId == CurrentUser.Id)
                .WhereIf(input.Type.HasValue, p => p.Type == input.Type)
                .LongCount();

            // 查询数据列表
            if (totalCount < 1) return new PagedResultDto<UserPointLogDto>();
            List<UserPointLog> userPointLogList = (await _userPointLogRepository.GetQueryableAsync())
                .Where(p => p.UserId == CurrentUser.Id)
                .WhereIf(input.Type.HasValue, p => p.Type == input.Type)
                .OrderByIf<UserPointLog, IQueryable<UserPointLog>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                .PageBy(input.SkipCount, input.MaxResultCount)
                .ToList();

            return new PagedResultDto<UserPointLogDto>(
                totalCount,
                ObjectMapper.Map<List<UserPointLog>, List<UserPointLogDto>>(userPointLogList)
            );
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        public async Task UpdateSysUserPwdAsync(string password)
        {
            Volo.Abp.Identity.IdentityUser user = await _userManager.GetByIdAsync(CurrentUser.Id.Value);
            (await _userManager.RemovePasswordAsync(user)).CheckErrors();
            (await _userManager.AddPasswordAsync(user, password)).CheckErrors();
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 修改用户资料信息
        /// </summary>
        /// <param name="input">用户资料</param>
        /// <returns></returns>
        public async Task UpdateSysUserInformationAsync(SysUserInformationUpdateDto input)
        {
            SysUser user = await _sysUserRepository.GetAsync(CurrentUser.Id.Value);
            user.Name = input.Name;
            user.Avatar = input.Avatar;
            user.Gender = input.Gender;
            if (input.BirthDate.HasValue) user.BirthDate = input.BirthDate;

            UserWeChat userWeChat = await _userWeChatRepository.GetAsync(x => x.UserId == CurrentUser.Id);
            if (userWeChat != null)
            {
                userWeChat.NickName = input.Name;
                userWeChat.Avatar = input.Avatar;
                userWeChat.Gender = input.Gender;
                if (input.BirthDate.HasValue) userWeChat.BirthDate = input.BirthDate;
            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}