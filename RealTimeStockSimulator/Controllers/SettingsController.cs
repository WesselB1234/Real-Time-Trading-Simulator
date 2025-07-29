using Microsoft.AspNetCore.Mvc;

namespace RealTimeStockSimulator.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
