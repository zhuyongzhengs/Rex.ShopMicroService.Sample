using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.Labels
{
    /// <summary>
    /// 标签仓储接口
    /// </summary>
    public interface ILabelRepository : IRepository<Label, Guid>
    {
    }
}