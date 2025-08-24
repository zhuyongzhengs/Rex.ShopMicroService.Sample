using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.PromotionService.PinTuans
{
    /// <summary>
    /// 拼团记录仓储接口
    /// </summary>
    public interface IPinTuanRecordRepository : IRepository<PinTuanRecord, Guid>
    {
    }
}