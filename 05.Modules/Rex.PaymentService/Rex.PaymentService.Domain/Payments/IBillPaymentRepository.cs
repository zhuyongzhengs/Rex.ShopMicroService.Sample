using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 支付单仓储接口
    /// </summary>
    public interface IBillPaymentRepository : IRepository<BillPayment, Guid>
    {
        /// <summary>
        /// 查询支付单号
        /// </summary>
        /// <param name="no">单号</param>
        /// <returns></returns>
        Task<BillPayment> GetBillPaymentByNoAsync(string no);

        /// <summary>
        /// 更新支付单状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="status">支付状态</param>
        /// <param name="paymentCode">支付类型编码</param>
        /// <param name="payedMsg">支付回调后的状态描述</param>
        /// <param name="tradeNo">第三方平台交易流水号</param>
        /// <returns></returns>
        Task UpdateBillPaymentStatusAsync(Guid id, int status, string paymentCode, string payedMsg = "", string tradeNo = "");
    }
}