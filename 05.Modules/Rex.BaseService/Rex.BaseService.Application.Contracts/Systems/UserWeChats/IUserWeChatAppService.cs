using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.BaseService.Systems.UserWeChats
{
    /// <summary>
    /// 微信用户服务接口
    /// </summary>
    public interface IUserWeChatAppService : ICrudAppService<UserWeChatDto, Guid, PagedAndSortedResultRequestDto, UserWeChatCreateDto, UserWeChatUpdateDto>
    {
        /// <summary>
        /// 获取(分页)微信用户列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<UserWeChatDto>> GetPageListAsync(GetUserWeChatInput input);

        /// <summary>
        /// 获取第三方登录类型
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetUserAccountTypeAsync();
    }
}