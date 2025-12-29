using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Repositories.Interfaces
{
    public interface ITradablesRepository
    {
        List<Tradable> GetAllTradables();
        int AddTradable(Tradable tradable);
        Tradable? GetTradableBySymbol(string symbol);
    }
}
