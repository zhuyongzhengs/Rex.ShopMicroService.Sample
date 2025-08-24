using Microsoft.EntityFrameworkCore;
using Rex.GoodService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.Forms
{
    /// <summary>
    ///表单仓储
    /// </summary>
    public class FormRepository : EfCoreRepository<GoodServiceDbContext, Form, Guid>, IFormRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public FormRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取(分页) 表单列表总数
        /// </summary>
        /// <param name="name">表单名称</param>
        /// <param name="type">表单类型</param>
        /// <returns></returns>
        public async Task<long> GetPageCountAsync(string? name, int? type, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .WhereIf(type.HasValue, p => p.Type == type)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取(分页) 表单列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="name">表单名称</param>
        /// <param name="type">表单类型</param>
        /// <returns></returns>
        public async Task<List<Form>> GetPageListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? name,
            int? type,
            CancellationToken cancellationToken = default
            )
        {
            return await (await GetDbSetAsync())
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .WhereIf(type.HasValue, p => p.Type == type)
                .Include(p => p.FormItems)
                .OrderByIf<Form, IQueryable<Form>>(!sorting.IsNullOrWhiteSpace(), sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}