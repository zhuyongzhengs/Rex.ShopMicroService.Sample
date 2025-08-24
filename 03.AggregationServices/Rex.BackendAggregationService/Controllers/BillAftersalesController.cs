using Microsoft.AspNetCore.Mvc;
using Rex.BackendAggregationService.Core.Mappers;
using Rex.BackendAggregationService.Orders;
using Rex.BaseService.Systems.SysUsers;
using Rex.OrderService.Bills;
using Rex.OrderService.Orders;
using Volo.Abp.Application.Dtos;

namespace Rex.BackendAggregationService.Controllers
{
    /// <summary>
    /// 售后单控制器
    /// </summary>
    [Route("api/backend/aggregation/billAftersales")]
    [ApiController]
    public partial class BillAftersalesController : ControllerBase
    {
        private readonly IOrdersAppService _orderAppService;
        private readonly IBillAftersalesAppService _billAftersalesAppService;
        private readonly ISysUserAppService _sysUserAppService;

        public BillAftersalesController(IOrdersAppService orderAppService, IBillAftersalesAppService billAftersalesAppService, ISysUserAppService sysUserAppService)
        {
            _orderAppService = orderAppService;
            _billAftersalesAppService = billAftersalesAppService;
            _sysUserAppService = sysUserAppService;
        }

        /// <summary>
        /// 获取(分页)售后单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetBillAftersalesPageListAsync([FromQuery] GetBillAftersalesInput input)
        {
            PagedResultDto<BillAftersalesDto> billAftersalesResult = await _billAftersalesAppService.GetListAsync(input);
            if (billAftersalesResult.Items == null || billAftersalesResult.Items.Count == 0) return Ok(new PagedResultDto<BillAftersalesManyDto>());

            Guid[] userIds = billAftersalesResult.Items.Select(x => x.UserId).Distinct().ToArray();
            Guid[] orderIds = billAftersalesResult.Items.Select(x => x.OrderId).Distinct().ToArray();
            List<SysUserDto> sysUsers = await _sysUserAppService.GetManyAsync(userIds);
            List<OrderDto> orders = await _orderAppService.GetManyAsync(orderIds);
            List<BillAftersalesManyDto> bAftersalesManyList = new List<BillAftersalesManyDto>();
            foreach (var bAftersales in billAftersalesResult.Items)
            {
                BillAftersalesManyDto billAftersalesItem = ObjectMapper.Map<BillAftersalesDto, BillAftersalesManyDto>(bAftersales);
                SysUserDto sysUser = sysUsers.FirstOrDefault(x => x.Id == bAftersales.UserId);
                if (sysUser != null) billAftersalesItem.UserName = sysUser.Name;
                OrderDto order = orders.FirstOrDefault(x => x.Id == bAftersales.OrderId);
                if (order != null) billAftersalesItem.OrderNo = order.No;
                bAftersalesManyList.Add(billAftersalesItem);
            }
            return Ok(new PagedResultDto<BillAftersalesManyDto>()
            {
                TotalCount = billAftersalesResult.TotalCount,
                Items = bAftersalesManyList
            });
        }

        /// <summary>
        /// 获取(分页)售后单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillAftersalesByIdAsync([FromRoute] Guid id)
        {
            BillAftersalesDto billAftersales = await _billAftersalesAppService.GetAsync(id, true);
            if (billAftersales == null) return BadRequest();

            BillAftersalesDtlDto billAftersalesDtl = ObjectMapper.Map<BillAftersalesDto, BillAftersalesDtlDto>(billAftersales);
            SysUserDto sysUser = await _sysUserAppService.GetAsync(billAftersalesDtl.UserId);
            billAftersalesDtl.UserName = sysUser.Name;
            if (billAftersales.BillAftersalesImagess != null && billAftersales.BillAftersalesImagess.Any())
            {
                billAftersalesDtl.Images = billAftersales.BillAftersalesImagess.Select(x => x.ImageUrl).ToList();
            }

            OrderDto order = await _orderAppService.GetAsync(billAftersales.OrderId, true);
            if (order == null || order.OrderItems == null || !order.OrderItems.Any()) Ok(billAftersalesDtl);
            billAftersalesDtl.OrderNo = order.No;

            billAftersalesDtl.ProductItems = new List<BillAftersalesDtlItemDto>();
            foreach (var orderItem in order.OrderItems)
            {
                BillAftersalesDtlItemDto bAftersalesDtlItem = new BillAftersalesDtlItemDto();
                bAftersalesDtlItem.AftersalesId = billAftersales.Id;
                bAftersalesDtlItem.OrderItemId = orderItem.Id;
                bAftersalesDtlItem.GoodId = orderItem.GoodId;
                bAftersalesDtlItem.ProductId = orderItem.ProductId;
                bAftersalesDtlItem.Sn = orderItem.Sn;
                bAftersalesDtlItem.Bn = orderItem.Bn;
                bAftersalesDtlItem.Name = orderItem.Name;
                bAftersalesDtlItem.ImageUrl = orderItem.ImageUrl;
                bAftersalesDtlItem.Amount = orderItem.Amount;
                bAftersalesDtlItem.BuyNums = orderItem.Nums;
                bAftersalesDtlItem.SendNums = orderItem.SendNums;
                bAftersalesDtlItem.Addon = orderItem.Addon;
                BillAftersalesItemDto bAftersalesItem = billAftersales.BillAftersalesItems.FirstOrDefault(x => x.OrderItemId == orderItem.Id);
                if (bAftersalesItem != null)
                {
                    bAftersalesDtlItem.Id = bAftersalesItem.Id;
                    bAftersalesDtlItem.Nums = bAftersalesItem.Nums;
                    bAftersalesDtlItem.Checked = true;
                }
                billAftersalesDtl.ProductItems.Add(bAftersalesDtlItem);
            }
            return Ok(billAftersalesDtl);
        }
    }
}