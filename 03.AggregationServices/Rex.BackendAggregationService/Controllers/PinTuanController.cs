using Microsoft.AspNetCore.Mvc;
using Rex.GoodService.Goods;
using Rex.PromotionService.PinTuans;
using System.Data;
using Volo.Abp.Application.Dtos;

namespace Rex.BackendPinTuanAggregationService.Controllers
{
    /// <summary>
    /// 拼团控制器
    /// </summary>
    [Route("api/backend/aggregation/pinTuan")]
    [ApiController]
    public class PinTuanController : ControllerBase
    {
        private readonly IPinTuanRuleAppService _pinTuanRuleAppService;
        private readonly IGoodsAppService _goodAppService;

        public PinTuanController(IPinTuanRuleAppService pinTuanRuleAppService, IGoodsAppService goodAppService)
        {
            _pinTuanRuleAppService = pinTuanRuleAppService;
            _goodAppService = goodAppService;
        }

        /// <summary>
        /// 获取(分页)拼团规则列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        [HttpGet("rule")]
        public async Task<PagedResultDto<PinTuanRuleDto>> GetPageListAsync([FromQuery] GetPinTuanRuleInput input)
        {
            PagedResultDto<PinTuanRuleDto> pagePinTuanRule = await _pinTuanRuleAppService.GetPageListAsync(input);
            if (pagePinTuanRule.Items.Count > 0)
            {
                Guid[] ruleIds = pagePinTuanRule.Items.Select(p => p.Id).ToArray();
                List<PinTuanGoodDto> pinTuanGoods = await _pinTuanRuleAppService.GetPinTuanGoodByRuleIdsAsync(ruleIds);
                LoadPinTuanGood(ref pinTuanGoods);
                foreach (var pinTuanRule in pagePinTuanRule.Items)
                {
                    pinTuanRule.PinTuanGoods = pinTuanGoods.Where(p => p.RuleId == pinTuanRule.Id).ToList();
                }
            }
            return pagePinTuanRule;
        }

        /// <summary>
        /// 根据拼团规则ID获取拼团商品
        /// </summary>
        /// <param name="ruleIds">拼团规则ID</param>
        /// <returns></returns>
        [HttpPost("good")]
        public async Task<List<PinTuanGoodDto>> GetPinTuanGoodByRuleIdsAsync([FromBody] Guid[] ruleIds)
        {
            List<PinTuanGoodDto> pinTuanGoods = await _pinTuanRuleAppService.GetPinTuanGoodByRuleIdsAsync(ruleIds);
            LoadPinTuanGood(ref pinTuanGoods);
            return pinTuanGoods;
        }

        /// <summary>
        /// 加载拼团商品信息
        /// </summary>
        /// <param name="pinTuanGoods"></param>
        private void LoadPinTuanGood(ref List<PinTuanGoodDto> pinTuanGoods)
        {
            if (pinTuanGoods.Any())
            {
                List<Guid> goodIds = pinTuanGoods.Select(p => p.GoodId).ToList();
                List<GoodDto> goods = _goodAppService.GetGoodByIdsAsync(goodIds).Result;
                foreach (var pinTuanGood in pinTuanGoods)
                {
                    GoodDto good = goods.Where(p => p.Id == pinTuanGood.GoodId).FirstOrDefault();
                    if (good == null) continue;
                    pinTuanGood.GoodName = good.Name;
                    pinTuanGood.GoodImage = good.Image;
                    pinTuanGood.GoodImages = good.Images;
                    pinTuanGood.GoodPrice = good.Price;
                }
            }
        }
    }
}