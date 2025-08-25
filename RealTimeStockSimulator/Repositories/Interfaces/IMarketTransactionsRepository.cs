using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Repositories.Interfaces
{
    public interface IMarketTransactionsRepository
    {
        MarketTransactions GetTransactionsByUser(User user);
        int AddTransaction(User user, MarketTransactionTradable transaction);
    }
}
