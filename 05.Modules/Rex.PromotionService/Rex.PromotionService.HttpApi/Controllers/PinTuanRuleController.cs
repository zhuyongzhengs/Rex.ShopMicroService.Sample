using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rex.Service.Permission.PromotionServices;

namespace Rex.PromotionService.Controllers
{
    /// <summary>
    /// 拼团规则控制器
    /// </summary>
    [Route("api/promotion/pinTuanRule")]
    [Authorize(PromotionServicePermissions.PinTuanRules.Default)]
    public class PinTuanRuleController : PromotionServiceController
    {
        private readonly ILogger<PinTuanRuleController> _logger;

        public PinTuanRuleController(ILogger<PinTuanRuleController> logger)
        {
            _logger = logger;
        }
    }
}