using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Rex.BaseService.Systems.SysUsers;
using Rex.BaseService.UserShips;
using Rex.FrontAggregationService.Core;
using Rex.FrontAggregationService.Orders;
using Rex.GoodService.Caching;
using Rex.GoodService.Goods;
using Rex.GoodService.Products;
using Rex.OrderService.Carts;
using Rex.OrderService.Ships;
using Rex.PromotionService.Promotions;
using Rex.Service.AspNetCore.Extensions;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Events.Orders;
using Rex.Service.Core.Helper;
using Rex.Service.Core.Models;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Nodes;
using Volo.Abp;
using Volo.Abp.Caching;
using static Rex.Service.Core.Configurations.GlobalEnums;
using IBaseCommonAppService = Rex.BaseService.Systems.Commons.ICommonAppService;
using IGoodCommonAppService = Rex.GoodService.Commons.ICommonAppService;
using IOrderCommonAppService = Rex.OrderService.Commons.ICommonAppService;
using IPromotionCommonAppService = Rex.PromotionService.Commons.ICommonAppService;

namespace Rex.FrontAggregationService.Controllers
{
    /// <summary>
    /// 购物车控制器
    /// </summary>
    [Route("api/front/aggregation/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IBaseCommonAppService _baseCommonAppService;
        private readonly ISysUserAppService _sysUserAppService;
        private readonly IGoodCommonAppService _goodCommonAppService;
        private readonly IPromotionCommonAppService _promotionCommonAppService;
        private readonly ICartAppService _cartAppService;
        private readonly IOrderCommonAppService _orderCommonAppService;
        private readonly ICouponAppService _couponAppService;
        private readonly IUserShipAppService _userShipAppService;
        private readonly ICapPublisher _capEventBus;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CartController> _logger;

        public IDistributedCache<ProductStockRc> ProductStockDistributedCache { get; set; }

        public CartController(IBaseCommonAppService baseCommonAppService, ISysUserAppService sysUserAppService, ICartAppService cartAppService, IGoodCommonAppService goodCommonAppService, IPromotionCommonAppService promotionCommonAppService, IOrderCommonAppService orderCommonAppService, ICouponAppService couponAppService, IUserShipAppService userShipAppService, ICapPublisher capEventBus, IConnectionMultiplexer connectionMultiplexer, IMemoryCache memoryCache, IConfiguration configuration, ILogger<CartController> logger)
        {
            _baseCommonAppService = baseCommonAppService;
            _sysUserAppService = sysUserAppService;
            _cartAppService = cartAppService;
            _goodCommonAppService = goodCommonAppService;
            _promotionCommonAppService = promotionCommonAppService;
            _orderCommonAppService = orderCommonAppService;
            _couponAppService = couponAppService;
            _userShipAppService = userShipAppService;
            _capEventBus = capEventBus;
            _connectionMultiplexer = connectionMultiplexer;
            _memoryCache = memoryCache;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 获取用户的购物车信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserCartInfoAsync([FromQuery] int type = 1)
        {
            // 查询购物车
            List<CartDto> carts = await _cartAppService.GetUserCartByTypeAsync(type);

            // 查询货品
            List<Guid> productIds = carts.Select(c => c.ProductId).ToList();
            List<ProductDto> products = new List<ProductDto>();
            if (productIds.Any())
            {
                products = await _goodCommonAppService.GetProductByIdsAsync(productIds);
            }

            // 查询商品
            List<Guid> goodIds = products.Where(x => x.GoodId.HasValue).Select(x => x.GoodId.Value).ToList();
            List<GoodDto> goods = new List<GoodDto>();
            if (goodIds.Any()) goods = await _goodCommonAppService.GetGoodByIdsAsync(goodIds, false);

            // 得到用户购物车信息
            List<ShoppingCartDto> shoppingCarts = new List<ShoppingCartDto>();
            foreach (var cart in carts)
            {
                ProductDto product = products?.FirstOrDefault(x => x.Id == cart.ProductId);
                if (product == null) continue;
                GoodDto good = goods?.FirstOrDefault(x => x.Id == product.GoodId);
                if (good == null) continue;
                ShoppingCartDto shoppingCart = new ShoppingCartDto();
                shoppingCart.Id = cart.Id;
                shoppingCart.UserId = cart.UserId;
                shoppingCart.GoodId = good.Id;
                shoppingCart.GoodName = good.Name;
                shoppingCart.ProductId = product.Id;
                shoppingCart.BarCode = product.BarCode;
                shoppingCart.Sn = product.Sn;
                shoppingCart.Price = product.Price;
                shoppingCart.Stock = product.Stock - product.FreezeStock;
                shoppingCart.Images = product.Images;
                shoppingCart.SpesDesc = product.SpesDesc;
                shoppingCart.Nums = cart.Nums;
                shoppingCart.Type = cart.Type;
                shoppingCart.ObjectId = cart.ObjectId;
                shoppingCart.IsStockSufficient = shoppingCart.Stock >= shoppingCart.Nums;
                shoppingCarts.Add(shoppingCart);
            }
            return Ok(shoppingCarts);
        }

        /// <summary>
        /// 确认下单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("orderConfirm")]
        public async Task<IActionResult> OrderConfirmAsync([FromBody] OrderConfirmDto input)
        {
            // 获取当前用户信息
            CurrentSysUserEto currentUser = await _baseCommonAppService.GetCurrentUserAsync();

            IDatabase redisDatabase = _connectionMultiplexer.GetDatabase();

            #region 0.检验

            if (input.ReceiptType != (int)OrderReceiptType.Logistics && input.ReceiptType != (int)OrderReceiptType.IntraCityService)
                throw new UserFriendlyException("该收货方式不适用于该接口，请检查！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
            if (!input.UserShipId.HasValue)
                throw new UserFriendlyException("请选择收货地址！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
            if (input.CartIds == null || input.CartIds.Count() == 0)
                throw new UserFriendlyException("未选择要下单的购物商品！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
            UserShipDto userShip = await _userShipAppService.GetAsync(input.UserShipId.Value);
            if (userShip == null)
                throw new UserFriendlyException($"该收货地址不存在(或已被删除)，请检查！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());

            RedisKey cOrderKey = new RedisKey($"PlaceOrderUser:{{{currentUser.Id}}}:OrderNo:{{{input.OrderNo}}}");
            bool kExists = await redisDatabase.KeyExistsAsync(cOrderKey);
            if (!kExists)
                throw new UserFriendlyException("请求失效(请勿重复提交)，请重新选择下单！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());

            #endregion 0.检验

            #region 1.查询购物车信息

            CartOrderDto cartOrder = await GetUserCartByIdsAsync(input.CartIds.ToList(), input.ReceiptType, input.ShipAreaId, input.OrderNo);
            // 验证商品是否下架
            List<Guid> goodIds = cartOrder.ShoppingCarts.Select(x => x.GoodId).Distinct().ToList();
            List<GoodDto> goods = await _goodCommonAppService.GetGoodByIdsAsync(goodIds, false);
            foreach (var good in goods)
            {
                if (!good.IsMarketable)
                    throw new UserFriendlyException($"选择的商品：{good.Name} 已下架，请重新选择！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
            }

            List<UserCouponExchangeDto> userCouponExchanges = null;
            if (input.CouponIds != null && input.CouponIds.Count() > 0 && cartOrder.CouponExchanges != null)
                userCouponExchanges = cartOrder.CouponExchanges.Where(x => input.CouponIds.Contains(x.Id)).ToList();

            #endregion 1.查询购物车信息

            #region 2.生成订单

            decimal discountAmount = 0;
            OrderCreateEto orderCreate = new OrderCreateEto();
            orderCreate.OrderType = input.OrderType;
            orderCreate.ReceiptType = input.ReceiptType;
            orderCreate.ObjectId = input.ObjectId;
            if (input.IsUsePoint && cartOrder.PointExchangeItem != null)
            {
                orderCreate.Point = cartOrder.PointExchangeItem.CanUsePoint;
                orderCreate.PointMoney = cartOrder.PointExchangeItem.Money;
                discountAmount += cartOrder.PointExchangeItem.Money;
            }
            if (userCouponExchanges != null)
            {
                string[] cCodes = userCouponExchanges.Select(x => x.CouponCode).ToArray();
                orderCreate.Coupon = JsonSerializer.Serialize(cCodes);
                orderCreate.PromotionList = JsonSerializer.Serialize(userCouponExchanges);
                orderCreate.CouponDiscountAmount = userCouponExchanges.Sum(x => x.Money);
                discountAmount += userCouponExchanges.Sum(x => x.Money);
            }
            orderCreate.Memo = input.Memo;
            Request.Headers.TryGetValue("source-client", out StringValues userAgent);
            if (userAgent.Count > 0) orderCreate.Source = Int32.Parse(userAgent.ToString());
            orderCreate.PaymentCode = input.PaymentCode;
            orderCreate.Ip = Request.HttpContext.GetRemoteIpAddress();
            orderCreate.ShipStatus = (int)OrderShipStatus.No;
            orderCreate.Status = (int)OrderStatus.Normal;
            orderCreate.ConfirmStatus = (int)OrderConfirmStatus.ReceiptNotConfirmed;
            orderCreate.PayStatus = (int)OrderPayStatus.No;

            // 快递邮寄
            orderCreate.ShipAreaId = userShip.AreaId;
            orderCreate.DisplayArea = userShip.DisplayArea;
            orderCreate.ShipAddress = userShip.Address;
            orderCreate.ShipName = userShip.Name;
            orderCreate.ShipMobile = userShip.Mobile;

            // 获取配送方式
            orderCreate.Weight = cartOrder.Weight;
            orderCreate.CostFreight = cartOrder.CostFreight;
            ShipDto ship = await _orderCommonAppService.GetShipByAreaIdAsync(orderCreate.ShipAreaId);
            if (ship != null)
            {
                orderCreate.LogisticsId = ship.Id;
                orderCreate.LogisticsName = ship.Name;
            }

            // 计算金额
            orderCreate.GoodAmount = cartOrder.GoodAmount;
            orderCreate.GradeDiscountAmount = cartOrder.GoodGradeMoney;
            orderCreate.SeckillDiscountAmount = cartOrder.GoodSeckillMoney;
            orderCreate.GoodsDiscountAmount = cartOrder.GoodPromotionMoney;
            orderCreate.OrderDiscountAmount = cartOrder.OrderPromotionMoney;
            orderCreate.OrderAmount = Math.Round(cartOrder.OrderAmount - discountAmount, 2);
            if (orderCreate.OrderAmount <= 0)
            {
                orderCreate.PayStatus = (int)OrderPayStatus.Yes;
                orderCreate.PaymentTime = DateTime.Now;
            }

            // 生成订单明细
            orderCreate.OrderItems = new List<OrderItemCreateEto>();
            List<Guid> productIds = cartOrder.ShoppingCarts.Select(x => x.ProductId).Distinct().ToList();
            List<ProductDto> productList = await _goodCommonAppService.GetProductByIdsAsync(productIds);
            foreach (var shoppingCart in cartOrder.ShoppingCarts)
            {
                OrderItemCreateEto orderItemCreate = new OrderItemCreateEto();
                orderItemCreate.GoodId = shoppingCart.GoodId;
                orderItemCreate.ProductId = shoppingCart.ProductId;
                orderItemCreate.Sn = shoppingCart.Sn;
                orderItemCreate.Bn = shoppingCart.BarCode;
                orderItemCreate.Name = shoppingCart.GoodName;
                ProductDto product = productList?.FirstOrDefault(x => x.Id == shoppingCart.ProductId);
                if (product != null)
                {
                    orderItemCreate.CostPrice = product.CostPrice;
                    orderItemCreate.MktPrice = product.MktPrice;
                    orderItemCreate.ImageUrl = string.IsNullOrWhiteSpace(product.Images) ? string.Empty : product.Images;
                }
                orderItemCreate.Price = shoppingCart.Price;
                orderItemCreate.Nums = shoppingCart.Nums;
                orderItemCreate.Amount = shoppingCart.Amount;
                orderItemCreate.PromotionAmount = 0;
                orderItemCreate.PromotionList = string.Empty;
                orderItemCreate.Weight = shoppingCart.Weight;
                orderItemCreate.SendNums = 0;
                orderItemCreate.Addon = shoppingCart.SpesDesc;
                orderCreate.OrderItems.Add(orderItemCreate);
            }

            #endregion 2.生成订单

            #region 3.保存订单

            orderCreate.No = input.OrderNo;
            orderCreate.UserId = currentUser.Id;
            orderCreate.IsUsePoint = input.IsUsePoint;
            orderCreate.CartIds = input.CartIds;

            if (orderCreate.OrderType == (int)OrderType.Skill)
            {
                return await OrderSeckillAsync(cartOrder.OrderNo, orderCreate);
            }

            return await PlaceOrderAsync(cartOrder.OrderNo, orderCreate);

            #endregion 3.保存订单
        }

        /// <summary>
        /// 秒杀订单
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="orderCreate">用户订单</param>
        [HttpPost("orderSkill/{orderNo}")]
        [NonAction]
        public async Task<IActionResult> OrderSeckillAsync([FromRoute] string orderNo, [FromBody] OrderCreateEto orderCreate)
        {
            if (!orderCreate.ObjectId.HasValue)
                throw new UserFriendlyException("该商品未关联到秒杀活动，请检查！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());

            PromotionDto promotion = await _promotionCommonAppService.GetPromotionByIdAsync(orderCreate.ObjectId.Value);
            if (promotion == null || promotion.Type != (int)PromotionType.Seckill)
                throw new UserFriendlyException("该商品未关联到秒杀活动，请检查！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());

            if (!promotion.IsEnable)
                throw new UserFriendlyException("该秒杀活动已关闭，感谢您的参与！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());

            // 1.预扣商品库存
            OrderItemCreateEto orderItem = orderCreate.OrderItems.First(); // 秒杀订单只会存在一个商品
            string seckillLimitKey = $"Rex.GoodService:{{{orderItem.GoodId}}}:SeckillLimit:{{{orderNo}}}";
            string orderGoodKey = $"Rex.GoodService:{{{orderItem.GoodId}}}:Order:{{{orderNo}}}";
            string userBuyLimitKey = $"Rex.GoodService:{{{orderItem.GoodId}}}:UserBuyLimit:{{{orderCreate.UserId}}}";
            string totalBuyLimitKey = $"Rex.GoodService:{{{orderItem.GoodId}}}:TotalBuyLimit";
            string productStockKey = $"Rex.GoodService:{{{orderItem.GoodId}}}:Product:{{{orderItem.ProductId}}}"; // 商品库存 Key 前缀

            IDatabase redisDatabase = _connectionMultiplexer.GetDatabase();
            int orderRateLimitCount = 1000; // 默认1000次
            int orderRateLimitExpire = 1; // 默认1秒
            if (int.TryParse(_configuration["OrderRateLimit:Count"], out int oRateLimitCount))
                orderRateLimitCount = oRateLimitCount;
            if (int.TryParse(_configuration["OrderRateLimit:Expire"], out int oRateLimitExpire))
                orderRateLimitExpire = oRateLimitExpire;

            var pStockKeys = new RedisKey[] { seckillLimitKey, orderGoodKey, userBuyLimitKey, totalBuyLimitKey, productStockKey };
            var pOrderValues = new RedisValue[] {
                orderRateLimitCount, // ARGV[1]
                orderRateLimitExpire, // ARGV[2]
                promotion.MaxNums, // ARGV[3]
                promotion.MaxGoodNums, // ARGV[4]
                orderItem.Nums // ARGV[5]
            };
            string sOrderScript = _memoryCache.Get<string>(FrontAggregationHostedService.SeckillOrderLuaKey);
            RedisResult seckillResult = await redisDatabase.ScriptEvaluateAsync(
                sOrderScript,
                pStockKeys,
                pOrderValues);
            string rResult = seckillResult?.ToString();
            if (rResult.IsNullOrWhiteSpace() || !rResult.Equals("1"))
                throw new UserFriendlyException($"{rResult ?? "系统繁忙，请稍后再试"}！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());

            // 2.创建订单
            await _capEventBus.PublishAsync(OrderCreateEto.EventName, orderCreate);

            // 3.删除下单缓存
            RedisKey cOrderKey = new RedisKey($"PlaceOrderUser:{{{orderCreate.UserId}}}:OrderNo:{{{orderNo}}}");
            await redisDatabase.KeyDeleteAsync(cOrderKey);

            return Ok(orderCreate.No);
        }

        /// <summary>
        /// 正常下单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="orderCreate"></param>
        /// <returns></returns>
        [HttpPost("placeOrder/{orderNo}")]
        [NonAction]
        public async Task<IActionResult> PlaceOrderAsync([FromRoute] string orderNo, [FromBody] OrderCreateEto orderCreate)
        {
            IDatabase redisDatabase = _connectionMultiplexer.GetDatabase();
            Dictionary<Guid, List<OrderItemCreateEto>> rollbackOrderItemDic = new Dictionary<Guid, List<OrderItemCreateEto>>();
            try
            {
                // 1.按商品分组预扣库存（每个商品独立Lua脚本）
                var goodOrderItems = orderCreate.OrderItems.GroupBy(x => x.GoodId).ToList();
                foreach (var goodOrderItem in goodOrderItems)
                {
                    string orderGoodKey = $"Rex.GoodService:{{{goodOrderItem.Key}}}:Order:{{{orderNo}}}";
                    string productStockKeyPrefix = $"Rex.GoodService:{{{goodOrderItem.Key}}}"; // 商品库存 Key 前缀

                    List<OrderItemCreateEto> orderItems = goodOrderItem.ToList();
                    List<RedisValue> redisValues = new List<RedisValue>();
                    foreach (var orderItem in orderItems)
                    {
                        string pName = orderItem.Name.Replace(":", "");
                        string rValue = $"{orderItem.ProductId}:{orderItem.Nums}:{pName}";
                        redisValues.Add(rValue);
                    }

                    string placeOrderScript = _memoryCache.Get<string>(FrontAggregationHostedService.PlaceOrderKey);
                    RedisResult seckillResult = await redisDatabase.ScriptEvaluateAsync(
                        placeOrderScript,
                        new RedisKey[] { orderGoodKey, productStockKeyPrefix },
                        redisValues.ToArray());
                    string rResult = seckillResult?.ToString();
                    if (rResult.IsNullOrWhiteSpace() || !rResult.Equals("1"))
                        throw new UserFriendlyException($"{rResult ?? "系统繁忙，请稍后再试"}！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());

                    rollbackOrderItemDic.Add(goodOrderItem.Key, orderItems);
                }
            }
            catch (Exception ex)
            {
                // 失败：调用回滚脚本
                if (rollbackOrderItemDic.Count > 0)
                    RollbackPlaceOrder(orderNo, rollbackOrderItemDic);

                throw new UserFriendlyException(ex.Message, SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
            }

            // 2.创建用户订单
            await _capEventBus.PublishAsync(OrderCreateEto.EventName, orderCreate);

            // 3.删除下单缓存
            RedisKey cOrderKey = new RedisKey($"PlaceOrderUser:{{{orderCreate.UserId}}}:OrderNo:{{{orderNo}}}");
            await redisDatabase.KeyDeleteAsync(cOrderKey);

            return Ok(orderCreate.No);
        }

        /// <summary>
        /// [回滚]正常下单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="rollbackOrderItemDic"></param>
        private void RollbackPlaceOrder(string orderNo, Dictionary<Guid, List<OrderItemCreateEto>> rollbackOrderItemDic)
        {
            IDatabase redisDatabase = _connectionMultiplexer.GetDatabase();
            foreach (var rBackorderItem in rollbackOrderItemDic)
            {
                string orderGoodKey = $"Rex.GoodService:{{{rBackorderItem.Key}}}:Order:{{{orderNo}}}";
                string productStockKeyPrefix = $"Rex.GoodService:{{{rBackorderItem.Key}}}"; // 商品库存 Key 前缀

                List<OrderItemCreateEto> orderItems = rBackorderItem.Value;
                List<RedisValue> redisValues = new List<RedisValue>();
                foreach (var orderItem in orderItems)
                {
                    string pName = orderItem.Name.Replace(":", "");
                    string rValue = $"{orderItem.ProductId}:{orderItem.Nums}:{pName}";
                    redisValues.Add(rValue);
                }

                string rollbackPlaceOrderScript = _memoryCache.Get<string>(FrontAggregationHostedService.RollbackPlaceOrderKey);
                RedisResult seckillResult = redisDatabase.ScriptEvaluate(
                    rollbackPlaceOrderScript,
                    new RedisKey[] { orderGoodKey, productStockKeyPrefix },
                    redisValues.ToArray());
                string rResult = seckillResult?.ToString();
                if (rResult.IsNullOrWhiteSpace() || !rResult.Equals("1"))
                    _logger.LogError($"{rResult ?? "系统繁忙，请稍后再试"}！");
            }
        }

        /// <summary>
        /// 根据购物(车)ID获取购物车信息
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="receiptType">[收货方式]1：物流快递、2：同城配送、3：门店自提</param>
        /// <param name="shipAreaId">区域ID</param>
        /// <param name="orderNo">订单号</param>
        /// <returns></returns>
        [HttpPost("ids")]
        public async Task<CartOrderDto> GetUserCartByIdsAsync([FromBody] List<Guid> ids, [FromQuery] int receiptType = 1, [FromQuery] int shipAreaId = 0, [FromQuery] string orderNo = "")
        {
            CartOrderDto cartOrder = new CartOrderDto();

            // 获取当前用户信息
            CurrentSysUserEto currentUser = await _baseCommonAppService.GetCurrentUserAsync();

            #region 购物车信息

            // 查询购物车
            List<CartDto> carts = await _cartAppService.GetUserCartByIdsAsync(ids);

            // 查询货品
            List<Guid> productIds = carts.Select(c => c.ProductId).ToList();
            List<ProductDto> products = new List<ProductDto>();
            if (productIds.Any())
            {
                products = await _goodCommonAppService.GetProductByIdsAsync(productIds);
            }

            // 查询商品
            List<Guid> goodIds = products.Where(x => x.GoodId.HasValue).Select(x => x.GoodId.Value).ToList();
            List<GoodDto> goods = new List<GoodDto>();
            if (goodIds.Any()) goods = await _goodCommonAppService.GetGoodByIdsAsync(goodIds, false);

            // 得到用户购物车信息
            cartOrder.ShoppingCarts = new List<ShoppingCartDto>();
            foreach (var cart in carts)
            {
                ProductDto product = products?.FirstOrDefault(x => x.Id == cart.ProductId);
                if (product == null) continue;
                GoodDto good = goods?.FirstOrDefault(x => x.Id == product.GoodId);
                if (good == null) continue;
                ShoppingCartDto shoppingCart = new ShoppingCartDto();
                shoppingCart.Id = cart.Id;
                shoppingCart.UserId = cart.UserId;
                shoppingCart.GoodId = good.Id;
                shoppingCart.GoodName = good.Name;
                shoppingCart.ProductId = product.Id;
                shoppingCart.BarCode = product.BarCode;
                shoppingCart.Sn = product.Sn;
                shoppingCart.Price = product.Price;
                shoppingCart.Nums = cart.Nums;
                shoppingCart.Stock = product.Stock - product.FreezeStock;
                shoppingCart.Images = string.IsNullOrWhiteSpace(product.Images) ? good.Image : product.Images;
                shoppingCart.SpesDesc = product.SpesDesc;
                shoppingCart.Type = cart.Type;
                shoppingCart.ObjectId = cart.ObjectId;
                shoppingCart.Amount = Math.Round(shoppingCart.Nums * shoppingCart.Price, 2);
                shoppingCart.Weight = Math.Round(shoppingCart.Nums * shoppingCart.Weight, 2);
                shoppingCart.IsStockSufficient = shoppingCart.Stock >= shoppingCart.Nums;
                cartOrder.ShoppingCarts.Add(shoppingCart);
            }

            #endregion 购物车信息

            #region 计算金额

            decimal goodAmount = cartOrder.ShoppingCarts.Sum(item => item.Amount);
            decimal goodWeight = cartOrder.ShoppingCarts.Sum(item => item.Weight);
            cartOrder.GoodAmount = Math.Round(goodAmount, 2);
            cartOrder.OrderAmount = Math.Round(goodAmount, 2);
            cartOrder.Weight = Math.Round(goodWeight, 2);
            cartOrder.ProductNums = cartOrder.ShoppingCarts.Sum(item => item.Nums);

            // 会员等级(优惠)
            SetUserGradeAmount(ref cartOrder, currentUser.Grade);

            // 全局促销(优惠)
            SetPromotionGlobalAmount(ref cartOrder);

            // 商品秒杀(优惠)
            List<Guid> objectIds = cartOrder.ShoppingCarts.Where(x => x.ObjectId.HasValue).Select(x => x.ObjectId.Value).Distinct().ToList();
            if (objectIds != null && objectIds.Any())
            {
                List<PromotionDto> promotions = await _promotionCommonAppService.GetPromotionByIdsAsync(objectIds.ToArray());
                foreach (var promotion in promotions)
                {
                    if (promotion.Type == (int)PromotionType.Seckill) SetGoodSeckillAmount(ref cartOrder, promotion);
                    // TODO：其他促销
                }
            }

            // 门店/同城配送订单，强制无运费
            if (receiptType == (int)OrderReceiptType.IntraCityService || receiptType == (int)OrderReceiptType.SelfDelivery)
            {
                cartOrder.CostFreight = 0;
            }
            else
            {
                ShipDto shipDto = await _orderCommonAppService.GetShipByAreaIdAsync(shipAreaId);
                cartOrder.CostFreight = FaHandle.GetShipCost(shipDto, cartOrder.Weight, cartOrder.OrderAmount, shipAreaId);
                cartOrder.OrderAmount = Math.Round(cartOrder.OrderAmount + cartOrder.CostFreight, 2);
            }

            #endregion 计算金额

            // 设置用户(可用)优惠卷
            SetUserCoupon(ref cartOrder);

            // 设置用户(可用)积分
            cartOrder.PointExchangeItem = await _sysUserAppService.GetUserPointExchangeMoneyAsync(cartOrder.OrderAmount);
            if (cartOrder.OrderAmount <= 0) cartOrder.OrderAmount = 0;

            // [提前]生成订单号
            cartOrder.OrderNo = orderNo.IsNullOrWhiteSpace() ? CommonHelper.GetSerialNumberType((int)SerialNumberType.OrderCode) : orderNo;

            IDatabase redisDatabase = _connectionMultiplexer.GetDatabase();
            string cartOrderJson = JsonSerializer.Serialize(cartOrder, AspNetCoreExtension.GetJsonSerializerOptions());
            RedisKey cOrderKey = new RedisKey($"PlaceOrderUser:{{{currentUser.Id}}}:OrderNo:{{{cartOrder.OrderNo}}}");
            bool kExists = await redisDatabase.KeyExistsAsync(cOrderKey);
            if (!kExists)
            {
                bool setResult = await redisDatabase.SetAddAsync(
                    cOrderKey,
                    new RedisValue(cartOrderJson)
                );
                if (setResult)
                {
                    DateTime nowDate = DateTime.Now.AddMinutes(30); // 有效期为 30 分钟
                    await redisDatabase.KeyExpireAsync(cOrderKey, nowDate);
                }
            }
            return cartOrder;
        }

        /// <summary>
        /// 获取用户可用优惠卷
        /// </summary>
        /// <param name="cartOrder"></param>
        private void SetUserCoupon(ref CartOrderDto cartOrder)
        {
            List<CouponDto> coupons = _couponAppService.GetUserCouponAsync(false).Result;
            if (coupons == null || !coupons.Any()) return;
            cartOrder.CouponExchanges = new List<UserCouponExchangeDto>();
            for (int i = 0; i < cartOrder.ShoppingCarts.Count; i++)
            {
                ShoppingCartDto shoppingCart = cartOrder.ShoppingCarts[i];
                foreach (var coupon in coupons)
                {
                    if (coupon.Promotion == null) continue;
                    PromotionDto promotion = coupon.Promotion;
                    if (promotion.PromotionConditions == null || !promotion.PromotionConditions.Any()) continue;
                    if (promotion.PromotionResults == null || !promotion.PromotionResults.Any()) continue;

                    // 验证是否满足(所有)促销条件
                    bool isPromotionCondition = ValidatePromotionCondition(cartOrder, promotion.PromotionConditions, shoppingCart);
                    if (!isPromotionCondition) continue;

                    // 得到优惠卷结果
                    UserCouponExchangeDto uCouponExchange = new UserCouponExchangeDto();
                    uCouponExchange.Id = coupon.Id;
                    uCouponExchange.CouponCode = coupon.CouponCode;
                    PromotionResultDto pResult = promotion.PromotionResults.FirstOrDefault();
                    //foreach (var pResult in promotion.PromotionResults)
                    JsonObject resultObj = JsonSerializer.Deserialize<JsonObject>(pResult.Parameters);
                    uCouponExchange.CouponName = promotion.Name;
                    uCouponExchange.Code = pResult.Code;
                    uCouponExchange.Parameters = pResult.Parameters;
                    uCouponExchange.StartTime = coupon.StartTime;
                    uCouponExchange.EndTime = coupon.EndTime;
                    if (pResult.Code.Equals("GOODS_REDUCE"))
                    {
                        #region 指定商品减固定金额

                        // {"money":"5"}
                        resultObj.TryGetPropertyValue("money", out JsonNode? moneyNode);
                        if (moneyNode == null || moneyNode.ToString().IsNullOrWhiteSpace()) continue;
                        string moneyStr = JsonSerializer.Deserialize<string>(moneyNode);
                        decimal.TryParse(moneyStr, out decimal money);
                        uCouponExchange.Money += money;

                        #endregion 指定商品减固定金额
                    }
                    else if (pResult.Code.Equals("GOODS_DISCOUNT"))
                    {
                        #region 指定商品打X折

                        // {"discount":"8"}
                        resultObj.TryGetPropertyValue("discount", out JsonNode? discountNode);
                        if (discountNode == null || discountNode.ToString().IsNullOrWhiteSpace()) continue;
                        string discountStr = JsonSerializer.Deserialize<string>(discountNode).Replace(".", "");

                        // 计算折扣金额
                        decimal discount = Convert.ToDecimal($"0.{discountStr}");
                        decimal discountMoney = Math.Round(shoppingCart.Amount * discount, 2);
                        uCouponExchange.Money += Math.Round(shoppingCart.Amount - discountMoney, 2);

                        #endregion 指定商品打X折
                    }
                    else if (pResult.Code.Equals("GOODS_ONE_PRICE"))
                    {
                        #region 指定商品一口价

                        // {"money":"5"}
                        resultObj.TryGetPropertyValue("money", out JsonNode? moneyNode);
                        if (moneyNode == null || moneyNode.ToString().IsNullOrWhiteSpace()) continue;
                        string moneyStr = JsonSerializer.Deserialize<string>(moneyNode);
                        decimal.TryParse(moneyStr, out decimal money);
                        if (money < cartOrder.OrderAmount)
                        {
                            decimal oneMoney = Math.Round(shoppingCart.Amount - money, 2);
                            uCouponExchange.Money += oneMoney;
                        }

                        #endregion 指定商品一口价
                    }
                    else if (pResult.Code.Equals("ORDER_REDUCE"))
                    {
                        #region 订单减指定金额

                        // {"money":"5"}
                        resultObj.TryGetPropertyValue("money", out JsonNode? moneyNode);
                        if (moneyNode == null || moneyNode.ToString().IsNullOrWhiteSpace()) continue;
                        string moneyStr = JsonSerializer.Deserialize<string>(moneyNode);
                        decimal.TryParse(moneyStr, out decimal money);
                        uCouponExchange.Money += money;

                        #endregion 订单减指定金额
                    }
                    else if (pResult.Code.Equals("ORDER_DISCOUNT"))
                    {
                        #region 订单打X折

                        // {"discount":"8","money":""}
                        resultObj.TryGetPropertyValue("discount", out JsonNode? discountNode);
                        if (discountNode == null || discountNode.ToString().IsNullOrWhiteSpace()) continue;
                        string discountStr = JsonSerializer.Deserialize<string>(discountNode).Replace(".", "");

                        // 计算折扣金额
                        decimal discount = Convert.ToDecimal($"0.{discountStr}");
                        decimal discountMoney = Math.Round(shoppingCart.Amount * discount, 2);
                        uCouponExchange.Money += Math.Round(shoppingCart.Amount - discountMoney, 2);

                        #endregion 订单打X折
                    }
                    else if (pResult.Code.Equals("GOODS_HALF_PRICE"))
                    {
                        #region 指定商品每第几件减指定金额

                        // {"num":2,"money":"2"}
                        resultObj.TryGetPropertyValue("num", out JsonNode? numNode);
                        if (numNode == null || numNode.ToString().IsNullOrWhiteSpace()) continue;
                        string numStr = JsonSerializer.Deserialize<string>(numNode);
                        int.TryParse(numStr, out int num);

                        resultObj.TryGetPropertyValue("money", out JsonNode? moneyNode);
                        if (moneyNode == null || moneyNode.ToString().IsNullOrWhiteSpace()) continue;
                        string moneyStr = JsonSerializer.Deserialize<string>(moneyNode);
                        decimal.TryParse(moneyStr, out decimal money);

                        // 计算金额
                        if (shoppingCart.Nums >= num) uCouponExchange.Money += money;

                        #endregion 指定商品每第几件减指定金额
                    }
                    cartOrder.CouponExchanges.Add(uCouponExchange);
                }
            }
        }

        /// <summary>
        /// 设置商品秒杀
        /// </summary>
        /// <param name="cartOrder">购物车</param>
        /// <param name="promotion">促销信息</param>
        private void SetGoodSeckillAmount(ref CartOrderDto cartOrder, PromotionDto promotion)
        {
            if (promotion.PromotionConditions == null || !promotion.PromotionConditions.Any()) return;
            if (promotion.PromotionResults == null || !promotion.PromotionResults.Any()) return;

            PromotionConditionDto pCondition = promotion.PromotionConditions.FirstOrDefault();
            JsonObject parametersObj = JsonSerializer.Deserialize<JsonObject>(pCondition.Parameters);
            if (parametersObj == null) return;

            bool isPromotionCondition = false;
            if (parametersObj.TryGetPropertyValue("goodsId", out JsonNode? goodsIdNode))
            {
                if (Guid.TryParse(goodsIdNode?.ToString(), out Guid goodId))
                    isPromotionCondition = cartOrder.ShoppingCarts.Any(x => x.GoodId == goodId);
            }
            if (!isPromotionCondition) return;

            decimal goodSeckillMoney = 0; // 商品秒杀优惠
            for (int i = 0; i < cartOrder.ShoppingCarts.Count; i++)
            {
                ShoppingCartDto shoppingCart = cartOrder.ShoppingCarts[i];

                #region 计算促销结果

                foreach (var pResult in promotion.PromotionResults)
                {
                    JsonObject resultObj = JsonSerializer.Deserialize<JsonObject>(pResult.Parameters);
                    if (pResult.Code.Equals("GOODS_REDUCE"))
                    {
                        #region 指定商品减固定金额

                        // {"money":"5"}
                        resultObj.TryGetPropertyValue("money", out JsonNode? moneyNode);
                        if (moneyNode == null || moneyNode.ToString().IsNullOrWhiteSpace()) continue;
                        string moneyStr = JsonSerializer.Deserialize<string>(moneyNode);
                        decimal.TryParse(moneyStr, out decimal money);
                        goodSeckillMoney += money;

                        #endregion 指定商品减固定金额
                    }
                    else if (pResult.Code.Equals("GOODS_DISCOUNT"))
                    {
                        #region 指定商品打X折

                        // {"discount":"8"}
                        resultObj.TryGetPropertyValue("discount", out JsonNode? discountNode);
                        if (discountNode == null || discountNode.ToString().IsNullOrWhiteSpace()) continue;
                        string discountStr = JsonSerializer.Deserialize<string>(discountNode).Replace(".", "");

                        // 计算折扣金额
                        decimal discount = Convert.ToDecimal($"0.{discountStr}");
                        decimal discountMoney = Math.Round(shoppingCart.Amount * discount, 2);
                        goodSeckillMoney += Math.Round(shoppingCart.Amount - discountMoney, 2);

                        #endregion 指定商品打X折
                    }
                    else if (pResult.Code.Equals("GOODS_ONE_PRICE"))
                    {
                        #region 指定商品一口价

                        // {"money":"5"}
                        resultObj.TryGetPropertyValue("money", out JsonNode? moneyNode);
                        if (moneyNode == null || moneyNode.ToString().IsNullOrWhiteSpace()) continue;
                        string moneyStr = JsonSerializer.Deserialize<string>(moneyNode);
                        decimal.TryParse(moneyStr, out decimal money);
                        if (money < cartOrder.OrderAmount)
                        {
                            decimal oneMoney = Math.Round(shoppingCart.Amount - money, 2);
                            goodSeckillMoney += oneMoney;
                        }

                        #endregion 指定商品一口价
                    }
                    else if (pResult.Code.Equals("ORDER_REDUCE"))
                    {
                        #region 订单减指定金额

                        // {"money":"5"}
                        resultObj.TryGetPropertyValue("money", out JsonNode? moneyNode);
                        if (moneyNode == null || moneyNode.ToString().IsNullOrWhiteSpace()) continue;
                        string moneyStr = JsonSerializer.Deserialize<string>(moneyNode);
                        decimal.TryParse(moneyStr, out decimal money);
                        goodSeckillMoney += money;

                        #endregion 订单减指定金额
                    }
                    else if (pResult.Code.Equals("ORDER_DISCOUNT"))
                    {
                        #region 订单打X折

                        // {"discount":"8","money":""}
                        resultObj.TryGetPropertyValue("discount", out JsonNode? discountNode);
                        if (discountNode == null || discountNode.ToString().IsNullOrWhiteSpace()) continue;
                        string discountStr = JsonSerializer.Deserialize<string>(discountNode).Replace(".", "");

                        // 计算折扣金额
                        decimal discount = Convert.ToDecimal($"0.{discountStr}");
                        decimal discountMoney = Math.Round(shoppingCart.Amount * discount, 2);
                        goodSeckillMoney += Math.Round(shoppingCart.Amount - discountMoney, 2);

                        #endregion 订单打X折
                    }
                    else if (pResult.Code.Equals("GOODS_HALF_PRICE"))
                    {
                        #region 指定商品每第几件减指定金额

                        // {"num":2,"money":"2"}
                        resultObj.TryGetPropertyValue("num", out JsonNode? numNode);
                        if (numNode == null || numNode.ToString().IsNullOrWhiteSpace()) continue;
                        string numStr = JsonSerializer.Deserialize<string>(numNode);
                        int.TryParse(numStr, out int num);

                        resultObj.TryGetPropertyValue("money", out JsonNode? moneyNode);
                        if (moneyNode == null || moneyNode.ToString().IsNullOrWhiteSpace()) continue;
                        string moneyStr = JsonSerializer.Deserialize<string>(moneyNode);
                        decimal.TryParse(moneyStr, out decimal money);

                        // 计算金额
                        if (shoppingCart.Nums >= num) goodSeckillMoney += money;

                        #endregion 指定商品每第几件减指定金额
                    }
                }

                #endregion 计算促销结果
            }
            if (goodSeckillMoney > 0)
            {
                cartOrder.GoodSeckillMoney += Math.Round(goodSeckillMoney, 2);
                cartOrder.OrderAmount = Math.Round(cartOrder.OrderAmount - goodSeckillMoney, 2);
            }
        }

        /// <summary>
        /// 设置用户会员等级
        /// </summary>
        /// <param name="cartOrder"></param>
        /// <param name="uGrade">用户等级</param>
        private void SetUserGradeAmount(ref CartOrderDto cartOrder, GradeEto? uGrade = null)
        {
            if (uGrade == null) return;
            decimal goodGradeMoney = 0; // 会员优惠
            for (int i = 0; i < cartOrder.ShoppingCarts.Count; i++)
            {
                ShoppingCartDto shoppingCart = cartOrder.ShoppingCarts[i];
                List<GoodGradeDto> goodGrades = _goodCommonAppService.GetGoodGradeByGoodIdAsync(shoppingCart.GoodId).Result;
                if (goodGrades == null || !goodGrades.Any()) continue;
                GoodGradeDto goodGrade = goodGrades.FirstOrDefault(g => g.GradeId == uGrade.Id);
                if (goodGrade == null) continue;
                decimal vipPrice = shoppingCart.Amount - goodGrade.GradePrice;
                decimal calcMoney = shoppingCart.Nums * (shoppingCart.Amount - vipPrice);
                goodGradeMoney += calcMoney;
            }
            if (goodGradeMoney > 0)
            {
                cartOrder.GoodGradeMoney += Math.Round(goodGradeMoney, 2);
                cartOrder.OrderAmount = Math.Round(cartOrder.OrderAmount - goodGradeMoney, 2);
            }
        }

        /// <summary>
        /// 设置全局促销
        /// </summary>
        /// <param name="cartOrder">购物车</param>
        private void SetPromotionGlobalAmount(ref CartOrderDto cartOrder)
        {
            List<PromotionDto> promotions = _promotionCommonAppService.GetPromotionGlobalIsEnableAsync(true, true).Result;
            if (promotions == null || !promotions.Any()) return;

            //List<CommonKeyValue> promotionConditionTypes = await _promotionAppService.GetPromotionConditionTypeAsync();
            //List<CommonKeyValue> promotionResultTypes = await _promotionAppService.GetPromotionResultTypeAsync();
            decimal goodPromotionMoney = 0; // 商品优惠
            decimal orderPromotionMoney = 0; // 订单优惠
            for (int i = 0; i < cartOrder.ShoppingCarts.Count; i++)
            {
                ShoppingCartDto shoppingCart = cartOrder.ShoppingCarts[i];
                foreach (var promotion in promotions)
                {
                    if (promotion.PromotionConditions == null || !promotion.PromotionConditions.Any()) continue;
                    if (promotion.PromotionResults == null || !promotion.PromotionResults.Any()) continue;

                    // 验证是否满足(所有)促销条件
                    bool isPromotionCondition = ValidatePromotionCondition(cartOrder, promotion.PromotionConditions, shoppingCart);
                    if (!isPromotionCondition) continue;

                    #region 计算促销结果

                    foreach (var pResult in promotion.PromotionResults)
                    {
                        JsonObject resultObj = JsonSerializer.Deserialize<JsonObject>(pResult.Parameters);
                        if (pResult.Code.Equals("GOODS_REDUCE"))
                        {
                            #region 指定商品减固定金额

                            // {"money":"5"}
                            resultObj.TryGetPropertyValue("money", out JsonNode? moneyNode);
                            if (moneyNode == null || moneyNode.ToString().IsNullOrWhiteSpace()) continue;
                            string moneyStr = JsonSerializer.Deserialize<string>(moneyNode);
                            decimal.TryParse(moneyStr, out decimal money);
                            goodPromotionMoney += money;

                            #endregion 指定商品减固定金额
                        }
                        else if (pResult.Code.Equals("GOODS_DISCOUNT"))
                        {
                            #region 指定商品打X折

                            // {"discount":"8"}
                            resultObj.TryGetPropertyValue("discount", out JsonNode? discountNode);
                            if (discountNode == null || discountNode.ToString().IsNullOrWhiteSpace()) continue;
                            string discountStr = JsonSerializer.Deserialize<string>(discountNode).Replace(".", "");

                            // 计算折扣金额
                            decimal discount = Convert.ToDecimal($"0.{discountStr}");
                            decimal discountMoney = Math.Round(shoppingCart.Amount * discount, 2);
                            goodPromotionMoney += Math.Round(shoppingCart.Amount - discountMoney, 2);

                            #endregion 指定商品打X折
                        }
                        else if (pResult.Code.Equals("GOODS_ONE_PRICE"))
                        {
                            #region 指定商品一口价

                            // {"money":"5"}
                            resultObj.TryGetPropertyValue("money", out JsonNode? moneyNode);
                            if (moneyNode == null || moneyNode.ToString().IsNullOrWhiteSpace()) continue;
                            string moneyStr = JsonSerializer.Deserialize<string>(moneyNode);
                            decimal.TryParse(moneyStr, out decimal money);
                            if (money < cartOrder.OrderAmount)
                            {
                                decimal oneMoney = Math.Round(shoppingCart.Amount - money, 2);
                                goodPromotionMoney += oneMoney;
                            }

                            #endregion 指定商品一口价
                        }
                        else if (pResult.Code.Equals("ORDER_REDUCE"))
                        {
                            #region 订单减指定金额

                            // {"money":"5"}
                            resultObj.TryGetPropertyValue("money", out JsonNode? moneyNode);
                            if (moneyNode == null || moneyNode.ToString().IsNullOrWhiteSpace()) continue;
                            string moneyStr = JsonSerializer.Deserialize<string>(moneyNode);
                            decimal.TryParse(moneyStr, out decimal money);
                            orderPromotionMoney += money;

                            #endregion 订单减指定金额
                        }
                        else if (pResult.Code.Equals("ORDER_DISCOUNT"))
                        {
                            #region 订单打X折

                            // {"discount":"8","money":""}
                            resultObj.TryGetPropertyValue("discount", out JsonNode? discountNode);
                            if (discountNode == null || discountNode.ToString().IsNullOrWhiteSpace()) continue;
                            string discountStr = JsonSerializer.Deserialize<string>(discountNode).Replace(".", "");

                            // 计算折扣金额
                            decimal discount = Convert.ToDecimal($"0.{discountStr}");
                            decimal discountMoney = Math.Round(shoppingCart.Amount * discount, 2);
                            orderPromotionMoney += Math.Round(shoppingCart.Amount - discountMoney, 2);

                            #endregion 订单打X折
                        }
                        else if (pResult.Code.Equals("GOODS_HALF_PRICE"))
                        {
                            #region 指定商品每第几件减指定金额

                            // {"num":2,"money":"2"}
                            resultObj.TryGetPropertyValue("num", out JsonNode? numNode);
                            if (numNode == null || numNode.ToString().IsNullOrWhiteSpace()) continue;
                            string numStr = JsonSerializer.Deserialize<string>(numNode);
                            int.TryParse(numStr, out int num);

                            resultObj.TryGetPropertyValue("money", out JsonNode? moneyNode);
                            if (moneyNode == null || moneyNode.ToString().IsNullOrWhiteSpace()) continue;
                            string moneyStr = JsonSerializer.Deserialize<string>(moneyNode);
                            decimal.TryParse(moneyStr, out decimal money);

                            // 计算金额
                            if (shoppingCart.Nums >= num) orderPromotionMoney += money;

                            #endregion 指定商品每第几件减指定金额
                        }
                    }

                    #endregion 计算促销结果

                    if (promotion.IsExclusive) break;
                }
            }
            if (goodPromotionMoney > 0)
            {
                cartOrder.GoodPromotionMoney += Math.Round(goodPromotionMoney, 2);
                cartOrder.OrderAmount = Math.Round(cartOrder.OrderAmount - goodPromotionMoney, 2);
            }
            if (orderPromotionMoney > 0)
            {
                cartOrder.OrderPromotionMoney += Math.Round(orderPromotionMoney, 2);
                cartOrder.OrderAmount = Math.Round(cartOrder.OrderAmount - orderPromotionMoney, 2);
            }
        }

        /// <summary>
        /// 验证是否满足(所有)促销条件
        /// </summary>
        /// <param name="cartOrder">购物车订单</param>
        /// <param name="promotionConditions">促销条件</param>
        /// <param name="shoppingCart">购物车</param>
        /// <returns></returns>
        private bool ValidatePromotionCondition(CartOrderDto cartOrder, List<PromotionConditionDto> promotionConditions, ShoppingCartDto shoppingCart)
        {
            bool isPromotionCondition = true;
            foreach (var pCondition in promotionConditions)
            {
                if (pCondition.Code.Equals("GOODS_ALL")) continue;
                if (pCondition.Parameters.IsNullOrWhiteSpace())
                {
                    isPromotionCondition = false;
                    break;
                }
                JsonObject paramObj = JsonSerializer.Deserialize<JsonObject>(pCondition.Parameters);
                GoodDto good = _goodCommonAppService.GetGoodByIdAsync(shoppingCart.GoodId, false).Result;
                if (pCondition.Code.Equals("GOODS_IDS"))
                {
                    #region 指定某些商品满足条件

                    // {"goods":"3a106291-2516-c1b9-ad23-43c77684e518,3a0e06e8-d298-7ccd-4901-d9fada7ad4b2","nums":1}
                    paramObj.TryGetPropertyValue("goods", out JsonNode? goodIdsNode);
                    if (goodIdsNode == null || goodIdsNode.ToString().IsNullOrWhiteSpace())
                    {
                        isPromotionCondition = false;
                        break;
                    }
                    string goodIdsStr = JsonSerializer.Deserialize<string>(goodIdsNode);
                    Guid[] goodIds = goodIdsStr.Split(',').Select(x => Guid.Parse(x.Trim())).ToArray();
                    if (!goodIds.Contains(shoppingCart.GoodId))
                    {
                        isPromotionCondition = false;
                        break;
                    }
                    paramObj.TryGetPropertyValue("nums", out JsonNode? numsNode);
                    if (numsNode == null || numsNode.ToString().IsNullOrWhiteSpace())
                    {
                        isPromotionCondition = false;
                        break;
                    }
                    int nums = JsonSerializer.Deserialize<int>(numsNode);
                    if (shoppingCart.Nums < nums)
                    {
                        isPromotionCondition = false;
                        break;
                    }

                    #endregion 指定某些商品满足条件
                }
                else if (pCondition.Code.Equals("GOODS_CATS"))
                {
                    #region 指定商品分类满足条件

                    // {"catId":"3a10a3c2-4cf1-9234-9bb7-c5297faef7c8","nums":1}
                    paramObj.TryGetPropertyValue("catId", out JsonNode? catIdNode);
                    if (catIdNode == null || catIdNode.ToString().IsNullOrWhiteSpace())
                    {
                        isPromotionCondition = false;
                        break;
                    }
                    Guid catId = JsonSerializer.Deserialize<Guid>(catIdNode);
                    if (catId != good.GoodCategoryId)
                    {
                        isPromotionCondition = false;
                        break;
                    }
                    paramObj.TryGetPropertyValue("nums", out JsonNode? numsNode);
                    if (numsNode == null || numsNode.ToString().IsNullOrWhiteSpace())
                    {
                        isPromotionCondition = false;
                        break;
                    }
                    int nums = JsonSerializer.Deserialize<int>(numsNode);
                    if (shoppingCart.Nums < nums)
                    {
                        isPromotionCondition = false;
                        break;
                    }

                    #endregion 指定商品分类满足条件
                }
                else if (pCondition.Code.Equals("GOODS_BRANDS"))
                {
                    #region 指定商品品牌满足条件

                    // {"brandId":"3a0e648b-9c2e-d106-cd54-c0a21d9ad54e","nums":1}
                    paramObj.TryGetPropertyValue("brandId", out JsonNode? brandIdNode);
                    if (brandIdNode == null || brandIdNode.ToString().IsNullOrWhiteSpace())
                    {
                        isPromotionCondition = false;
                        break;
                    }
                    Guid catId = JsonSerializer.Deserialize<Guid>(brandIdNode);
                    if (catId != good.BrandId)
                    {
                        isPromotionCondition = false;
                        break;
                    }
                    paramObj.TryGetPropertyValue("nums", out JsonNode? numsNode);
                    if (numsNode == null || numsNode.ToString().IsNullOrWhiteSpace())
                    {
                        isPromotionCondition = false;
                        break;
                    }
                    int nums = JsonSerializer.Deserialize<int>(numsNode);
                    if (shoppingCart.Nums < nums)
                    {
                        isPromotionCondition = false;
                        break;
                    }

                    #endregion 指定商品品牌满足条件
                }
                else if (pCondition.Code.Equals("ORDER_FULL"))
                {
                    #region 订单满××金额满足条件

                    // {"money":"10"}
                    paramObj.TryGetPropertyValue("money", out JsonNode? moneyNode);
                    if (moneyNode == null || moneyNode.ToString().IsNullOrWhiteSpace())
                    {
                        isPromotionCondition = false;
                        break;
                    }
                    string moneyStr = JsonSerializer.Deserialize<string>(moneyNode);
                    decimal.TryParse(moneyStr, out decimal money);
                    if (cartOrder.OrderAmount < money)
                    {
                        isPromotionCondition = false;
                        break;
                    }

                    #endregion 订单满××金额满足条件
                }
                else if (pCondition.Code.Equals("USER_GRADE"))
                {
                    #region 用户符合指定等级

                    // {"grades":"3a0d4972-872c-9198-d9f6-d4e3bba35f5e,3a0d4972-e6b6-93ef-45eb-bbca4ae40270"}
                    SysUserDto sysUser = _sysUserAppService.GetAsync(shoppingCart.UserId).Result;
                    if (sysUser == null || sysUser.GradeId == null)
                    {
                        isPromotionCondition = false;
                        break;
                    }
                    paramObj.TryGetPropertyValue("grades", out JsonNode? gradesNode);
                    if (gradesNode == null || gradesNode.ToString().IsNullOrWhiteSpace())
                    {
                        isPromotionCondition = false;
                        break;
                    }

                    string gradeIdsStr = JsonSerializer.Deserialize<string>(gradesNode);
                    Guid[] gradeIds = gradeIdsStr.Split(',').Select(x => Guid.Parse(x.Trim())).ToArray();
                    if (!gradeIds.Contains(sysUser.GradeId.Value))
                    {
                        isPromotionCondition = false;
                        break;
                    }

                    #endregion 用户符合指定等级
                }
            }
            return isPromotionCondition;
        }

        /// <summary>
        /// 更新用户的购物车货品数量
        /// </summary>
        /// <param name="id">购物车ID</param>
        /// <param name="nums">货品数量</param>
        /// <returns></returns>
        [HttpPut("{id}/nums/{nums}")]
        [NonAction]
        public async Task<IActionResult> UpdateUserCartNumsAsnyc([FromRoute] Guid id, [FromRoute] int nums)
        {
            List<CartDto> cartList = await _cartAppService.GetUserCartByIdsAsync(new List<Guid> { id });
            if (cartList == null || !cartList.Any()) return BadRequest();
            // 验证库存是否充足
            CartDto cart = cartList.First();
            ProductDto product = await _goodCommonAppService.GetProductByIdAsync(cart.ProductId);
            if (product == null) return BadRequest();
            if (product.Stock - product.FreezeStock < nums) throw new UserFriendlyException("该商品库存不足！", SystemStatusCode.Fail.ToFrontAggregationServicePrefix());
            await _cartAppService.UpdateUserCartNumsAsnyc(id, nums);
            return NoContent();
        }
    }
}