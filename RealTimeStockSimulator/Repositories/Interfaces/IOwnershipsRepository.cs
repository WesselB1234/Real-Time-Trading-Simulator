using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Repositories.Interfaces
{
    public interface IOwnershipsRepository
    {
        Ownership GetOwnershipByUser(User user);
        OwnershipTradable? GetOwnershipTradableByUser(User user, string symbol);
        void AddOwnershipTradableToUser(User user, OwnershipTradable tradable);
        void UpdateOwnershipTradable(User user, OwnershipTradable tradable);
        void RemoveOwnershipTradableFromUser(User user, OwnershipTradable tradable);
    }
}
