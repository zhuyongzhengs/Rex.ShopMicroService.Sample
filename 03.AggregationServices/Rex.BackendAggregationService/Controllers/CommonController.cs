using Microsoft.AspNetCore.Mvc;
using Rex.BaseService.Systems.PlatformSettings;
using Rex.Service.Setting;
using IPaymentCommonsAppService = Rex.PaymentService.Commons.ICommonAppService;
using IOrderCommonsAppService = Rex.OrderService.Commons.ICommonAppService;
using IGoodCommonsAppService = Rex.GoodService.Commons.ICommonAppService;
using Rex.Service.Setting.BaseServices;

namespace Rex.BackendAggregationService.Controllers
{
    /// <summary>
    /// 聚合公共控制器
    /// </summary>
    [Route("api/backend/aggregation/common")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IPlatformSettingAppService _platformSettingAppService;
        private readonly IPaymentCommonsAppService _paymentCommonsAppService;
        private readonly IOrderCommonsAppService _orderCommonsAppService;
        private readonly IGoodCommonsAppService _goodCommonsAppService;

        public CommonController(IPlatformSettingAppService platformSettingAppService, IPaymentCommonsAppService paymentCommonsAppService, IOrderCommonsAppService orderCommonsAppService, IGoodCommonsAppService goodCommonsAppService)
        {
            _platformSettingAppService = platformSettingAppService;
            _paymentCommonsAppService = paymentCommonsAppService;
            _orderCommonsAppService = orderCommonsAppService;
            _goodCommonsAppService = goodCommonsAppService;
        }

        /// <summary>
        /// [保存]平台设置
        /// </summary>
        /// <param name="platformSetting">平台设置Dto</param>
        /// <returns></returns>
        [HttpPost("savePlatformSetting")]
        public async Task<IActionResult> SavePlatformSettingAsync([FromBody] PlatformSettingDto platformSetting)
        {
            await _platformSettingAppService.UpdateAsync(platformSetting);
            List<SettingValueDo> settingValueDos = platformSetting.MergeAllSettings();
            foreach (var settingValueDo in settingValueDos)
            {
                if (settingValueDo.Name.StartsWith("EasyAbp.Abp.WeChat"))
                {
                    await _paymentCommonsAppService.UpdateSettingCurrentTenantAsync(settingValueDo.Name, settingValueDo.Value);
                }

                if (
                    settingValueDo.Name.StartsWith(BaseServiceSettings.OrderSetting.Default) ||
                    settingValueDo.Name.StartsWith(BaseServiceSettings.PointsSetting.Default)
                )
                {
                    await _orderCommonsAppService.UpdateSettingCurrentTenantAsync(settingValueDo.Name, settingValueDo.Value);
                }
            }
            return NoContent();
        }

        /// <summary>
        /// 获取待办事项数量
        /// </summary>
        /// <returns></returns>
        [HttpGet("waitItemCount")]
        public async Task<IActionResult> GetWaitItemCountAsync()
        {
            Dictionary<string, int> waitItemDic = new Dictionary<string, int>();

            // 待支付数量
            int pendingPaymentCount = await _orderCommonsAppService.GetPendingPaymentCountAsync();
            waitItemDic.Add("pendingPaymentCount", pendingPaymentCount);

            // 待发货数量
            int pendingShipmentCount = await _orderCommonsAppService.GetPendingShipmentCountAsync();
            waitItemDic.Add("pendingShipmentCount", pendingShipmentCount);

            // 待售后数量
            int pendingAftersalesCount = await _orderCommonsAppService.GetPendingAftersalesCountAsync();
            waitItemDic.Add("pendingAftersalesCount", pendingAftersalesCount);

            // 库存报警数量
            //int stockWarnNum = 0;
            //PlatformSettingDto platformSetting = await _platformSettingAppService.GetPlatformSettingAsync();
            //if (platformSetting != null)
            //{
            //    SettingValueDo? settingValue = platformSetting.GoodsSettings.Where(x => x.Name.Equals(BaseServiceSettings.GoodsSetting.GoodsStocksWarn)).FirstOrDefault();
            //    if (settingValue != null) int.TryParse(settingValue.Value, out stockWarnNum);
            //}
            //int stockWarnCount = await _goodCommonsAppService.GetStockWarnCountAsync(stockWarnNum);
            //waitItemDic.Add("stockWarnCount", stockWarnCount);
            return Ok(waitItemDic);
        }
    }
}