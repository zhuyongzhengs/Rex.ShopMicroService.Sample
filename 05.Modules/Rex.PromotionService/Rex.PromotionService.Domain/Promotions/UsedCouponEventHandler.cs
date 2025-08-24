using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using Rex.Service.Core.Events.Promotions;
using System.Diagnostics;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Rex.PromotionService.Promotions
{
    /// <summary>
    /// 调整优惠券使用情况 事件处理
    /// </summary>
    /// <remarks>
    /// 订阅事件
    /// </remarks>
    [Dependency(ServiceLifetime.Scoped)]
    public class UsedCouponEventHandler : ICapSubscribe
    {
        private readonly ICouponRepository _couponRepository;

        public UsedCouponEventHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [CapSubscribe(UsedCouponEto.EventName)]
        public async Task UsedCouponAsync(UsedCouponEto eventData)
        {
            await _couponRepository.UpdateUsedCouponAsync(eventData.CouponCode, eventData.UsedId);
            Debug.WriteLine("调整优惠券成功。");
            Task.CompletedTask.Wait();
        }
    }
}