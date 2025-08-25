using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Repositories.Interfaces
{
    public interface IMarketTransactionsRepository
    {
        MarketTransactions GetTransactionsByUser(User user);
        void AddTransaction(User user, MarketTransactionTradable transaction);
    }
}
