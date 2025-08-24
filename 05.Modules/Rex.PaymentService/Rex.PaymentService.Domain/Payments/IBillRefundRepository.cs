using Rex.Service.Core.Events.Payments;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 退款单仓储接口
    /// </summary>
    public interface IBillRefundRepository : IRepository<BillRefund, Guid>
    {
        /// <summary>
        /// 根据退款单号更新备注信息
        /// </summary>
        /// <param name="no">退款单号</param>
        /// <param name="memo">退款内容</param>
        /// <returns></returns>
        Task UpdateMemoByNoAsync(string no, string memo);

        /// <summary>
        /// 创建退款单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateBillRefundAsync(BillRefundCreateEto input);
    }
}