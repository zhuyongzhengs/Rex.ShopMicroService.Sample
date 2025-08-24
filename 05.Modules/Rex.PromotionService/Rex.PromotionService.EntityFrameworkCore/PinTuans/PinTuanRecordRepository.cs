using Microsoft.Extensions.DependencyInjection;
using Rex.PromotionService.EntityFrameworkCore;
using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 拼团记录仓储
    /// </summary>
    public class PinTuanRecordRepository : EfCoreRepository<PromotionServiceDbContext, PinTuanRecord, Guid>, IPinTuanRecordRepository
    {
        public PromotionServiceDbContext gServiceDbContext { get; set; }

        public PinTuanRecordRepository(IDbContextProvider<PromotionServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}