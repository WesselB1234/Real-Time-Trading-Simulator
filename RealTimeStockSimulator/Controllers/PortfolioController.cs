using Microsoft.AspNetCore.Mvc;
using RealTimeStockSimulator.Services.Interfaces;

namespace RealTimeStockSimulator.Controllers
{
    public class PortfolioController : BaseController
    {
        private IOwnershipsService _ownershipsService;

        public PortfolioController(IOwnershipsService ownershipsService)
        {
            _ownershipsService = ownershipsService;
        }
        
        public IActionResult Index()
        {
            return View(_ownershipsService.GetOwnershipByUser(LoggedInUser));
        }
    }
}
