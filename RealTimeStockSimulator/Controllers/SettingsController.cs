using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeStockSimulator.Services.Interfaces;

namespace RealTimeStockSimulator.Controllers
{
    [Authorize]
    public class SettingsController : AuthenticatedUserController
    {
        public SettingsController(IUsersService usersService): base(usersService)
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
