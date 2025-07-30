using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RealTimeStockSimulator.Models;

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
            if (_memoryCache.TryGetValue("TradablesDictionary", out Dictionary<string, Tradable>? tradablesDictionary) 
                && tradablesDictionary.TryGetValue("BINANCE:BTCUSDT", out Tradable? tradable))
            {
                
                Console.WriteLine(tradable.Price);
            }

            return View();
        }
    }
}
