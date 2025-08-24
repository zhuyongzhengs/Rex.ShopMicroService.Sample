using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.BaseService.Systems.UserWeChats
{
    /// <summary>
    /// 微信用户仓储接口
    /// </summary>
    public interface IUserWeChatRepository : IRepository<UserWeChat, Guid>
    {
    }
}