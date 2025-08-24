using Rex.GoodService.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.Forms
{
    /// <summary>
    ///表单项仓储
    /// </summary>
    public class FormItemRepository : EfCoreRepository<GoodServiceDbContext, FormItem, Guid>, IFormItemRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public FormItemRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}