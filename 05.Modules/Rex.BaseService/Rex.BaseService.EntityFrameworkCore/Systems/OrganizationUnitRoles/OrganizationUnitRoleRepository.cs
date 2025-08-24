using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rex.BaseService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace Rex.BaseService.Systems.OrganizationUnitRoles
{
    /// <summary>
    /// 组织单元【角色】仓储
    /// </summary>
    public class OrganizationUnitRoleRepository : EfCoreRepository<BaseServiceDbContext, OrganizationUnitRole>, IOrganizationUnitRoleRepository
    {
        public IIdentityRoleRepository IdentityRoleRepository { get; set; }

        public OrganizationUnitRoleRepository(IDbContextProvider<BaseServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

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
        public async Task<List<OrganizationUnitRole>> GetListAsync(string? filter, int maxResultCount, int skipCount, Guid? organizationUnitId = null, string sorting = null, CancellationToken cancellationToken = default)
        {
            IdentityRole identityRole = null;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                identityRole = await IdentityRoleRepository.FindByNormalizedNameAsync(filter);
            }
            if (!string.IsNullOrWhiteSpace(filter) && identityRole == null)
            {
                return new List<OrganizationUnitRole>();
            }

            List<OrganizationUnitRole> OurList = await (await GetDbSetAsync())
                .WhereIf(identityRole != null, u => u.RoleId == identityRole.Id)
                .WhereIf(organizationUnitId != null, u => u.OrganizationUnitId == organizationUnitId.Value)
                .OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(OrganizationUnitRole.RoleId) : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));

            return OurList;
        }

        /// <summary>
        /// 获取组织单元【角色】信息数量
        /// </summary>
        /// <param name="filter">过滤筛选</param>
        /// <param name="organizationUnitId">组织单元ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<long> GetListCountAsync(string? filter, Guid? organizationUnitId = null, CancellationToken cancellationToken = default)
        {
            IdentityRole identityRole = null;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                identityRole = await IdentityRoleRepository.FindByNormalizedNameAsync(filter);
            }
            if (!string.IsNullOrWhiteSpace(filter) && identityRole == null)
            {
                return 0;
            }

            return await (await GetDbSetAsync())
                .WhereIf(identityRole != null, u => u.RoleId == identityRole.Id)
                .WhereIf(organizationUnitId != null, u => u.OrganizationUnitId == organizationUnitId.Value)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 选择组织单元角色
        /// </summary>
        /// <returns></returns>
        public async Task<List<IdentityRole>> GetSelectRoleListAsync(List<Guid> notRoleIds = null, string sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, string filter = null, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return await (await IdentityRoleRepository.GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .WhereIf((notRoleIds != null && notRoleIds.Count > 0), p => !notRoleIds.Contains(p.Id))
                .WhereIf(!filter.IsNullOrWhiteSpace(),
                        x => x.Name.Contains(filter) ||
                        x.NormalizedName.Contains(filter))
                .OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(IdentityRole.Name) : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 选择组织单元角色数量
        /// </summary>
        /// <returns></returns>
        public async Task<long> GetSelectRoleCountAsync(List<Guid> notRoleIds = null, string filter = null, CancellationToken cancellationToken = default)
        {
            return await (await IdentityRoleRepository.GetDbSetAsync())
                .WhereIf((notRoleIds != null && notRoleIds.Count > 0), p => !notRoleIds.Contains(p.Id))
                .WhereIf(!filter.IsNullOrWhiteSpace(),
                    x => x.Name.Contains(filter) ||
                         x.NormalizedName.Contains(filter))
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }
    }
}