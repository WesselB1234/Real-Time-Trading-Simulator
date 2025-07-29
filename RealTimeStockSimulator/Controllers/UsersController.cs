using Microsoft.AspNetCore.Mvc;

namespace RealTimeStockSimulator.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
