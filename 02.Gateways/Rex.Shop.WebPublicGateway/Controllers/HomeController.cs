using Microsoft.AspNetCore.Mvc;

namespace Rex.Shop.WebPublicGateway.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}