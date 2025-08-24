using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rex.BaseService.EntityFrameworkCore;
using Rex.BaseService.Systems.SysUsers;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Helper;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.BaseService.Systems.UserBalances
{
    /// <summary>
    /// 用户余额仓储
    /// </summary>
    public class UserBalanceRepository : EfCoreRepository<BaseServiceDbContext, UserBalance, Guid>, IUserBalanceRepository
    {
        public BaseServiceDbContext bServiceDbContext { get; set; }

        private readonly ISysUserRepository _sysUserRepository;

        public UserBalanceRepository(IDbContextProvider<BaseServiceDbContext> dbContextProvider, ISysUserRepository sysUserRepository) : base(dbContextProvider)
        {
            _sysUserRepository = sysUserRepository;
        }

        /// <summary>
        /// 变更余额
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="type">变更类型</param>
        /// <param name="money">变更金额</param>
        /// <param name="sourceId">变更来源ID</param>
        /// <param name="cateMoney">服务费金额(提现)</param>
        /// <returns></returns>
        public async Task ChangeBalanceAsync(Guid userId, int type, decimal money, string sourceId, decimal? cateMoney = 0)
        {
            SysUser sysUser = await _sysUserRepository.GetAsync(userId);
            if (sysUser == null)
                throw new UserFriendlyException("用户不存在！", SystemStatusCode.Fail.ToBaseServicePrefix()).WithData(nameof(userId), userId);

            // 变更描述
            string memo = CommonHelper.GetUserBalanceTypeMemo(type, money, cateMoney);
            if (memo.IsNullOrWhiteSpace())
                throw new UserFriendlyException("系统异常：未配置余额变更描述信息！", SystemStatusCode.Fail.ToBaseServicePrefix());

            // 判断：是减余额的操作，还是加余额操作
            if (type == (int)GlobalEnums.UserBalanceType.Pay || type == (int)GlobalEnums.UserBalanceType.Tocash)
            {
                money = -money;
                if (cateMoney.HasValue && cateMoney.Value > 0)
                    money = money - cateMoney.Value;
            }

            // 判断：余额是否足够
            decimal balance = sysUser.Balance + money;
            if (balance < 0)
                throw new UserFriendlyException("余额不足！", SystemStatusCode.Fail.ToBaseServicePrefix());

            // 保存余额变更记录
            UserBalance userBalance = new UserBalance();
            userBalance.UserId = userId;
            userBalance.Type = type;
            userBalance.Money = money;
            userBalance.Balance = balance;
            userBalance.SourceId = sourceId;
            userBalance.Memo = memo;
            await (await GetDbSetAsync()).AddAsync(userBalance);

            // 更新用户余额
            /*
            await (await _sysUserRepository.GetDbSetAsync())
                .Where(x => x.Id == userId)
                .ExecuteUpdateAsync(x => x.SetProperty(xb => xb.Balance, balance));
            */
            sysUser.Balance = balance;

            // 更新用户信息
            await _sysUserRepository.UpdateCurrentUserAsync(userId);
        }
    }
}