using Microsoft.EntityFrameworkCore;
using Rex.PaymentService.EntityFrameworkCore;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Events.Payments;
using Rex.Service.Core.Helper;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Guids;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 退款单仓储
    /// </summary>
    public class BillRefundRepository : EfCoreRepository<PaymentServiceDbContext, BillRefund, Guid>, IBillRefundRepository
    {
        public PaymentServiceDbContext pServiceDbContext { get; set; }
        private readonly IBillPaymentRepository _billPaymentRepository;

        private readonly IGuidGenerator _guidGenerator;
        public BillRefundRepository(IDbContextProvider<PaymentServiceDbContext> dbContextProvider, IBillPaymentRepository billPaymentRepository, IGuidGenerator guidGenerator) : base(dbContextProvider)
        {
            _billPaymentRepository = billPaymentRepository;
            _guidGenerator = guidGenerator;
        }

        /// <summary>
        /// 根据退款单号更新备注信息
        /// </summary>
        /// <param name="no">退款单号</param>
        /// <param name="memo">退款内容</param>
        /// <returns></returns>
        public async Task UpdateMemoByNoAsync(string no, string memo)
        {
            BillRefund billRefund = await pServiceDbContext.BillRefunds.FirstOrDefaultAsync(x => x.No == no);
            if (billRefund == null) return;
            billRefund.Memo = memo;
            await pServiceDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 创建退款单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateBillRefundAsync(BillRefundCreateEto input)
        {
            BillRefund billRefund = new BillRefund(_guidGenerator.Create());
            billRefund.No = CommonHelper.GetSerialNumberType((int)GlobalEnums.SerialNumberType.RefundCode);
            billRefund.BillAftersalesId = input.BillAftersalesId;
            billRefund.Money = input.Money;
            billRefund.UserId = input.UserId;
            billRefund.SourceId = input.SourceId;
            billRefund.Type = input.Type;

            // 取支付成功的支付单号
            BillPayment billPayment = await (await _billPaymentRepository.GetQueryableAsync()).Where(x => x.SourceId == input.SourceId && x.Type == input.Type && x.Status == (int)GlobalEnums.BillPaymentsStatus.Payed).FirstOrDefaultAsync();
            if (billPayment != null)
            {
                billRefund.PaymentCode = billPayment.PaymentCode;
                billRefund.TradeNo = billPayment.TradeNo;
            }

            billRefund.Status = (int)GlobalEnums.BillRefundStatus.Norefund;
            await pServiceDbContext.BillRefunds.AddAsync(billRefund);
            await pServiceDbContext.SaveChangesAsync();
        }
    }
}