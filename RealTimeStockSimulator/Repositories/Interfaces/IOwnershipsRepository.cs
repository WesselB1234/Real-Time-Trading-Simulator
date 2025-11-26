using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Repositories.Interfaces
{
    public interface IOwnershipsRepository
    {
        Ownership GetOwnershipByUser(UserAccount user);
        OwnershipTradable? GetOwnershipTradableByUser(UserAccount user, string symbol);
        void AddOwnershipTradableToUser(UserAccount user, OwnershipTradable tradable);
        void UpdateOwnershipTradable(UserAccount user, OwnershipTradable tradable);
        void RemoveOwnershipTradableFromUser(UserAccount user, OwnershipTradable tradable);
    }
}
