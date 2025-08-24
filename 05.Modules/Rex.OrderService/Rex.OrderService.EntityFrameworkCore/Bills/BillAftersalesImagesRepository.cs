using Microsoft.Extensions.DependencyInjection;
using Rex.OrderService.EntityFrameworkCore;
using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 售后单图片关联仓储
    /// </summary>
    public class BillAftersalesImagesRepository : EfCoreRepository<OrderServiceDbContext, BillAftersalesImages, Guid>, IBillAftersalesImagesRepository
    {
        public OrderServiceDbContext oServiceDbContext { get; set; }

        public BillAftersalesImagesRepository(IDbContextProvider<OrderServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}