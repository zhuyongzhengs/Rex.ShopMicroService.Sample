using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using Rex.Service.Core.Events.Bases;
using System.Diagnostics;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Rex.BaseService.Systems.SysUsers
{
    /// <summary>
    /// 用户积分变更 事件处理
    /// </summary>
    /// <remarks>
    /// 订阅事件
    /// </remarks>
    [Dependency(ServiceLifetime.Scoped)]
    public class UserChangePointEventHandler : ICapSubscribe
    {
        private readonly ISysUserRepository _sysUserRepository;

        public UserChangePointEventHandler(ISysUserRepository sysUserRepository)
        {
            _sysUserRepository = sysUserRepository;
        }

        [CapSubscribe(UserChangePointEto.EventName)]
        public async Task UserChangePointAsync(UserChangePointEto eventData)
        {
            await _sysUserRepository.UpdateUserChangePointAsync(eventData.UserId, eventData.PointType, eventData.Point, eventData.Remark);
            Debug.WriteLine("用户积分变更成功！");
            Task.CompletedTask.Wait();
        }
    }
}