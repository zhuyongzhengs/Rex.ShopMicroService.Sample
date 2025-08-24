using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Rex.BaseService.Systems.OrganizationUnitRoles
{
    /// <summary>
    /// 组织单元【角色】仓储接口
    /// </summary>
    public interface IOrganizationUnitRoleRepository : IRepository<OrganizationUnitRole>
    {
        /// <summary>
        /// 获取组织单元【角色】信息
        /// </summary>
        /// <param name="filter">过滤筛选</param>
        /// <param name="maxResultCount">查询数量</param>
        /// <param name="skipCount">跳过数</param>
        /// <param name="organizationUnitId">组织单元ID</param>
        /// <param name="sorting">排序</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<OrganizationUnitRole>> GetListAsync(
            string? filter,
            int maxResultCount,
            int skipCount,
            Guid? organizationUnitId = null,
            string sorting = null,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// 获取组织单元【角色】信息数量
        /// </summary>
        /// <param name="filter">过滤筛选</param>
        /// <param name="maxResultCount">查询数量</param>
        /// <param name="skipCount">跳过数</param>
        /// <param name="organizationUnitId">组织单元ID</param>
        /// <param name="sorting">排序</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> GetListCountAsync(
            string? filter,
            Guid? organizationUnitId = null,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// 选择组织单元角色
        /// </summary>
        /// <returns></returns>
        Task<List<IdentityRole>> GetSelectRoleListAsync(
            List<Guid> notRoleIds,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// 选择组织单元角色数量
        /// </summary>
        /// <returns></returns>
        Task<long> GetSelectRoleCountAsync(
            List<Guid> notRoleIds = null,
            string filter = null,
            CancellationToken cancellationToken = default
        );
    }
}