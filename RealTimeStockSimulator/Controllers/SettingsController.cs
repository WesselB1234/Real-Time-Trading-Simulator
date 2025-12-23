using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RealTimeStockSimulator.Models;
using RealTimeStockSimulator.Services.Interfaces;

namespace RealTimeStockSimulator.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private IUsersService _usersService;
        private UserAccount _loggedInUser;

        public SettingsController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _loggedInUser = _usersService.GetUserFromClaimsPrinciple(User);

            ViewBag.loggedInUser = _loggedInUser;
            base.OnActionExecuting(context);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
