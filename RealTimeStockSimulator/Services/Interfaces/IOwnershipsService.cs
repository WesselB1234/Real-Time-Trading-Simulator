using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Services.Interfaces
{
    public interface IOwnershipsService
    {
        Ownership GetOwnershipByUser(User user);
        OwnershipTradable? GetOwnershipTradableByUser(User user, string symbol);
        void AddOwnershipTradableToUser(User user, OwnershipTradable tradable);
        void UpdateOwnershipTradable(User user, OwnershipTradable tradable);
        void RemoveOwnershipTradableFromUser(User user, OwnershipTradable tradable);
        decimal BuyTradable(User user, Tradable tradable, int amount);
        decimal SellTradable(User user, OwnershipTradable tradable, int amount);
    }
}
