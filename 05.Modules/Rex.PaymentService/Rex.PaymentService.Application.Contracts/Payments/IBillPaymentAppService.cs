using Rex.Service.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 支付单服务接口
    /// </summary>
    public interface IBillPaymentAppService : IApplicationService
    {
        /// <summary>
        /// 查询单据类型
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetTypesAsync();

        /// <summary>
        /// 查询支付状态
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetStatusAsync();

        /// <summary>
        /// 获取(分页)支付单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<BillPaymentDto>> GetListAsync(GetBillPaymentInput input);

        /// <summary>
        /// 获取用户支付单信息
        /// </summary>
        /// <param name="no">单号</param>
        /// <returns></returns>
        Task<BillPaymentDto> GetUserBillPaymentAsync(string no);

        /// <summary>
        /// 根据[资源编号]获取支付单
        /// </summary>
        /// <param name="sourceId">资源编号</param>
        /// <returns></returns>
        Task<List<BillPaymentDto>> GetSourceIdAsync(string sourceId);
    }
}