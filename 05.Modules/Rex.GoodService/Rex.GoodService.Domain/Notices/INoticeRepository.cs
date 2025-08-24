using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.Notices
{
    /// <summary>
    /// 公告仓储接口
    /// </summary>
    public interface INoticeRepository : IRepository<Notice, Guid>
    {
    }
}