using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Repositories.Interfaces
{
    public interface ITradablesRepository
    {
        public List<Tradable> GetAllTradables();
    }
}
