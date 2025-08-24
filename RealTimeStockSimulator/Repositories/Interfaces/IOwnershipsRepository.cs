using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Repositories.Interfaces
{
    public interface IOwnershipsRepository
    {
        Ownership GetOwnershipByUser(User user);
        OwnershipTradable? GetOwnershipTradableByUser(User user, string symbol);
    }
}
