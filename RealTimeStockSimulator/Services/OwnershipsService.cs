using Microsoft.Extensions.Caching.Memory;
using RealTimeStockSimulator.Models;
using RealTimeStockSimulator.Models.Interfaces;
using RealTimeStockSimulator.Repositories.Interfaces;
using RealTimeStockSimulator.Services.Interfaces;

namespace RealTimeStockSimulator.Services
{
    public class OwnershipsService : IOwnershipsService
    {
        private IOwnershipsRepository _ownershipsRepository;
        private IMemoryCache _memoryCache;
        private IDataMapper _mapper;

        public OwnershipsService(IOwnershipsRepository ownershipsRepository, IMemoryCache memoryCache, IDataMapper mapper)
        {
            _ownershipsRepository = ownershipsRepository;
            _memoryCache = memoryCache;
            _mapper = mapper;
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

        public OwnershipTradable? GetOwnershipTradableByUser(User user, string symbol)
        {
            OwnershipTradable? tradable = _ownershipsRepository.GetOwnershipTradableByUser(user, symbol);
            Dictionary<string, TradablePriceInfos>? tradablePriceInfosDictionary = _memoryCache.Get<Dictionary<string, TradablePriceInfos>?>("TradablePriceInfosDictionary");

            if (tradable != null &&  tradablePriceInfosDictionary != null)
            {
                tradable.TradablePriceInfos = tradablePriceInfosDictionary[tradable.Symbol];
            }

            return tradable;
        }

        public void AddOwnershipTradableToUser(User user, OwnershipTradable tradable)
        {
            _ownershipsRepository.AddOwnershipTradableToUser(user, tradable);
        }

        public void UpdateOwnershipTradable(User user, OwnershipTradable tradable)
        {
            _ownershipsRepository.UpdateOwnershipTradable(user, tradable);
        }

        public void RemoveOwnershipTradableFromUser(User user, OwnershipTradable tradable)
        {
            _ownershipsRepository.RemoveOwnershipTradableFromUser(user, tradable);
        }

        public decimal BuyTradable(User user, Tradable tradable, int amount)
        {
            decimal moneyAfterPurchase = user.Money - (tradable.TradablePriceInfos.Price * amount);

            if (moneyAfterPurchase < 0)
            {
                throw new ArgumentException("You do not have enough money for this order.");
            }

            OwnershipTradable? ownershipTradable = _ownershipsRepository.GetOwnershipTradableByUser(user, tradable.Symbol);

            if (ownershipTradable != null)
            {
                ownershipTradable.Amount += amount;
                UpdateOwnershipTradable(user, ownershipTradable);
            }
            else
            {
                AddOwnershipTradableToUser(user, _mapper.MapOwnershipTradableByTradable(tradable, amount));
            }

            return moneyAfterPurchase;
        }

        public decimal SellTradable(User user, OwnershipTradable tradable, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
