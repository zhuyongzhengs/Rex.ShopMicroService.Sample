using Rex.GoodService.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.Notices
{
    /// <summary>
    /// 公告仓储
    /// </summary>
    public class NoticeRepository : EfCoreRepository<GoodServiceDbContext, Notice, Guid>, INoticeRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public NoticeRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}