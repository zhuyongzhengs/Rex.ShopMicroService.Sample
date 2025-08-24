using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 支付方式服务接口
    /// </summary>
    public interface IPaymentsAppService : IApplicationService
    {
        /// <summary>
        /// 获取(分页)支付方式列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<PaymentDto>> GetWayAsync(GetPaymentInput input);

        /// <summary>
        /// 修改支付方式是否启用
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isEnable">是否启用</param>
        /// <returns></returns>
        Task UpdateIsEnableAsync(Guid id, bool isEnable);
    }
}