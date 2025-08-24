using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Rex.BaseService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;

namespace Rex.BaseService.Systems.UserOrganizationUnits
{
    /// <summary>
    /// 组织单元【用户】仓储
    /// </summary>
    public class UserOrganizationUnitRepository : EfCoreRepository<BaseServiceDbContext, IdentityUserOrganizationUnit>, IUserOrganizationUnitRepository
    {
        public IIdentityUserRepository IdentityUserRepository { get; set; }
        public BaseServiceDbContext bServiceDbContext { get; set; }

        public UserOrganizationUnitRepository(IDbContextProvider<BaseServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取组织单元【用户】信息
        /// </summary>
        /// <param name="filter">过滤筛选</param>
        /// <param name="maxResultCount">查询数量</param>
        /// <param name="skipCount">跳过数</param>
        /// <param name="organizationUnitId">组织单元ID</param>
        /// <param name="sorting">排序</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<IdentityUserOrganizationUnit>> GetListAsync(string? filter, int maxResultCount, int skipCount, Guid? organizationUnitId = null, string sorting = null, CancellationToken cancellationToken = default)
        {
            IdentityUser identityUser = null;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                identityUser = await IdentityUserRepository.FindByNormalizedUserNameAsync(filter);
            }
            if (!string.IsNullOrWhiteSpace(filter) && identityUser == null)
            {
                return new List<IdentityUserOrganizationUnit>();
            }

            List<IdentityUserOrganizationUnit> uOuList = await (await GetDbSetAsync())
                .WhereIf(identityUser != null, u => u.UserId == identityUser.Id)
                .WhereIf(organizationUnitId != null, u => u.OrganizationUnitId == organizationUnitId.Value)
                .OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(IdentityUserOrganizationUnit.UserId) : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));

