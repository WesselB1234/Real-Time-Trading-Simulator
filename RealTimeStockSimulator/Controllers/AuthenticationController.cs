using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RealTimeStockSimulator.Models;
using RealTimeStockSimulator.Models.ViewModels;
using RealTimeStockSimulator.Services.Interfaces;

namespace RealTimeStockSimulator.Controllers
{
    public class AuthenticationController : Controller
    {
        private IUsersService _usersService;
        public AuthenticationController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LoginIntoAccount()
        {
            Console.WriteLine("login account");

            return RedirectToAction("Login");
        }

        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            return View(registerViewModel);
        }

        public IActionResult RegisterNewAccount(RegisterViewModel registerViewModel)
        {
            if(_usersService.GetUserByName(registerViewModel.UserName) == null)
            {
                User user = new User
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    Password = registerViewModel.Password,
                    Money = 0
                };

                _usersService.AddUser(user);
            }
            else
            {
                Console.WriteLine("already exists");
            }

            return RedirectToAction("Register", registerViewModel);
        }
    }
}
