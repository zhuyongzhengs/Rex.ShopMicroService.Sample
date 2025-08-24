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
    /// 退货单控制器
    /// </summary>
    [Route("api/backend/aggregation/billReship")]
    [ApiController]
    public partial class BillReshipController : ControllerBase
    {
        private readonly IOrdersAppService _orderAppService;
        private readonly IBillReshipAppService _billReshipAppService;
        private readonly ISysUserAppService _sysUserAppService;

        public BillReshipController(IOrdersAppService orderAppService, IBillReshipAppService billReshipAppService, ISysUserAppService sysUserAppService)
        {
            _orderAppService = orderAppService;
            _billReshipAppService = billReshipAppService;
            _sysUserAppService = sysUserAppService;
        }

        /// <summary>
        /// 获取(分页)退货单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetBillReshipPageListAsync([FromQuery] GetBillReshipInput input)
        {
            PagedResultDto<BillReshipDto> billReshipResult = await _billReshipAppService.GetListAsync(input);
            if (billReshipResult.Items == null || billReshipResult.Items.Count == 0) return Ok(new PagedResultDto<BillReshipManyDto>());

            Guid[] userIds = billReshipResult.Items.Select(x => x.UserId).Distinct().ToArray();
            List<SysUserDto> sysUsers = await _sysUserAppService.GetManyAsync(userIds);
            List<BillReshipManyDto> bReshipManyList = new List<BillReshipManyDto>();
            foreach (var bReship in billReshipResult.Items)
            {
                BillReshipManyDto billReshipItem = ObjectMapper.Map<BillReshipDto, BillReshipManyDto>(bReship);
                billReshipItem.OrderNo = billReshipItem.Order?.No;
                billReshipItem.AftersalesNo = billReshipItem.Aftersales?.No;
                SysUserDto sysUser = sysUsers.FirstOrDefault(x => x.Id == bReship.UserId);
                if (sysUser != null) billReshipItem.UserName = sysUser.Name;
                bReshipManyList.Add(billReshipItem);
            }
            return Ok(new PagedResultDto<BillReshipManyDto>()
            {
                TotalCount = billReshipResult.TotalCount,
                Items = bReshipManyList
            });
        }
    }
}