using Microsoft.AspNetCore.Mvc;
using Rex.BaseService.Systems.SysUsers;
using Rex.BackendAggregationService.Core.Mappers;
using Rex.BackendAggregationService.Payments;
using Rex.OrderService.Bills;
using Rex.OrderService.Orders;
using Rex.PaymentService.Payments;
using Volo.Abp.Application.Dtos;

namespace Rex.BackendAggregationService.Controllers
{
    /// <summary>
    /// 退货单控制器
    /// </summary>
    [Route("api/backend/aggregation/billRefund")]
    [ApiController]
    public class BillRefundController : ControllerBase
    {
        private readonly IBillRefundAppService _billRefundAppService;
        private readonly IBillAftersalesAppService _billAftersalesAppService;
        private readonly IOrdersAppService _orderAppService;
        private readonly ISysUserAppService _sysUserAppService;

        public BillRefundController(IBillRefundAppService billRefundAppService, IBillAftersalesAppService billAftersalesAppService, IOrdersAppService orderAppService, ISysUserAppService sysUserAppService)
        {
            _billRefundAppService = billRefundAppService;
            _billAftersalesAppService = billAftersalesAppService;
            _orderAppService = orderAppService;
            _sysUserAppService = sysUserAppService;
        }

        /// <summary>
        /// 获取(分页)退款单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPageListAsync([FromQuery] GetBillRefundInput input)
        {
            if (!input.UserName.IsNullOrWhiteSpace())
            {
                SysUserDto user = await _sysUserAppService.GetSysUserByNameAsync(input.UserName);
                if (user != null) input.UserId = user.Id;
            }

            if (!input.AftersalesNo.IsNullOrWhiteSpace())
            {
                BillAftersalesDto bAftersales = await _billAftersalesAppService.GetNoAsync(input.AftersalesNo);
                if (bAftersales != null) input.AftersalesId = bAftersales.Id;
            }

            PagedResultDto<BillRefundDto> bRefundResult = await _billRefundAppService.GetListAsync(input);
            if (bRefundResult.TotalCount <= 0) return Ok(new PagedResultDto<BillRefundManyDto>());

            Guid[] userIds = bRefundResult.Items.Select(x => x.UserId).Distinct().ToArray();
            List<SysUserDto> users = await _sysUserAppService.GetManyAsync(userIds);

            Guid[] orderIds = bRefundResult.Items.Where(x => x.Type == 1).Select(x => Guid.Parse(x.SourceId)).Distinct().ToArray();
            List<OrderDto> orders = await _orderAppService.GetManyAsync(orderIds);

            Guid[] aftersalesIds = bRefundResult.Items.Select(x => x.BillAftersalesId).Distinct().ToArray();
            List<BillAftersalesDto> aftersales = await _billAftersalesAppService.GetManyAsync(aftersalesIds);

            List<BillRefundManyDto> bRefundManys = new List<BillRefundManyDto>();
            foreach (var bRefund in bRefundResult.Items)
            {
                BillRefundManyDto bRefundMany = ObjectMapper.Map<BillRefundDto, BillRefundManyDto>(bRefund);
                bRefundMany.BillAftersalesNo = aftersales.FirstOrDefault(x => x.Id == bRefund.BillAftersalesId)?.No;
                bRefundMany.UserName = users.FirstOrDefault(x => x.Id == bRefund.UserId)?.Name;
                bRefundMany.SourceNo = orders.FirstOrDefault(x => x.Id == Guid.Parse(bRefund.SourceId))?.No;
                bRefundManys.Add(bRefundMany);
            }
            return Ok(new PagedResultDto<BillRefundManyDto>()
            {
                TotalCount = bRefundResult.TotalCount,
                Items = bRefundManys
            });
        }
    }
}