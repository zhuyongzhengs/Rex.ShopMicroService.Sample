using Microsoft.AspNetCore.Mvc;
using Rex.FrontAggregationService.Core.Mappers;
using Rex.FrontAggregationService.Goods;
using Rex.FrontAggregationService.Users;
using Rex.GoodService.Articles;
using Rex.GoodService.Caching;
using Rex.GoodService.Goods;
using Rex.GoodService.Notices;
using Rex.GoodService.PageDesigns;
using Rex.GoodService.Products;
using Rex.GoodService.ServiceDescriptions;
using Rex.GoodService.ServiceGoods;
using Rex.PromotionService.PinTuans;
using Rex.PromotionService.Promotions;
using Rex.Service.AspNetCore.Extensions;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Models;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using Volo.Abp.Application.Dtos;
using static Rex.Service.Core.Configurations.GlobalEnums;
using IBaseCommonAppService = Rex.BaseService.Systems.Commons.ICommonAppService;
using IGoodCommonAppService = Rex.GoodService.Commons.ICommonAppService;
using IOrderCommonAppService = Rex.OrderService.Commons.ICommonAppService;
using IPromotionCommonAppService = Rex.PromotionService.Commons.ICommonAppService;

namespace Rex.FrontCommonAggregationService.Controllers
{
    /// <summary>
    /// 聚合公共控制器
    /// </summary>
    [Route("api/front/aggregation/common")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IBaseCommonAppService _baseCommonAppService;
        private readonly IGoodCommonAppService _goodCommonAppService;
        private readonly IPromotionCommonAppService _promotionCommonAppService;
        private readonly IPromotionGlobalAppService _promotionGlobalAppService;
        private readonly IOrderCommonAppService _orderCommonAppService;

        public CommonController(IBaseCommonAppService baseCommonAppService, IGoodCommonAppService goodCommonAppService, IPromotionCommonAppService promotionCommonAppService, IPromotionGlobalAppService promotionGlobalAppService, IOrderCommonAppService orderCommonAppService)
        {
            _baseCommonAppService = baseCommonAppService;
            _goodCommonAppService = goodCommonAppService;
            _promotionCommonAppService = promotionCommonAppService;
            _promotionGlobalAppService = promotionGlobalAppService;
            _orderCommonAppService = orderCommonAppService;
        }

        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        [HttpGet("goods")]
        public async Task<IActionResult> GetGoodPageListAsync([FromQuery] GetGoodInput input)
        {
            PagedResultDto<GoodDto> goodResults = await _goodCommonAppService.GetGoodPageListAsync(input);
            return Ok(goodResults);
        }

        /// <summary>
        /// 根据ID获取商品信息
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <returns></returns>
        [HttpGet("goods/{id}")]
        public async Task<IActionResult> GetGoodByIdAsync([FromRoute] Guid id)
        {
            GoodDto good = await _goodCommonAppService.GetGoodByIdAsync(id);
            return Ok(good);
        }

        /// <summary>
        /// 根据ID获取秒杀商品信息
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <param name="promotionId">促销ID</param>
        /// <returns></returns>
        [HttpGet("seckillGood/{id}/{promotionId}")]
        public async Task<SeckillGoodDto> GetSeckillGoodByIdAsync([FromRoute] Guid id, [FromRoute] Guid promotionId)
        {
            GoodDto good = await _goodCommonAppService.GetGoodByIdAsync(id);
            if (good == null || !good.IsMarketable) return null;
            SeckillGoodDto seckillGood = ObjectMapper.Map<GoodDto, SeckillGoodDto>(good);
            seckillGood.BrandName = good.Brand?.Name;
            PromotionDto promotion = await _promotionCommonAppService.GetPromotionByIdAsync(promotionId);
            if (promotion == null || promotion.Type != (int)PromotionType.Seckill) return null;
            DateTime nowDate = DateTime.Now;
            seckillGood.PromotionId = promotion.Id;
            seckillGood.PromotionType = promotion.Type;
            seckillGood.PromotionIsEnable = promotion.IsEnable;
            seckillGood.PromotionTime = nowDate;
            seckillGood.PromotionStartTime = promotion.StartTime;
            seckillGood.PromotionEndTime = promotion.EndTime;

            TimeSpan ts = promotion.EndTime.Subtract(nowDate);
            seckillGood.PromotionSeconds = (int)ts.TotalSeconds;
            if (seckillGood.PromotionStartTime > nowDate)
            {
                seckillGood.StartStatus = (int)PinTuanRuleStatus.NotBegun;
            }
            else if (seckillGood.PromotionStartTime <= nowDate && seckillGood.PromotionEndTime > nowDate)
            {
                seckillGood.StartStatus = (int)PinTuanRuleStatus.Begin;
            }
            else
            {
                seckillGood.StartStatus = (int)PinTuanRuleStatus.HaveExpired;
                seckillGood.IsOverdue = true;
            }

            #region 计算秒杀优惠价格

            ProductDto product = good.Products?.FirstOrDefault(x => x.IsDefault);
            if (product != null)
            {
                seckillGood.GoodSeckillMoney = product.Price;
                SetPromotionGlobalAmount(ref seckillGood, product);
            }

            #endregion 计算秒杀优惠价格

            //Guid[] productIds = good.Products?.Select(x => x.Id).ToArray();
            List<ProductStockRc> productStocks = await _goodCommonAppService.GetProductStocksAsync(id);
            seckillGood.TotalStock = productStocks.Sum(x => x.Stock);
            seckillGood.TotalFreezeStock = productStocks.Sum(x => x.FreezeStock);

            return seckillGood;
        }

        /// <summary>
        /// 设置全局促销
        /// </summary>
        /// <param name="seckillGood">秒杀商品</param>
        /// <param name="product">货品信息</param>
        private void SetPromotionGlobalAmount(ref SeckillGoodDto seckillGood, ProductDto product)
        {
            List<PromotionDto> promotions = _promotionCommonAppService.GetPromotionGlobalIsEnableAsync(true, true).Result;
            if (promotions == null || !promotions.Any()) return;

            decimal goodPromotionMoney = 0; // 商品优惠
            decimal orderPromotionMoney = 0; // 订单优惠
            const int buyNum = 1; // (假设)购买数量
            foreach (var promotion in promotions)
            {
                if (promotion.PromotionConditions == null || !promotion.PromotionConditions.Any()) continue;
                if (promotion.PromotionResults == null || !promotion.PromotionResults.Any()) continue;

                // 验证是否满足(所有)促销条件
                bool isPromotionCondition = ValidatePromotionCondition(seckillGood, promotion.PromotionConditions, product);
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

                        decimal discountMoney = Math.Round(product.Price * discount, 2);
                        goodPromotionMoney += Math.Round(product.Price - discountMoney, 2);

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
                        if (money < product.Price)
                        {
                            decimal oneMoney = Math.Round(product.Price - money, 2);
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
                        decimal discountMoney = Math.Round(product.Price * discount, 2);
                        orderPromotionMoney += Math.Round(product.Price - discountMoney, 2);

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
                        if (buyNum >= num) orderPromotionMoney += money;

                        #endregion 指定商品每第几件减指定金额
                    }
                }

                #endregion 计算促销结果

                if (promotion.IsExclusive) break;
            }
            if (goodPromotionMoney > 0)
            {
                seckillGood.GoodSeckillMoney = Math.Round(seckillGood.GoodSeckillMoney - goodPromotionMoney, 2);
            }
            if (orderPromotionMoney > 0)
            {
                seckillGood.GoodSeckillMoney = Math.Round(seckillGood.GoodSeckillMoney - orderPromotionMoney, 2);
            }
            seckillGood.SeckillDiscountAmount = Math.Round(goodPromotionMoney + orderPromotionMoney, 2);
        }

