using System;
using Volo.Abp.Domain.Repositories;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 售后单图片关联仓储接口
    /// </summary>
    public interface IBillAftersalesImagesRepository : IRepository<BillAftersalesImages, Guid>
    {
        // ...
    }
}