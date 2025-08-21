using Microsoft.Extensions.Caching.Memory;
using RealTimeStockSimulator.Models;
using RealTimeStockSimulator.Repositories.Interfaces;
using RealTimeStockSimulator.Services.Interfaces;

namespace RealTimeStockSimulator.Services
{
    public class OwnershipsService : IOwnershipsService
    {
        private IOwnershipsRepository _ownershipsRepository;
        private IMemoryCache _memoryCache;

        public OwnershipsService(IOwnershipsRepository ownershipsRepository, IMemoryCache memoryCache)
        {
            _ownershipsRepository = ownershipsRepository;
            _memoryCache = memoryCache;
        }

        public Ownership GetOwnershipByUser(User user)
        {
            Ownership ownership = _ownershipsRepository.GetOwnershipByUser(user);
            Dictionary<string, TradablePriceInfos>? tradablePriceInfosDictionary = _memoryCache.Get<Dictionary<string, TradablePriceInfos>?>("TradablePriceInfosDictionary");

            if (tradablePriceInfosDictionary != null)
            {
                foreach (OwnershipTradable tradable in ownership.Tradables)
                {
                    tradable.TradablePriceInfos = tradablePriceInfosDictionary[tradable.Symbol];
                }
            }

            return ownership;
        }
    }
}
