using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.BaseService.Systems.UserBalances
{
    /// <summary>
    /// 用户余额仓储接口
    /// </summary>
    public interface IUserBalanceRepository : IRepository<UserBalance, Guid>
    {
        /// <summary>
        /// 变更余额
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="type">变更类型</param>
        /// <param name="money">变更金额</param>
        /// <param name="sourceId">变更来源ID</param>
        /// <param name="cateMoney">服务费金额(提现)</param>
        /// <returns></returns>
        Task ChangeBalanceAsync(Guid userId, int type, decimal money, string sourceId, decimal? cateMoney = 0);
    }
}