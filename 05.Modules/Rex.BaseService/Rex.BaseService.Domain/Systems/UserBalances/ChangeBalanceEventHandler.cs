using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using Rex.Service.Core.Events.Bases;
using System.Diagnostics;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Rex.BaseService.Systems.UserBalances
{
    /// <summary>
    /// 余额变更事件处理
    /// </summary>
    /// <remarks>
    /// 订阅事件
    /// </remarks>
    [Dependency(ServiceLifetime.Scoped)]
    public class ChangeBalanceEventHandler : ICapSubscribe
    {
        private readonly IUserBalanceRepository _userBalanceRepository;

        public ChangeBalanceEventHandler(IUserBalanceRepository repository)
        {
            _userBalanceRepository = repository;
        }

        [CapSubscribe(ChangeUserBalanceEto.EventName)]
        public async Task ChangeBalanceAsync(ChangeUserBalanceEto eventData)
        {
            await _userBalanceRepository.ChangeBalanceAsync(eventData.UserId, eventData.Type, eventData.Money, eventData.SourceId, eventData.CateMoney);
            Debug.WriteLine("用户余额变更成功！");
            Task.CompletedTask.Wait();
        }
    }
}