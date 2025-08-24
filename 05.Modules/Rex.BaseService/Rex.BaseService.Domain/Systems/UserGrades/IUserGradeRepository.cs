using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.BaseService.Systems.UserGrades
{
    /// <summary>
    /// 用户等级仓储接口
    /// </summary>
    public interface IUserGradeRepository : IRepository<UserGrade, Guid>
    {
    }
}