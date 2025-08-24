using Rex.PaymentService.Payments;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Rex.PaymentService.Commons
{
    /// <summary>
    /// 公共服务接口
    /// </summary>
    public interface ICommonAppService : IApplicationService
    {
        /// <summary>
        /// 设置当前租户值
        /// </summary>
        /// <param name="name">Key</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        Task UpdateSettingCurrentTenantAsync(string name, string? value);

        /// <summary>
        /// 获取启用的支付方式
        /// </summary>
        /// <param name="code">支付编码</param>
        /// <returns></returns>
        Task<PaymentDto> GetPaymentEnableByCodeAsync(string code);

        /// <summary>
        /// 创建支付单
        /// </summary>
        /// <param name="input">支付单Dto</param>
        /// <returns></returns>
        Task<BillPaymentDto> CreateBillPaymentAsync(BillPaymentCreateDto input);

        /// <summary>
        /// 更新支付单状态
        /// </summary>
        /// <param name="no">支付单号</param>
        /// <param name="input">支付单状态</param>
        /// <returns></returns>
        Task<BillPaymentDto> UpdateBillPaymentStatusAsync(string no, BillPaymentStatusUpdateDto input);

        /// <summary>
        /// 发起微信[小程序]支付
        /// </summary>
        /// <param name="openId">微信用户OpenID</param>
        /// <param name="billPayment">支付单</param>
        /// <returns></returns>
        Task<WeChatPayParameterResultDto> PubWxmpPayAsync(string openId, BillPaymentDto billPayment);
    }
}