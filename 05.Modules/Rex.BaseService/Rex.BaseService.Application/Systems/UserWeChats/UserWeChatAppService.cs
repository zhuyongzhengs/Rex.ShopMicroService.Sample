using Microsoft.AspNetCore.Authorization;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Helper;
using Rex.Service.Core.Models;
using Rex.Service.Permission.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Rex.BaseService.Systems.UserWeChats
{
    /// <summary>
    /// 微信用户服务
    /// </summary>
    [RemoteService]
    [Authorize(BaseServicePermissions.UserWeChats.Default)]
    public class UserWeChatAppService : CrudAppService<UserWeChat, UserWeChatDto, Guid, PagedAndSortedResultRequestDto, UserWeChatCreateDto, UserWeChatUpdateDto>, IUserWeChatAppService
    {
        private readonly IUserWeChatRepository _userWeChatRepository;
        public IRepository<UserWeChat, Guid> UserWeChatRepository { get; set; }
        public IRepository<IdentityUser, Guid> IdentityUserRepository { get; set; }

        public UserWeChatAppService(IUserWeChatRepository repository) : base(repository)
        {
            _userWeChatRepository = repository;
        }

        /// <summary>
        /// 获取(分页)用户等级列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<UserWeChatDto>> GetPageListAsync(GetUserWeChatInput input)
        {
            // 查询数量
            long totalCount = (await _userWeChatRepository.GetQueryableAsync())
                .WhereIf(!input.NickName.IsNullOrWhiteSpace(), p => p.NickName.Contains(input.NickName))
                .WhereIf(!input.Mobile.IsNullOrWhiteSpace(), p => p.Mobile.Equals(input.Mobile))
                .WhereIf(!input.OpenId.IsNullOrWhiteSpace(), p => p.OpenId.Equals(input.OpenId)).LongCount();

            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<UserWeChatDto>();
            }
            List<UserWeChat> userWeChatList = (await _userWeChatRepository.GetQueryableAsync())
                    .WhereIf(!input.NickName.IsNullOrWhiteSpace(), p => p.NickName.Contains(input.NickName))
                    .WhereIf(!input.Mobile.IsNullOrWhiteSpace(), p => p.Mobile.Equals(input.Mobile))
                    .WhereIf(!input.OpenId.IsNullOrWhiteSpace(), p => p.OpenId.Equals(input.OpenId))
                    .OrderByIf<UserWeChat, IQueryable<UserWeChat>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                    .PageBy(input.SkipCount, input.MaxResultCount)
                    .ToList();

            List<UserWeChatDto> userWeChatDtos = ObjectMapper.Map<List<UserWeChat>, List<UserWeChatDto>>(userWeChatList);

            #region 查询关系的用户

            List<Guid?> userIds = userWeChatList.Where(p => p.UserId != null).Select(p => p.UserId).Distinct().ToList();
            if (userIds.Count > 0)
            {
                var userList = (await IdentityUserRepository.GetQueryableAsync()).Select(p => new { Id = p.Id, UserName = p.UserName }).Where(p => userIds.Contains(p.Id)).ToList();
                foreach (var user in userList)
                {
                    UserWeChatDto uWeChatDto = userWeChatDtos.Where(p => p.UserId == user.Id).FirstOrDefault();
                    if (uWeChatDto != null)
                    {
                        uWeChatDto.UserName = user.UserName;
                    }
                }
            }

            #endregion 查询关系的用户

            return new PagedResultDto<UserWeChatDto>(
                totalCount,
                userWeChatDtos
            );
        }

        /// <summary>
        /// 获取第三方登录类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetUserAccountTypeAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.UserAccountTypes>());
        }
    }
}