using Microsoft.AspNetCore.Mvc;

namespace RealTimeStockSimulator.Controllers
{
    public class TradablesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
