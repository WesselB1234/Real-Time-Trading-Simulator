using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RealTimeStockSimulator.Models;
using RealTimeStockSimulator.Models.ViewModels;
using RealTimeStockSimulator.Services.Interfaces;

namespace RealTimeStockSimulator.Controllers
{
    public class TradablesController : BaseController
    {
        private ITradablesService _tradablesService { get; set; }
        private IOwnershipsService _ownershipsService { get; set; }

        public TradablesController(ITradablesService tradablesService, IOwnershipsService ownershipsService)
        {
            _tradablesService = tradablesService;
            _ownershipsService = ownershipsService;
        }

        public IActionResult Index()
        {
            List<Tradable> tradables = _tradablesService.GetAllTradables();

            return View(tradables);
        }

        public IActionResult Buy(ConfirmBuySellViewModel confirmViewModel)
        {
            if (confirmViewModel.Symbol == null)
            {
                return NotFound();
            }

            Tradable? tradable = _tradablesService.GetTradableBySymbol(confirmViewModel.Symbol);

            if (tradable == null)
            {
                return NotFound();
            }

            BuyViewModel viewModel = new BuyViewModel(tradable, confirmViewModel.Amount);

            return View(viewModel);
        }

        public IActionResult ConfirmBuy(ConfirmBuySellViewModel confirmViewModel)
        {
            return RedirectToAction("Buy", confirmViewModel);
        }

        public IActionResult Sell(ConfirmBuySellViewModel confirmViewModel)
        {
            if (confirmViewModel.Symbol == null)
            {
                return NotFound();
            }

            OwnershipTradable? tradable = _ownershipsService.GetOwnershipTradableByUser(LoggedInUser, confirmViewModel.Symbol);

            if (tradable == null)
            {
                return NotFound();
            }

            SellViewModel viewModel = new SellViewModel(tradable, confirmViewModel.Amount);

            return View(viewModel);
        }

        public IActionResult ConfirmSell(ConfirmBuySellViewModel confirmViewModel)
        {
            return RedirectToAction("Sell", confirmViewModel);
        }
    }
}
