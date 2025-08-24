using Microsoft.AspNetCore.Mvc;
using Rex.BackendAggregationService.Core.Mappers;
using Rex.BackendAggregationService.Goods;
using Rex.BaseService.Systems.SysUsers;
using Rex.BaseService.Systems.UserGrades;
using Rex.GoodService.Caching;
using Rex.GoodService.Goods;
using Rex.GoodService.Products;
using Rex.OrderService.Orders;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Helper;
using Rex.Service.Core.Models;
using Volo.Abp.Application.Dtos;
using IGoodCommonAppService = Rex.GoodService.Commons.ICommonAppService;

namespace Rex.BackendAggregationService.Controllers
{
    /// <summary>
    /// 商品控制器
    /// </summary>
    [Route("api/backend/aggregation/good")]
    [ApiController]
    public class GoodController : ControllerBase
    {
        private readonly IGoodsAppService _goodAppService;
        private readonly IGoodCommonAppService _goodCommonAppService;
        private readonly IGoodCommentAppService _goodCommentAppService;
        private readonly IUserGradeAppService _userGradeAppService;
        private readonly ISysUserAppService _sysUserAppService;
        private readonly IOrdersAppService _orderAppService;

        public GoodController(IGoodsAppService goodAppService, IGoodCommonAppService goodCommonAppService, IGoodCommentAppService goodCommentAppService, IUserGradeAppService userGradeAppService, ISysUserAppService sysUserAppService, IOrdersAppService orderAppService)
        {
            _goodAppService = goodAppService;
            _goodCommonAppService = goodCommonAppService;
            _goodCommentAppService = goodCommentAppService;
            _userGradeAppService = userGradeAppService;
            _sysUserAppService = sysUserAppService;
            _orderAppService = orderAppService;
        }

        /// <summary>
        /// 获取商品价格类型
        /// </summary>
        /// <returns></returns>
        [HttpGet("priceType")]
        public async Task<IActionResult> GetGoodPriceType()
        {
            List<EnumEntity> goodPriceTypes = EnumHelper.EnumToList<GlobalEnums.PriceType>();
            List<UserGradeDto> userGrades = await _userGradeAppService.GetManyAsync();
            if (userGrades.Any())
            {
                userGrades.ForEach(uGrade =>
                {
                    goodPriceTypes.Add(new EnumEntity
                    {
                        Description = uGrade.Title,
                        Title = "grade_price_" + uGrade.Id
                    });
                });
            }
            return Ok(goodPriceTypes);
        }

        /// <summary>
        /// 获取商品库存详情
        /// </summary>
        /// <param name="goodId">商品ID</param>
        /// <returns></returns>
        [HttpGet("stockDetail/{goodId}")]
        public async Task<IActionResult> GetProductStockDetails([FromRoute] Guid goodId)
        {
            List<ProductDto> products = await _goodCommonAppService.GetProductByGoodIdsAsync(goodId);
            if (!products.Any()) return NoContent();

            List<ProductDetailDto> pStockDetails = ObjectMapper.Map<List<ProductDto>, List<ProductDetailDto>>(products);
            foreach (var pStockDetail in pStockDetails)
            {
                ProductStockRc productStockRc = await _goodCommonAppService.GetProductStockAsync(pStockDetail.Id, goodId);
                if (pStockDetail == null) continue;
                pStockDetail.Stock = productStockRc.Stock;
                pStockDetail.FreezeStock = productStockRc.FreezeStock;
                pStockDetail.RemainingStock = productStockRc.Stock - productStockRc.FreezeStock;
            }
            return Ok(pStockDetails);
        }

        /// <summary>
        /// 获取(分页)商品评价列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        [HttpGet("comment/pageList")]
        public async Task<IActionResult> GetGoodCommentPageListAsync([FromQuery] GetGoodCommentInput input)
        {
            PagedResultDto<GoodCommentDto> goodCommentResult = await _goodCommentAppService.GetListAsync(input);
            if (goodCommentResult.TotalCount <= 0) return Ok(new PagedResultDto<GoodCommentManyDto>());

            Guid[] userIds = goodCommentResult.Items.Select(i => i.UserId).Distinct().ToArray();
            List<SysUserDto> sysUsers = await _sysUserAppService.GetManyAsync(userIds);

            List<Guid> goodIds = goodCommentResult.Items.Select(i => i.GoodId).Distinct().ToList();
            List<GoodDto> goods = await _goodAppService.GetGoodByIdsAsync(goodIds, false);

            Guid[] orderIds = goodCommentResult.Items.Select(i => i.OrderId).Distinct().ToArray();
            List<OrderDto> orders = await _orderAppService.GetManyAsync(orderIds, false);

            PagedResultDto<GoodCommentManyDto> gCommentManyResult = new PagedResultDto<GoodCommentManyDto>();
            gCommentManyResult.TotalCount = goodCommentResult.TotalCount;

            List<GoodCommentManyDto> gCommentManyList = new List<GoodCommentManyDto>();
            foreach (var goodComment in goodCommentResult.Items)
            {
                GoodCommentManyDto gCommentMany = ObjectMapper.Map<GoodCommentDto, GoodCommentManyDto>(goodComment);
                SysUserDto sysUser = sysUsers.FirstOrDefault(u => u.Id == goodComment.UserId);
                if (sysUser != null)
                {
                    gCommentMany.UserName = sysUser.Name;
                    gCommentMany.Avatar = sysUser.Avatar;
                    gCommentMany.PhoneNumber = sysUser.PhoneNumber;
                }
                GoodDto good = goods.FirstOrDefault(g => g.Id == goodComment.GoodId);
                if (good != null) gCommentMany.GoodName = good.Name;
                OrderDto order = orders.FirstOrDefault(o => o.Id == goodComment.OrderId);
                if (order != null)
                {
                    gCommentMany.OrderNo = order.No;
                    gCommentMany.ShipName = order.ShipName;
                    gCommentMany.ShipMobile = order.ShipMobile;
                }

                gCommentManyList.Add(gCommentMany);
            }
            gCommentManyResult.Items = gCommentManyList;
            return Ok(gCommentManyResult);
        }
    }
}