            return uOuList;
        }

        /// <summary>
        /// 获取组织单元【用户】信息数量
        /// </summary>
        /// <param name="filter">过滤筛选</param>
        /// <param name="organizationUnitId">组织单元ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<long> GetListCountAsync(string? filter, Guid? organizationUnitId = null, CancellationToken cancellationToken = default)
        {
            IdentityUser identityUser = null;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                identityUser = await IdentityUserRepository.FindByNormalizedUserNameAsync(filter);
            }
            if (!string.IsNullOrWhiteSpace(filter) && identityUser == null)
            {
                return 0;
            }

            return await (await GetDbSetAsync())
                .WhereIf(identityUser != null, u => u.UserId == identityUser.Id)
                .WhereIf(organizationUnitId != null, u => u.OrganizationUnitId == organizationUnitId.Value)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 选择组织单元用户
        /// </summary>
        /// <returns></returns>
        public async Task<List<IdentityUser>> GetSelectUserListAsync(List<Guid> notUserIds = null, string sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, string filter = null)
        {
            var userSql = $"SELECT Id, TenantId, UserName, NormalizedUserName, Name, Surname, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, IsExternal, PhoneNumber, PhoneNumberConfirmed, IsActive, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount, ShouldChangePasswordOnNextLogin, EntityVersion, LastPasswordChangeTime, ExtraProperties, ConcurrencyStamp, CreationTime, CreatorId, LastModificationTime, LastModifierId, IsDeleted, DeleterId, DeletionTime FROM sys_users WHERE IsDeleted = 0 AND (Discriminator IS NULL OR Discriminator='') ";
            List<MySqlParameter> parameters = new List<MySqlParameter>()
            {
                new MySqlParameter("@Limit", maxResultCount),
                new MySqlParameter("@Offset", skipCount)
            };
            if (!filter.IsNullOrWhiteSpace())
            {
                userSql += " AND (UserName LIKE CONCAT('%',@Filter,'%') OR Email LIKE CONCAT('%',@Filter,'%') OR Name LIKE CONCAT('%',@Filter,'%') OR Surname LIKE CONCAT('%',@Filter,'%') OR PhoneNumber LIKE CONCAT('%',@Filter,'%') ) ";
                parameters.Add(new MySqlParameter("@Filter", filter));
            }
            if (notUserIds != null && notUserIds.Count > 0)
            {
                List<string> userIds = new List<string>();
                foreach (var nUser in notUserIds)
                {
                    userIds.Add($"'{nUser}'");
                }
                userSql += $" AND Id NOT IN({string.Join(",", userIds)})";
            }
            if (!sorting.IsNullOrWhiteSpace())
            {
                userSql += $" ORDER BY {sorting} ";
            }
            //userSql += " LIMIT @Limit OFFSET @Offset ";
            userSql += " LIMIT @Offset,@Limit ";
            return await Task.Run(() =>
            {
                DataTable dataTable = bServiceDbContext.ExecuteQuery(userSql, CommandType.Text, parameters.ToArray());
                List<DataRow> dataRowList = dataTable.AsEnumerable().ToList();
                List<IdentityUser> userList = new List<IdentityUser>();
                foreach (DataRow dataRow in dataRowList)
                {
                    Guid id = dataRow.Field<Guid>("Id");
                    string userName = dataRow.Field<string>("UserName");
                    string email = dataRow.Field<string>("Email");
                    IdentityUser identityUser = new IdentityUser(id, userName, email);
                    Type userType = typeof(IdentityUser);
                    userType?.GetProperty("Id")?.SetValue(identityUser, dataRow.Field<Guid>("Id"));
                    userType?.GetProperty("TenantId")?.SetValue(identityUser, dataRow.Field<Guid?>("TenantId"));
                    userType?.GetProperty("UserName")?.SetValue(identityUser, userName);
                    userType?.GetProperty("NormalizedUserName")?.SetValue(identityUser, dataRow.Field<string>("NormalizedUserName"));
                    userType?.GetProperty("Name")?.SetValue(identityUser, dataRow.Field<string>("Name"));
                    userType?.GetProperty("Surname")?.SetValue(identityUser, dataRow.Field<string>("Surname"));
                    userType?.GetProperty("Email")?.SetValue(identityUser, email);
                    userType?.GetProperty("NormalizedEmail")?.SetValue(identityUser, dataRow.Field<string>("NormalizedEmail"));
                    userType?.GetProperty("EmailConfirmed")?.SetValue(identityUser, dataRow.Field<bool>("EmailConfirmed"));
                    userType?.GetProperty("PasswordHash")?.SetValue(identityUser, dataRow.Field<string>("PasswordHash"));
                    userType?.GetProperty("SecurityStamp")?.SetValue(identityUser, dataRow.Field<string>("SecurityStamp"));
                    userType?.GetProperty("IsExternal")?.SetValue(identityUser, dataRow.Field<bool>("IsExternal"));
                    userType?.GetProperty("PhoneNumber")?.SetValue(identityUser, dataRow.Field<string>("PhoneNumber"));
                    userType?.GetProperty("PhoneNumberConfirmed")?.SetValue(identityUser, dataRow.Field<bool>("PhoneNumberConfirmed"));
                    userType?.GetProperty("IsActive")?.SetValue(identityUser, dataRow.Field<bool>("IsActive"));
                    userType?.GetProperty("TwoFactorEnabled")?.SetValue(identityUser, dataRow.Field<bool>("TwoFactorEnabled"));
                    userType?.GetProperty("LockoutEnd")?.SetValue(identityUser, dataRow.Field<DateTimeOffset?>("LockoutEnd"));
                    userType?.GetProperty("LockoutEnabled")?.SetValue(identityUser, dataRow.Field<bool>("LockoutEnabled"));
                    userType?.GetProperty("AccessFailedCount")?.SetValue(identityUser, dataRow.Field<int>("AccessFailedCount"));
                    userType?.GetProperty("ShouldChangePasswordOnNextLogin")?.SetValue(identityUser, dataRow.Field<bool>("ShouldChangePasswordOnNextLogin"));
                    userType?.GetProperty("EntityVersion")?.SetValue(identityUser, dataRow.Field<int>("EntityVersion"));
                    userType?.GetProperty("ConcurrencyStamp")?.SetValue(identityUser, dataRow.Field<string>("ConcurrencyStamp"));
                    userType?.GetProperty("CreationTime")?.SetValue(identityUser, dataRow.Field<DateTime?>("CreationTime"));
                    userType?.GetProperty("CreatorId")?.SetValue(identityUser, dataRow.Field<Guid?>("CreatorId"));
                    userType?.GetProperty("LastModificationTime")?.SetValue(identityUser, dataRow.Field<DateTime?>("LastModificationTime"));
                    userType?.GetProperty("LastModifierId")?.SetValue(identityUser, dataRow.Field<Guid?>("LastModifierId"));
                    userType?.GetProperty("IsDeleted")?.SetValue(identityUser, dataRow.Field<bool>("IsDeleted"));
                    userType?.GetProperty("DeleterId")?.SetValue(identityUser, dataRow.Field<Guid?>("DeleterId"));
                    userType?.GetProperty("DeletionTime")?.SetValue(identityUser, dataRow.Field<DateTime?>("DeletionTime"));
                    userList.Add(identityUser);
                }
                return userList;
            });
        }

        /// <summary>
        /// 选择组织单元用户数量
        /// </summary>
        /// <returns></returns>
        public async Task<long> GetSelectUserCountAsync(List<Guid> notUserIds = null, string filter = null)
        {
            string userSql = "SELECT COUNT(*) FROM sys_users WHERE IsDeleted = 0 AND (Discriminator IS NULL OR Discriminator='')";
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            if (!filter.IsNullOrWhiteSpace())
            {
                userSql += " AND (UserName LIKE CONCAT('%',@Filter,'%') OR Email LIKE CONCAT('%',@Filter,'%') OR Name LIKE CONCAT('%',@Filter,'%') OR Surname LIKE CONCAT('%',@Filter,'%') OR PhoneNumber LIKE CONCAT('%',@Filter,'%') ) ";
                parameters.Add(new MySqlParameter("@Filter", filter));
            }
            if (notUserIds != null && notUserIds.Count > 0)
            {
                List<string> userIds = new List<string>();
                foreach (var nUser in notUserIds)
                {
                    userIds.Add($"'{nUser}'");
                }
                userSql += $" AND Id NOT IN({string.Join(",", userIds)})";
            }

            return await Task.Run(() =>
            {
                var totalCount = bServiceDbContext.ExecuteScalar(userSql, CommandType.Text, parameters.ToArray());
                if (totalCount != null)
                {
                    return Convert.ToInt64(totalCount);
                }
                return 0;
            });
        }
    }
}