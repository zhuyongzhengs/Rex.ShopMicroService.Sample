using Rex.BaseService.Systems.SysUsers;
using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.BaseService.Systems.UserPointLogs
{
    /// <summary>
    /// 用户积分记录仓储接口
    /// </summary>
    public interface IUserPointLogRepository : IRepository<UserPointLog, Guid>
    {
        // TODO: 定义方法
    }
}