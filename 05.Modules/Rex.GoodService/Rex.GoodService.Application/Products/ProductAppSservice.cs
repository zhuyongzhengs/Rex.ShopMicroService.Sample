using Microsoft.AspNetCore.Authorization;
using Rex.Service.Permission.GoodServices;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Products
{
    /// <summary>
    /// 货品服务
    /// </summary>
    [RemoteService]
    [Authorize(GoodServicePermissions.Goods.Default)]
    public class ProductAppSservice : ApplicationService, IProductAppService
    {
        // ...
    }
}