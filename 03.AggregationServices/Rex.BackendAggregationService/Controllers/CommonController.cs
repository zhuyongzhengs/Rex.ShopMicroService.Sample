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
            // 并发启动所有异步任务
            var pendingPaymentTask = _orderCommonsAppService.GetPendingPaymentCountAsync();
            var pendingShipmentTask = _orderCommonsAppService.GetPendingShipmentCountAsync();
            var pendingAftersalesTask = _orderCommonsAppService.GetPendingAftersalesCountAsync();

            // 等待所有任务完成
            await Task.WhenAll(pendingPaymentTask, pendingShipmentTask, pendingAftersalesTask);

            Dictionary<string, int> waitItemDic = new Dictionary<string, int>
            {
                { "pendingPaymentCount", pendingPaymentTask.Result },
                { "pendingShipmentCount", pendingShipmentTask.Result },
                { "pendingAftersalesCount", pendingAftersalesTask.Result }
            };

            // 如需并发获取库存报警数量，也可类似处理
            // int stockWarnNum = ...;
            // var stockWarnTask = _goodCommonsAppService.GetStockWarnCountAsync(stockWarnNum);
            // await stockWarnTask;
            // waitItemDic.Add("stockWarnCount", stockWarnTask.Result);

            return Ok(waitItemDic);
        }
    }
}