        /// <summary>
        /// 验证是否满足(所有)促销条件
        /// </summary>
        /// <param name="seckillGood">秒杀商品</param>
        /// <param name="promotionConditions">促销条件</param>
        /// <param name="product">货品信息</param>
        /// <returns></returns>
        private bool ValidatePromotionCondition(SeckillGoodDto seckillGood, List<PromotionConditionDto> promotionConditions, ProductDto product)
        {
            bool isPromotionCondition = true;
            const int buyNum = 1; // (假设)购买数量
            foreach (var pCondition in promotionConditions)
            {
                if (pCondition.Code.Equals("GOODS_ALL")) continue;
                if (pCondition.Parameters.IsNullOrWhiteSpace())
                {
                    isPromotionCondition = false;
                    break;
                }
                JsonObject paramObj = JsonSerializer.Deserialize<JsonObject>(pCondition.Parameters);
                GoodDto good = _goodCommonAppService.GetGoodByIdAsync(product.GoodId.Value, false).Result;
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
                    if (!goodIds.Contains(product.GoodId.Value))
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
                    if (buyNum < nums)
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
                    if (buyNum < nums)
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
                    if (buyNum < nums)
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
                    if (seckillGood.Price < money)
                    {
                        isPromotionCondition = false;
                        break;
                    }

                    #endregion 订单满××金额满足条件
                }
                /*
                else if (pCondition.Code.Equals("USER_GRADE"))
                {
                    #region 用户符合指定等级

                    // {"grades":"3a0d4972-872c-9198-d9f6-d4e3bba35f5e,3a0d4972-e6b6-93ef-45eb-bbca4ae40270"}
                    SysUserDto sysUser = _sysUserAppService.GetAsync(UserId).Result;
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
                */
            }
            return isPromotionCondition;
        }

        /// <summary>
        /// 获取页面布局信息
        /// </summary>
        /// <param name="layout">布局样式：1、手机端  2、PC端</param>
        /// <param name="pageCode">页面编码</param>
        /// <returns></returns>
        [HttpGet("pageLayout/{layout}")]
        public async Task<IActionResult> GetPageLayoutAsync([FromRoute] int layout, [FromQuery] string pageCode = "")
        {
            // 查询页面布局数据
            PageDesignDto pageDesign = await _goodCommonAppService.GetPageDesignByLayoutCodeAsync(layout, pageCode);
            List<PageDesignItemDto> pageDesignItems = await _goodCommonAppService.GetPageDesignItemByPageCodeAsync(pageDesign.Code);

            // 查询布局明细信息
            List<PageDesignItemDto> layoutItems = new List<PageDesignItemDto>();
            foreach (var pageDesignItem in pageDesignItems)
            {
                PageDesignItemDto pDesignItem = new PageDesignItemDto();
                pDesignItem.Id = pageDesignItem.Id;
                pDesignItem.WidgetCode = pageDesignItem.WidgetCode;
                pDesignItem.PageCode = pageDesignItem.PageCode;
                pDesignItem.PositionId = pageDesignItem.PositionId;
                pDesignItem.Sort = pageDesignItem.Sort;

                string parameters = pageDesignItem.Parameters.As<string>();
                parameters = Regex.Replace(parameters, "/src/assets/design/empty.png", "/static/images/common/empty.png");
                parameters = Regex.Replace(parameters, "/src/assets/design/empty-banner.png", "/static/images/common/empty-banner.png");
                switch (pDesignItem.WidgetCode)
                {
                    case "notice": // 公告
                        LoadNotices(ref pDesignItem, pageDesignItem);
                        break;

                    case "coupon": // 优惠券组
                        LoadCoupons(ref pDesignItem, pageDesignItem);
                        break;

                    case "textarea": // 文本域
                        LoadTextareas(ref pDesignItem, pageDesignItem);
                        break;

                    case "goodTabBar": // 商品选项卡
                        LoadGoodTabBars(ref pDesignItem, pageDesignItem);
                        break;

                    case "goods": // 商品组
                        LoadGoods(ref pDesignItem, pageDesignItem);
                        break;

                    case "articleClassify": // 文章分类
                        LoadArticleByClassifys(ref pDesignItem, pageDesignItem);
                        break;

                    case "groupPurchase": // 团购秒杀
                        LoadGroupPurchases(ref pDesignItem, pageDesignItem);
                        break;

                    case "pinTuan": // 拼团
                        LoadPinTuans(ref pDesignItem, pageDesignItem);
                        break;

                    case "service": // 服务组
                        LoadServices(ref pDesignItem, pageDesignItem);
                        break;

                    default:
                        pDesignItem.Parameters = JsonSerializer.Deserialize<JsonObject>(parameters);
                        break;
                }
                pDesignItem.ConcurrencyStamp = pageDesignItem.ConcurrencyStamp;
                layoutItems.Add(pDesignItem);
            }
            return Ok(new PageLayoutDto()
            {
                Id = pageDesign.Id,
                Code = pageDesign.Code,
                Desc = pageDesign.Description,
                Layout = pageDesign.Layout,
                Name = pageDesign.Name,
                Type = pageDesign.Type,
                Items = layoutItems
            });
        }

        #region 加载页面布局信息

        /// <summary>
        /// 加载公告信息
        /// </summary>
        /// <param name="pDesignItem">页面设计项Dto</param>
        /// <param name="pageDesignItem">页面设计项</param>
        private void LoadNotices(ref PageDesignItemDto pDesignItem, PageDesignItemDto pageDesignItem)
        {
            string parameters = pageDesignItem.Parameters.As<string>();
            JsonObject parametersObj = JsonSerializer.Deserialize<JsonObject>(parameters);
            if (parametersObj == null) return;
            JsonNode? paramType = null;
            parametersObj.TryGetPropertyValue("type", out paramType);

            JsonArray noticelist = new JsonArray();
            if (paramType != null && paramType.ToString().Equals("auto"))
            {
                // 自动选择
                List<NoticeDto> notices = _goodCommonAppService.GetNoticePagedListAsync(0, 10, "Sort ASC").Result;
                if (notices.Any()) noticelist = JsonSerializer.Deserialize<JsonArray>(JsonSerializer.Serialize(notices, AspNetCoreExtension.GetJsonSerializerOptions()));
            }
            else if (paramType != null && paramType.ToString().Equals("choose"))
            {
                // 手动选择
                if (parametersObj.ContainsKey("list"))
                {
                    JsonArray noticeArray = JsonSerializer.Deserialize<JsonArray>(JsonSerializer.Serialize(parametersObj["list"], AspNetCoreExtension.GetJsonSerializerOptions()));
                    List<Guid> noticeIds = new List<Guid>();
                    foreach (JsonNode notice in noticeArray)
                    {
                        string noticeId = notice?.AsObject()["id"]?.ToString();
                        if (noticeId.IsNullOrWhiteSpace()) continue;
                        noticeIds.Add(Guid.Parse(noticeId));
                    }
                    if (noticeIds.Any())
                    {
                        List<NoticeDto> noticeList = _goodCommonAppService.GetNoticeByIdsAsync(noticeIds).Result;
                        List<NoticeDto> notices = new List<NoticeDto>();
                        foreach (var nId in noticeIds)
                        {
                            NoticeDto notice = noticeList.Where(p => p.Id == nId).FirstOrDefault();
                            if (notice == null) continue;
                            notices.Add(notice);
                        }
                        if (notices.Any()) noticelist = JsonSerializer.Deserialize<JsonArray>(JsonSerializer.Serialize(notices, AspNetCoreExtension.GetJsonSerializerOptions()));
                    }
                }
            }
            parametersObj.Remove("list");
            parametersObj.Add("list", noticelist);
            pDesignItem.Parameters = parametersObj;
        }

        /// <summary>
        /// 加载优惠券组
        /// </summary>
        /// <param name="pDesignItem">页面设计项Dto</param>
        /// <param name="pageDesignItem">页面设计项</param>
        private void LoadCoupons(ref PageDesignItemDto pDesignItem, PageDesignItemDto pageDesignItem)
        {
            string parameters = pageDesignItem.Parameters.As<string>();
            JsonObject parametersObj = JsonSerializer.Deserialize<JsonObject>(parameters);
            if (parametersObj == null) return;
            JsonNode? limitNode = null;
            parametersObj.TryGetPropertyValue("limit", out limitNode);

            JsonArray couponlist = new JsonArray();
            int.TryParse(limitNode?.ToString(), out int limit);
            if (limit > 0)
            {
                // 查询优惠劵
                List<PromotionDto> promotions = _promotionCommonAppService.GetReceiveCouponAsync(limit).Result;
                if (promotions.Any()) couponlist = JsonSerializer.Deserialize<JsonArray>(JsonSerializer.Serialize(promotions, AspNetCoreExtension.GetJsonSerializerOptions()));
            }
            parametersObj.Remove("list");
            parametersObj.Add("list", couponlist);
            pDesignItem.Parameters = parametersObj;
        }

        /// <summary>
        /// 加载文本域
        /// </summary>
        /// <param name="pDesignItem">页面设计项Dto</param>
        /// <param name="pageDesignItem">页面设计项</param>
        private void LoadTextareas(ref PageDesignItemDto pDesignItem, PageDesignItemDto pageDesignItem)
        {
            string parameters = pageDesignItem.Parameters.As<string>();
            JsonObject parametersObj = new JsonObject();
            parametersObj["value"] = parameters;
            pDesignItem.Parameters = parametersObj;
        }

        /// <summary>
        /// 加载商品选项卡
        /// </summary>
        /// <param name="pDesignItem">页面设计项Dto</param>
        /// <param name="pageDesignItem">页面设计项</param>
        private void LoadGoodTabBars(ref PageDesignItemDto pDesignItem, PageDesignItemDto pageDesignItem)
        {
            string parameters = pageDesignItem.Parameters.As<string>();
            JsonObject parametersObj = JsonSerializer.Deserialize<JsonObject>(parameters);
            if (parametersObj == null) return;
            JsonNode? jsonNode = null;
            parametersObj.TryGetPropertyValue("list", out jsonNode);

            JsonArray goodTabBarList = new JsonArray();
            JsonArray jsonList = new JsonArray();
            if (jsonNode != null) jsonList = jsonNode.AsArray();
            foreach (var jsonResult in jsonList)
            {
                JsonObject jsonObj = jsonResult.AsObject();
                JsonNode? listNode = null; // 列表
                JsonNode? typeNode = null; // 类型
                JsonNode? classifyIdNode = null; // 分类ID
                JsonNode? brandIdNode = null; // 品牌ID
                JsonNode? limitNode = null; // 数量
                jsonObj.TryGetPropertyValue("list", out listNode);
                jsonObj.TryGetPropertyValue("type", out typeNode);
                jsonObj.TryGetPropertyValue("classifyId", out classifyIdNode);
                jsonObj.TryGetPropertyValue("brandId", out brandIdNode);
                jsonObj.TryGetPropertyValue("limit", out limitNode);

                JsonArray goodlist = new JsonArray();
                if (typeNode != null && typeNode.ToString().Equals("auto"))
                {
                    // 自动选择
                    List<Guid> categoryIds = new List<Guid>();
                    if (classifyIdNode != null)
                    {
                        // 查找分类
                        if (Guid.TryParse(classifyIdNode.ToString(), out Guid categoryId))
                        {
                            categoryIds.Add(categoryId);
                            List<GoodCategoryDto> goodCategorys = _goodCommonAppService.GetGoodCategoryByParentIdAsync(categoryId).Result;
                            categoryIds.AddRange(goodCategorys.Select(p => p.Id));
                        }
                    }
                    Guid brandId = Guid.Empty;
                    Guid.TryParse(brandIdNode?.ToString(), out brandId);
                    int limit = 10;
                    int.TryParse(limitNode?.ToString(), out limit);
                    List<GoodDto> goods = _goodCommonAppService.GetGoodTabBarListAsync(categoryIds, brandId, limit).Result;
                    if (goods.Any()) goodlist = JsonSerializer.Deserialize<JsonArray>(JsonSerializer.Serialize(goods, AspNetCoreExtension.GetJsonSerializerOptions()));
                }
                else if (typeNode != null && typeNode.ToString().Equals("choose"))
                {
                    // 手动选择
                    if (listNode != null)
                    {
                        JsonArray jsonItems = listNode.AsArray();
                        List<Guid> goodIds = new List<Guid>();
                        foreach (var jsonItem in jsonItems)
                        {
                            if (Guid.TryParse(jsonItem?.AsObject()["id"]?.ToString(), out Guid goodId))
                            {
                                goodIds.Add(goodId);
                            }
                        }
                        if (goodIds.Any())
                        {
                            List<GoodDto> goodItems = _goodCommonAppService.GetGoodByIdsAsync(goodIds).Result;
                            if (!goodItems.Any()) return;
                            List<GoodDto> goods = new List<GoodDto>();
                            foreach (var goodId in goodIds)
                            {
                                GoodDto good = goodItems?.Where(p => p.Id == goodId).FirstOrDefault();
                                if (good == null) continue;
                                goods.Add(good);
                            }
                            if (goods.Any()) goodlist = JsonSerializer.Deserialize<JsonArray>(JsonSerializer.Serialize(goods, AspNetCoreExtension.GetJsonSerializerOptions()));
                        }
                    }
                }
                jsonObj.Remove("list");
                jsonObj.Add("list", goodlist);
                JsonNode cloneJsonObj = JsonSerializer.Deserialize<JsonNode>(jsonObj);
                goodTabBarList.Add(cloneJsonObj);
            }
            if (goodTabBarList.Any())
            {
                parametersObj.Remove("list");
                parametersObj.Add("list", goodTabBarList);
            }
            pDesignItem.Parameters = parametersObj;
        }

        /// <summary>
        /// 加载商品组
        /// </summary>
        /// <param name="pDesignItem">页面设计项Dto</param>
        /// <param name="pageDesignItem">页面设计项</param>
        private void LoadGoods(ref PageDesignItemDto pDesignItem, PageDesignItemDto pageDesignItem)
        {
            string parameters = pageDesignItem.Parameters.As<string>();
            JsonObject parametersObj = JsonSerializer.Deserialize<JsonObject>(parameters);
            if (parametersObj == null) return;
            JsonNode? listNode = null; // 列表
            JsonNode? typeNode = null; // 类型
            JsonNode? classifyIdNode = null; // 分类ID
            JsonNode? brandIdNode = null; // 品牌ID
            JsonNode? limitNode = null; // 数量
            parametersObj.TryGetPropertyValue("list", out listNode);
            parametersObj.TryGetPropertyValue("type", out typeNode);
            parametersObj.TryGetPropertyValue("classifyId", out classifyIdNode);
            parametersObj.TryGetPropertyValue("brandId", out brandIdNode);
            parametersObj.TryGetPropertyValue("limit", out limitNode);

            JsonArray goodlist = new JsonArray();
            if (typeNode != null && typeNode.ToString().Equals("auto"))
            {
                // 自动选择
                List<Guid> categoryIds = new List<Guid>();
                if (classifyIdNode != null)
                {
                    // 查找分类
                    if (Guid.TryParse(classifyIdNode.ToString(), out Guid categoryId))
                    {
                        categoryIds.Add(categoryId);
                        List<GoodCategoryDto> goodCategorys = _goodCommonAppService.GetGoodCategoryByParentIdAsync(categoryId).Result;
                        categoryIds.AddRange(goodCategorys.Select(p => p.Id));
                    }
                }
                Guid brandId = Guid.Empty;
                Guid.TryParse(brandIdNode?.ToString(), out brandId);
                int limit = 10;
                int.TryParse(limitNode?.ToString(), out limit);
                List<GoodDto> goods = _goodCommonAppService.GetGoodTabBarListAsync(categoryIds, brandId, limit).Result;
                if (goods.Any()) goodlist = JsonSerializer.Deserialize<JsonArray>(JsonSerializer.Serialize(goods, AspNetCoreExtension.GetJsonSerializerOptions()));
            }
            else if (typeNode != null && typeNode.ToString().Equals("choose"))
            {
                // 手动选择
                if (listNode != null)
                {
                    JsonArray jsonItems = listNode.AsArray();
                    List<Guid> goodIds = new List<Guid>();
                    foreach (var jsonItem in jsonItems)
                    {
                        if (Guid.TryParse(jsonItem?.AsObject()["id"]?.ToString(), out Guid goodId))
                        {
                            goodIds.Add(goodId);
                        }
                    }
                    if (goodIds.Any())
                    {
                        List<GoodDto> goodItems = _goodCommonAppService.GetGoodByIdsAsync(goodIds).Result;
                        if (!goodItems.Any()) return;
                        List<GoodDto> goods = new List<GoodDto>();
                        foreach (var goodId in goodIds)
                        {
                            GoodDto good = goodItems.Where(p => p.Id == goodId).FirstOrDefault();
                            if (good == null) continue;
                            goods.Add(good);
                        }
                        if (goods.Any()) goodlist = JsonSerializer.Deserialize<JsonArray>(JsonSerializer.Serialize(goods, AspNetCoreExtension.GetJsonSerializerOptions()));
                    }
                }
            }
            if (goodlist != null && goodlist.Any())
            {
                parametersObj.Remove("list");
                parametersObj.Add("list", goodlist);
            }
            pDesignItem.Parameters = parametersObj;
        }

        /// <summary>
        /// 加载文章分类
        /// </summary>
        /// <param name="pDesignItem">页面设计项Dto</param>
        /// <param name="pageDesignItem">页面设计项</param>
        private void LoadArticleByClassifys(ref PageDesignItemDto pDesignItem, PageDesignItemDto pageDesignItem)
        {
            string parameters = pageDesignItem.Parameters.As<string>();
            JsonObject parametersObj = JsonSerializer.Deserialize<JsonObject>(parameters);
            if (parametersObj == null) return;

            JsonNode? articleClassifyIdNode = null; // 文章分类ID
            JsonNode? limitNode = null; // 数量
            parametersObj.TryGetPropertyValue("articleClassifyId", out articleClassifyIdNode);
            parametersObj.TryGetPropertyValue("limit", out limitNode);

            int limit = 10;
            int.TryParse(limitNode?.ToString(), out limit);
            JsonArray articlelist = new JsonArray();
            if (Guid.TryParse(articleClassifyIdNode?.ToString(), out Guid classifyId))
            {
                List<ArticleDto> articles = _goodCommonAppService.GetArticleByClassifyIdAsync(classifyId, true, limit).Result;
                if (articles.Any()) articlelist = JsonSerializer.Deserialize<JsonArray>(JsonSerializer.Serialize(articles, AspNetCoreExtension.GetJsonSerializerOptions()));
            }
            parametersObj.Remove("list");
            parametersObj.Add("list", articlelist);
            pDesignItem.Parameters = parametersObj;
        }

        /// <summary>
        /// 加载团购秒杀
        /// </summary>
        /// <param name="pDesignItem">页面设计项Dto</param>
        /// <param name="pageDesignItem">页面设计项</param>
        private void LoadGroupPurchases(ref PageDesignItemDto pDesignItem, PageDesignItemDto pageDesignItem)
        {
            string parameters = pageDesignItem.Parameters.As<string>();
            JsonObject parametersObj = JsonSerializer.Deserialize<JsonObject>(parameters);
            if (parametersObj == null) return;

            JsonArray newSeckillList = new JsonArray();
            int limit = 5;
            if (parametersObj.TryGetPropertyValue("limit", out JsonNode? limitNode))
            {
                limit = JsonSerializer.Deserialize<int>(limitNode);
            }
            if (parametersObj.TryGetPropertyValue("list", out JsonNode? listNode))
            {
                JsonArray jsonlist = listNode.AsArray();
                foreach (var jsonItem in jsonlist)
                {
                    JsonObject jsonObj = jsonItem.AsObject();
                    if (jsonObj == null) continue;
                    if (Guid.TryParse(jsonObj["id"]?.ToString(), out Guid id))
                    {
                        DateTime nowDate = DateTime.Now;
                        PromotionDto promotion = _promotionCommonAppService.GetGroupPurchaseAsync(id, nowDate, isEnable: true).Result;
                        if (promotion != null && promotion.PromotionConditions.Any())
                        {
                            PromotionConditionDto promotionCondition = promotion.PromotionConditions.FirstOrDefault();
                            JsonObject pcParameter = JsonSerializer.Deserialize<JsonObject>(promotionCondition.Parameters);
                            if (pcParameter.TryGetPropertyValue("goodsId", out JsonNode? goodsIdNode))
                            {
                                if (Guid.TryParse(goodsIdNode?.ToString(), out Guid goodId))
                                {
                                    SeckillGoodDto seckillGood = GetSeckillGoodByIdAsync(goodId, promotion.Id).Result;
                                    if (seckillGood == null || seckillGood.IsOverdue) continue;
                                    JsonNode seckillGoodObj = JsonSerializer.Deserialize<JsonNode>(JsonSerializer.Serialize(seckillGood, AspNetCoreExtension.GetJsonSerializerOptions()));
                                    newSeckillList.Add(seckillGoodObj);
                                    if (newSeckillList.Count >= limit) break;
                                }
                            }
                        }
                    }
                }
            }
            parametersObj.Remove("list");
            parametersObj.Add("list", newSeckillList);
            pDesignItem.Parameters = parametersObj;
        }

        /// <summary>
        /// 加载拼团
        /// </summary>
        /// <param name="pDesignItem">页面设计项Dto</param>
        /// <param name="pageDesignItem">页面设计项</param>
        private void LoadPinTuans(ref PageDesignItemDto pDesignItem, PageDesignItemDto pageDesignItem)
        {
            string parameters = pageDesignItem.Parameters.As<string>();
            JsonObject parametersObj = JsonSerializer.Deserialize<JsonObject>(parameters);
            if (parametersObj == null) return;
            if (parametersObj.TryGetPropertyValue("list", out JsonNode? listNode))
            {
                JsonArray jsonList = listNode.AsArray();
                DateTime nowDate = DateTime.Now;
                JsonArray resultList = new JsonArray();
                foreach (var jsonItem in jsonList)
                {
                    var pinTuanGoodItems = jsonItem?.AsObject()["pinTuanGoods"]?.AsArray();
                    if (pinTuanGoodItems == null || !pinTuanGoodItems.Any()) continue;
                    foreach (var pinTuanGoodItem in pinTuanGoodItems)
                    {
                        if (Guid.TryParse(pinTuanGoodItem?.AsObject()["goodId"]?.ToString(), out Guid goodsId))
                        {
                            // 查询拼团(规则)商品
                            PinTuanGoodDto pinTuanGood = _promotionCommonAppService.GetPinTuanGoodsAsync(goodsId).Result;
                            if (pinTuanGood != null && pinTuanGood.PinTuanRule != null && pinTuanGood.PinTuanRule.IsStatusOpen && pinTuanGood.PinTuanRule.EndTime > nowDate)
                            {
                                // 查询商品
                                GoodDto good = _goodCommonAppService.GetGoodByIdsAsync(new List<Guid>() { goodsId }).Result.FirstOrDefault();
                                ProductDto product = good.Products?.FirstOrDefault(p => p.IsDefault);
                                if (good == null || product == null) continue;

                                int pinTuanStartStatus = 1;
                                int lastTime = 0;
                                bool isOverdue = false;
                                if (pinTuanGood.PinTuanRule.StartTime > nowDate)
                                {
                                    pinTuanStartStatus = (int)GlobalEnums.PinTuanRuleStatus.NotBegun;
                                    TimeSpan ts = pinTuanGood.PinTuanRule.StartTime.Subtract(nowDate);
                                    lastTime = (int)ts.TotalSeconds;
                                    isOverdue = lastTime > 0;
                                }
                                else if (pinTuanGood.PinTuanRule.StartTime <= nowDate && pinTuanGood.PinTuanRule.EndTime > nowDate)
                                {
                                    pinTuanStartStatus = (int)GlobalEnums.PinTuanRuleStatus.Begin;
                                    TimeSpan ts = pinTuanGood.PinTuanRule.EndTime.Subtract(nowDate);
                                    lastTime = (int)ts.TotalSeconds;
                                    isOverdue = lastTime > 0;
                                }
                                else
                                {
                                    pinTuanStartStatus = (int)GlobalEnums.PinTuanRuleStatus.HaveExpired;
                                }

                                decimal pinTuanPrice = product.Price - pinTuanGood.PinTuanRule.DiscountAmount;
                                if (pinTuanPrice < 0) pinTuanPrice = 0;
                                JsonObject jsonResult = new JsonObject
                                {
                                    { "pinTuanStartStatus", pinTuanStartStatus },
                                    { "lastTime", lastTime },
                                    { "isOverdue", isOverdue },
                                    { "pinTuanPrice", pinTuanPrice },
                                    { "createTime", pinTuanGood.PinTuanRule.CreationTime },
                                    { "discountAmount", pinTuanGood.PinTuanRule.DiscountAmount },
                                    { "endTime", pinTuanGood.PinTuanRule.EndTime },
                                    { "goodsId", pinTuanGood.GoodId },
                                    { "goodsImage", good.Image },
                                    { "goodsName", good.Name },
                                    { "goodsPrice", product.Price },
                                    { "id", pinTuanGood.PinTuanRule.Id },
                                    { "isStatusOpen", pinTuanGood.PinTuanRule.IsStatusOpen },
                                    { "maxGoodsNums", pinTuanGood.PinTuanRule.MaxGoodsNums },
                                    { "maxNums", pinTuanGood.PinTuanRule.MaxNums },
                                    { "name", pinTuanGood.PinTuanRule.Name },
                                    { "peopleNumber", pinTuanGood.PinTuanRule.PeopleNumber },
                                    { "significantInterval", pinTuanGood.PinTuanRule.SignificantInterval },
                                    { "sort", pinTuanGood.PinTuanRule.Sort },
                                    { "startTime", pinTuanGood.PinTuanRule.StartTime },
                                    { "updateTime", pinTuanGood.PinTuanRule.LastModificationTime }
                                };
                                resultList.Add(jsonResult);
                            }
                        }
                    }
                }
                parametersObj.Remove("list");
                parametersObj.Add("list", resultList);
                pDesignItem.Parameters = parametersObj;
            }
        }

        /// <summary>
        /// 加载服务组
        /// </summary>
        /// <param name="pDesignItem">页面设计项Dto</param>
        /// <param name="pageDesignItem">页面设计项</param>
        private void LoadServices(ref PageDesignItemDto pDesignItem, PageDesignItemDto pageDesignItem)
        {
            string parameters = pageDesignItem.Parameters.As<string>();
            JsonObject parametersObj = JsonSerializer.Deserialize<JsonObject>(parameters);
            if (parametersObj == null) return;
            if (parametersObj.TryGetPropertyValue("list", out JsonNode? listNode))
            {
                JsonArray jsonList = listNode.AsArray();
                foreach (var jsonItem in jsonList)
                {
                    JsonObject jsonObj = jsonItem?.AsObject();
                    if (Guid.TryParse(jsonObj["id"]?.ToString(), out Guid id))
                    {
                        ServiceGoodDto serviceGood = _goodCommonAppService.GetServiceGoodByIdAsync(id).Result;
                        if (serviceGood == null) continue;
                        int pinTuanStartStatus = 1;
                        double lastTime = 0;
                        bool isOverdue = false;
                        //判断拼团状态
                        DateTime nowDate = DateTime.Now;

                        if (serviceGood.StartTime > nowDate)
                        {
                            pinTuanStartStatus = (int)GlobalEnums.ServicesOpenStatus.NotBegun;
                            TimeSpan ts = serviceGood.StartTime.Subtract(nowDate);
                            lastTime = ts.TotalMilliseconds;
                            isOverdue = lastTime > 0;
                        }
                        else if (serviceGood.StartTime <= nowDate && serviceGood.EndTime > nowDate)
                        {
                            pinTuanStartStatus = (int)GlobalEnums.ServicesOpenStatus.Begin;

                            TimeSpan ts = serviceGood.EndTime.Subtract(nowDate);
                            lastTime = ts.TotalMilliseconds;
                            isOverdue = lastTime > 0;
                        }
                        else
                        {
                            pinTuanStartStatus = (int)GlobalEnums.ServicesOpenStatus.HaveExpired;
                        }
                        jsonObj.Add("pinTuanStartStatus", pinTuanStartStatus);
                        jsonObj.Add("lastTime", lastTime);
                        jsonObj.Add("isOverdue", isOverdue);
                    }
                }
                parametersObj.Remove("list");
                parametersObj.Add("list", jsonList);
                pDesignItem.Parameters = parametersObj;
            }
        }

        #endregion 加载页面布局信息

        /// <summary>
        /// 获取树形商品分类信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("goodCategorysTree")]
        public async Task<IActionResult> GetGoodCategorysTreeAsync([FromQuery] Guid? parentId = null)
        {
            List<GoodCategoryTreeDto> goodCategoryTrees = new List<GoodCategoryTreeDto>();
            if (parentId.HasValue)
            {
                goodCategoryTrees = await _goodCommonAppService.GetGoodCategorysTreeAsync(parentId);
            }
            else
            {
                goodCategoryTrees = await _goodCommonAppService.GetGoodCategorysTreeAsync();
            }
            return Ok(goodCategoryTrees);
        }

        /// <summary>
        /// 获取商品分类中的子分类
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <returns></returns>
        [HttpGet("goodCategory/parentId/{parentId}")]
        public async Task<IActionResult> GetGoodCategoryByParentIdAsync([FromRoute] Guid parentId)
        {
            List<GoodCategoryDto> goodCategories = await _goodCommonAppService.GetGoodCategoryByParentIdAsync(parentId);
            return Ok(goodCategories);
        }

        /// <summary>
        /// 查询商城服务描述
        /// </summary>
        /// <returns></returns>
        [HttpGet("serviceDescriptionAll")]
        public async Task<IActionResult> GetServiceDescriptionAllAsync()
        {
            List<ServiceDescriptionDto> serviceDescriptionAll = await _goodCommonAppService.GetServiceDescriptionAllAsync();
            return Ok(serviceDescriptionAll);
        }

        /// <summary>
        /// 获取商品推荐数据
        /// </summary>
        /// <param name="limit">获取数量</param>
        /// <param name="isRecommend">是否推荐[默认：true]</param>
        /// <returns></returns>
        [HttpGet("goods/recommend")]
        public async Task<IActionResult> GetGoodRecommendListAsync([FromQuery] int limit = 10, [FromQuery] bool isRecommend = true)
        {
            List<GoodDto> goods = await _goodCommonAppService.GetGoodRecommendListAsync(limit, isRecommend);
            return Ok(goods);
        }

        /// <summary>
        /// 根据参数ID查询商品参数
        /// </summary>
        /// <param name="paramIds">参数ID</param>
        /// <returns></returns>
        [HttpPost("goods/param")]
        public async Task<IActionResult> GetGoodParamByIdsAsync([FromBody] List<Guid> paramIds)
        {
            List<GoodParamDto> goodParams = await _goodCommonAppService.GetGoodParamByIdsAsync(paramIds);
            return Ok(goodParams);
        }

        /// <summary>
        /// 秒杀促销信息
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="name">促销名称</param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        [HttpGet("promotionSeckills")]
        public async Task<PagedResultDto<SeckillGoodDto>> GetPromotionSeckills([FromQuery] int? status, [FromQuery] string? name, [FromQuery] int skipCount, [FromQuery] int maxResultCount)
        {
            GetPromotionInput promotionInput = new GetPromotionInput();
            promotionInput.Types = [4];
            promotionInput.Name = name;
            promotionInput.IsEnable = true;
            promotionInput.StartAndEndTime = new List<DateTime>();
            promotionInput.Status = status;
            promotionInput.Sorting = "CreationTime DESC";
            promotionInput.SkipCount = skipCount;
            promotionInput.MaxResultCount = maxResultCount;

            PagedResultDto<PromotionDto> pagedResult = await _promotionGlobalAppService.GetPageListAsync(promotionInput);
            if (pagedResult.TotalCount == 0) return new PagedResultDto<SeckillGoodDto>();
            // 查询秒杀商品
            List<PromotionConditionDto> promotionConditions = pagedResult.Items.SelectMany(p => p.PromotionConditions).ToList();
            List<Guid> goodIds = new List<Guid>();
            foreach (var pCondition in promotionConditions)
            {
                JsonObject paramObj = JsonSerializer.Deserialize<JsonObject>(pCondition.Parameters);
                if (paramObj.TryGetPropertyValue("goodsId", out JsonNode? goodIdsNode))
                {
                    string goodIdsStr = JsonSerializer.Deserialize<string>(goodIdsNode);
                    goodIds.Add(Guid.Parse(goodIdsStr));
                }
            }
            List<GoodDto> goods = await _goodCommonAppService.GetGoodByIdsAsync(goodIds);
            if (goods == null || goods.Count == 0) return new PagedResultDto<SeckillGoodDto>();
            PagedResultDto<SeckillGoodDto> pSeckillSesult = new PagedResultDto<SeckillGoodDto>();
            pSeckillSesult.TotalCount = pagedResult.TotalCount;
            List<SeckillGoodDto> seckillGoodItems = new List<SeckillGoodDto>();
            foreach (var promotion in pagedResult.Items)
            {
                PromotionConditionDto pCondition = promotion.PromotionConditions.FirstOrDefault();
                JsonObject paramObj = JsonSerializer.Deserialize<JsonObject>(pCondition.Parameters);
                if (!paramObj.TryGetPropertyValue("goodsId", out JsonNode? goodIdsNode)) continue;
                string goodIdsStr = JsonSerializer.Deserialize<string>(goodIdsNode);
                SeckillGoodDto seckillGood = await GetSeckillGoodByIdAsync(Guid.Parse(goodIdsStr), promotion.Id);
                seckillGoodItems.Add(seckillGood);
            }
            pSeckillSesult.Items = seckillGoodItems;
            return pSeckillSesult;
        }

        /// <summary>
        /// 获取用户通知数量
        /// </summary>
        /// <returns></returns>
        [HttpGet("userNoticeNumbers")]
        public async Task<IActionResult> GetUserNoticeNumberAsync()
        {
            UserNoticeNumberDto userNoticeNumber = new UserNoticeNumberDto();
            CurrentSysUserEto currentUser = await _baseCommonAppService.GetCurrentUserAsync();
            if (currentUser == null) return Ok(userNoticeNumber);
            userNoticeNumber.TotalGoodBrowsing = await _goodCommonAppService.GetUserGoodBrowsingCountAsync();
            userNoticeNumber.TotalCoupon = await _promotionCommonAppService.GetUserCouponCountAsync(false);
            userNoticeNumber.TotalGoodCollection = await _goodCommonAppService.GetUserGoodCollectionCountAsync();
            userNoticeNumber.TotalBillAftersale = await _orderCommonAppService.GetUserBillAftersaleCountAsync();
            userNoticeNumber.TotalPendingPayment = await _orderCommonAppService.GetUserPendingPaymentCountAsync();
            userNoticeNumber.TotalPendingShipment = await _orderCommonAppService.GetUserPendingShipmentCountAsync();
            userNoticeNumber.TotalPendingDelivery = await _orderCommonAppService.GetUserPendingDeliveryCountAsync();
            userNoticeNumber.TotalPendingEvaluate = await _orderCommonAppService.GetUserPendingEvaluateCountAsync();
            return Ok(userNoticeNumber);
        }

        /// <summary>
        /// 获取用户商品评价
        /// </summary>
        /// <param name="goodId">商品ID</param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        [HttpGet("userGoodComments/{goodId}")]
        public async Task<IActionResult> GetUserGoodCommentAsync([FromRoute] Guid goodId, [FromQuery] int skipCount, [FromQuery] int maxResultCount)
        {
            GetGoodCommentInput input = new GetGoodCommentInput();
            input.GoodId = goodId;
            input.IsDisplay = true;
            input.Sorting = "CreationTime DESC";
            input.SkipCount = skipCount;
            input.MaxResultCount = maxResultCount;
            PagedResultDto<GoodCommentDto> gCommentResult = await _goodCommonAppService.GetGoodCommentPageListAsync(input);
            PagedResultDto<UserGoodCommentDto> uGoodCommentResult = new PagedResultDto<UserGoodCommentDto>();
            if (gCommentResult.TotalCount < 1) Ok(uGoodCommentResult);
            List<UserGoodCommentDto> uGoodCommentList = new List<UserGoodCommentDto>();
            foreach (var gComment in gCommentResult.Items)
            {
                UserGoodCommentDto uGoodComment = ObjectMapper.Map<GoodCommentDto, UserGoodCommentDto>(gComment);
                uGoodCommentList.Add(uGoodComment);
            }

            uGoodCommentResult.TotalCount = gCommentResult.TotalCount;
            uGoodCommentResult.Items = uGoodCommentList;
            return Ok(uGoodCommentResult);
        }
    }
}