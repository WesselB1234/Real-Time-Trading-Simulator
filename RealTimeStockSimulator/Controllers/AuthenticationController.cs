using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RealTimeStockSimulator.Extensions;
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

        public IActionResult Login(LoginViewModel loginViewModel)
        {
            return View(loginViewModel);
        }

        public IActionResult LoginIntoAccount(LoginViewModel loginViewModel)
        {
            User? user = _usersService.GetUserByLoginCredentials(loginViewModel.UserName, loginViewModel.Password);

            if (user != null)
            {
                HttpContext.Session.SetObject("LoggedInUser", user);
                Console.WriteLine("logged in");
            }
            else
            {
                Console.WriteLine("User does not exist or password incorrect");
            }

            return RedirectToAction("Login", loginViewModel);
        }

        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            return View(registerViewModel);
        }

        public IActionResult RegisterNewAccount(RegisterViewModel registerViewModel)
        {
            try
            {
                User user = new User
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    Password = registerViewModel.Password,
                    Money = 0
                };

                User? addedUser = _usersService.AddUser(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction("Register", registerViewModel);
        }

        public IActionResult Logout()
        {
            User? loggedInUser = HttpContext.Session.GetObject<User>("LoggedInUser");

            if (loggedInUser != null)
            {
                HttpContext.Session.Remove("LoggedInUser");
                Console.WriteLine("Logged out");
            }
            else
            {
                Console.WriteLine("not logged in");
            }

            return View("Login");
        }
    }
}
