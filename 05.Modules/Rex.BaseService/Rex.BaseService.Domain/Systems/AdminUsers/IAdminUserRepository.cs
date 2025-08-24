using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Rex.BaseService.Systems.AdminUsers
{
    /// <summary>
    /// 管理员用户仓储接口
    /// </summary>
    public interface IAdminUserRepository : IRepository<IdentityUser, Guid>
    {
        /// <summary>
        /// 获取管理员用户数量
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        Task<long> GetPageCountAsync(
            string? userName = null,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// 获取(分页)管理员用户列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        Task<List<IdentityUser>> GetPageListAsync(
            string? userName = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );
    }
}