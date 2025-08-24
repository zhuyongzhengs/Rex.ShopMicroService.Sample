using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Events.Goods;
using System.Diagnostics;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.GoodService.Products
{
    /// <summary>
    /// 库存变更事件处理
    /// </summary>
    /// <remarks>
    /// 订阅事件
    /// </remarks>
    [Dependency(ServiceLifetime.Scoped)]
    public class ChangeStockEventHandler : ICapSubscribe
    {
        private readonly IProductRepository _productRepository;

        public ChangeStockEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [CapSubscribe(ChangeStockEto.EventName)]
        public async Task ChangeStockAsync(ChangeStockEto eventData)
        {
            bool result = await _productRepository.ChangeStockAsync(eventData.ProductId, eventData.ChangeStockType, eventData.Nums);
            string changeStockTypeDesc = eventData.ChangeStockType.GetEnum<OrderChangeStockType>().GetDescription();
            if (!result)
                throw new UserFriendlyException($"库存变更失败，变更类型为：{changeStockTypeDesc}！", SystemStatusCode.Fail.ToGoodServicePrefix());

            Debug.WriteLine($"{changeStockTypeDesc}成功！");
        }
    }
}