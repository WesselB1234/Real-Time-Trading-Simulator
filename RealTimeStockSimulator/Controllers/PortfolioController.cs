using Microsoft.AspNetCore.Mvc;

namespace RealTimeStockSimulator.Controllers
{
    public class PortfolioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
