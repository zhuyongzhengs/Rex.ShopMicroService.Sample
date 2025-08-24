using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.Products
{
    /// <summary>
    /// 货品仓储接口
    /// </summary>
    public interface IProductRepository : IRepository<Product, Guid>
    {
        /// <summary>
        /// 库存变更
        /// </summary>
        /// <param name="productId">货品ID</param>
        /// <param name="type">类型：1[下单]、2[发货]、3[退款]、4[退货]、5[取消订单]、6[完成订单]</param>
        /// <param name="num">数量</param>
        /// <returns></returns>
        Task<bool> ChangeStockAsync(Guid productId, int type = 1, int num = 0);

        /// <summary>
        /// 修改商品冻结库存
        /// </summary>
        /// <param name="freezeStockDic">冻结库存</param>
        /// <returns></returns>
        Task UpdateFreezeStockAsync(Dictionary<Guid, int> freezeStockDic);
    }
}