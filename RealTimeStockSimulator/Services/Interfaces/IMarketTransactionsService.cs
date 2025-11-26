using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Services.Interfaces
{
    public interface IMarketTransactionsService
    {
        MarketTransactions GetTransactionsByUserPagnated(UserAccount user);
        int AddTransaction(UserAccount user, MarketTransactionTradable transaction);
    }
}
