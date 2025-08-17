using Microsoft.AspNetCore.Mvc;

namespace RealTimeStockSimulator.Controllers
{
    public class PortfolioController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
