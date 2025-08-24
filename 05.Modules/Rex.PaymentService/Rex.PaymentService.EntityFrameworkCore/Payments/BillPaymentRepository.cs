using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rex.PaymentService.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 支付单仓储
    /// </summary>
    public class BillPaymentRepository : EfCoreRepository<PaymentServiceDbContext, BillPayment, Guid>, IBillPaymentRepository
    {
        public PaymentServiceDbContext pServiceDbContext { get; set; }

        public BillPaymentRepository(IDbContextProvider<PaymentServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 查询支付单号
        /// </summary>
        /// <param name="no">单号</param>
        /// <returns></returns>
        public async Task<BillPayment> GetBillPaymentByNoAsync(string no)
        {
            return await (await GetDbSetAsync()).Where(p => p.No == no).AsNoTracking().FirstOrDefaultAsync();
        }

        /// <summary>
        /// 更新支付单状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="status">支付状态</param>
        /// <param name="paymentCode">支付类型编码</param>
        /// <param name="payedMsg">支付回调后的状态描述</param>
        /// <param name="tradeNo">第三方平台交易流水号</param>
        /// <returns></returns>
        public async Task UpdateBillPaymentStatusAsync(Guid id, int status, string paymentCode, string payedMsg = "", string tradeNo = "")
        {
            BillPayment billPayment = await (await GetDbSetAsync()).Where(p => p.Id == id).FirstOrDefaultAsync();
            if (billPayment == null) return;
            billPayment.Status = status;
            billPayment.PaymentCode = paymentCode;
            billPayment.PayedMsg = payedMsg;
            billPayment.TradeNo = tradeNo;
        }
    }
}