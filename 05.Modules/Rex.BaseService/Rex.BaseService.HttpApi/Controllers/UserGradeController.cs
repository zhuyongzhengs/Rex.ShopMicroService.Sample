using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rex.BaseService.Controllers
{
    /// <summary>
    /// 用户等级控制器
    /// </summary>
    [Route("api/base/userGrade")]
    public class UserGradeController : BaseServiceController
    {
        private readonly ILogger<UserGradeController> _logger;

        public UserGradeController(ILogger<UserGradeController> logger)
        {
            _logger = logger;
        }
    }
}