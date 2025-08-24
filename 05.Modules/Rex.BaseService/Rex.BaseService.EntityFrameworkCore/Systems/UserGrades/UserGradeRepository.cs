using Microsoft.Extensions.DependencyInjection;
using Rex.BaseService.EntityFrameworkCore;
using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.BaseService.Systems.UserGrades
{
    /// <summary>
    /// 用户等级仓储
    /// </summary>
    [Dependency(ServiceLifetime.Singleton)]
    public class UserGradeRepository : EfCoreRepository<BaseServiceDbContext, UserGrade, Guid>, IUserGradeRepository
    {
        public BaseServiceDbContext bServiceDbContext { get; set; }

        public UserGradeRepository(IDbContextProvider<BaseServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}