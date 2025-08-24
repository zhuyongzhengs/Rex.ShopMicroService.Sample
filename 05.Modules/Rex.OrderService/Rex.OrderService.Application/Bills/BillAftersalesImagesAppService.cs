using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Rex.OrderService.Bills
{
    /// <summary>
    /// 售后单图片关联服务
    /// </summary>
    [RemoteService]
    [Authorize]
    public class BillAftersalesImagesAppService : ApplicationService, IBillAftersalesImagesAppService
    {
        private readonly IBillAftersalesImagesRepository _billAftersalesImagesRepository;

        public BillAftersalesImagesAppService(IBillAftersalesImagesRepository billAftersalesImagesRepository)
        {
            _billAftersalesImagesRepository = billAftersalesImagesRepository;
        }
    }
}