using Microsoft.AspNetCore.Mvc;

namespace RealTimeStockSimulator.Controllers
{
    public class SettingsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
