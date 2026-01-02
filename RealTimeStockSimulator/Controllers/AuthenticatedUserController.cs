using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RealTimeStockSimulator.Models;
using RealTimeStockSimulator.Services.Interfaces;

namespace RealTimeStockSimulator.Controllers
{
    public class AuthenticatedUserController : Controller
    {
        protected UserAccount LoggedInUser;
        private IUsersService _usersService;

        public AuthenticatedUserController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            LoggedInUser = _usersService.GetUserFromClaimsPrinciple(User);

            ViewBag.loggedInUser = LoggedInUser;
            base.OnActionExecuting(context);
        }
    }
}
