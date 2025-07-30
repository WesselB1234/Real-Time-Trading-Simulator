using RealTimeStockSimulator.Models;
using RealTimeStockSimulator.Repositories.Interfaces;

namespace RealTimeStockSimulator.Repositories
{
    public class DbTradablesRepository : DbBaseRepository, ITradablesRepository
    {
        public DbTradablesRepository(IConfiguration configuration) : base(configuration) { }

        public List<Tradable> GetAllTradables()
        {
            throw new NotImplementedException();
        }
    }
}
