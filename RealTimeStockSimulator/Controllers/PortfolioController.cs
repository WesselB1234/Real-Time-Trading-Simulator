using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RealTimeStockSimulator.Models;
using RealTimeStockSimulator.Services.Interfaces;

namespace RealTimeStockSimulator.Controllers
{
    [Authorize]
    public class PortfolioController : Controller
    {
        private IOwnershipsService _ownershipsService;
        private IUsersService _usersService;
        private UserAccount _loggedInUser;

        public PortfolioController(IOwnershipsService ownershipsService, IUsersService usersService)
        {
            _ownershipsService = ownershipsService;
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
            return View(_ownershipsService.GetAllOwnershipTradablesByUserId(_loggedInUser.UserId));
        }
    }
}
