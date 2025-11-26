using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Repositories.Interfaces
{
    public interface IMarketTransactionsRepository
    {
        MarketTransactions GetTransactionsByUserPagnated(UserAccount user);
        int AddTransaction(UserAccount user, MarketTransactionTradable transaction);
    }
}
