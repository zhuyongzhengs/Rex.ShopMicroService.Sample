using Microsoft.Extensions.DependencyInjection;
using Rex.PaymentService.EntityFrameworkCore;
using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.PaymentService.Payments
{
    /// <summary>
    /// 支付方式仓储
    /// </summary>
    public class PaymentRepository : EfCoreRepository<PaymentServiceDbContext, Payment, Guid>, IPaymentRepository
    {
        public PaymentServiceDbContext pServiceDbContext { get; set; }

        public PaymentRepository(IDbContextProvider<PaymentServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}