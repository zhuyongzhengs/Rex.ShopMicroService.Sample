using Rex.BaseService.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.BaseService.UserShips
{
    /// <summary>
    /// 用户(收货)地址仓储
    /// </summary>
    public class UserShipRepository : EfCoreRepository<BaseServiceDbContext, UserShip, Guid>, IUserShipRepository
    {
        public BaseServiceDbContext bServiceDbContext { get; set; }

        public UserShipRepository(IDbContextProvider<BaseServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}