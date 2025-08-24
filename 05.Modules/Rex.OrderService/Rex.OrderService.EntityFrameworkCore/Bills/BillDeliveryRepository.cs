using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rex.OrderService.EntityFrameworkCore;
using Rex.Service.Core.Events;
using Rex.Service.Core.Events.Goods;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EventBus.Distributed;
using static Rex.Service.Core.Configurations.GlobalEnums;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 发货单仓储
    /// </summary>
    public class BillDeliveryRepository : EfCoreRepository<OrderServiceDbContext, BillDelivery, Guid>, IBillDeliveryRepository
    {
        public OrderServiceDbContext oServiceDbContext { get; set; }

        private readonly ICapPublisher _capEventBus;

        public BillDeliveryRepository(IDbContextProvider<OrderServiceDbContext> dbContextProvider, ICapPublisher capEventBus) : base(dbContextProvider)
        {
            _capEventBus = capEventBus;
        }

        /// <summary>
        /// 订单发货
        /// </summary>
        /// <param name="input">发货信息</param>
        /// <returns></returns>
        public async Task CreateOrderDeliveryAsync(BillDelivery input)
        {
            await InsertAsync(input);

            // 更新[库存变更]状态
            foreach (var deliveryItem in input.BillDeliveryItems)
            {
                await _capEventBus.PublishAsync(ChangeStockEto.EventName, new ChangeStockEto()
                {
                    ProductId = deliveryItem.ProductId,
                    ChangeStockType = (int)OrderChangeStockType.Send,
                    Nums = deliveryItem.Nums
                });
            }
        }

        /// <summary>
        /// 查询发货单信息
        /// </summary>
        /// <param name="id">发货单ID</param>
        /// <returns></returns>
        public async Task<BillDelivery> GetBillDeliveryByIdAsync(Guid id, bool includeDetails = false)
        {
            return await (await GetDbSetAsync())
                .Where(p => p.Id == id)
                .IncludeIf(includeDetails, p => p.BillDeliveryItems).FirstOrDefaultAsync();
        }
    }
}