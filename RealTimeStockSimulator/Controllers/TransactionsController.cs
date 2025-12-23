using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RealTimeStockSimulator.Models;
using RealTimeStockSimulator.Services;
using RealTimeStockSimulator.Services.Interfaces;

namespace RealTimeStockSimulator.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private IMarketTransactionsService _marketTransactionsService;
        private IUsersService _usersService;
        private UserAccount _loggedInUser;

        public TransactionsController(IMarketTransactionsService marketTransactionsService, IUsersService usersService)
        {
            _marketTransactionsService = marketTransactionsService;
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
            MarketTransactions transactions = _marketTransactionsService.GetTransactionsByUserPagnated(_loggedInUser);

            return View(transactions);
        }
    }
}
