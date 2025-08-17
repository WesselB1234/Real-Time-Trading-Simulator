using Microsoft.AspNetCore.Mvc;

namespace RealTimeStockSimulator.Controllers
{
    public class UsersController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
