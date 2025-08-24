using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.BaseService.UserShips
{
    /// <summary>
    /// 用户(收货)地址仓储接口
    /// </summary>
    public interface IUserShipRepository : IRepository<UserShip, Guid>
    {
    }
}