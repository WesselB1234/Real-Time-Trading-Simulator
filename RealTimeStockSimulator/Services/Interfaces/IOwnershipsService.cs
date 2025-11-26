using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Services.Interfaces
{
    public interface IOwnershipsService
    {
        Ownership GetOwnershipByUser(UserAccount user);
        OwnershipTradable? GetOwnershipTradableByUser(UserAccount user, string symbol);
        void AddOwnershipTradableToUser(UserAccount user, OwnershipTradable tradable);
        void UpdateOwnershipTradable(UserAccount user, OwnershipTradable tradable);
        void RemoveOwnershipTradableFromUser(UserAccount user, OwnershipTradable tradable);
        decimal BuyTradable(UserAccount user, Tradable tradable, int amount);
        decimal SellTradable(UserAccount user, OwnershipTradable tradable, int amount);
    }
}
