using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Repositories.Interfaces
{
    public interface IOwnershipsRepository
    {
        Ownership GetOwnershipByUser(User user);
    }
}
