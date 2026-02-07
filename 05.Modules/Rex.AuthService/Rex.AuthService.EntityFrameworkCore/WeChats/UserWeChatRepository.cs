using Npgsql;
using Rex.AuthService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.AuthService.WeChats
{
    /// <summary>
    /// 微信用户服务仓储
    /// </summary>
    public class UserWeChatRepository : EfCoreRepository<AuthServiceDbContext, IdentityUser, Guid>, IUserWeChatRepository
    {
        public AuthServiceDbContext authServiceDbContext { get; set; }
        private readonly IIdentityUserRepository _identityUserRepository;

        public UserWeChatRepository(IDbContextProvider<AuthServiceDbContext> dbContextProvider, IIdentityUserRepository identityUserRepository) : base(dbContextProvider)
        {
            _identityUserRepository = identityUserRepository;
        }

        /// <summary>
        /// 微信小程序登录
        /// </summary>
        /// <param name="wxMpLogin">微信小程序登录信息</param>
        /// <param name="isCreateUser">是否创建用户</param>
        /// <remarks>
        /// isCreateUser：True[不存在则创建该用户]、False[不创建新用户,不存在则登录失败]
        /// </remarks>
        /// <returns></returns>
        public async Task<IdentityUser> WeChatMpLoginAsync(WxMpLoginDo wxMpLogin, bool isCreateUser = true)
        {
            string sql = "SELECT \"ID\", \"UserId\", \"OpenId\", \"SessionKey\" FROM \"Sys_UserWeChat\" WHERE \"IsDeleted\" = '0' AND \"OpenId\" = @OpenId;";

            NpgsqlParameter dbParameter = new NpgsqlParameter();

            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>() { new NpgsqlParameter("@OpenId", wxMpLogin.OpenId) };
            DataTable userWechatDataTable = authServiceDbContext.ExecuteQuery(sql, CommandType.Text, parameters.ToArray());
            DataRow dataRow = userWechatDataTable?.AsEnumerable()?.FirstOrDefault();
            if (dataRow == null && !isCreateUser) return null;

            Guid? uId = null;
            if (dataRow == null)
            {
                #region 创建新用户信息

                List<SingleCommand> execCommands = new List<SingleCommand>();

                // 0.查询默认的用户等级
                string userGradeSql = "SELECT \"Id\" FROM \"Sys_UserGrade\" WHERE \"IsDefault\" = '1' LIMIT 1 OFFSET 0;";
                object userGradeId = authServiceDbContext.ExecuteScalar(userGradeSql, CommandType.Text);
                Guid? uGradeId = null;
                if (Guid.TryParse(userGradeId?.ToString(), out Guid gradeId))
                {
                    uGradeId = gradeId;
                }

                // 1.创建用户信息
                Guid userId = GuidGenerator.Create();
                decimal defaultPoint = 0;
                long timestampMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                string userName = $"MP-{timestampMilliseconds}";
                string name = $"微信用户_{userName}";
                string insertUserSql = @"
                    INSERT INTO ""Sys_Users"" (""Id"", ""UserName"", ""NormalizedUserName"", ""Name"", ""GradeId"", ""Balance"", ""Point"", ""Email"", ""NormalizedEmail"", ""EmailConfirmed"", ""SecurityStamp"", ""IsExternal"", ""PhoneNumber"", ""PhoneNumberConfirmed"", ""IsActive"", ""TwoFactorEnabled"", ""LockoutEnabled"", ""AccessFailedCount"", ""ShouldChangePasswordOnNextLogin"", ""EntityVersion"", ""CreationTime"", ""IsDeleted"", ""Discriminator"", ""ExtraProperties"", ""ConcurrencyStamp"")
                    VALUES(@Id, @UserName, @NormalizedUserName, @Name, @GradeId, @Balance, @Point, @Email, @NormalizedEmail, @EmailConfirmed, @SecurityStamp, @IsExternal, @PhoneNumber, @PhoneNumberConfirmed, @IsActive, @TwoFactorEnabled, @LockoutEnabled, @AccessFailedCount, @ShouldChangePasswordOnNextLogin, @EntityVersion, @CreationTime, @IsDeleted, @Discriminator, @ExtraProperties, @ConcurrencyStamp);
                ";
                List<NpgsqlParameter> userParameters = new List<NpgsqlParameter>();
                decimal defaultBalance = wxMpLogin.DefaultBalance;
                int integerValue = 0;
                userParameters.Add(new NpgsqlParameter("@Id", userId));
                userParameters.Add(new NpgsqlParameter("@UserName", userName));
                userParameters.Add(new NpgsqlParameter("@NormalizedUserName", userName.ToUpper()));
                userParameters.Add(new NpgsqlParameter("@Name", name));
                userParameters.Add(new NpgsqlParameter("@GradeId", uGradeId));
                userParameters.Add(new NpgsqlParameter("@Balance", defaultBalance));
                userParameters.Add(new NpgsqlParameter("@Point", defaultPoint));
                userParameters.Add(new NpgsqlParameter("@Email", string.Empty));
                userParameters.Add(new NpgsqlParameter("@NormalizedEmail", string.Empty));
                userParameters.Add(new NpgsqlParameter("@EmailConfirmed", false));
                userParameters.Add(new NpgsqlParameter("@SecurityStamp", Guid.NewGuid().ToString()));
                userParameters.Add(new NpgsqlParameter("@IsExternal", false));
                userParameters.Add(new NpgsqlParameter("@PhoneNumber", wxMpLogin.PhoneNumber));
                userParameters.Add(new NpgsqlParameter("@PhoneNumberConfirmed", false));
                userParameters.Add(new NpgsqlParameter("@IsActive", true));
                userParameters.Add(new NpgsqlParameter("@TwoFactorEnabled", false));
                userParameters.Add(new NpgsqlParameter("@LockoutEnabled", true));
                userParameters.Add(new NpgsqlParameter("@AccessFailedCount", integerValue));
                userParameters.Add(new NpgsqlParameter("@ShouldChangePasswordOnNextLogin", false));
                userParameters.Add(new NpgsqlParameter("@EntityVersion", integerValue));
                userParameters.Add(new NpgsqlParameter("@CreationTime", DateTime.UtcNow));
                userParameters.Add(new NpgsqlParameter("@IsDeleted", false));
                userParameters.Add(new NpgsqlParameter("@Discriminator", "SysUser"));
                userParameters.Add(new NpgsqlParameter("@ExtraProperties", "{}"));
                userParameters.Add(new NpgsqlParameter("@ConcurrencyStamp", Guid.NewGuid().ToString("n")));
                execCommands.Add(new SingleCommand()
                {
                    CommandText = insertUserSql,
                    Parameters = userParameters
                });

                // 2.创建微信[关联]用户
                string insertUserWechatSql = @"
                    INSERT INTO ""Sys_UserWeChat"" (""Id"", ""Type"", ""UserId"", ""OpenId"", ""SessionKey"", ""UnionId"", ""NickName"", ""CountryCode"", ""Mobile"", ""CreationTime"", ""IsDeleted"", ""ExtraProperties"", ""ConcurrencyStamp"")
                    VALUES(@Id, @Type, @UserId, @OpenId, @SessionKey, @UnionId, @NickName, @CountryCode, @Mobile, @CreationTime, @IsDeleted, @ExtraProperties, @ConcurrencyStamp);
                ";
                List<NpgsqlParameter> userWechatParameters = new List<NpgsqlParameter>();
                userWechatParameters.Add(new NpgsqlParameter("@Id", GuidGenerator.Create()));
                int uType = (int)UserAccountTypes.MiniProgram;
                userWechatParameters.Add(new NpgsqlParameter("@Type", uType));
                userWechatParameters.Add(new NpgsqlParameter("@UserId", userId));
                userWechatParameters.Add(new NpgsqlParameter("@OpenId", wxMpLogin.OpenId));
                userWechatParameters.Add(new NpgsqlParameter("@SessionKey", wxMpLogin.SessionKey));
                userWechatParameters.Add(new NpgsqlParameter("@UnionId", wxMpLogin.Unionid));
                userWechatParameters.Add(new NpgsqlParameter("@NickName", name));
                userWechatParameters.Add(new NpgsqlParameter("@CountryCode", wxMpLogin.CountryCode));
                userWechatParameters.Add(new NpgsqlParameter("@Mobile", wxMpLogin.PhoneNumber));
                userWechatParameters.Add(new NpgsqlParameter("@CreationTime", DateTime.UtcNow));
                userWechatParameters.Add(new NpgsqlParameter("@IsDeleted", false));
                userWechatParameters.Add(new NpgsqlParameter("@ExtraProperties", "{}"));
                userWechatParameters.Add(new NpgsqlParameter("@ConcurrencyStamp", Guid.NewGuid().ToString("n")));
                execCommands.Add(new SingleCommand()
                {
                    CommandText = insertUserWechatSql,
                    Parameters = userWechatParameters
                });

                // 3.执行Sql
                int rows = authServiceDbContext.ExecuteTransaction(execCommands);
                if (rows < 1) return null;

                uId = userId;

                #endregion 创建新用户信息
            }
            else
            {
                uId = dataRow.Field<Guid>("UserId");

                // 验证该用户是否已启用
                object isActive = authServiceDbContext.ExecuteScalar(@"SELECT ""IsActive"" FROM ""Sys_Users"" WHERE Id=@UserId;", CommandType.Text, new NpgsqlParameter[] { new NpgsqlParameter("@UserId", uId) });
                bool.TryParse(isActive?.ToString(), out bool active);
                if (!active) return null;

                // 更新【SessionKey】
                string updateSql = @"
                    UPDATE ""Sys_UserWeChat"" SET ""SessionKey""=@SessionKey, ""ConcurrencyStamp""=@ConcurrencyStamp WHERE ""OpenId""=@OpenId;
                ";
                List<NpgsqlParameter> updateParameters = new List<NpgsqlParameter>() {
                    new NpgsqlParameter("@OpenId", wxMpLogin.OpenId),
                    new NpgsqlParameter("@SessionKey", wxMpLogin.SessionKey),
                    new NpgsqlParameter("@ConcurrencyStamp", Guid.NewGuid().ToString("n")),
                };
                int rows = authServiceDbContext.ExecuteNonQuery(updateSql, CommandType.Text, updateParameters.ToArray());
                if (rows < 1) return null;
            }
            return await _identityUserRepository.GetAsync(uId.Value);
        }
    }
}