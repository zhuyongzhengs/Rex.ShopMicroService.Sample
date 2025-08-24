using MySqlConnector;
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
            string sql = "SELECT ID, UserId, OpenId, SessionKey FROM sys_userwechat WHERE IsDeleted = 0 AND OpenId = @OpenId;";
            List<MySqlParameter> parameters = new List<MySqlParameter>() { new MySqlParameter("@OpenId", wxMpLogin.OpenId) };
            DataTable userWechatDataTable = authServiceDbContext.ExecuteQuery(sql, CommandType.Text, parameters.ToArray());
            DataRow dataRow = userWechatDataTable?.AsEnumerable()?.FirstOrDefault();
            if (dataRow == null && !isCreateUser) return null;

            Guid? uId = null;
            if (dataRow == null)
            {
                #region 创建新用户信息

                List<SingleCommand> execCommands = new List<SingleCommand>();

                // 0.查询默认的用户等级
                string userGradeSql = "SELECT ID FROM sys_usergrade WHERE IsDefault = 1 limit 0,1;";
                object userGradeId = authServiceDbContext.ExecuteScalar(userGradeSql, CommandType.Text);
                Guid? uGradeId = null;
                if (Guid.TryParse(userGradeId?.ToString(), out Guid gradeId))
                {
                    uGradeId = gradeId;
                }

                // 1.创建用户信息
                Guid userId = GuidGenerator.Create();
                long timestampMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                string userName = $"MP-{timestampMilliseconds}";
                string name = $"微信用户_{userName}";
                string insertUserSql = @"
                    INSERT INTO sys_users (Id, UserName, NormalizedUserName, Name, GradeId, Balance, Point, Email, NormalizedEmail, EmailConfirmed, SecurityStamp, IsExternal, PhoneNumber, PhoneNumberConfirmed, IsActive, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, ShouldChangePasswordOnNextLogin, EntityVersion, CreationTime, IsDeleted, Discriminator)
                    VALUES(@Id, @UserName, @NormalizedUserName, @Name, @GradeId, @Balance, @Point, @Email, @NormalizedEmail, @EmailConfirmed, @SecurityStamp, @IsExternal, @PhoneNumber, @PhoneNumberConfirmed, @IsActive, @TwoFactorEnabled, @LockoutEnabled, @AccessFailedCount, @ShouldChangePasswordOnNextLogin, @EntityVersion, @CreationTime, @IsDeleted, @Discriminator);
                ";
                List<MySqlParameter> userParameters = new List<MySqlParameter>();
                userParameters.Add(new MySqlParameter("@Id", userId));
                userParameters.Add(new MySqlParameter("@UserName", userName));
                userParameters.Add(new MySqlParameter("@NormalizedUserName", userName.ToUpper()));
                userParameters.Add(new MySqlParameter("@Name", name));
                userParameters.Add(new MySqlParameter("@GradeId", uGradeId));
                userParameters.Add(new MySqlParameter("@Balance", 0));
                userParameters.Add(new MySqlParameter("@Point", 0));
                userParameters.Add(new MySqlParameter("@Email", string.Empty));
                userParameters.Add(new MySqlParameter("@NormalizedEmail", string.Empty));
                userParameters.Add(new MySqlParameter("@EmailConfirmed", "0"));
                userParameters.Add(new MySqlParameter("@SecurityStamp", Guid.NewGuid().ToString()));
                userParameters.Add(new MySqlParameter("@IsExternal", "0"));
                userParameters.Add(new MySqlParameter("@PhoneNumber", wxMpLogin.PhoneNumber));
                userParameters.Add(new MySqlParameter("@PhoneNumberConfirmed", "0"));
                userParameters.Add(new MySqlParameter("@IsActive", "1"));
                userParameters.Add(new MySqlParameter("@TwoFactorEnabled", "0"));
                userParameters.Add(new MySqlParameter("@LockoutEnabled", "1"));
                userParameters.Add(new MySqlParameter("@AccessFailedCount", "0"));
                userParameters.Add(new MySqlParameter("@ShouldChangePasswordOnNextLogin", "0"));
                userParameters.Add(new MySqlParameter("@EntityVersion", "0"));
                userParameters.Add(new MySqlParameter("@CreationTime", DateTime.Now));
                userParameters.Add(new MySqlParameter("@IsDeleted", "0"));
                userParameters.Add(new MySqlParameter("@Discriminator", "SysUser"));
                execCommands.Add(new SingleCommand()
                {
                    CommandText = insertUserSql,
                    Parameters = userParameters
                });

                // 2.创建微信[关联]用户
                string insertUserWechatSql = @"
                    INSERT INTO sys_userwechat (Id, `Type`, UserId, OpenId, SessionKey, UnionId, NickName, CountryCode, Mobile, CreationTime, IsDeleted)
                    VALUES(@Id, @Type, @UserId, @OpenId, @SessionKey, @UnionId, @NickName, @CountryCode, @Mobile, @CreationTime, @IsDeleted);
                ";
                List<MySqlParameter> userWechatParameters = new List<MySqlParameter>();
                userWechatParameters.Add(new MySqlParameter("@Id", GuidGenerator.Create()));
                userWechatParameters.Add(new MySqlParameter("@Type", UserAccountTypes.MiniProgram));
                userWechatParameters.Add(new MySqlParameter("@UserId", userId));
                userWechatParameters.Add(new MySqlParameter("@OpenId", wxMpLogin.OpenId));
                userWechatParameters.Add(new MySqlParameter("@SessionKey", wxMpLogin.SessionKey));
                userWechatParameters.Add(new MySqlParameter("@UnionId", wxMpLogin.Unionid));
                userWechatParameters.Add(new MySqlParameter("@NickName", name));
                userWechatParameters.Add(new MySqlParameter("@CountryCode", wxMpLogin.CountryCode));
                userWechatParameters.Add(new MySqlParameter("@Mobile", wxMpLogin.PhoneNumber));
                userWechatParameters.Add(new MySqlParameter("@CreationTime", DateTime.Now));
                userWechatParameters.Add(new MySqlParameter("@IsDeleted", "0"));
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
                object isActive = authServiceDbContext.ExecuteScalar("SELECT IsActive FROM sys_users WHERE Id=@UserId;", CommandType.Text, new MySqlParameter[] { new MySqlParameter("@UserId", uId) });
                bool.TryParse(isActive?.ToString(), out bool active);
                if (!active) return null;

                // 更新【SessionKey】
                string updateSql = @"
                    UPDATE sys_userwechat SET SessionKey=@SessionKey, ConcurrencyStamp=@ConcurrencyStamp WHERE OpenId=@OpenId;
                ";
                List<MySqlParameter> updateParameters = new List<MySqlParameter>() {
                    new MySqlParameter("@OpenId", wxMpLogin.OpenId),
                    new MySqlParameter("@SessionKey", wxMpLogin.SessionKey),
                    new MySqlParameter("@ConcurrencyStamp", Guid.NewGuid().ToString("n")),
                };
                int rows = authServiceDbContext.ExecuteNonQuery(updateSql, CommandType.Text, updateParameters.ToArray());
                if (rows < 1) return null;
            }
            return await _identityUserRepository.GetAsync(uId.Value);
        }
    }
}