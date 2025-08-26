using Microsoft.AspNetCore.Mvc;
using RealTimeStockSimulator.Services.Interfaces;

namespace RealTimeStockSimulator.Controllers
{
    public class UsersController : BaseController
    {
        private IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public IActionResult Index()
        {
            return View(_usersService.GetAllUsers());
        }
    }
}
