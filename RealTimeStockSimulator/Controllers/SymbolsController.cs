using Microsoft.AspNetCore.Mvc;

namespace RealTimeStockSimulator.Controllers
{
    public class SymbolsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
