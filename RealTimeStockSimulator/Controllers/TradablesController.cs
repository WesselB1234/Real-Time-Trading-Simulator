using Microsoft.AspNetCore.Mvc;

namespace RealTimeStockSimulator.Controllers
{
    public class TradablesController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
