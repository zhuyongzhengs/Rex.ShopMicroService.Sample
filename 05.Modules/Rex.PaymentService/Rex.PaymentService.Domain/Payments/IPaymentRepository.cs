using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 支付方式仓储接口
    /// </summary>
    public interface IPaymentRepository : IRepository<Payment, Guid>
    {
        // ...
    }
}