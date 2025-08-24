using Microsoft.AspNetCore.Mvc;
using Rex.OrderService.Orders;
using Volo.Abp.Application.Dtos;

namespace Rex.FrontAggregationService.Controllers
{
    /// <summary>
    /// 订单控制器
    /// </summary>
    [Route("api/front/aggregation/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrdersAppService _orderAppService;

        public OrderController(IOrdersAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        /*
        提示：已挪到 CartController ---> OrderConfirmAsync

        /// <summary>
        /// 创建用户订单
        /// </summary>
        /// <param name="input">用户订单Dto</param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] UserOrderCreateDto input)
        {
            // 获取当前用户信息
            CurrentSysUserEto currentUser = await _baseCommonAppService.GetCurrentUserAsync();

            #region 0.校验

            if (input.ReceiptType == (int)OrderReceiptType.Logistics || input.ReceiptType == (int)OrderReceiptType.IntraCityService)
            {
                if (!input.UserShipId.HasValue) throw new UserFriendlyException("请选择收货地址！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
            }
            else if (input.ReceiptType == (int)OrderReceiptType.SelfDelivery)
            {
                if (!input.StoreId.HasValue) throw new UserFriendlyException("请选择自提门店！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
                if (input.LadingName.IsNullOrWhiteSpace()) throw new UserFriendlyException("请输入姓名！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
                if (input.LadingMobile.IsNullOrWhiteSpace()) throw new UserFriendlyException("请输入电话！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
            }
            else
            {
                throw new UserFriendlyException("为查询到配送方式！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
            }

            #endregion 0.校验

            #region 1.创建订单明细

            List<OrderItemCreateDto> orderItemCreates = new List<OrderItemCreateDto>();
            List<CartDto> cartList = await _orderCommonAppService.GetUserCartsAsync(input.OrderType);
            if (cartList != null && cartList.Count > 0) cartList = cartList.Where(x => input.CartIds.Contains(x.Id)).ToList();
            if (cartList == null || cartList.Count == 0) throw new UserFriendlyException("购物车为空，请先添加商品到购物车！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
            foreach (CartDto cart in cartList)
            {
                // 获取货品信息
                ProductDto productDto = await _goodCommonAppService.GetProductByIdAsync(cart.ProductId);
                if (productDto == null || !productDto.GoodId.HasValue)
                {
                    // 商品不存在，删除购物车
                    await _cartAppService.DeleteCartAsnyc(new List<Guid>() { cart.Id });
                    continue;
                }

                // 获取商品信息
                GoodDto goodDto = await _goodCommonAppService.GetGoodByIdAsync(productDto.GoodId.Value, false);
                if (!goodDto.IsMarketable)
                {
                    // 商品下架，删除购物车
                    await _cartAppService.DeleteCartAsnyc(new List<Guid>() { cart.Id });
                    continue;
                }

                // 创建订单明细
                OrderItemCreateDto orderItemCreate = new OrderItemCreateDto();
                orderItemCreate.GoodId = goodDto.Id;
                orderItemCreate.ProductId = productDto.Id;
                orderItemCreate.Sn = productDto.Sn;
                orderItemCreate.Bn = goodDto.BarCode;
                orderItemCreate.Name = goodDto.Name;

                // 获取商品会员折扣价格
                decimal price = Convert.ToInt32(productDto.Price);
                decimal preferentialPrice = 0;
                if (currentUser.Grade != null)
                {
                    List<GoodGradeDto> goodGrades = await _goodCommonAppService.GetGoodGradeByGoodIdAsync(goodDto.Id);
                    if (goodGrades != null && goodGrades.Count > 0)
                    {
                        GoodGradeDto goodGrade = goodGrades.FirstOrDefault(g => g.GradeId == currentUser.Grade.Id);
                        if (goodGrade != null)
                        {
                            preferentialPrice = goodGrade.GradePrice;
                            decimal vipPrice = productDto.Price - preferentialPrice;
                            price = vipPrice > 0 ? vipPrice : 0;
                        }
                    }
                }
                orderItemCreate.Price = price;
                orderItemCreate.CostPrice = productDto.CostPrice;
                orderItemCreate.MktPrice = productDto.MktPrice;
                orderItemCreate.ImageUrl = string.IsNullOrWhiteSpace(productDto.Images) ? goodDto.Image : productDto.Images;
                orderItemCreate.Nums = cart.Nums;
                orderItemCreate.Amount = Math.Round(orderItemCreate.Nums * orderItemCreate.Price, 2);
                orderItemCreate.PromotionAmount = preferentialPrice > 0 ? Math.Round(orderItemCreate.Nums * preferentialPrice, 2) : 0;
                orderItemCreate.PromotionList = string.Empty;
                orderItemCreate.Weight = Math.Round(orderItemCreate.Nums * productDto.Weight, 2);
                orderItemCreate.SendNums = 0;
                orderItemCreate.Addon = productDto.SpesDesc;
                orderItemCreates.Add(orderItemCreate);
            }

            #endregion 1.创建订单明细

            #region 2.库存变更(扣减库存)

            List<OrderItemCreateDto> insertOrderItems = new List<OrderItemCreateDto>();
            foreach (OrderItemCreateDto orderItem in orderItemCreates)
            {
                bool changeStockResult = await _goodCommonAppService.ChangeStockAsync(orderItem.ProductId, (int)OrderChangeStockType.Order, orderItem.Nums);
                if (!changeStockResult)
                {
                    throw new UserFriendlyException($"该商品库存不足，请重新选择！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
                }
                insertOrderItems.Add(orderItem);
            }

            #endregion 2.库存变更(扣减库存)

            #region 3.创建订单

            OrderCreateDto orderCreate = new OrderCreateDto();
            orderCreate.PayedAmount = 0;
            orderCreate.OrderType = input.OrderType;
            orderCreate.Point = input.Point;
            orderCreate.Coupon = input.Coupon;
            orderCreate.ReceiptType = input.ReceiptType;
            orderCreate.ObjectId = input.ObjectId;
            orderCreate.Memo = input.Memo;
            Request.Headers.TryGetValue("source-client", out StringValues userAgent);
            if (userAgent.Count > 0)
            {
                orderCreate.Source = Int32.Parse(userAgent.ToString());
            }
            orderCreate.PaymentCode = input.PaymentCode;
            orderCreate.Ip = Request.HttpContext.GetRemoteIpAddress();
            orderCreate.ShipStatus = (int)OrderShipStatus.No;
            orderCreate.Status = (int)OrderStatus.Normal;
            orderCreate.ConfirmStatus = (int)OrderConfirmStatus.ReceiptNotConfirmed;
            if (orderCreate.ReceiptType == (int)OrderReceiptType.Logistics || orderCreate.ReceiptType == (int)OrderReceiptType.IntraCityService)
            {
                // 快递邮寄
                UserShipDto userShip = await _userShipAppService.GetAsync(input.UserShipId.Value);
                if (userShip == null) throw new UserFriendlyException($"您还未选择收货地址或该收货地址不存在，请检查！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
                orderCreate.ShipAreaId = userShip.AreaId;
                orderCreate.DisplayArea = userShip.DisplayArea;
                orderCreate.ShipAddress = userShip.Address;
                orderCreate.ShipName = userShip.Name;
                orderCreate.ShipMobile = userShip.Mobile;
                // 获取配送方式
                ShipDto ship = await _orderCommonAppService.GetShipByAreaIdAsync(orderCreate.ShipAreaId);
                if (ship != null)
                {
                    orderCreate.LogisticsId = ship.Id;
                    orderCreate.LogisticsName = ship.Name;
                    orderCreate.StoreId = null;
                }
            }
            else
            {
                StoreDto store = await _goodCommonAppService.GetStoreByIdAsync(input.StoreId.Value);
                if (store == null) throw new UserFriendlyException($"自提门店不存在，请检查！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
                orderCreate.ShipAreaId = store.AreaId;
                orderCreate.ShipAddress = store.Address;
                orderCreate.ShipName = input.LadingName;
                orderCreate.ShipMobile = input.LadingMobile;
                orderCreate.LogisticsId = null;
                orderCreate.LogisticsName = null;
            }
            decimal goodAmount = insertOrderItems.Sum(item => item.Amount);
            decimal promotionAmount = insertOrderItems.Sum(item => item.PromotionAmount);
            decimal goodWeight = insertOrderItems.Sum(item => item.Weight);
            orderCreate.GoodAmount = Math.Round(goodAmount, 2);
            orderCreate.OrderAmount = Math.Round(goodAmount - promotionAmount, 2);
            orderCreate.Weight = Math.Round(goodWeight, 2);
            orderCreate.PayStatus = (int)OrderPayStatus.No;
            if (orderCreate.OrderAmount == 0)
            {
                orderCreate.PayStatus = (int)OrderPayStatus.Yes;
                orderCreate.PaymentTime = DateTime.Now;
            }

            // 门店/同城配送订单，强制无运费
            if (input.ReceiptType == (int)OrderReceiptType.IntraCityService || input.ReceiptType == (int)OrderReceiptType.SelfDelivery)
            {
                orderCreate.CostFreight = 0;
            }
            else
            {
                ShipDto shipDto = await _orderCommonAppService.GetShipByAreaIdAsync(input.ShipAreaId);
                orderCreate.CostFreight = FaHandle.GetShipCost(shipDto, orderCreate.Weight, orderCreate.OrderAmount, input.ShipAreaId);
                orderCreate.OrderAmount = Math.Round(orderCreate.OrderAmount + orderCreate.CostFreight, 2);
            }

            #endregion 3.创建订单

            #region 4.保存订单

            OrderDto order = await _orderAppService.CreateOrderAsync(orderCreate);
            orderItemCreates.ForEach(item => item.OrderId = order.Id);
            insertOrderItems.ForEach(item => item.OrderId = order.Id);
            List<OrderItemDto> outOrderItems = new List<OrderItemDto>();
            if (insertOrderItems.Count < 1)
            {
                outOrderItems = await _orderAppService.CreateOrderItemAsync(orderItemCreates);
                await _orderAppService.UpdateOrderStatusByIdAsync(order.Id, (int)OrderStatus.Cancel);
            }
            else
            {
                outOrderItems = await _orderAppService.CreateOrderItemAsync(insertOrderItems);
            }
            if (outOrderItems.Count < 1) throw new UserFriendlyException("订单创建失败，请稍后重试！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());

            // TODO: 优惠劵核销 处理
            // TODO: 优积分核销 处理
            // TODO: 不同的订单类型会有不同的操作 处理

            // 清空购物车
            await _cartAppService.DeleteCartAsnyc(cartList.Select(x => x.Id).ToList());

            // 订单记录
            OrderDto orderDto = await _orderAppService.GetUserOrderByNoAsync(order.No);
            OrderLogCreateDto orderLogCreate = new OrderLogCreateDto();
            orderLogCreate.OrderId = order.Id;
            orderLogCreate.UserId = currentUser.Id;
            orderLogCreate.Type = (int)OrderLogType.LogTypeCreate;
            orderLogCreate.Msg = "订单创建";
            orderLogCreate.Data = JsonSerializer.Serialize(orderDto, AspNetCoreExtension.GetJsonSerializerOptions());
            await _orderAppService.CreateOrderLogAsync(orderLogCreate);

            // 0元订单，直接支付成功
            if (order.OrderAmount <= 0)
            {
                orderLogCreate.Type = (int)OrderLogType.LogTypePay;
                orderLogCreate.Msg = "0元订单直接支付成功";
                await _orderAppService.CreateOrderLogAsync(orderLogCreate);
            }

            // 消息推送

            #endregion 4.保存订单

            return Ok(orderDto);
        }
        */

        /// <summary>
        /// 获取(分页)订单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        [HttpGet("user")]
        public async Task<IActionResult> GetUserOrderPageListAsync([FromQuery] GetUserOrderInput input)
        {
            PagedResultDto<UserOrderDto> userOrderPagedResult = await _orderAppService.GetUserOrdersAsync(input);
            return Ok(userOrderPagedResult);
        }
    }
}