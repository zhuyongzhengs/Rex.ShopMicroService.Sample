using Rex.GoodService.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.Labels
{
    /// <summary>
    /// 标签仓储
    /// </summary>
    public class LabelRepository : EfCoreRepository<GoodServiceDbContext, Label, Guid>, ILabelRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public LabelRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}