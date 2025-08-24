using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.Forms
{
    /// <summary>
    /// 表单项仓储接口
    /// </summary>
    public interface IFormItemRepository : IRepository<FormItem, Guid>
    {
    }
}