using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public TransactionsController(IMarketTransactionsService marketTransactionsService, IUsersService usersService)
        {
            _marketTransactionsService = marketTransactionsService;
            _usersService = usersService;
        }

        public IActionResult Index()
        {
            UserAccount loggedInUser = _usersService.GetUserFromClaimsPrinciple(User);
            MarketTransactions transactions = _marketTransactionsService.GetTransactionsByUserPagnated(loggedInUser);

            return View(transactions);
        }
    }
}
