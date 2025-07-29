using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace RealTimeStockSimulator.Controllers
{
    public class AuthenticationController : Controller
    {
        private IMemoryCache _memoryCache;

        public AuthenticationController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Login()
        {
            if (_memoryCache.TryGetValue("test", out string? cacheValue))
            {
                Console.WriteLine(cacheValue);
                Console.WriteLine("login");
            }

            return View();
        }
    }
}
