using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Services.Interfaces
{
    public interface IMarketTransactionsService
    {
        MarketTransactions GetTransactionsByUser(User user);
        int AddTransaction(User user, MarketTransactionTradable transaction);
    }
}
