using Npgsql;
using Rex.BaseService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;

namespace Rex.BaseService.Systems.AdminUsers
{
    /// <summary>
    /// 管理员用户仓储
    /// </summary>
    public class AdminUserRepository : EfCoreRepository<BaseServiceDbContext, IdentityUser, Guid>, IAdminUserRepository
    {
        public BaseServiceDbContext bServiceDbContext { get; set; }

        public AdminUserRepository(IDbContextProvider<BaseServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取管理员用户数量
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public async Task<long> GetPageCountAsync(string? userName = null, CancellationToken cancellationToken = default)
        {
            string userSql = "SELECT COUNT(*) FROM \"Sys_Users\" WHERE \"IsDeleted\" = '0' AND (\"Discriminator\" IS NULL OR \"Discriminator\"='')";
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
            if (!userName.IsNullOrWhiteSpace())
            {
                userSql += " AND \"UserName\" LIKE CONCAT('%',@UserName,'%') ";
                parameters.Add(new NpgsqlParameter("@UserName", userName));
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

        /// <summary>
        /// 获取(分页)管理员用户列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public async Task<List<IdentityUser>> GetPageListAsync(string? userName = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var userSql = $"SELECT \"Id\", \"TenantId\", \"UserName\", \"NormalizedUserName\", \"Name\", \"Surname\", \"Email\", \"NormalizedEmail\", \"EmailConfirmed\", \"PasswordHash\", \"SecurityStamp\", \"IsExternal\", \"PhoneNumber\", \"PhoneNumberConfirmed\", \"IsActive\", \"TwoFactorEnabled\", \"LockoutEnd\", \"LockoutEnabled\", \"AccessFailedCount\", \"ShouldChangePasswordOnNextLogin\", \"EntityVersion\", \"LastPasswordChangeTime\", \"ExtraProperties\", \"ConcurrencyStamp\", \"CreationTime\", \"CreatorId\", \"LastModificationTime\", \"LastModifierId\", \"IsDeleted\", \"DeleterId\", \"DeletionTime\" FROM \"Sys_Users\" WHERE \"IsDeleted\" = '0' AND (\"Discriminator\" IS NULL OR \"Discriminator\"='') ";
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>()
            {
                new NpgsqlParameter("@Limit", maxResultCount),
                new NpgsqlParameter("@Offset", skipCount)
            };
            if (!userName.IsNullOrWhiteSpace())
            {
                userSql += " AND \"UserName\" LIKE CONCAT('%',@UserName,'%') ";
                parameters.Add(new NpgsqlParameter("@UserName", userName));
            }
            if (!sorting.IsNullOrWhiteSpace())
            {
                userSql += $" ORDER BY {sorting} ";
            }
            userSql += " LIMIT @Limit OFFSET @Offset ";
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
                    userType?.GetProperty("Id")?.SetValue(identityUser, id);
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
    }
}